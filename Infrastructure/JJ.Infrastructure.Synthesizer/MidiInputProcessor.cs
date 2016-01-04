using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Canonical;
using JJ.Business.Canonical;

namespace JJ.Infrastructure.Synthesizer
{
    /// <summary> This code is really just a prototype at the moment. </summary>
    public class MidiInputProcessor : IDisposable
    {
        private const double DEFAULT_AMPLIFIER = 0.2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;
        private const int MAX_CONCURRENT_NOTES = 4; // TODO: Increase after testing. Currently (2015-12-31), it cannot handle much more, though, performance-wise (on my laptop not plugged in).

        private readonly AutoPatchPolyphonicResult _autoPatchResult;
        private readonly IPatchCalculator _patchCalculator;
        private readonly Scale _scale;
        private readonly MidiIn _midiIn;
        private readonly AudioOutputProcessor _audioOutputProcessor;

        private double[] _noteNumber_To_Frequency_Array;
        private Dictionary<int, int> _noteNumber_To_NoteListIndex_Dictionary = new Dictionary<int, int>();

        public MidiInputProcessor(Scale scale, IList<Patch> patches, PatchRepositories repositories)
        {
            if (scale == null) throw new NullException(() => scale);

            // Setup Scale
            _scale = scale;
            IList<double> frequencies = new List<double>(MAX_NOTE_NUMBER + 1);
            for (int i = 0; i < MAX_NOTE_NUMBER; i++)
            {
                double frequency = GetFrequencyByNoteNumber(i);
                frequencies.Add(frequency);
            }
            _noteNumber_To_Frequency_Array = frequencies.ToArray();

            var patchManager = new PatchManager(repositories);
            _autoPatchResult = patchManager.AutoPatchPolyphonic(patches, MAX_CONCURRENT_NOTES);
            _patchCalculator = patchManager.CreateOptimizedCalculator(_autoPatchResult.SignalOutlet);

            _audioOutputProcessor = new AudioOutputProcessor(_patchCalculator);
            _midiIn = TryCreateMidiIn();

            _audioOutputProcessor.Start();
            _audioOutputProcessor.Pause(); // Prevent calculations at startup.
        }

        ~MidiInputProcessor()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_midiIn != null)
            {
                _midiIn.MessageReceived -= _midiIn_MessageReceived;
                // Not sure if I need to call of these methods, but when I omitted one I got an error upon application exit.
                _midiIn.Stop();
                _midiIn.Close();
                _midiIn.Dispose();
            }

            if (_audioOutputProcessor != null)
            {
                _audioOutputProcessor.Stop();
                _audioOutputProcessor.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        /// <summary> For now will only work with the first MIDI device it finds. </summary>
        private MidiIn TryCreateMidiIn()
        {
            if (MidiIn.NumberOfDevices == 0)
            {
                // TODO: Handle this better.
                return null;
                //throw new Exception("No connected MIDI devices.");
            }

            int deviceIndex = 0;

            var midiIn = new MidiIn(deviceIndex);
            midiIn.MessageReceived += _midiIn_MessageReceived;
            midiIn.Start();

            return midiIn;
        }

        private void _midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            switch (e.MidiEvent.CommandCode)
            {
                case MidiCommandCode.NoteOn:
                    HandleNoteOn(e.MidiEvent);
                    break;

                case MidiCommandCode.NoteOff:
                    HandleNoteOff(e.MidiEvent);
                    break;
            }
        }

        private void HandleNoteOn(MidiEvent midiEvent)
        {
            bool mustStartPlayingAudioOutput = _noteNumber_To_NoteListIndex_Dictionary.Count == 0;

            var noteOnEvent = (NoteOnEvent)midiEvent;

            int? noteListIndex = TryGetNoteListIndex(noteOnEvent.NoteNumber);
            if (!noteListIndex.HasValue)
            {
                // No more note slots available.
                return;
            }

            double frequency = _noteNumber_To_Frequency_Array[noteOnEvent.NoteNumber];
            string frequencyInletName = _autoPatchResult.FrequencyInletNames[noteListIndex.Value];
            _patchCalculator.SetValue(frequencyInletName, frequency);

            double volume = noteOnEvent.Velocity / MAX_VELOCITY;
            string volumeInletName = _autoPatchResult.VolumeInletNames[noteListIndex.Value];
            _patchCalculator.SetValue(volumeInletName, volume);

            string delayInletName = _autoPatchResult.DelayInletNames[noteListIndex.Value];
            double delay = _audioOutputProcessor.Time;
            _patchCalculator.SetValue(delayInletName, delay);

            if (mustStartPlayingAudioOutput)
            {
                _audioOutputProcessor.Continue();
            }
        }

        private void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            int? noteListIndex = TryGetNoteListIndex(noteEvent.NoteNumber);
            if (!noteListIndex.HasValue)
            {
                // Note was ignored earlier, due to not enough slots.
                return;
            }

            double newVolume = 0.0;
            string volumeInletName = _autoPatchResult.VolumeInletNames[noteListIndex.Value];
            _patchCalculator.SetValue(volumeInletName, newVolume);

            ResetNoteInletListIndex(noteEvent.NoteNumber);

            if (_noteNumber_To_NoteListIndex_Dictionary.Count == 0)
            {
                _audioOutputProcessor.Pause();
            }
        }

        // Helpers

        /// <summary>
        /// Used both for getting a list index for a new note,
        /// as well as getting the list index for the already playing note.
        /// Returns null if max concurrent notes was exceeded.
        /// </summary>
        private int? TryGetNoteListIndex(int noteNumber)
        {
            int listIndex;
            if (_noteNumber_To_NoteListIndex_Dictionary.TryGetValue(noteNumber, out listIndex))
            {
                return listIndex;
            }

            for (int i = 0; i < MAX_CONCURRENT_NOTES; i++)
            {
                if (!_noteNumber_To_NoteListIndex_Dictionary.ContainsValue(i))
                {
                    _noteNumber_To_NoteListIndex_Dictionary[noteNumber] = i;
                    return i;
                }
            }
            
            return null;
        }

        private void ResetNoteInletListIndex(int noteNumber)
        {
            _noteNumber_To_NoteListIndex_Dictionary.Remove(noteNumber);
        }

        private double GetFrequencyByNoteNumber(int noteNumber)
        {
            double frequency = LOWEST_FREQUENCY * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }
    }
}

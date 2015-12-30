using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Extensions;
using System.Media;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Infrastructure.Synthesizer
{
    /// <summary> This code is really just a prototype at the moment. </summary>
    public class MidiInputProcessor : IDisposable
    {
        private const double DEFAULT_AMPLIFIER = 0.2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;
        private const int MAX_CONCURRENT_NOTES = 5; // TODO: Increase after testing.

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

            _patchCalculator = CreatePatchCalculator(patches, repositories);

            _audioOutputProcessor = new AudioOutputProcessor(_patchCalculator);
            _midiIn = TryCreateMidiIn();
        }

        /// <summary>
        /// Auto-patches the provided patches and makes a custom operator from it.
        /// Then creates a wrapper patch around it, that enables polyphony.
        /// </summary>
        private IPatchCalculator CreatePatchCalculator(IList<Patch> patches, PatchRepositories repositories)
        {
            var patchManager = new PatchManager(repositories);
            patchManager.AutoPatch(patches);
            Patch autoPatch = patchManager.Patch;

            patchManager.CreatePatch();
            Patch wrapperPatch = patchManager.Patch;

            var outlets = new List<Outlet>(MAX_CONCURRENT_NOTES);

            for (int i = 0; i < MAX_CONCURRENT_NOTES; i++)
            {
                PatchInlet_OperatorWrapper volumePatchInletWrapper = patchManager.Inlet(InletTypeEnum.Volume);
                volumePatchInletWrapper.Name = GetVolumeInletName(i);

                PatchInlet_OperatorWrapper frequencyPatchInletWrapper = patchManager.Inlet(InletTypeEnum.Frequency);
                frequencyPatchInletWrapper.Name = GetFrequencyInletName(i);

                PatchInlet_OperatorWrapper delayPatchInletWrapper = patchManager.Inlet();
                delayPatchInletWrapper.Name = GetDelayInletName(i);

                CustomOperator_OperatorWrapper customOperatorWrapper = patchManager.CustomOperator(autoPatch);

                customOperatorWrapper.Inlets.Where(x => x.GetInletTypeEnum() == InletTypeEnum.Volume).ForEach(x => x.InputOutlet = volumePatchInletWrapper);
                customOperatorWrapper.Inlets.Where(x => x.GetInletTypeEnum() == InletTypeEnum.Frequency).ForEach(x => x.InputOutlet = frequencyPatchInletWrapper);

                IEnumerable<Outlet> signalOutlets = customOperatorWrapper.Outlets.Where(x => x.GetOutletTypeEnum() == OutletTypeEnum.Signal);
                foreach (Outlet signalOutlet in signalOutlets)
                {
                    Delay_OperatorWrapper delayWrapper = patchManager.Delay(signalOutlet, delayPatchInletWrapper);
                    outlets.Add(delayWrapper);
                }
            }

            Adder_OperatorWrapper adderWrapper = patchManager.Adder(outlets);

            Outlet outlet = adderWrapper.Result;

            var patchCalculator = patchManager.CreateOptimizedCalculator(outlet);

            return patchCalculator;
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
            double volume = noteOnEvent.Velocity / MAX_VELOCITY;

            // TODO: None of these outcommented code lines work, because Bundle operators now result in repetition of the Frequency and Volme PatchInlet operators.
            //_patchCalculator.SetValue(InletTypeEnum.Frequency, noteListIndex, frequency);
            //_patchCalculator.SetValue(InletTypeEnum.Volume, noteListIndex, volume);

            //_patchCalculator.SetValue(InletTypeEnum.Frequency, frequency);
            //_patchCalculator.SetValue(InletTypeEnum.Volume, volume);

            string frequencyInletName = GetFrequencyInletName(noteListIndex.Value);
            _patchCalculator.SetValue(frequencyInletName, frequency);

            string volumeInletName = GetVolumeInletName(noteListIndex.Value);
            _patchCalculator.SetValue(volumeInletName, volume);

            string delayInletName = GetDelayInletName(noteListIndex.Value);
            double delay = _audioOutputProcessor.Time;
            _patchCalculator.SetValue(delayInletName, delay);

            if (mustStartPlayingAudioOutput)
            {
                _audioOutputProcessor.Play();
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
            //_patchCalculator.SetValue(InletTypeEnum.Volume, noteListIndex, newVolume);

            string volumeInletName = GetVolumeInletName(noteListIndex.Value);
            _patchCalculator.SetValue(volumeInletName, newVolume);

            ResetNoteInletListIndex(noteEvent.NoteNumber);

            if (_noteNumber_To_NoteListIndex_Dictionary.Count == 0)
            {
                _audioOutputProcessor.Stop();
            }
        }

        private double GetFrequencyByNoteNumber(int noteNumber)
        {
            double frequency = LOWEST_FREQUENCY * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }

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

        private string GetFrequencyInletName(int noteListIndex)
        {
            return "f" + noteListIndex.ToString();
        }

        private string GetVolumeInletName(int noteListIndex)
        {
            return "v" + noteListIndex.ToString();
        }

        private string GetDelayInletName(int noteListIndex)
        {
            return "d" + noteListIndex.ToString();
        }
    }
}

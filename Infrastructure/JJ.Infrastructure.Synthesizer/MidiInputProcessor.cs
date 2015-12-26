using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
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
        private const double DEFAULT_DURATION = 2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;

        private readonly IPatchCalculator _patchCalculator;
        private readonly Scale _scale;
        private readonly MidiIn _midiIn;
        private readonly AudioOutputProcessor _audioOutput;

        private int _previousNoteNumber;
        private double[] _noteNumberToFrequencyArray;

        public MidiInputProcessor(Scale scale, IList<Patch> patches, RepositoryWrapper repositories)
        {
            if (scale == null) throw new NullException(() => scale);

            // Setup Scale
            _scale = scale;
            IList<double> frequencies = new List<double>(MAX_NOTE_NUMBER);
            for (int i = 0; i < MAX_NOTE_NUMBER; i++)
            {
                double frequency = GetFrequencyByNoteNumber(i);
                frequencies.Add(frequency);
            }
            _noteNumberToFrequencyArray = frequencies.ToArray();

            // Setup Patch
            var patchManager = new PatchManager(new PatchRepositories(repositories));
            patchManager.AutoPatch(patches);
            Patch autoPatch = patchManager.Patch;

            // TODO: Add up all signal outlets. Note that it might be hard, because you cannot just add to the patch?
            // Oh, it is an auto-patch, you might be able to do whatever you want to it.
            // Beware to not wrap it all into another CustomOperator, because you need to Patch to use their PatchInlets
            // as freely controllable values.
            Outlet signalOutlet = autoPatch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                           .Select(x => x.Result)
                                           .FirstOrDefault();

            _patchCalculator = patchManager.CreateOptimizedCalculator(signalOutlet);

            // AudioOutput
            _audioOutput = new AudioOutputProcessor(_patchCalculator);

            // MidiIn
            _midiIn = TryCreateMidiIn();
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

            if (_audioOutput != null)
            {
                _audioOutput.Dispose();
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
            var noteOnEvent = (NoteOnEvent)midiEvent;

            double frequency = _noteNumberToFrequencyArray[noteOnEvent.NoteNumber];
            double volume = noteOnEvent.Velocity / MAX_VELOCITY;

            _patchCalculator.SetValue(InletTypeEnum.Frequency, frequency);
            _patchCalculator.SetValue(InletTypeEnum.Volume, volume);

            _audioOutput.Play();

            _previousNoteNumber = noteOnEvent.NoteNumber;
        }

        private void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            if (_previousNoteNumber == noteEvent.NoteNumber)
            {
                _audioOutput.Stop();
            }
        }

        private double GetFrequencyByNoteNumber(int noteNumber)
        {
            double frequency = LOWEST_FREQUENCY * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }
    }
}

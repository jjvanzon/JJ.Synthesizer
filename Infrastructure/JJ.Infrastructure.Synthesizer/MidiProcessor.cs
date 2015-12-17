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

namespace JJ.Infrastructure.Synthesizer
{
    /// <summary> This code is really just a prototype at the moment. </summary>
    public class MidiProcessor : IDisposable
    {
        // TODO: I do not understand why the patch produces such loud sound.
        private const double DEFAULT_AMPLIFIER = 0.05;
        private const double DEFAULT_DURATION = 2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;


        private readonly PatchManager _patchManager;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        private readonly Scale _scale;
        private readonly AudioFileOutput _audioFileOutput;
        private readonly Number_OperatorWrapper _frequency_Number_OperatorWrapper;
        private readonly Number_OperatorWrapper _volume_Number_OperatorWrapper;

        private readonly MidiIn _midiIn;
        private readonly SoundPlayer _soundPlayer;

        private int _previousNoteNumber;

        private double[] _noteNumberToFrequencyArray;

        public MidiProcessor(Scale scale, IList<Patch> patches, RepositoryWrapper repositories, string tempWavFilePath)
        {
            if (scale == null) throw new NullException(() => scale);
            if (String.IsNullOrEmpty(tempWavFilePath)) throw new NullOrEmptyException(() => tempWavFilePath);

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
            _patchManager = new PatchManager(new PatchRepositories(repositories));
            CustomOperator_OperatorWrapper customOperator = _patchManager.AutoPatch_ToCustomOperator(patches);

            // Setup Frequency Inlets
            _frequency_Number_OperatorWrapper = _patchManager.Number();
            IList<Inlet> frequencyInlets = customOperator.Inlets
                                                      .Where(x => x.GetInletTypeEnum() == InletTypeEnum.Frequency)
                                                      .ToArray();
            foreach (Inlet frequencyInlet in frequencyInlets)
            {
                frequencyInlet.InputOutlet = _frequency_Number_OperatorWrapper;
            }

            // Setup Volume Inlets
            _volume_Number_OperatorWrapper = _patchManager.Number();
            IList<Inlet> volumeInlets = customOperator.Inlets
                                                      .Where(x => x.GetInletTypeEnum() == InletTypeEnum.Volume)
                                                      .ToArray();
            foreach (Inlet volumeInlet in volumeInlets)
            {
                volumeInlet.InputOutlet = _volume_Number_OperatorWrapper;
            }

            Outlet signalOutlet = customOperator.Outlets[OutletTypeEnum.Signal];

            // AudioFileOutput
            _audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
            _audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities();
            _audioFileOutput.Duration = DEFAULT_DURATION;
            _audioFileOutput.Amplifier = DEFAULT_AMPLIFIER;
            _audioFileOutput.FilePath = tempWavFilePath;
            _audioFileOutput.AudioFileOutputChannels[0].Outlet = signalOutlet;

            // SoundPlayer
            _soundPlayer = new SoundPlayer();
            _soundPlayer.SoundLocation = _audioFileOutput.FilePath;

            // MidiIn
            _midiIn = CreateMidiIn();
        }

        ~MidiProcessor()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_midiIn != null)
            {
                _midiIn.MessageReceived -= _midiIn_MessageReceived;
                // Not sure if I need to call of these methods.
                _midiIn.Stop();
                _midiIn.Close();
                _midiIn.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        /// <summary> For now will only work with the first MIDI device it finds. </summary>
        private MidiIn CreateMidiIn()
        {
            if (MidiIn.NumberOfDevices == 0)
            {
                throw new Exception("No connected MIDI devices.");
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

            _volume_Number_OperatorWrapper.Number = volume;
            _frequency_Number_OperatorWrapper.Number = frequency;

            _audioFileOutputManager.WriteFile(_audioFileOutput);

            _previousNoteNumber = noteOnEvent.NoteNumber;

            _soundPlayer.Play();
        }

        private void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            if (_previousNoteNumber == noteEvent.NoteNumber)
            {
                _soundPlayer.Stop();
            }
        }

        private double GetFrequencyByNoteNumber(int noteNumber)
        {
            double frequency = LOWEST_FREQUENCY * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }
    }
}

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
        private const double DEFAULT_DURATION = 2;
        private const double MAX_VELOCITY = 127.0;

        private readonly PatchManager _patchManager;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        private readonly Scale _scale;
        private readonly AudioFileOutput _audioFileOutput;
        private readonly Number_OperatorWrapper _frequency_Number_OperatorWrapper;
        private readonly Number_OperatorWrapper _volume_Number_OperatorWrapper;

        private readonly MidiIn _midiIn;
        private readonly SoundPlayer _soundPlayer;

        public MidiProcessor(Scale scale, IList<Patch> patches, RepositoryWrapper repositories, string tempWavFilePath)
        {
            if (scale == null) throw new NullException(() => scale);
            if (String.IsNullOrEmpty(tempWavFilePath)) throw new NullOrEmptyException(() => tempWavFilePath);

            _scale = scale;

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

            _audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
            _audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities();
            _audioFileOutput.Duration = DEFAULT_DURATION;
            _audioFileOutput.FilePath = tempWavFilePath;
            _audioFileOutput.AudioFileOutputChannels[0].Outlet = signalOutlet;

            _soundPlayer = new SoundPlayer();
            _soundPlayer.SoundLocation = _audioFileOutput.FilePath;

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
                _midiIn.Stop();
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

        private int _previousNoteNumber;

        private void HandleNoteOn(MidiEvent midiEvent)
        {
            var noteOnEvent = (NoteOnEvent)midiEvent;

            double frequency = GetFrequencyByNoteNumber(noteOnEvent.NoteNumber);
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
            // This is just some prototype testing code.
            double baseFreq = 16;
            double frequency = baseFreq * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }
    }
}

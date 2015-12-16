using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using System.Media;
using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Infrastructure.Synthesizer
{
    public class MidiProcessor : IDisposable
    {
        private const double DEFAULT_DURATION = 2;

        private const double DEFAULT_FREQUENCY = 525;

        private readonly PatchManager _patchManager;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        private readonly Scale _scale;
        private readonly AudioFileOutput _audioFileOutput;
        private readonly Number_OperatorWrapper _frequency_Number_OperatorWrapper;

        private readonly MidiIn _midiIn;
        private readonly SoundPlayer _soundPlayer;

        public MidiProcessor(Scale scale, IList<Patch> patches, RepositoryWrapper repositories, string tempWavFilePath)
        {
            if (scale == null) throw new NullException(() => scale);
            if (String.IsNullOrEmpty(tempWavFilePath)) throw new NullOrEmptyException(() => tempWavFilePath);

            var patchRepositories = new PatchRepositories(repositories);
            _patchManager = new PatchManager(patchRepositories);

            _frequency_Number_OperatorWrapper = _patchManager.Number();

            CustomOperator_OperatorWrapper customOperator = _patchManager.AutoPatch_ToCustomOperator(patches);
            Inlet frequencyInlet = customOperator.Inlets[InletTypeEnum.Frequency]; // TODO: It may be too harsh to let an exception go off. You would like a validation message.
            frequencyInlet.InputOutlet = _frequency_Number_OperatorWrapper;

            Outlet signalOutlet = customOperator.Outlets[OutletTypeEnum.Signal];

            var audioFileOutputRepositories = new AudioFileOutputRepositories(repositories);
            _audioFileOutputManager = new AudioFileOutputManager(audioFileOutputRepositories);
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
            if (e.MidiEvent.CommandCode == MidiCommandCode.NoteOn)
            {
                var noteOnEvent = (NoteOnEvent)e.MidiEvent;
                double frequency = GetFrequencyByNoteNumber(noteOnEvent.NoteNumber);

                _frequency_Number_OperatorWrapper.Number = frequency;
                _audioFileOutputManager.WriteFile(_audioFileOutput);
                _soundPlayer.Play();
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

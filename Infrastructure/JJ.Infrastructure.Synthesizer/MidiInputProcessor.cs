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
        private const double DEFAULT_DURATION = 2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;
        private const int MAX_CONCURRENT_NOTES = 4; // TODO: Increase after testing.

        private readonly IPatchCalculator _patchCalculator;
        private readonly Scale _scale;
        private readonly MidiIn _midiIn;
        private readonly AudioOutputProcessor _audioOutputProcessor;

        private double[] _noteNumber_To_Frequency_Array;

        private int _playingNoteCount;
        private int _currentNoteListIndex;
        private int?[] _noteNumber_To_NoteListIndex_Array = new int?[MAX_NOTE_NUMBER + 1];

        public MidiInputProcessor(Scale scale, IList<Patch> patches, RepositoryWrapper repositories)
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
            //_patchCalculator = CraatePatchCalculator_Old(patches, repositories);

            _audioOutputProcessor = new AudioOutputProcessor(_patchCalculator);
            _midiIn = TryCreateMidiIn();

            //_audioOutputProcessor.Play();
        }

        private IPatchCalculator CraatePatchCalculator_Old(IList<Patch> patches, RepositoryWrapper repositories)
        {
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

            IPatchCalculator patchCalculator = patchManager.CreateOptimizedCalculator(signalOutlet);
            return patchCalculator;
        }

        /// <summary>
        /// Makes a custom operator from the provided patches.
        /// Then creates a wrapper patch around it, that enables polyphony.
        /// </summary>
        private IPatchCalculator CreatePatchCalculator(IList<Patch> patches, RepositoryWrapper repositories)
        {
            var patchManager = new PatchManager(new PatchRepositories(repositories));
            patchManager.AutoPatch(patches);
            Patch autoPatch = patchManager.Patch;

            patchManager.CreatePatch();
            Patch wrapperPatch = patchManager.Patch;

            var customOperatorSignalOutlets = new List<Outlet>(MAX_CONCURRENT_NOTES);

            for (int i = 0; i < MAX_CONCURRENT_NOTES; i++)
            {
                PatchInlet_OperatorWrapper volumePatchInletWrapper = patchManager.PatchInlet(InletTypeEnum.Volume);
                volumePatchInletWrapper.Name = GetVolumeInletName(i);

                PatchInlet_OperatorWrapper frequencyPatchInletWrapper = patchManager.PatchInlet(InletTypeEnum.Frequency);
                frequencyPatchInletWrapper.Name = GetFrequencyInletName(i);

                CustomOperator_OperatorWrapper customOperatorWrapper = patchManager.CustomOperator(autoPatch);

                customOperatorWrapper.Inlets.Where(x => x.GetInletTypeEnum() == InletTypeEnum.Volume).ForEach(x => x.InputOutlet = volumePatchInletWrapper);
                customOperatorWrapper.Inlets.Where(x => x.GetInletTypeEnum() == InletTypeEnum.Frequency).ForEach(x => x.InputOutlet = frequencyPatchInletWrapper);

                IList<Outlet> signalOutlets = customOperatorWrapper.Outlets
                                                                   .Where(x => x.GetOutletTypeEnum() == OutletTypeEnum.Signal)
                                                                   .ToArray();

                customOperatorSignalOutlets.AddRange(signalOutlets);
            }

            Adder_OperatorWrapper adderWrapper = patchManager.Adder(customOperatorSignalOutlets);

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
            var noteOnEvent = (NoteOnEvent)midiEvent;

            double frequency = _noteNumber_To_Frequency_Array[noteOnEvent.NoteNumber];
            double volume = noteOnEvent.Velocity / MAX_VELOCITY;

            int noteListIndex = GetNoteInletListIndex(noteOnEvent.NoteNumber);

            //_patchCalculator.SetValue(InletTypeEnum.Frequency, noteListIndex, frequency);
            //_patchCalculator.SetValue(InletTypeEnum.Volume, noteListIndex, volume);

            //_patchCalculator.SetValue(InletTypeEnum.Frequency, frequency);
            //_patchCalculator.SetValue(InletTypeEnum.Volume, volume);

            string frequencyInletName = GetFrequencyInletName(noteListIndex);
            _patchCalculator.SetValue(frequencyInletName, frequency);

            string volumeInletName = GetVolumeInletName(noteListIndex);
            _patchCalculator.SetValue(volumeInletName, volume);

            if (_playingNoteCount == 0)
            {
                _audioOutputProcessor.Play();
            }

            _playingNoteCount++;
        }

        private void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            int noteListIndex = GetNoteInletListIndex(noteEvent.NoteNumber);

            double newVolume = 0.0;
            //_patchCalculator.SetValue(InletTypeEnum.Volume, noteListIndex, newVolume);

            string volumeInletName = GetVolumeInletName(noteListIndex);
            _patchCalculator.SetValue(volumeInletName, newVolume);

            ResetNoteInletListIndex(noteEvent.NoteNumber);

            _playingNoteCount--;

            if (_playingNoteCount == 0)
            {
                _audioOutputProcessor.Stop();
                //_audioOutputProcessor.ResetTime();
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
        /// </summary>
        private int GetNoteInletListIndex(int noteNumber)
        {
            int? listIndex = _noteNumber_To_NoteListIndex_Array[noteNumber];

            if (!listIndex.HasValue)
            {
                listIndex = _currentNoteListIndex;

                _noteNumber_To_NoteListIndex_Array[noteNumber] = listIndex;

                _currentNoteListIndex++;
                if (_currentNoteListIndex == MAX_CONCURRENT_NOTES)
                {
                    _currentNoteListIndex = 0;
                }
            }

            
            return listIndex.Value;
        }

        private void ResetNoteInletListIndex(int noteNumber)
        {
            _noteNumber_To_NoteListIndex_Array[noteNumber] = null;
        }

        private string GetFrequencyInletName(int noteListIndex)
        {
            return "f" + noteListIndex.ToString();
        }

        private string GetVolumeInletName(int noteListIndex)
        {
            return "v" + noteListIndex.ToString();
        }
    }
}

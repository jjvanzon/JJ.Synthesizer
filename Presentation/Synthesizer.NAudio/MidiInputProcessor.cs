﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Common;
using JJ.Framework.Exceptions.Basic;
using NAudio.Midi;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.NAudio
{
    /// <summary> thread-safe </summary>
    internal class MidiInputProcessor : IDisposable
    {
        public event EventHandler<EventArgs<(int midiNoteNumber, int midiVelocity, int midiChannel)>> MidiNoteOnOccurred;

        public event EventHandler<EventArgs<(int midiControllerCode, int absoluteMidiControllerValue, int relativeMidiControllerValue, int
            midiChannel)>> MidiControllerValueChanged;

        /// <summary> Position is left out, because there is still ambiguity between NoteIndex and ListIndex in the system. </summary>
        public event EventHandler<EventArgs<IList<(DimensionEnum dimensionEnum, string name, int? position, double value)>>> DimensionValuesChanged;

        public event EventHandler<EventArgs<Exception>> ExceptionOnMidiThreadOccurred;

        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly TimeProvider _timeProvider;
        private readonly NoteRecycler _noteRecycler;
        private readonly ScaleFacade _scaleFacade;
        private Dictionary<int, int> _midiControllerDictionary;
        private MidiIn _midiIn;
        private MidiMappingCalculator _midiMappingCalculator;

        /// <summary> Index is note number. </summary>
        private ToneDto[] _toneDtos;

        private readonly object _lock = new object();

        /// <summary> Can be called more than once. </summary>
        public MidiInputProcessor(
            Scale scale,
            IList<MidiMapping> midiMappings,
            IPatchCalculatorContainer patchCalculatorContainer,
            TimeProvider timeProvider,
            NoteRecycler noteRecycler,
            ScaleFacade scaleFacade)
        {
            _patchCalculatorContainer = patchCalculatorContainer ?? throw new NullException(() => patchCalculatorContainer);
            _timeProvider = timeProvider ?? throw new NullException(() => timeProvider);
            _noteRecycler = noteRecycler ?? throw new NullException(() => noteRecycler);
            _scaleFacade = scaleFacade ?? throw new ArgumentNullException(nameof(scaleFacade));

            UpdateScaleAndMidiMappings(scale, midiMappings);
        }

        public void UpdateScaleAndMidiMappings(Scale scale, IList<MidiMapping> midiMappings)
        {
            lock (_lock)
            {
                _midiControllerDictionary = new Dictionary<int, int>();
                _midiMappingCalculator = new MidiMappingCalculator(midiMappings);
                _toneDtos = _scaleFacade.GetToneDtosWithCompleteSetOfOctaves(scale).ToArray();
            }
        }

        public void TryStartThread()
        {
            var thread = new Thread(TryStart);
            thread.Start();
        }

        /// <summary> 
        /// For now will only work with the first MIDI device it finds. 
        /// Does nothing when no MIDI devices.
        /// </summary>
        private void TryStart()
        {
            if (MidiIn.NumberOfDevices == 0)
            {
                // TODO: Handle this better.
                return;
                //throw new Exception("No connected MIDI devices.");
            }

            const int deviceIndex = 0;

            try
            {
                lock (_lock)
                {
                    _midiIn = new MidiIn(deviceIndex);
                    _midiIn.MessageReceived += _midiIn_MessageReceived;
                    _midiIn.Start();
                }
            }
            catch
            {
                // Do not crash your whole application, if midi device communication fails.
                Dispose();
            }
        }

        public void Dispose() => Stop();

        public void Stop()
        {
            lock (_lock)
            {
                if (_midiIn == null)
                {
                    return;
                }

                try
                {
                    _midiIn.MessageReceived -= _midiIn_MessageReceived;
                    // Not sure if I need to call of these met\hods,
                    // but when I omitted one I got an error upon application exit.
                    _midiIn.Stop();
                    _midiIn.Close();
                    _midiIn.Dispose();
                }
                catch
                {
                    // Do not crash your whole application, if midi device communication fails.
                }
            }
        }

        private void _midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            try
            {
                switch (e.MidiEvent.CommandCode)
                {
                    case MidiCommandCode.NoteOn:
                        HandleNoteOn(e.MidiEvent);
                        break;

                    case MidiCommandCode.NoteOff:
                        HandleNoteOff(e.MidiEvent);
                        break;

                    case MidiCommandCode.ControlChange:
                        HandleControlChange(e.MidiEvent);
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionOnMidiThreadOccurred(this, new EventArgs<Exception>(ex));
            }
        }

        private void HandleNoteOn(MidiEvent midiEvent)
        {
            // Copy to local variables
            var noteOnEvent = (NoteOnEvent)midiEvent;
            int midiNoteNumber = noteOnEvent.NoteNumber;
            int midiVelocity = noteOnEvent.Velocity;
            int midiChannel = noteOnEvent.Channel;

            (DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)[] dimensionValues;
            (DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)[] extraDimensionValues = null;

            MidiNoteOnOccurred(this, new EventArgs<(int, int, int)>((midiNoteNumber, midiVelocity, midiChannel)));

            // Lock wide enough to freeze time. (You cannot get note infos and reset notes at a different time.)
            // As a consequence, you also have to lock the calculation while applying MidiMappings,
            ReaderWriterLockSlim calculatorLock = _patchCalculatorContainer.Lock;
            calculatorLock.EnterWriteLock();
            try
            {
                IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
                if (calculator == null)
                {
                    return;
                }

                double time = _timeProvider.Time;

                NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToStart(midiNoteNumber, time);
                if (noteInfo == null)
                {
                    return;
                }

                noteInfo.MidiNoteNumber = midiNoteNumber;
                noteInfo.MidiVelocity = midiVelocity;
                noteInfo.MidiChannel = midiChannel;
                int noteIndex = noteInfo.ListIndex;

                calculator.Reset(time, noteIndex);

                dimensionValues = _midiMappingCalculator.CalculateForMidiNote(noteInfo.MidiNoteNumber, noteInfo.MidiVelocity, noteInfo.MidiChannel);

                int count = dimensionValues.Length;
                for (int i = 0; i < count; i++)
                {
                    (DimensionEnum dimensionEnum, string canonicalName, _, double dimensionValue) = dimensionValues[i];

                    // Apply Dimension-Related MIDI Mappings
                    if (dimensionEnum != default)
                    {
                        calculator.SetValue(dimensionEnum, noteIndex, dimensionValue);
                    }

                    if (canonicalName != "")
                    {
                        calculator.SetValue(canonicalName, noteIndex, dimensionValue);
                    }

                    // Apply Scale-Related MIDI Mappings
                    ToneDto toneDto = TryGetToneDto(dimensionEnum, dimensionValue);
                    if (toneDto != null)
                    {
                        double frequency = toneDto.Frequency;
                        double octave = toneDto.Octave;
                        double ordinal = toneDto.Ordinal;
                        double toneValue = toneDto.Value;
                        double scaleBaseFrequency = toneDto.ScaleBaseFrequency;

                        calculator.SetValue(DimensionEnum.Frequency, noteIndex, frequency);
                        calculator.SetValue(DimensionEnum.Octave, noteIndex, octave);
                        calculator.SetValue(DimensionEnum.Ordinal, noteIndex, ordinal);
                        calculator.SetValue(DimensionEnum.ToneValue, noteIndex, toneValue);
                        calculator.SetValue(DimensionEnum.ScaleBaseFrequency, noteIndex, scaleBaseFrequency);

                        extraDimensionValues = new (DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)[]
                        {
                            (DimensionEnum.Frequency, "", null, frequency),
                            (DimensionEnum.Octave, "", null, octave),
                            (DimensionEnum.Ordinal, "", null, ordinal)
                        };
                    }
                }

                calculator.SetValue(DimensionEnum.NoteStart, noteIndex, time);
                calculator.SetValue(DimensionEnum.NoteDuration, noteIndex, CalculationHelper.VERY_HIGH_VALUE);
            }
            finally
            {
                calculatorLock.ExitWriteLock();
            }

            RaiseDimensionValuesChanged(dimensionValues, extraDimensionValues);
        }

        private void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;
            lck.EnterWriteLock();
            try
            {
                IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
                if (calculator == null)
                {
                    return;
                }

                double time = _timeProvider.Time;

                NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToRelease(noteEvent.NoteNumber, time);
                if (noteInfo == null)
                {
                    return;
                }

                double noteStart = calculator.GetValue(DimensionEnum.NoteStart, noteInfo.ListIndex);
                double noteDuration = time - noteStart;
                calculator.SetValue(DimensionEnum.NoteDuration, noteInfo.ListIndex, noteDuration);

                double releaseDuration = calculator.GetValue(DimensionEnum.ReleaseDuration, noteInfo.ListIndex);
                double releaseTime = noteStart + noteDuration;
                double endTime = releaseTime + releaseDuration;

                _noteRecycler.ReleaseNote(noteInfo, releaseTime, endTime);
            }
            finally
            {
                lck.ExitWriteLock();
            }
        }

        private void HandleControlChange(MidiEvent midiEvent)
        {
            var midiControlChangeEvent = (ControlChangeEvent)midiEvent;
            int midiControllerCode = (int)midiControlChangeEvent.Controller;
            int midiControllerValue = midiControlChangeEvent.ControllerValue;
            int midiChannel = midiControlChangeEvent.Channel;
            int absoluteMidiControllerValue;
            (DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)[] dimensionValues;

            ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;
            lck.EnterWriteLock();
            try
            {
                IPatchCalculator patchCalculator = _patchCalculatorContainer.Calculator;
                if (patchCalculator == null)
                {
                    return;
                }

                if (!_midiControllerDictionary.TryGetValue(midiControllerCode, out int previousControllerValue))
                {
                    // Initialize Remembered ControllerValue

                    // Get Dimension from MidiMapping
                    (DimensionEnum dimensionEnum, string canonicalName, int? position) =
                        _midiMappingCalculator.GetDimensionForMidiControllerOrDefault(midiControllerCode);

                    // Get Value from PatchCalculator
                    double? dimensionValue = null;
                    if (position.HasValue)
                    {
                        if (dimensionEnum != default) dimensionValue = patchCalculator.GetValue(dimensionEnum, position.Value);
                        if (canonicalName != "") dimensionValue = patchCalculator.GetValue(canonicalName, position.Value);
                    }
                    else
                    {
                        if (dimensionEnum != default) dimensionValue = patchCalculator.GetValue(dimensionEnum);
                        if (canonicalName != "") dimensionValue = patchCalculator.GetValue(canonicalName);
                    }

                    // Convert Dimension Value to Midi Controller Value
                    int? previousControllerValueNullable = null;
                    if (dimensionValue.HasValue)
                    {
                        previousControllerValueNullable = _midiMappingCalculator.CalculateMidiControllerValueOrNull(midiControllerCode, dimensionValue.Value);
                    }

                    previousControllerValue = previousControllerValueNullable ?? MidiMappingCalculator.CENTER_CONTROLLER_VALUE;
                }

                absoluteMidiControllerValue = _midiMappingCalculator.ToAbsoluteControllerValue(
                    midiControllerCode,
                    midiControllerValue,
                    previousControllerValue);

                _midiControllerDictionary[midiControllerCode] = absoluteMidiControllerValue;

                dimensionValues = _midiMappingCalculator.CalculateForMidiController(midiControllerCode, absoluteMidiControllerValue);

                int count = dimensionValues.Length;
                for (int i = 0; i < count; i++)
                {
                    (DimensionEnum dimensionEnum, string canonicalName, _, double dimensionValue) = dimensionValues[i];

                    if (dimensionEnum != default)
                    {
                        patchCalculator.SetValue(dimensionEnum, dimensionValue);
                    }

                    if (canonicalName != "")
                    {
                        patchCalculator.SetValue(canonicalName, dimensionValue);
                    }
                }
            }
            finally
            {
                lck.ExitWriteLock();
            }

            MidiControllerValueChanged(
                this,
                new EventArgs<(int, int, int, int)>((midiControllerCode, absoluteMidiControllerValue, midiControllerValue, midiChannel)));
            RaiseDimensionValuesChanged(dimensionValues);
        }

        private ToneDto TryGetToneDto(DimensionEnum dimensionEnum, double dimensionValue)
        {
            if (dimensionEnum != DimensionEnum.NoteNumber) return null;

            double value = dimensionValue;

            if (value <= 0) value = 0;
            if (value > _toneDtos.Length - 1) value = _toneDtos.Length - 1;

            int index = (int)value;

            ToneDto toneDto = _toneDtos[index];

            return toneDto;
        }

        private void RaiseDimensionValuesChanged(
            IList<(DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)> dimensionValues,
            IList<(DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)> extraDimensionValues = null)
        {
            if (extraDimensionValues != null)
            {
                var e = new EventArgs<IList<(DimensionEnum, string, int?, double)>>(dimensionValues.Concat(extraDimensionValues).ToArray());
                DimensionValuesChanged(this, e);
            }
            else
            {
                var e = new EventArgs<IList<(DimensionEnum, string, int?, double)>>(dimensionValues);
                DimensionValuesChanged(this, e);
            }
        }
    }
}
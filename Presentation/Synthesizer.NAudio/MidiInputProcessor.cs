using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using System.Threading;
using System.Diagnostics;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.NAudio
{
    /// <summary> thread-safe </summary>
    internal static class MidiInputProcessor
    {
        private class ControllerInfo
        {
            public DimensionEnum DimensionEnum { get; set; }
            public int ControllerCode { get; set; }
            public double MinValue { get; set; } = CalculationHelper.VERY_LOW_VALUE;
            public double ConversionFactor { get; set; }
            public double TempValue { get; set; }
        }

        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;
        private const double MAX_CONTROLLER_VALUE = 127.0;

        private static readonly Dictionary<int, ControllerInfo> _controllerCode_To_ControllerInfo_Dictionary = Create_ControllerCode_To_ControllerInfo_Dictionary();
        private static readonly double[] _noteNumber_To_Frequency_Array = Create_NoteNumber_To_Frequency_Array();

        private static IPatchCalculatorContainer _patchCalculatorContainer;
        private static TimeProvider _timeProvider;
        private static NoteRecycler _noteRecycler;

        private static MidiIn _midiIn;

        private static readonly object _lock = new object();

        /// <summary> Can be called more than once. </summary>
        public static void Initialize(
            IPatchCalculatorContainer patchCalculatorContainer, 
            TimeProvider timeProvider, 
            NoteRecycler noteRecycler)
        {
            lock (_lock)
            {
                // NOTE: Don't turn into throw expressions.
                // You want to check them all for null, before assigning any of them.
                if (patchCalculatorContainer == null) throw new NullException(() => patchCalculatorContainer);
                if (timeProvider == null) throw new NullException(() => timeProvider);
                if (noteRecycler == null) throw new NullException(() => noteRecycler);

                _patchCalculatorContainer = patchCalculatorContainer;
                _timeProvider = timeProvider;
                _noteRecycler = noteRecycler;
            }
        }

        public static Thread StartThread()
        {
            var thread = new Thread(TryStart);
            thread.Start();

            return thread;
        }

        /// <summary> 
        /// For now will only work with the first MIDI device it finds. 
        /// Does nothing when no MIDI devices. 
        /// </summary>
        private static void TryStart()
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
                Stop();
            }
        }

        public static void Stop()
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
                    // Not sure if I need to call of these methods,
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

        private static void _midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
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

        private static void HandleNoteOn(MidiEvent midiEvent)
        {
            var noteOnEvent = (NoteOnEvent)midiEvent;

            ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

            lck.EnterWriteLock();
            try
            {
                double time = _timeProvider.Time;

                NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToStart(noteOnEvent.NoteNumber, time);
                if (noteInfo == null)
                {
                    return;
                }

                IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
                if (calculator == null)
                {
                    return;
                }

                double frequency = _noteNumber_To_Frequency_Array[noteOnEvent.NoteNumber];
                double volume = noteOnEvent.Velocity / MAX_VELOCITY;

                // Remember controller values.
                foreach (ControllerInfo controllerInfo in _controllerCode_To_ControllerInfo_Dictionary.Values)
                {
                    double controllerValue = calculator.GetValue(controllerInfo.DimensionEnum);
                    controllerInfo.TempValue = controllerValue;
                }

                calculator.Reset(time, noteInfo.ListIndex);

                calculator.SetValue(DimensionEnum.Frequency, noteInfo.ListIndex, frequency);
                calculator.SetValue(DimensionEnum.Volume, noteInfo.ListIndex, volume);
                calculator.SetValue(DimensionEnum.NoteStart, noteInfo.ListIndex, time);
                calculator.SetValue(DimensionEnum.NoteDuration, noteInfo.ListIndex, CalculationHelper.VERY_HIGH_VALUE);

                // Re-apply controller values
                foreach (ControllerInfo controllerInfo in _controllerCode_To_ControllerInfo_Dictionary.Values)
                {
                    double controllerValue = controllerInfo.TempValue;

                    if (controllerValue < controllerInfo.MinValue)
                    {
                        controllerValue = controllerInfo.MinValue;
                    }

                    calculator.SetValue(controllerInfo.DimensionEnum, noteInfo.ListIndex, controllerValue);
                }
            }
            finally
            {
                lck.ExitWriteLock();
            }
        }

        private static void HandleNoteOff(MidiEvent midiEvent)
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
                _noteRecycler.ReleaseNoteInfo(noteInfo, releaseTime, endTime);
            }
            finally
            {
                lck.ExitWriteLock();
            }
        }

        private static void HandleControlChange(MidiEvent midiEvent)
        {
            var controlChangeEvent = (ControlChangeEvent)midiEvent;

            Debug.WriteLine("ControlChange value received: {0} = {1}", controlChangeEvent.Controller, controlChangeEvent.ControllerValue);

            int controllerCode = (int)controlChangeEvent.Controller;

            if (!_controllerCode_To_ControllerInfo_Dictionary.TryGetValue(controllerCode, out ControllerInfo controllerInfo))
            {
                return;
            }

            double delta = (controlChangeEvent.ControllerValue - 64) * controllerInfo.ConversionFactor;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (delta == 0.0)
            {
                return;
            }

            ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

            lck.EnterWriteLock();
            try
            {
                IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
                if (calculator == null)
                {
                    return;
                }

                double value = calculator.GetValue(controllerInfo.DimensionEnum);

                value += delta;

                if (value < controllerInfo.MinValue)
                {
                    value = controllerInfo.MinValue;
                }

                calculator.SetValue(controllerInfo.DimensionEnum, value);

                Debug.WriteLine($"{controllerInfo.DimensionEnum} = {value}");
            }
            finally
            {
                lck.ExitWriteLock();
            }
        }

        // Helpers

        private static double[] Create_NoteNumber_To_Frequency_Array()
        {
            IList<double> frequencies = new List<double>(MAX_NOTE_NUMBER + 1);

            for (int i = 0; i < MAX_NOTE_NUMBER; i++)
            {
                double frequency = GetFrequencyByNoteNumber(i);
                frequencies.Add(frequency);
            }

            double[] noteNumber_To_Frequency_Array = frequencies.ToArray();

            return noteNumber_To_Frequency_Array;
        }

        private static double GetFrequencyByNoteNumber(int noteNumber)
        {
            double frequency = LOWEST_FREQUENCY * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }

        private static Dictionary<int, ControllerInfo> Create_ControllerCode_To_ControllerInfo_Dictionary()
        {
            const double controllerFactorForVolumeChangeRate = 4.0 / MAX_CONTROLLER_VALUE;
            const double controllerFactorForFilters = 8.0 / MAX_CONTROLLER_VALUE;
            const double controllerFactorForModulationSpeed = 30.0 / MAX_CONTROLLER_VALUE;

            var controllerInfos = new[]
            {
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.AttackDuration,
                    ControllerCode = 73, // Recommended code
                    MinValue = 0.001,
                    ConversionFactor = controllerFactorForVolumeChangeRate
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.ReleaseDuration,
                    ControllerCode = 72, // Recommended code
                    MinValue = 0.001,
                    ConversionFactor = controllerFactorForVolumeChangeRate
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.Brightness,
                    ControllerCode = 74, // Recommended code
                    MinValue =  1.00001, // 1 shuts off the sound.
                    ConversionFactor = controllerFactorForFilters
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.VibratoSpeed,
                    ControllerCode = 76, // Default on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.VibratoDepth,
                    ControllerCode = 77, // Default on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = 0.0005 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.TremoloDepth,
                    ControllerCode = 92, // Recommended code. However, not mapped by default on my Arturia MiniLab.
                    MinValue = 0,
                    ConversionFactor = 4.0 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.TremoloSpeed,
                    ControllerCode = 16, // Right below vibrato on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.TremoloDepth,
                    ControllerCode = 17, // Right below vibrato on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = 1.0 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.Intensity,
                    ControllerCode = 71, // Resonance on Arturia MiniLab. Recommended code for Timbre/Harmonic Content.
                    MinValue = 0,
                    ConversionFactor = controllerFactorForFilters
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.DecayDuration,
                    ControllerCode = 75, // Decay on Arturia MiniLab. Recommended code for 'Sound Controller 6'
                    MinValue = 0.00001,
                    ConversionFactor = controllerFactorForVolumeChangeRate
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.SustainVolume,
                    ControllerCode = 79, // Decay on Arturia MiniLab. Recommended code for 'Sound Controller 10'.
                    MinValue = 0,
                    ConversionFactor = 1.0 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.BrightnessModulationSpeed,
                    ControllerCode = 18, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed 
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.BrightnessModulationDepth,
                    ControllerCode = 19, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForFilters
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.IntensityModulationSpeed,
                    ControllerCode = 93, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed
                },
                new ControllerInfo
                {
                    DimensionEnum = DimensionEnum.IntensityModulationDepth,
                    ControllerCode = 91, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForFilters
                },
            };

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = controllerInfos.ToDictionary(x => x.ControllerCode);
            return dictionary;
        }
    }
}
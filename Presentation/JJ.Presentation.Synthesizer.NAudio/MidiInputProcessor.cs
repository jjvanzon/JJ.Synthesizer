using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Calculation;
using System.Threading;
using System.Diagnostics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class MidiInputProcessor
    {
        private class ControllerInfo
        {
            public InletTypeEnum InletTypeEnum { get; set; }
            public int ControllerCode { get; set; }
            public double MinValue { get; set; } = CalculationHelper.VERY_LOW_VALUE;
            public double ConversionFactor { get; set; }
            public double TempValue { get; set; }
        }

        private const int DEFAULT_CHANNEL_INDEX = 0; // TODO: Make mult-channel.
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;
        private const double MAX_CONTROLLER_VALUE = 127.0;

        private static readonly Dictionary<int, ControllerInfo> _controllerCode_To_ControllerInfo_Dictionary = Create_ControllerCode_To_ControllerInfo_Dictionary();
        private static readonly double[] _noteNumber_To_Frequency_Array = Create_NoteNumber_To_Frequency_Array();

        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly AudioOutputProcessor _audioOutputProcessor;

        private MidiIn _midiIn;
        private NoteRecycler _noteRecycler;

        public MidiInputProcessor(
            IPatchCalculatorContainer patchCalculatorContainer, 
            AudioOutputProcessor audioOutputProcessor,
            NoteRecycler noteRecycler)
        {
            if (patchCalculatorContainer == null) throw new NullException(() => patchCalculatorContainer);
            if (audioOutputProcessor == null) throw new NullException(() => audioOutputProcessor);
            if (noteRecycler == null) throw new NullException(() => noteRecycler);

            _patchCalculatorContainer = patchCalculatorContainer;
            _audioOutputProcessor = audioOutputProcessor;
            _noteRecycler = noteRecycler;
        }

        /// <summary> 
        /// For now will only work with the first MIDI device it finds. 
        /// Does nothing when no MIDI devices. 
        /// </summary>
        public void TryStart()
        {
            if (MidiIn.NumberOfDevices == 0)
            {
                // TODO: Handle this better.
                return;
                //throw new Exception("No connected MIDI devices.");
            }

            int deviceIndex = 0;

            var midiIn = new MidiIn(deviceIndex);
            midiIn.MessageReceived += _midiIn_MessageReceived;
            midiIn.Start();

            _midiIn = midiIn;
        }

        public void Stop()
        {
            if (_midiIn != null)
            {
                _midiIn.MessageReceived -= _midiIn_MessageReceived;
                // Not sure if I need to call of these methods, but when I omitted one I got an error upon application exit.
                _midiIn.Stop();
                _midiIn.Close();
                _midiIn.Dispose();
            }
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

                case MidiCommandCode.ControlChange:
                    HandleControlChange(e.MidiEvent);
                    break;
            }
        }

        private void HandleNoteOn(MidiEvent midiEvent)
        {
            var noteOnEvent = (NoteOnEvent)midiEvent;

            ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

            lck.EnterWriteLock();
            try
            {
                double time = _audioOutputProcessor.Time;

                NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToStart(noteOnEvent.NoteNumber, time);
                if (noteInfo == null)
                {
                    return;
                }

                IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
                if (calculator != null)
                {
                    double frequency = _noteNumber_To_Frequency_Array[noteOnEvent.NoteNumber];
                    double volume = noteOnEvent.Velocity / MAX_VELOCITY;

                    // Remember controller values.
                    foreach (ControllerInfo controllerInfo in _controllerCode_To_ControllerInfo_Dictionary.Values)
                    {
                        double controllerValue = calculator.GetValue(controllerInfo.InletTypeEnum);
                        controllerInfo.TempValue = controllerValue;
                    }

                    calculator.Reset(time, DEFAULT_CHANNEL_INDEX, noteInfo.ListIndex);

                    calculator.SetValue(InletTypeEnum.Frequency, noteInfo.ListIndex, frequency);
                    calculator.SetValue(InletTypeEnum.Volume, noteInfo.ListIndex, volume);
                    calculator.SetValue(InletTypeEnum.NoteStart, noteInfo.ListIndex, time);
                    calculator.SetValue(InletTypeEnum.NoteDuration, noteInfo.ListIndex, CalculationHelper.VERY_HIGH_VALUE);

                    // Apply controller values
                    foreach (ControllerInfo controllerInfo in _controllerCode_To_ControllerInfo_Dictionary.Values)
                    {
                        double controllerValue = controllerInfo.TempValue;

                        if (controllerValue < controllerInfo.MinValue)
                        {
                            controllerValue = controllerInfo.MinValue;
                        }

                        calculator.SetValue(controllerInfo.InletTypeEnum, noteInfo.ListIndex, controllerValue);
                    }
                }
            }
            finally
            {
                lck.ExitWriteLock();
            }
        }

        private void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

            lck.EnterWriteLock();
            try
            {
                IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
                if (calculator != null)
                {
                    double time = _audioOutputProcessor.Time;

                    NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToRelease(noteEvent.NoteNumber, time);
                    if (noteInfo == null)
                    {
                        return;
                    }

                    double noteStart = calculator.GetValue(InletTypeEnum.NoteStart, noteInfo.ListIndex);
                    double noteDuration = time - noteStart;
                    calculator.SetValue(InletTypeEnum.NoteDuration, noteInfo.ListIndex, noteDuration);

                    double releaseDuration = calculator.GetValue(InletTypeEnum.ReleaseDuration, noteInfo.ListIndex);
                    double releaseTime = noteStart + noteDuration;
                    double endTime = releaseTime + releaseDuration;
                    _noteRecycler.ReleaseNoteInfo(noteInfo, releaseTime, endTime);
                }
            }
            finally
            {
                lck.ExitWriteLock();
            }
        }

        private void HandleControlChange(MidiEvent midiEvent)
        {
            var controlChangeEvent = (ControlChangeEvent)midiEvent;

            Debug.WriteLine("ControlChange value received: {0} = {1}", controlChangeEvent.Controller, controlChangeEvent.ControllerValue);

            int controllerCode = (int)controlChangeEvent.Controller;

            ControllerInfo controllerInfo;
            if (_controllerCode_To_ControllerInfo_Dictionary.TryGetValue(controllerCode, out controllerInfo))
            {
                double delta = (controlChangeEvent.ControllerValue - 64) * controllerInfo.ConversionFactor;

                if (delta == 0.0)
                {
                    return;
                }

                ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

                lck.EnterWriteLock();
                try
                {
                    IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
                    if (calculator != null)
                    {
                        double value = calculator.GetValue(controllerInfo.InletTypeEnum);

                        value += delta;

                        if (value < controllerInfo.MinValue)
                        {
                            value = controllerInfo.MinValue;
                        }

                        calculator.SetValue(controllerInfo.InletTypeEnum, value);

                        Debug.WriteLine(String.Format("{0} = {1}", controllerInfo.InletTypeEnum, value));
                    }
                }
                finally
                {
                    lck.ExitWriteLock();
                }
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
            double controllerFactorForVolumeChangeRate = 4.0 / MAX_CONTROLLER_VALUE;
            double controllerFactorForFilters = 8.0 / MAX_CONTROLLER_VALUE;
            double controllerFactorForModulationSpeed = 30.0 / MAX_CONTROLLER_VALUE;

            var controllerInfos = new ControllerInfo[]
            {
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.AttackDuration,
                    ControllerCode = 73, // Recommended code
                    MinValue = 0.001,
                    ConversionFactor = controllerFactorForVolumeChangeRate
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.ReleaseDuration,
                    ControllerCode = 72, // Recommended code
                    MinValue = 0.001,
                    ConversionFactor = controllerFactorForVolumeChangeRate
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.Brightness,
                    ControllerCode = 74, // Recommended code
                    MinValue =  1.00001, // 1 shuts off the sound.
                    ConversionFactor = controllerFactorForFilters
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.VibratoSpeed,
                    ControllerCode = 76, // Default on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.VibratoDepth,
                    ControllerCode = 77, // Default on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = 0.0005 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.TremoloDepth,
                    ControllerCode = 92, // Recommended code. However, not mapped by default on my Arturia MiniLab.
                    MinValue = 0,
                    ConversionFactor = 4.0 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.TremoloSpeed,
                    ControllerCode = 16, // Right below vibrato on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.TremoloDepth,
                    ControllerCode = 17, // Right below vibrato on Arturia MiniLab
                    MinValue = 0,
                    ConversionFactor = 1.0 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.Intensity,
                    ControllerCode = 71, // Resonance on Arturia MiniLab. Recommended code for Timbre/Harmonic Content.
                    MinValue = 0,
                    ConversionFactor = controllerFactorForFilters
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.DecayDuration,
                    ControllerCode = 75, // Decay on Arturia MiniLab. Recommended code for 'Sound Controller 6'
                    MinValue = 0.00001,
                    ConversionFactor = controllerFactorForVolumeChangeRate
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.SustainVolume,
                    ControllerCode = 79, // Decay on Arturia MiniLab. Recommended code for 'Sound Controller 10'.
                    MinValue = 0,
                    ConversionFactor = 1.0 / MAX_CONTROLLER_VALUE
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.BrightnessModulationSpeed,
                    ControllerCode = 18, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed 
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.BrightnessModulationDepth,
                    ControllerCode = 19, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForFilters
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.IntensityModulationSpeed,
                    ControllerCode = 93, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForModulationSpeed
                },
                new ControllerInfo
                {
                    InletTypeEnum = InletTypeEnum.IntensityModulationDepth,
                    ControllerCode = 91, // Completely arbitrarily mapped on left-over knobs on my Artirua MiniLab
                    MinValue = 0,
                    ConversionFactor = controllerFactorForFilters
                },
            };

            var dictionary = controllerInfos.ToDictionary(x => x.ControllerCode);
            return dictionary;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Calculation;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public static class MidiInputProcessor
    {
        private class NoteInfo
        {
            public int NoteNumber { get; set; }
            public int ListIndex { get; set; }
            public double EndTime { get; set; }
        }

        private const double DEFAULT_AMPLIFIER = 0.2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;
        private const int DEFAULT_MAX_CONCURRENT_NOTES = 4;

        private static readonly double[] _noteNumber_To_Frequency_Array = Create_NoteNumber_To_Frequency_Array();
        private static readonly List<NoteInfo> _noteInfos = new List<NoteInfo>();
    
        private static MidiIn _midiIn;
        private static volatile int _maxConcurrentNotes = DEFAULT_MAX_CONCURRENT_NOTES;

        public static int MaxConcurrentNotes
        {
            get { return _maxConcurrentNotes; }
            set { _maxConcurrentNotes = value; }
        }

        /// <summary> 
        /// For now will only work with the first MIDI device it finds. 
        /// Does nothing when no MIDI devices. 
        /// </summary>
        public static void TryStart()
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

        public static void Stop()
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
            }
        }

        private static void HandleNoteOn(MidiEvent midiEvent)
        {
            var noteOnEvent = (NoteOnEvent)midiEvent;

            double time = AudioOutputProcessor.Time;

            NoteInfo noteInfo = TryUpdateOrCreateNoteInfo(noteOnEvent.NoteNumber, time);
            if (noteInfo == null)
            {
                // No more note slots available.
                return;
            }

            double frequency = _noteNumber_To_Frequency_Array[noteOnEvent.NoteNumber];
            double volume = noteOnEvent.Velocity / MAX_VELOCITY;

            PatchCalculatorContainer.Lock.EnterUpgradeableReadLock();
            try
            {
                IPatchCalculator patchCalculator = PatchCalculatorContainer.PatchCalculator;
                if (patchCalculator != null)
                {
                    PatchCalculatorContainer.Lock.EnterWriteLock();
                    try
                    {
                        // Beware that if initial state produces NaN, this destroys all other notes than the new one. (2016-01-13)
                        patchCalculator.ResetState();
                        patchCalculator.SetValue(InletTypeEnum.Frequency, noteInfo.ListIndex, frequency);
                        patchCalculator.SetValue(InletTypeEnum.Volume, noteInfo.ListIndex, volume);
                        patchCalculator.SetValue(InletTypeEnum.NoteStart, noteInfo.ListIndex, time);
                        patchCalculator.SetValue(InletTypeEnum.NoteDuration, noteInfo.ListIndex, CalculationHelper.VERY_HIGH_VALUE);
                        // TODO: This is a hack to make ReleaseNote read release duration,
                        // because the value dictionaries in the calculator does not contain the default values of the patch.
                        patchCalculator.SetValue(InletTypeEnum.ReleaseDuration, noteInfo.ListIndex, 1);
                    }
                    finally
                    {
                        PatchCalculatorContainer.Lock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                PatchCalculatorContainer.Lock.ExitUpgradeableReadLock();
            }
        }

        private static void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            double time = AudioOutputProcessor.Time;

            NoteInfo noteInfo = TryGetNoteInfo(noteEvent.NoteNumber, time);
            if (noteInfo == null)
            {
                // Note was ignored earlier, due to not enough slots.
                return;
            }

            PatchCalculatorContainer.Lock.EnterUpgradeableReadLock();
            try
            {
                IPatchCalculator patchCalculator = PatchCalculatorContainer.PatchCalculator;
                if (patchCalculator != null)
                {
                    PatchCalculatorContainer.Lock.EnterWriteLock();
                    try
                    {
                        double noteStart = patchCalculator.GetValue(InletTypeEnum.NoteStart, noteInfo.ListIndex);
                        double noteDuration = time - noteStart;
                        patchCalculator.SetValue(InletTypeEnum.NoteDuration, noteInfo.ListIndex, noteDuration);

                        double releaseDuration = patchCalculator.GetValue(InletTypeEnum.ReleaseDuration, noteInfo.ListIndex);
                        double endTime = noteStart + noteDuration + releaseDuration;
                        ReleaseNote(noteEvent.NoteNumber, time, endTime);
                    }
                    finally
                    {
                        PatchCalculatorContainer.Lock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                PatchCalculatorContainer.Lock.ExitUpgradeableReadLock();
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

        /// <summary> Returns null if max concurrent notes was exceeded. </summary>
        private static NoteInfo TryUpdateOrCreateNoteInfo(int noteNumber, double time)
        {
            NoteInfo alreadyPlayingNoteInfo = _noteInfos.Where(x => x.NoteNumber == noteNumber &&
                                                                    x.EndTime > time)
                                                        .SingleOrDefault();  // TODO: Perhaps later change to FirstOrDefault for performance.
            if (alreadyPlayingNoteInfo != null)
            {
                alreadyPlayingNoteInfo.EndTime = CalculationHelper.VERY_HIGH_VALUE;
                return alreadyPlayingNoteInfo;
            }

            NoteInfo freeNoteInfoSlot = _noteInfos.Where(x => x.EndTime <= time).FirstOrDefault();
            if (freeNoteInfoSlot != null)
            {
                freeNoteInfoSlot.NoteNumber = noteNumber;
                freeNoteInfoSlot.EndTime = CalculationHelper.VERY_HIGH_VALUE;
                return freeNoteInfoSlot;
            }

            if (_noteInfos.Count < _maxConcurrentNotes)
            {
                var newNoteInfo = new NoteInfo
                {
                    NoteNumber = noteNumber,
                    EndTime = CalculationHelper.VERY_HIGH_VALUE,
                    ListIndex = _noteInfos.Count
                };

                _noteInfos.Add(newNoteInfo);

                return newNoteInfo;
            }

            return null;
        }

        private static NoteInfo TryGetNoteInfo(int noteNumber, double time)
        {
            NoteInfo noteInfo = _noteInfos.Where(x => x.NoteNumber == noteNumber &&
                                                      x.EndTime > time)
                                          .SingleOrDefault(); // TODO: Perhaps later change to FirstOrDefault for performance.
            return noteInfo;
        }

        private static NoteInfo GetNoteInfo(int noteNumber, double time)
        {
            NoteInfo noteInfo = TryGetNoteInfo(noteNumber, time);
            if (noteInfo == null)
            {
                throw new Exception(String.Format("NoteInfo for found for noteNumber '{0}' and time '{0}'.", noteNumber, time));
            }
            return noteInfo;
        }

        private static void ReleaseNote(int noteNumber, double time, double endTime)
        {
            NoteInfo noteInfo = GetNoteInfo(noteNumber, time);
            noteInfo.EndTime = endTime;
        }

        private static double GetFrequencyByNoteNumber(int noteNumber)
        {
            double frequency = LOWEST_FREQUENCY * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }
    }
}

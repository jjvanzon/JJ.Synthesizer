using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Calculation;
using System.Threading;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public static class MidiInputProcessor
    {
        private const double DEFAULT_AMPLIFIER = 0.2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;

        private static readonly double[] _noteNumber_To_Frequency_Array = Create_NoteNumber_To_Frequency_Array();

        private static MidiIn _midiIn;
        private static NoteRecycler _noteRecycler = new NoteRecycler();

        public static int MaxConcurrentNotes
        {
            get { return _noteRecycler.MaxConcurrentNotes; }
            set { _noteRecycler.MaxConcurrentNotes = value; }
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

            ReaderWriterLockSlim lck = PolyphonyCalculatorContainer.Lock;

            lck.EnterWriteLock();
            try
            {
                double time = AudioOutputProcessor.Time;

                NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToStart(noteOnEvent.NoteNumber, time);
                if (noteInfo == null)
                {
                    return;
                }

                PolyphonyCalculator calculator = PolyphonyCalculatorContainer.Calculator;
                if (calculator != null)
                {
                    double frequency = _noteNumber_To_Frequency_Array[noteOnEvent.NoteNumber];
                    double volume = noteOnEvent.Velocity / MAX_VELOCITY;

                    calculator.ResetState(noteInfo.ListIndex);
                    calculator.SetValue(InletTypeEnum.Frequency, noteInfo.ListIndex, frequency);
                    calculator.SetValue(InletTypeEnum.Volume, noteInfo.ListIndex, volume);
                    calculator.SetValue(InletTypeEnum.NoteStart, noteInfo.ListIndex, time);
                    calculator.SetValue(InletTypeEnum.NoteDuration, noteInfo.ListIndex, CalculationHelper.VERY_HIGH_VALUE);
                    // TODO: This is a hack to make ReleaseNote read release duration,
                    // because the value dictionaries in the calculator does not contain the default values of the patch.
                    calculator.SetValue(InletTypeEnum.ReleaseDuration, noteInfo.ListIndex, 1);
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

            ReaderWriterLockSlim lck = PolyphonyCalculatorContainer.Lock;

            lck.EnterWriteLock();
            try
            {
                PolyphonyCalculator calculator = PolyphonyCalculatorContainer.Calculator;
                if (calculator != null)
                {
                    double time = AudioOutputProcessor.Time;

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
    }
}

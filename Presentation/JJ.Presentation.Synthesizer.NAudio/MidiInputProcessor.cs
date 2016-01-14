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
        private const double DEFAULT_AMPLIFIER = 0.2;
        private const double LOWEST_FREQUENCY = 8.1757989156;
        private const double MAX_VELOCITY = 127.0;
        private const int MAX_NOTE_NUMBER = 127;
        private const int DEFAULT_MAX_CONCURRENT_NOTES = 4;

        private static readonly double[] _noteNumber_To_Frequency_Array = Create_NoteNumber_To_Frequency_Array();
        private static readonly Dictionary<int, int> _noteNumber_To_NoteListIndex_Dictionary = new Dictionary<int, int>();
    
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
            bool mustStartPlayingAudioOutput = _noteNumber_To_NoteListIndex_Dictionary.Count == 0;

            var noteOnEvent = (NoteOnEvent)midiEvent;

            int? noteListIndex = TryGetNoteListIndex(noteOnEvent.NoteNumber);
            if (!noteListIndex.HasValue)
            {
                // No more note slots available.
                return;
            }

            double frequency = _noteNumber_To_Frequency_Array[noteOnEvent.NoteNumber];
            double volume = noteOnEvent.Velocity / MAX_VELOCITY;
            double noteStart = AudioOutputProcessor.Time; // TODO: This may be a little early to get the time.

            PatchCalculatorContainer.Lock.EnterUpgradeableReadLock();
            try
            {
                IPatchCalculator patchCalculator = PatchCalculatorContainer.PatchCalculator;
                if (patchCalculator != null)
                {
                    PatchCalculatorContainer.Lock.EnterWriteLock();
                    try
                    {
                        // Temporarily disabled, because if initial state produces NaN, this destroys all other notes than the new one. (2016-01-13)
                        patchCalculator.ResetState();
                        patchCalculator.SetValue(InletTypeEnum.Frequency, noteListIndex.Value, frequency);
                        patchCalculator.SetValue(InletTypeEnum.Volume, noteListIndex.Value, volume);
                        patchCalculator.SetValue(InletTypeEnum.NoteStart, noteListIndex.Value, noteStart);
                        patchCalculator.SetValue(InletTypeEnum.NoteDuration, noteListIndex.Value, CalculationHelper.VERY_HIGH_VALUE);
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

            if (mustStartPlayingAudioOutput)
            {
                // Temporarily call another method for debugging (2016-01-09).
                //AudioOutputProcessor.Continue();
                //AudioOutputProcessor.Start();
            }
        }

        private static void HandleNoteOff(MidiEvent midiEvent)
        {
            var noteEvent = (NoteEvent)midiEvent;

            int? noteListIndex = TryGetNoteListIndex(noteEvent.NoteNumber);
            if (!noteListIndex.HasValue)
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
                        // MidiEvent itself does not give us the information needed to determine note duration.
                        double noteEnd = AudioOutputProcessor.Time;
                        double noteStart = patchCalculator.GetValue(InletTypeEnum.NoteStart, noteListIndex.Value);
                        double noteDuration = noteEnd - noteStart;
                        patchCalculator.SetValue(InletTypeEnum.NoteDuration, noteListIndex.Value, noteDuration);

                        // NoteDuration does not work properly yet, so keep the old solution for now. (Abruptly stopping the note.)
                        //double newVolume = 0.0;
                        //patchCalculator.SetValue(InletTypeEnum.Volume, noteListIndex.Value, newVolume);
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

            ResetNoteListIndex(noteEvent.NoteNumber);

            if (_noteNumber_To_NoteListIndex_Dictionary.Count == 0)
            {
                // Temporarily call another method for debugging (2016-01-09).
                //AudioOutputProcessor.Pause();
                //AudioOutputProcessor.Stop();
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

        /// <summary>
        /// Used both for getting a list index for a new note,
        /// as well as getting the list index for the already playing note.
        /// Returns null if max concurrent notes was exceeded.
        /// </summary>
        private static int? TryGetNoteListIndex(int noteNumber)
        {
            int listIndex;
            if (_noteNumber_To_NoteListIndex_Dictionary.TryGetValue(noteNumber, out listIndex))
            {
                return listIndex;
            }

            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                if (!_noteNumber_To_NoteListIndex_Dictionary.ContainsValue(i))
                {
                    _noteNumber_To_NoteListIndex_Dictionary[noteNumber] = i;
                    return i;
                }
            }
            
            return null;
        }

        private static void ResetNoteListIndex(int noteNumber)
        {
            _noteNumber_To_NoteListIndex_Dictionary.Remove(noteNumber);
        }

        private static double GetFrequencyByNoteNumber(int noteNumber)
        {
            double frequency = LOWEST_FREQUENCY * Math.Pow(2.0, noteNumber / 12.0);
            return frequency;
        }
    }
}

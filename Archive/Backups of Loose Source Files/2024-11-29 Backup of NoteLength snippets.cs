            // Evaluate duration for the time at the end of the tape,
            // to prevent premature cut-off.
            int durationCompoundCount = 10;
            duration = duration ?? GetAudioLength;
            double evaluatedDuration = duration.Value;
            for (int i = 0; i < durationCompoundCount; i++)
            {
                double evaluatedDuration2 = duration.Calculate(time: evaluatedDuration);
                if (evaluatedDuration2 <= 0) break;
                if (evaluatedDuration2 == evaluatedDuration) break;
                evaluatedDuration = duration.Calculate(time: evaluatedDuration);
            }

        public FlowNode ResolveNoteLength(SynthWishes synthWishes, FlowNode noteLength)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            noteLength = noteLength ?? _noteLength ?? _beatLength;

            if (noteLength != null)
            {
                // Tape snapshot value of noteLength,
                // for consistent volume curve lengths and buffer size cut-offs.
                double value = noteLength.Value;
                return synthWishes.Value(value);
            }
            else
            {
                double value = _section.NoteLength ?? DefaultNoteLength;
                return synthWishes.Value(value);
            }
        }

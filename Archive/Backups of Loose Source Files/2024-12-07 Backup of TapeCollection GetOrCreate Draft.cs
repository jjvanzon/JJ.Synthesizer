        public Tape GetOrCreate(FlowNode signal, [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            
            Tape tape = _tapes[signal];
            if (tape == null)
            {
                _tapes[signal] = tape = new Tape
                {
                    Signal = signal,
                    Channel = _synthWishes.GetChannel,
                    FallBackName = callerMemberName
                };
            }
            else
            {
                if (tape.Signal != signal) throw new Exception("Existing Tape found, but signal doesn't match.");
                if (tape.Channel != _synthWishes.GetChannel) throw new Exception("Existing Tape found, but Channel doesn't match.");
                if (!string.Equals(tape.FallBackName, callerMemberName, StringComparison.Ordinal)
            }
            
            return tape;
        }
        

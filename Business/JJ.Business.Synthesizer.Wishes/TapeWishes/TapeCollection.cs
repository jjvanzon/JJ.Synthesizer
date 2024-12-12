using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeCollection : IEnumerable<Tape>
    {
        private readonly SynthWishes _synthWishes;
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        public TapeCollection(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public int Count => _tapes.Count;

        public Tape GetOrCreate(
            FlowNode signal, FlowNode duration, 
            Func<Buff, Buff> callback, Func<Buff, int, Buff> channelCallback,
            string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            
            if (!_tapes.TryGetValue(signal, out Tape tape))
            {
                _tapes[signal] = tape = new Tape();
            }

            // TODO: Move parameters and all set-once properties in a constructor?
            tape.Signal = signal;
            tape.Duration = duration ?? _synthWishes.GetAudioLength;
            tape.Channel = _synthWishes.GetChannel;
            tape.FilePath = filePath;
            tape.FallBackName = callerMemberName;
            
            // Don't overwrite callback with null.
            // TODO: Employ Callbacks collection instead of a single one.
            tape.Callback = tape.Callback ?? callback; 
            tape.ChannelCallback = tape.ChannelCallback ?? channelCallback; 

            return tape;
        }
        
        public bool IsTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return _tapes.ContainsKey(outlet);
        }
        
        public Tape TryGet(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.TryGetValue(outlet, out Tape tape);
            return tape;
        }
        
        
        public void Clear() => _tapes.Clear();

        public IEnumerator<Tape> GetEnumerator()
        {
            foreach (Tape tape in _tapes.Values)
            {
                yield return tape;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
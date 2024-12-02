using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeCollection
    {
        private readonly SynthWishes _synthWishes;
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        public TapeCollection(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public int Count => _tapes.Count;
        
        internal Tape AddTape(FlowNode signal, [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            
            var tape = new Tape
            {
                Signal = signal,
                ChannelIndex = _synthWishes.GetChannelIndex,
                FallBackName = callerMemberName
            };
            
            _tapes[signal] = tape;
            
            return tape;
        }
        
        internal bool IsTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return _tapes.ContainsKey(outlet);
        }
        
        public Tape TryGetTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.TryGetValue(outlet, out Tape tape);
            return tape;
        }
        
        public Tape[] GetAllTapes() => _tapes.Values.ToArray();
        
        public void ClearTapes() => _tapes.Clear();
    }
}
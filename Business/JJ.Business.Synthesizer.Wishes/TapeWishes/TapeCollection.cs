using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeCollection
    {
        private readonly ConfigResolver _configResolver;
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        public TapeCollection(ConfigResolver configResolver)
        {
            _configResolver = configResolver ?? throw new ArgumentNullException(nameof(configResolver));
        }
        
        public int Count => _tapes.Count;
        
        internal Tape Add(FlowNode signal, [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            
            var tape = new Tape
            {
                Signal = signal,
                ChannelIndex = _configResolver.GetChannelIndex,
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
        
        public Tape TryGet(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.TryGetValue(outlet, out Tape tape);
            return tape;
        }
        
        public Tape[] GetAll() => _tapes.Values.ToArray();
        
        public void Clear() => _tapes.Clear();
    }
}
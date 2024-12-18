using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        internal void Record(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            tape.Buff = Record(tape.Signal, tape.Duration, tape.GetName);
            //MakeBuff(tape);
        }

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            FlowNode signal, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { signal }, duration,
                inMemory: true, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            IList<FlowNode> channelSignals, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration, 
                inMemory: true, default, null, name, null, callerMemberName);
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        internal void Record(Tape tape)
        {
            MakeBuff(tape);
        }

        /// <inheritdoc cref="docs._makebuff" />
        [Obsolete("")]
        public Buff RecordOld(
            FlowNode signal, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => this.MakeBuff(
                new[] { signal }, duration,
                inMemory: true, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        [Obsolete("")]
        public Buff RecordOld(
            IList<FlowNode> channelSignals, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => this.MakeBuff(
                channelSignals, duration, 
                inMemory: true, default, null, name, null, callerMemberName);
    }
}
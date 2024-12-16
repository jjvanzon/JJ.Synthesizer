using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
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
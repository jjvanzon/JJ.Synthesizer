using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.Obsolete.StreamerObsoleteMessages;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public void Record(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.IsBuff) return;
            MakeBuff(tape);
            tape.LogAction("Update");
        }
        
        /// <inheritdoc cref="_makebuff" />
        [Obsolete(ObsoleteMessage)]
        public Buff Record(
            FlowNode signal, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacy(
                signal, duration,
                inMemory: true, mustPad: default, name, null, callerMemberName);

        /// <inheritdoc cref="_makebuff" />
        [Obsolete(ObsoleteMessage)]
        public Buff Record(
            IList<FlowNode> channelSignals, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuffLegacy(
                channelSignals, duration, 
                inMemory: true, mustPad: default, name, null, callerMemberName);
    }
}
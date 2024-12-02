using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;

// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Wishes
{
    // Tape Method
    
    public partial class FlowNode
    {
        public FlowNode Tape(FlowNode duration = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Tape(this, duration, callerMemberName);
    }

    public partial class SynthWishes
    {
        public FlowNode Tape(FlowNode signal, FlowNode duration = null, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.Add(signal, callerMemberName);
            tape.Duration = duration ?? GetAudioLength;
            return signal;
        }
    }
}

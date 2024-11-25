using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Persistence;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class MySynthWishes : SynthWishes
    {
        public MySynthWishes(IContext context) 
            : base(context)
        { }
        
        public MySynthWishes(IContext context, double beat = 1, double bar = 4) 
            : base(context, beat, bar)
        { }
        
        public MySynthWishes(double beat = 1, double bar = 4)
            : base(beat, bar)
        { }
        
        public void WithShortDuration() => WithAudioLength(0.5).WithLeadingSilence(0).WithTrailingSilence(0);
        public FlowNode RecorderEnvelope     => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        public FlowNode DelayedPulseEnvelope => Curve((0, 0), (0.2, 0),  (0.3, 1),  (0.7, 1), (0.8, 0), (1.0, 0));
    }
}

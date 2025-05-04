namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class MySynthWishes : SynthWishes
    {
        public FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));

        /// <summary>
        /// A volume curve that produces a recorder flute-like sound with a built-in delay to combat audio skips during playback in Windows.
        /// This is used instead of the LeadingSilence setting, which is incompatible with mid-chain audio streaming commands.
        /// Mid-chain commands typically reuse buffers for reprocessing, where LeadingSilence and TrailingSilence are undesirable.
        /// This curve allows tests to play audio using mid-chain streaming commands without sounding too abrasive.
        /// </summary>
        public FlowNode DelayedPulseCurve => Curve((0, 0), (0.2, 0), (0.3, 1),  (0.7, 1), (0.8, 0), (1.0, 0));
    
        /// <summary>
        /// Can work alongside DelayedPulseCurve to make the tests go by a little faster.
        /// </summary>
        public void WithShortDuration() => WithAudioLength(0.5).WithLeadingSilence(0).WithTrailingSilence(0.05);
    
        public void WithDefaultDurations() => ResetAudioLength().ResetPadding();
    }
}

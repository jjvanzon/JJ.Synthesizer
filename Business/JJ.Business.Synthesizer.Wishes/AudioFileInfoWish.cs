using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._audiofileinfowish"/>
    public class AudioFileInfoWish
    {
        public int Bits { get; set; }
        public int ChannelCount { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="docs._framecount"/>
        public int FrameCount { get; set; }
    }
}

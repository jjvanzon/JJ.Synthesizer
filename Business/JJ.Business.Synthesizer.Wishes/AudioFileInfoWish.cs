namespace JJ.Business.Synthesizer.Wishes
{
    /// <summary> Replacement Wish version of AudioFileInfo with more intuitive member names. </summary>
    public class AudioFileInfoWish
    {
        public int Bits { get; set; }
        public int ChannelCount { get; set; }
        /// <summary> A.k.a. SampleCount </summary>
        public int FrameCount { get; set; }
        public int SamplingRate { get; set; }
    }
}

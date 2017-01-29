namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class SampleCalculator_Zero : ISampleCalculator
    {
        public SampleCalculator_Zero(int channelCount)
        {
            ChannelCount = channelCount;
        }

        public int ChannelCount { get; }

        public double Calculate(double time, int channelIndex)
        {
            return 0.0;
        }
    }
}

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class SampleCalculator_Zero : ISampleCalculator
    {
        private readonly int _channelCount;

        public SampleCalculator_Zero(int channelCount)
        {
            _channelCount = channelCount;
        }

        public int ChannelCount
        {
            get { return _channelCount; }
        }

        public double CalculateValue(double time, int channelIndex)
        {
            return 0.0;
        }
    }
}

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Zero_SampleCalculator : ISampleCalculator
    {
        private int _channelCount;

        public Zero_SampleCalculator(int channelCount)
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

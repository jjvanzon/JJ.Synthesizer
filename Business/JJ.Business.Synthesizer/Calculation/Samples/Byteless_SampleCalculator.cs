using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Byteless_SampleCalculator : ISampleCalculator
    {
        private int _channelCount;

        public Byteless_SampleCalculator(int channelCount)
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

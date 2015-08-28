using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Byteless_SampleCalculator : ISampleCalculator
    {
        private int _channelCount;
        private Sample _sample;

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

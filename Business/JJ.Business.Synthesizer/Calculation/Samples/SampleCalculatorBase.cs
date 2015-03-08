using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class SampleCalculatorBase : ISampleCalculator
    {
        protected Sample _sample;
        protected double _rate;
        protected double[,] _samples;

        /// <summary>
        /// For performance, so we can use this value directly. 
        /// </summary>
        public int ChannelCount { get; private set; }

        public SampleCalculatorBase(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.TimeMultiplier == 0) throw new Exception("sample.TimeMultiplier cannot be 0.");

            _sample = sample;
            _rate = _sample.SamplingRate / _sample.TimeMultiplier;

            ChannelCount = _sample.GetChannelCount();
        }

        public abstract double CalculateValue(int channelIndex, double time);
    }
}

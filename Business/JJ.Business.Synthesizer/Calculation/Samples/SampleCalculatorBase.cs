using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class SampleCalculatorBase : ISampleCalculator
    {
        protected SampleChannel _sampleChannel;
        protected Sample _sample;
        protected double _rate;
        protected double[] _samples;

        public SampleCalculatorBase(SampleChannel sampleChannel)
        {
            if (sampleChannel == null) throw new NullException(() => sampleChannel);

            _sampleChannel = sampleChannel;
            _sample = sampleChannel.Sample;
            _rate = _sample.SamplingRate / _sample.TimeMultiplier;
        }

        public abstract double CalculateValue(double time);
    }
}

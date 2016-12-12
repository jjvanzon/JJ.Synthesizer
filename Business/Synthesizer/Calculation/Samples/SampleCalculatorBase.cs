using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.IO;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class SampleCalculatorBase : ISampleCalculator
    {
        /// <summary> SamplingRate divided by TimeMultiplier </summary>
        protected readonly double _rate;

        public SampleCalculatorBase(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.TimeMultiplier == 0.0) throw new ZeroException(() => sample.TimeMultiplier);
            if (!sample.IsActive) throw new Exception("sample.IsActive cannot be false, because it needs to be handled by a Zero_SampleCalculator.");
            if (bytes == null) throw new Exception("bytes cannot be null. A null byte array can only be handled by a Zero_SampleCalculator.");

            IValidator validator = new SampleValidator(sample);
            validator.Assert();

            _rate = sample.SamplingRate / sample.TimeMultiplier;

            ChannelCount = sample.GetChannelCount();
        }

        public int ChannelCount { get; }

        public abstract double CalculateValue(double time, int channelIndex);
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation.Arrays;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Calculation.Samples
//{
//    internal class SampleCalculator_Int16_Mono_Line : ISampleCalculator
//    {
//        private readonly double _rate;
//        private ArrayCalculator_MinTimeZero_Line _arrayCalculator;

//        public SampleCalculator_Int16_Mono_Line(Sample sample, byte[] bytes)
//        {
//            if (sample == null) throw new NullException(() => sample);
//            if (sample.TimeMultiplier == 0.0) throw new ZeroException(() => sample.TimeMultiplier);
//            if (sample.GetChannelCount() != 1) throw new NotEqualException(() => sample.GetChannelCount(), 1);
//            if (!sample.IsActive) throw new Exception("sample.IsActive cannot be false, because it needs to be handled by a Zero_SampleCalculator.");
//            if (bytes == null) throw new Exception("bytes cannot be null. A null byte array can only be handled by a Zero_SampleCalculator.");

//            IValidator validator = new SampleValidator(sample);
//            validator.Assert();

//            double rate = sample.SamplingRate / sample.TimeMultiplier;

//            double[][] samples = SampleCalculatorHelper.ReadInt16Samples(sample, bytes);

//            _arrayCalculator = new ArrayCalculator_MinTimeZero_Line(samples[0], rate);
//        }

//        public double CalculateValue(double time, int channelIndex)
//        {
//            return _arrayCalculator.CalculateValue(time);
//        }
//    }
//}

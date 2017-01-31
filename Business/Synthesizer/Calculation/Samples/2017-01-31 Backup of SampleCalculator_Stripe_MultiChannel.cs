//using System.Linq;
//using JJ.Business.Synthesizer.Calculation.Arrays;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Samples
//{
//    internal class SampleCalculator_Stripe_MultiChannel : SampleCalculatorBase
//    {
//        private readonly ArrayCalculator_MinPositionZero_Stripe[] _arrayCalculators;

//        public SampleCalculator_Stripe_MultiChannel(Sample sample, byte[] bytes)
//            : base(sample, bytes)
//        {
//            if (sample.GetChannelCount() == 1) throw new EqualException(() => sample.GetChannelCount(), 1);

//            double[][] samples = SampleCalculatorHelper.ReadSamples(sample, bytes);

//            _arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Stripe(x, _rate)).ToArray();
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            return _arrayCalculators[channelIndex].Calculate(time);
//        }
//    }
//}

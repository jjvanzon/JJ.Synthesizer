using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class SumOverDimension_OperatorCalculator_Base
        : OperatorCalculatorBase_SamplerOverDimension
    {
        protected double _aggregate;

        public SumOverDimension_OperatorCalculator_Base(
            OperatorCalculatorBase collectionCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(collectionCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        { }

        /// <summary> Just returns _aggregate. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _aggregate;
        }

        protected override void ProcessFirstSample(double sample)
        {
            _aggregate = sample;
        }

        protected override void ProcessNextSample(double sample)
        {
            _aggregate += sample;
        }
    }
}
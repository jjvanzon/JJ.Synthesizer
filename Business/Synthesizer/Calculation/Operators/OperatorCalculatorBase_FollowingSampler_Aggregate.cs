using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_FollowingSampler_Aggregate : OperatorCalculatorBase_FollowingSampler_ManyPoint
    {
        private readonly OperatorCalculatorBase _sliceLengthCalculator;

        protected double _aggregate;

        public OperatorCalculatorBase_FollowingSampler_Aggregate(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionCalculator)
            : base(signalCalculator, samplingRateCalculator, positionCalculator, sliceLengthCalculator)
            => _sliceLengthCalculator = sliceLengthCalculator ?? throw new NullException(() => sliceLengthCalculator);

        protected sealed override double Calculate(double x) => _aggregate;

        protected override double GetSampleCount()
        {
            double sliceLength = _sliceLengthCalculator.Calculate();
            double samplingRate = _samplingRateCalculator.Calculate();
            double sampleCount = sliceLength * samplingRate;
            return sampleCount;
        }
    }
}
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_Aggregate_Backup : OperatorCalculatorBase_FollowingSampler_2Y
    {
		private readonly OperatorCalculatorBase _sliceLengthCalculator;

        protected double _aggregate;

		public OperatorCalculatorBase_FollowingSampler_Aggregate_Backup(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
		    : base(signalCalculator, samplingRateCalculator, positionCalculator, sliceLengthCalculator)
		    => _sliceLengthCalculator = sliceLengthCalculator ?? throw new NullException(() => sliceLengthCalculator);

        protected sealed override double Calculate(double x) => _aggregate;

        protected double GetSampleCount()
		{
			double sliceLength = _sliceLengthCalculator.Calculate();
		    double samplingRate = _samplingRateCalculator.Calculate();
			double sampleCount = sliceLength * samplingRate;
		    return sampleCount;
		}
	}
}

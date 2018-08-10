using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_2Point_LookAhead : OperatorCalculatorBase_FollowingSampler_2Point
	{
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		public OperatorCalculatorBase_FollowingSampler_2Point_LookAhead(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator)
			=> _positionOutputCalculator = positionOutputCalculator ?? throw new ArgumentNullException(nameof(positionOutputCalculator));

		protected sealed override void SetNextSample()
		{
			_x1 += GetLargeDx();

			double originalPosition = _positionOutputCalculator._value;
			_positionOutputCalculator._value = _x1;

			_y1 = _signalCalculator.Calculate();

			_positionOutputCalculator._value = originalPosition;
		}

		protected sealed override void SetPreviousSample()
		{
			_x0 -= GetLargeDx();

			double originalPosition = _positionOutputCalculator._value;
			_positionOutputCalculator._value = _x0;

			_y0 = _signalCalculator.Calculate();

			_positionOutputCalculator._value = originalPosition;
		}

		protected sealed override void ResetNonRecursive()
		{
		    base.ResetNonRecursive();

			_x0 = _positionCalculator.Calculate();
			_y0 = _signalCalculator.Calculate();

			double originalPosition = _positionOutputCalculator._value;

			_x1 = _x0 + GetLargeDx();
			_positionOutputCalculator._value = _x1;
			_y1 = _signalCalculator.Calculate();

			_positionOutputCalculator._value = originalPosition;

			Precalculate();
		}
	}
}
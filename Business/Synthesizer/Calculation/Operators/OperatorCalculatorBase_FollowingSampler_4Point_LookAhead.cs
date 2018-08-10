using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_4Point_LookAhead : OperatorCalculatorBase_FollowingSampler_4Point
	{
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		public OperatorCalculatorBase_FollowingSampler_4Point_LookAhead(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator)
			=> _positionOutputCalculator = positionOutputCalculator ?? throw new ArgumentNullException(nameof(positionOutputCalculator));

		protected sealed override void SetNextSample()
		{
			_x2 += GetLargeDx();

			double originalPosition = _positionOutputCalculator._value;
			_positionOutputCalculator._value = _x2;

			_y2 = _signalCalculator.Calculate();

			_positionOutputCalculator._value = originalPosition;
		}

		protected sealed override void SetPreviousSample()
		{
			_xMinus1 -= GetLargeDx();

			double originalPosition = _positionOutputCalculator._value;
			_positionOutputCalculator._value = _xMinus1;

			_yMinus1 = _signalCalculator.Calculate();

			_positionOutputCalculator._value = originalPosition;
		}

		protected sealed override void ResetNonRecursive()
		{
		    base.ResetNonRecursive();

		    double dx = GetLargeDx();

			_x0 = _positionCalculator.Calculate();
			_y0 = _signalCalculator.Calculate();

			double originalPosition = _positionOutputCalculator._value;

			_xMinus1 = _x0 - dx;
			_positionOutputCalculator._value = _xMinus1;
			_yMinus1 = _signalCalculator.Calculate();

			_x1 = _x0 + dx;
			_positionOutputCalculator._value = _x1;
			_y1 = _signalCalculator.Calculate();

			_x2 = _x1 + dx;
			_positionOutputCalculator._value = _x2;
			_y2 = _signalCalculator.Calculate();

			_positionOutputCalculator._value = originalPosition;

			Precalculate();
		}
	}
}
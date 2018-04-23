using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Interpolate_OperatorCalculator_Cubic_LagBehind : Interpolate_OperatorCalculator_Base
	{
		private double _xMinus1;
		private double _x0;
		private double _x1;
		private double _x2;
		private double _yMinus1;
		private double _y0;
		private double _y1;
		private double _y2;
		private double _a;
		private double _b;
		private double _c;

		public Interpolate_OperatorCalculator_Cubic_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();
	 
			// TODO: What if position goes in reverse?
			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
			// When x goes past _x1 you must shift things.
			if (x > _x1)
			{
				// Shift the samples to the left.
				_xMinus1 = _x0;
				_x0 = _x1;
				_x1 = _x2;
				_yMinus1 = _y0;
				_y0 = _y1;
				_y1 = _y2;

				// Determine next sample
				_positionOutputCalculator._value = _x1;

				double samplingRate = GetSamplingRate();
				double dx1 = 1.0 / samplingRate;
				_x2 = _x1 + dx1;

				_positionOutputCalculator._value = _x2;

				_y2 = _signalCalculator.Calculate();

				// Precalculate
				(_a, _b, _c) = Interpolator.Cubic_SmoothSlope_PrecalculateVariables(
					_xMinus1, _x0, _x1, _x2,
					_yMinus1, _y0, _y1, _y2);
			}

			// Calculate
			double y = Interpolator.Cubic_SmoothSlope_FromPrecalculatedVariables(_x0, _y0, _a, _b, _c, x);

			return y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();
			double samplingRate = GetSamplingRate();

			double dx = 1.0 / samplingRate;

			_xMinus1 = x - dx - dx;
			_x0 = x - dx;
			_x1 = x;
			_x2 = x + dx;

			_yMinus1 = y;
			_y0 = y;
			_y1 = y;
			_y2 = y;

			(_a, _b, _c) = Interpolator.Cubic_SmoothSlope_PrecalculateVariables(
				_xMinus1, _x0, _x1, _x2,
				_yMinus1, _y0, _y1, _y2);
		}
	}
}

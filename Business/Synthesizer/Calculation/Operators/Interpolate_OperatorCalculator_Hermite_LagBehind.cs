using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Interpolate_OperatorCalculator_Hermite_LagBehind : Interpolate_OperatorCalculator_Base
	{
		private double _xMinus1;
		private double _x0;
		private double _x1;
		private double _x2;
		private double _dx;
		private double _yMinus1;
		private double _y0;
		private double _y1;
		private double _y2;
		private double _c0;
		private double _c1;
		private double _c2;
		private double _c3;

		public Interpolate_OperatorCalculator_Hermite_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();
	 
			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
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
				_dx = Dx();

				_x2 += _dx;
				_y2 = _signalCalculator.Calculate();

				// Precalculate
				(_c0, _c1, _c2, _c3) = Interpolator.Hermite_4pt3oX_PrecalculateVariables(_yMinus1, _y0, _y1, _y2);
			}
			else if (x < _x0)
			{
				// Shift the samples to the right.
				_x0 = _xMinus1;
				_x1 = _x0;
				_x2 = _x1;
				_y0 = _yMinus1;
				_y1 = _y0;
				_y2 = _y1;

				// Determine next sample
				_dx = Dx();

				_xMinus1 -= _dx;
				_yMinus1 = _signalCalculator.Calculate();

				// Precalculate
				(_c0, _c1, _c2, _c3) = Interpolator.Hermite_4pt3oX_PrecalculateVariables(_yMinus1, _y0, _y1, _y2);
			}

			// Calculate
			double t = (x - _x0) / _dx;
			double y = Interpolator.Hermite_4pt4oX_FromPrecalculatedVariables(_c0, _c1, _c2, _c3, t);
			return y;
		}

		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();
			double dx = Dx();

			_xMinus1 = x - dx - dx;
			_x0 = x - dx;
			_x1 = x;
			_x2 = x + dx;
			_dx = dx;

			_yMinus1 = y;
			_y0 = y;
			_y1 = y;
			_y2 = y;

			(_c0, _c1, _c2, _c3) = Interpolator.Hermite_4pt3oX_PrecalculateVariables(_yMinus1, _y0, _y1, _y2);
		}
	}
}

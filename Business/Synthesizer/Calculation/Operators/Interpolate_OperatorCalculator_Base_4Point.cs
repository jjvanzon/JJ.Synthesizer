using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base_4Point 
		: Interpolate_OperatorCalculator_Base
	{
		protected Interpolate_OperatorCalculator_Base_4Point(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator) { }

		protected double _xMinus1;
		protected double _x0;
		protected double _x1;
		protected double _x2;
		protected double _yMinus1;
		protected double _y0;
		protected double _y1;
		protected double _y2;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();

			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
			if (x > _x1)
			{
				ShiftLeft();
				SetNextSample();
				Precalculate();
			}
			else if (x < _x0)
			{
				ShiftRight();
				SetPreviousSample();
				Precalculate();
			}

			return Calculate(x);
		}

		private void ShiftLeft()
		{
			_xMinus1 = _x0;
			_x0 = _x1;
			_x1 = _x2;
			_yMinus1 = _y0;
			_y0 = _y1;
			_y1 = _y2;
		}

		protected abstract void SetNextSample();

		private void ShiftRight()
		{
			_x0 = _xMinus1;
			_x1 = _x0;
			_x2 = _x1;
			_y0 = _yMinus1;
			_y1 = _y0;
			_y2 = _y1;
		}

		protected abstract void SetPreviousSample();

		protected abstract void Precalculate();

		protected abstract double Calculate(double x);
	}
}
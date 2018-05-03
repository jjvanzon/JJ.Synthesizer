using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Block : Interpolate_OperatorCalculator_Base
	{
		private double _x0;
		private double _x1;
		private double _y0;

		public Interpolate_OperatorCalculator_Block(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		{
			ResetNonRecursive();
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();
	 
			// TODO: What if _x1 is way off? How will it correct itself?
			if (x > _x1)
			{
				// Shift samples.
				_x0 = _x1;

				// Determine next sample
				_x1 += Dx();
				_y0 = _signalCalculator.Calculate();
			}
			else if (x < _x0)
			{
				// Going in reverse.

				// Shift samples.
				_x1 = _x0;

				// Determine previous sample
				_x0 -= Dx();
				_y0 = _signalCalculator.Calculate();
			}

			return _y0;
		}

		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();

			_x0 = x;
			_x1 = x + Dx();

			_y0 = y;
		}
	}
}
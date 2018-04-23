using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Interpolate_OperatorCalculator_Block : Interpolate_OperatorCalculator_Base
	{
		private double _x0;
		private double _x1;
		private double _y0;

		public Interpolate_OperatorCalculator_Block(
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
	 
			// TODO: What if _x1 is way off? How will it correct itself?
			if (x > _x1)
			{
				// Shift samples to the left
				_x0 = _x1;

				// Determine next sample
				_positionOutputCalculator._value = _x1;

				double samplingRate1 = GetSamplingRate();
				double dx1 = 1.0 / samplingRate1;
				_x1 += dx1;

				_y0 = _signalCalculator.Calculate();
			}
			else if (x < _x0)
			{
				// Shift samples to the right.
				_x1 = _x0;

				// Determine previous sample
				_positionOutputCalculator._value = _x0;

				double samplingRate0 = GetSamplingRate();
				double dx0 = 1.0 / samplingRate0;
				_x0 -= dx0;

				_y0 = _signalCalculator.Calculate();
			}

			return _y0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();
			double samplingRate = GetSamplingRate();

			double dx = 1.0 / samplingRate;

			_x0 = x;
			_x1 = x + dx;

			_y0 = y;
		}
	}
}
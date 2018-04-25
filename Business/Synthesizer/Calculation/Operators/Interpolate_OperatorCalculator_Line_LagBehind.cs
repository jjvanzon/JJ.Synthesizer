using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// It seems to work, except for the artifacts that linear interpolation gives us.
	/// A weakness though is, that the sampling rate is remembered until the next sample,
	/// which may work poorly when a very low sampling rate is provided.
	/// </summary>
	internal class Interpolate_OperatorCalculator_Line_LagBehind : Interpolate_OperatorCalculator_Base
	{
		private double _x0;
		private double _x1;
		private double _y0;
		private double _y1;
		private double _a;

		public Interpolate_OperatorCalculator_Line_LagBehind(
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

			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
			if (x > _x1)
			{
				// Shift samples to the left
				_x0 = _x1;
				_y0 = _y1;

				// Determine next sample
				double originalValue = _positionOutputCalculator._value;
				_positionOutputCalculator._value = _x1;
				double samplingRate1 = GetSamplingRate();
				_positionOutputCalculator._value = originalValue;

				double dx1 = 1.0 / samplingRate1;
				_x1 += dx1;

				_y1 = _signalCalculator.Calculate();

				// Precalculate
				double dy = _y1 - _y0;
				_a = dy / dx1;
			}
			else if (x < _x0)
			{
				// Going in reverse.

				// Shift samples to the right
				_x1 = _x0;
				_y1 = _y0;

				// Determine previous sample
				double originalValue = _positionOutputCalculator._value;
				_positionOutputCalculator._value = _x0;
				double samplingRate0 = GetSamplingRate();
				_positionOutputCalculator._value = originalValue;

				double dx0 = 1.0 / samplingRate0;
				_x0 -= dx0;

				_y0 = _signalCalculator.Calculate();

				// Precalculate
				double dy = _y1 - _y0;
				_a = dy / dx0;
			}

			// Calculate
			double y = _y0 + _a * (x - _x0);
			return y;
		}

		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();
			double samplingRate = GetSamplingRate();

			double dx = 1.0 / samplingRate;

			_x0 = x - dx;
			_x1 = x;

			_y0 = y;
			_y1 = y;

			_a = 0.0;
		}
	}
}

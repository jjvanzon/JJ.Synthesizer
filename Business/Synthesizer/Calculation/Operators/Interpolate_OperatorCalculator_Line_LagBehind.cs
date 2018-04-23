using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// It seems to work, except for the artifacts that linear interpolation gives us.
	/// A weakness though is, that the sampling rate is remembered until the next sample,
	/// which may work poorly when a very low sampling rate is provided.
	/// </summary>
	internal class Interpolate_OperatorCalculator_Line_LagBehind : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		private readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _samplingRateCalculator;
		private readonly OperatorCalculatorBase _positionInputCalculator;
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

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
			: base(new[] { signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator })
		{
			_signalCalculator = signalCalculator;
			_samplingRateCalculator = samplingRateCalculator;
			_positionInputCalculator = positionInputCalculator;
			_positionOutputCalculator = positionOutputCalculator;

			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();

			if (x > _x1)
			{
				_x0 = _x1;
				_y0 = _y1;

				_positionOutputCalculator._value = _x1;

				double samplingRate = GetSamplingRate();
				double dx = 1.0 / samplingRate;

				_x1 += dx;

				_positionOutputCalculator._value = _x1;

				_y1 = _signalCalculator.Calculate();

				double dy = _y1 - _y0;
				_a = dy / dx;
			}
			else if (x < _x0)
			{
				// Going in reverse, take sample in reverse position.
				_x1 = _x0;
				_y1 = _y0;

				_positionOutputCalculator._value = _x0;

				double samplingRate0 = GetSamplingRate();
				double dx0 = 1.0 / samplingRate0;
				_x0 -= dx0;

				_positionOutputCalculator._value = _x0;

				_y0 = _signalCalculator.Calculate();

				double dy = _y1 - _y0;
				_a = dy / dx0;
			}

			double y = _y0 + _a * (x - _x0);
			return y;
		}

		/// <summary>
		/// Gets the sampling rate, converts it to an absolute number
		/// and ensures a minimum value.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double GetSamplingRate()
		{
			// _x1 was recently (2015-08-22) corrected to x which might make going in reverse work better.
			double samplingRate = _samplingRateCalculator.Calculate();

			samplingRate = Math.Abs(samplingRate);

			if (samplingRate < MINIMUM_SAMPLING_RATE)
			{
				samplingRate = MINIMUM_SAMPLING_RATE;
			}

			return samplingRate;
		}

		public override void Reset()
		{
			base.Reset();

			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();

			double y = _signalCalculator.Calculate();
			double samplingRate = GetSamplingRate();

			double dx = 1.0 / samplingRate;

			_x0 = x - dx;
			_x1 = x;

			// Y's are just set at a slightly more practical default than 0.
			_y0 = y;
			_y1 = y;

			_a = 0.0;
		}
	}
}

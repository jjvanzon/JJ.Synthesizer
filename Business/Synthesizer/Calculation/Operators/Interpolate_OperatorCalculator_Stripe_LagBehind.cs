using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Interpolate_OperatorCalculator_Stripe_LagBehind : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		private readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _samplingRateCalculator;
		private readonly OperatorCalculatorBase _positionInputCalculator;
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		private double _x0;
		private double _xAtHalf;
		private double _y0;

		public Interpolate_OperatorCalculator_Stripe_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(new[] { signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator })
		{
			_signalCalculator = signalCalculator;
			_positionInputCalculator = positionInputCalculator;
			_positionOutputCalculator = positionOutputCalculator;
			_samplingRateCalculator = samplingRateCalculator;

			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();
	 
			// TODO: What if position goes in reverse?
			// TODO: What if _x1 is way off? How will it correct itself?
			// When x goes past _x1 you must shift things.
			if (x > _xAtHalf)
			{
				// Determine next sample
				// Note that you would like to look into the future, at x1,
				// but you cannot do that real-time, in a lag-behind situation.
				// What matters is that you lag behind no more than necessary,
				// and that you get the proper alignment that comes with striped
				// interpolation.
				_positionOutputCalculator._value = _x0;

				double samplingRate0 = GetSamplingRate();
				double dx0 = 1.0 / samplingRate0;
				_x0 += dx0;
				_xAtHalf += dx0;

				// It seems you should set x on the dimension stack
				// to _xAtMinusHalf here, but x on the dimension stack is the 'old' _xAtHalf, 
				// which is the new _xAtMinusHalf. So x on the dimension stack is already _xAtMinusHalf.
				_y0 = _signalCalculator.Calculate();
			}

			return _y0;
		}

		/// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double GetSamplingRate()
		{
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

			_x0 = x;
			_xAtHalf = x + dx / 2.0;

			// Y's are just set at a more practical default than 0.
			_y0 = y;
		}
	}
}
using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// A weakness though is, that the sampling rate is remembered until the next sample,
	/// which may work poorly when a very low sampling rate is provided.
	/// </summary>
	[Obsolete("Will be refactored away at some point.")]
	internal class Interpolate_OperatorCalculator_CubicAbruptSlope : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		private readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _samplingRateCalculator;
		private readonly OperatorCalculatorBase _positionInputCalculator;
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		// HACK: These defaults are hacks that are meaningless in practice.
		private double _x0;
		private double _x1 = 0.2;
		private double _x2 = 0.4;

		private double _y0;
		private double _y1 = 12000.0 / short.MaxValue;
		private double _y2 = -24000.0 / short.MaxValue;

		private double _dx0 = 0.2;
		private double _dx1 = 0.2;
		private double _a0;
		private double _a1;

		public Interpolate_OperatorCalculator_CubicAbruptSlope(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(new[]
			{
				signalCalculator,
				samplingRateCalculator,
				positionInputCalculator,
				positionOutputCalculator
			})
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
	 
			// TODO: What if position goes in reverse?
			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
			// When x goes past _x1 you must shift things.
			if (x > _x1)
			{
				_x0 = _x1;
				_x1 = _x2;

				_y0 = _y1;
				_y1 = _y2;

				_dx0 = _dx1;
				_a0 = _a1;

				_positionOutputCalculator._value = _x1;

				double samplingRate1 = GetSamplingRate();

				_dx1 = 1.0 / samplingRate1;
				_x2 += _dx1;

				_positionOutputCalculator._value = _x2;

				_y2 = _signalCalculator.Calculate();

				_a1 = (_y2 - _y0) / (_x2 - _x0);
			}

			// TODO: What if _x1 exceeds _x2 already? What happens then?
			double dx = x - _x0;
			double t;
			t = dx / _dx0;

			// TODO: Figure out how to prevent t from becoming out of range.
			if (t > 1.0)
			{
				return 0;
			}
			else if (t < 0.0)
			{
				return 0;
			}

			double y = (1.0 - t) * (_y0 + _a0 * (x - _x0)) + t * (_y1 + _a1 * (x - _x1));
			return y;
		}

		/// <summary>
		/// Gets the sampling rate, converts it to an absolute number
		/// and ensures a minimum value.
		/// </summary>
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

			_x0 = x - CalculationHelper.VERY_SMALL_POSITIVE_VALUE;
			_x1 = x;
			_x2 = x + CalculationHelper.VERY_SMALL_POSITIVE_VALUE;

			_y0 = y;
			_y1 = y;
			_y2 = y;

			_dx0 = CalculationHelper.VERY_SMALL_POSITIVE_VALUE;
			_dx1 = CalculationHelper.VERY_SMALL_POSITIVE_VALUE;

			_a0 = 0.0;
			_a1 = 0.0;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// A weakness though is, that the sampling rate is remembered until the next sample,
	/// which may work poorly when a very low sampling rate is provided.
	/// </summary>
	[Obsolete("Will be refactored away at some point.")]
	internal class Interpolate_OperatorCalculator_CubicEquidistant : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		private readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _samplingRateCalculator;
		private readonly OperatorCalculatorBase _positionInputCalculator;
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		private double _x0;
		private double _x1;
		private double _x2;
		private double _dx1;
		private double _yMinus1;
		private double _y0;
		private double _y1;
		private double _y2;

		public Interpolate_OperatorCalculator_CubicEquidistant(
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
	 
			// TODO: What if position goes in reverse?
			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
			// When x goes past _x1 you must shift things.
			if (x > _x1)
			{
				// Shift the samples to the left.
				_x0 = _x1;
				_x1 = _x2;
				_yMinus1 = _y0;
				_y0 = _y1;
				_y1 = _y2;

				_positionOutputCalculator._value = _x1;

				double samplingRate1 = GetSamplingRate();

				_dx1 = 1.0 / samplingRate1;
				_x2 = _x1 + _dx1;

				_positionOutputCalculator._value = _x2;

				_y2 = _signalCalculator.Calculate();
			}

			double t = (x - _x0) / _dx1;

			double y = Interpolator.Interpolate_Cubic_Equidistant(_yMinus1, _y0, _y1, _y2, t);
			return y;
		}

		/// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
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
			_dx1 = CalculationHelper.VERY_SMALL_POSITIVE_VALUE;

			_yMinus1 = y;
			_y0 = y;
			_y1 = y;
			_y2 = y;
		}
	}
}

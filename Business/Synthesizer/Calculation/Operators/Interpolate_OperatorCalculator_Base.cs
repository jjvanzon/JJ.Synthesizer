using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		protected internal readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _samplingRateCalculator;
		protected internal readonly OperatorCalculatorBase _positionInputCalculator;

		public Interpolate_OperatorCalculator_Base(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(new[] { signalCalculator, samplingRateCalculator, positionInputCalculator })
		{
			_signalCalculator = signalCalculator;
			_samplingRateCalculator = samplingRateCalculator;
			_positionInputCalculator = positionInputCalculator;
		}

		public sealed override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();

			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
			if (MustShiftForward(x))
			{
				ShiftForward();
				SetNextSample();
				Precalculate();
			}
			else if (MustShiftBackward(x))
			{
				ShiftBackward();
				SetPreviousSample();
				Precalculate();
			}

			return Calculate(x);
		}

		protected abstract bool MustShiftForward(double x);
		protected abstract void ShiftForward();
		protected abstract void SetNextSample();
		protected abstract bool MustShiftBackward(double x);
		protected abstract void ShiftBackward();
		protected abstract void SetPreviousSample();

		protected abstract void Precalculate();
		protected abstract double Calculate(double x);

		public override void Reset()
		{
			base.Reset();
			ResetNonRecursive();
		}

		protected abstract void ResetNonRecursive();

		/// <summary> Gets the sampling rate, converts it to an absolute number, ensures a minimum value and returns dx. </summary>
		protected internal double Dx()
		{
			double samplingRate = _samplingRateCalculator.Calculate();

			samplingRate = Math.Abs(samplingRate);

			if (samplingRate < MINIMUM_SAMPLING_RATE)
			{
				samplingRate = MINIMUM_SAMPLING_RATE;
			}

			return 1.0 / samplingRate;
		}
	}
}

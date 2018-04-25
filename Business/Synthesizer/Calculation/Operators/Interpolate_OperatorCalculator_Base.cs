using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		protected readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _samplingRateCalculator;
		protected readonly OperatorCalculatorBase _positionInputCalculator;

		public Interpolate_OperatorCalculator_Base(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(new[] { signalCalculator, samplingRateCalculator, positionInputCalculator })
		{
			_signalCalculator = signalCalculator;
			_samplingRateCalculator = samplingRateCalculator;
			_positionInputCalculator = positionInputCalculator;

			// ReSharper disable once VirtualMemberCallInConstructor
			ResetNonRecursive();
		}

		/// <summary> Gets the sampling rate, converts it to an absolute number, ensures a minimum value and returns dx. </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected double Dx()
		{
			double samplingRate = _samplingRateCalculator.Calculate();

			samplingRate = Math.Abs(samplingRate);

			if (samplingRate < MINIMUM_SAMPLING_RATE)
			{
				samplingRate = MINIMUM_SAMPLING_RATE;
			}

			return 1.0 / samplingRate;
		}

		public override void Reset()
		{
			base.Reset();
			ResetNonRecursive();
		}

		protected abstract void ResetNonRecursive();
	}
}

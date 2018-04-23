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
		protected readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		public Interpolate_OperatorCalculator_Base(
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

			// ReSharper disable once VirtualMemberCallInConstructor
			ResetNonRecursive();
		}

		/// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected double GetSamplingRate()
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

		protected abstract void ResetNonRecursive();
	}
}

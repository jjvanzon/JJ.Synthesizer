using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class LowPassFilter_OperatorCalculator
		: OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _soundCalculator;
		private readonly OperatorCalculatorBase _maxFrequencyCalculator;
		private readonly OperatorCalculatorBase _blobVolumeCalculator;
		private readonly double _targetSamplingRate;
		private readonly double _nyquistFrequency;
		private readonly BiQuadFilter _biQuadFilter;

		public LowPassFilter_OperatorCalculator(
			OperatorCalculatorBase soundCalculator,
			OperatorCalculatorBase maxFrequencyCalculator,
			OperatorCalculatorBase blobVolumeCalculator,
			double targetSamplingRate)
				: base(new []
				{
					soundCalculator,
					maxFrequencyCalculator,
					blobVolumeCalculator
				})
		{
			_soundCalculator = soundCalculator ?? throw new NullException(() => soundCalculator);
			_maxFrequencyCalculator = maxFrequencyCalculator ?? throw new NullException(() => maxFrequencyCalculator);
			_blobVolumeCalculator = blobVolumeCalculator ?? throw new NullException(() => blobVolumeCalculator);
			_targetSamplingRate = targetSamplingRate;
			_biQuadFilter = new BiQuadFilter();

			_nyquistFrequency = _targetSamplingRate / 2.0;

			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			SetFilterVariables();

			double sound = _soundCalculator.Calculate();
			double result = _biQuadFilter.Transform(sound);

			return result;
		}

		public override void Reset()
		{
			base.Reset();

			ResetNonRecursive();
		}

		private void ResetNonRecursive()
		{
			SetFilterVariables();
			_biQuadFilter.ResetSamples();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetFilterVariables()
		{
			double maxFrequency = _maxFrequencyCalculator.Calculate();
			double blobVolume = _blobVolumeCalculator.Calculate();

			if (maxFrequency > _nyquistFrequency) maxFrequency = _nyquistFrequency;

			_biQuadFilter.SetLowPassFilterVariables(_targetSamplingRate, maxFrequency, blobVolume);
		}
	}
}

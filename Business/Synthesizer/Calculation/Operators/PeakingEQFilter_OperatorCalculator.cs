using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class PeakingEQFilter_OperatorCalculator
		: OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _soundCalculator;
		private readonly OperatorCalculatorBase _centerFrequencyCalculator;
		private readonly OperatorCalculatorBase _widthCalculator;
		private readonly OperatorCalculatorBase _dbGainCalculator;
		private readonly double _samplingRate;
		private readonly double _nyquistFrequency;
		private readonly BiQuadFilter _biQuadFilter;

		public PeakingEQFilter_OperatorCalculator(
			OperatorCalculatorBase soundCalculator,
			OperatorCalculatorBase centerFrequencyCalculator,
			OperatorCalculatorBase widthCalculator,
			OperatorCalculatorBase dbGainCalculator,
			double samplingRate)
			: base(new[]
			{
				soundCalculator,
				centerFrequencyCalculator,
				widthCalculator,
				dbGainCalculator
			})
		{
			_soundCalculator = soundCalculator ?? throw new NullException(() => soundCalculator);
			_centerFrequencyCalculator = centerFrequencyCalculator ?? throw new NullException(() => centerFrequencyCalculator);
			_widthCalculator = widthCalculator ?? throw new NullException(() => widthCalculator);
			_dbGainCalculator = dbGainCalculator ?? throw new NullException(() => dbGainCalculator);
			_samplingRate = samplingRate;
			_biQuadFilter = new BiQuadFilter();

			_nyquistFrequency = _samplingRate / 2.0;

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
			double centerFrequency = _centerFrequencyCalculator.Calculate();
			double width = _widthCalculator.Calculate();
			double dbGain = _dbGainCalculator.Calculate();

			if (centerFrequency > _nyquistFrequency) centerFrequency = _nyquistFrequency;

			_biQuadFilter.SetPeakingEQVariables(_samplingRate, centerFrequency, width, dbGain);
		}
	}
}

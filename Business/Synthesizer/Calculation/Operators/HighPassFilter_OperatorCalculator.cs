using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighPassFilter_OperatorCalculator
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _minFrequencyCalculator;
        private readonly OperatorCalculatorBase _blobVolumeCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly BiQuadFilter _biQuadFilter;

        public HighPassFilter_OperatorCalculator(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase minFrequencyCalculator,
            OperatorCalculatorBase blobVolumeCalculator,
            double targetSamplingRate)
            : base(new[] 
            {
                soundCalculator,
                minFrequencyCalculator,
                blobVolumeCalculator })
        {
            _soundCalculator = soundCalculator ?? throw new NullException(() => soundCalculator);
            _minFrequencyCalculator = minFrequencyCalculator ?? throw new NullException(() => minFrequencyCalculator);
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

            double signal = _soundCalculator.Calculate();
            double result = _biQuadFilter.Transform(signal);

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
            double minFrequency = _minFrequencyCalculator.Calculate();
            double blobVolume = _blobVolumeCalculator.Calculate();

            if (minFrequency > _nyquistFrequency) minFrequency = _nyquistFrequency;

            _biQuadFilter.SetHighPassFilterVariables(_targetSamplingRate, minFrequency, blobVolume);
        }
    }
}

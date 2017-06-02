using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighPassFilter_OperatorCalculator_AllVars
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _minFrequencyCalculator;
        private readonly OperatorCalculatorBase _blobVolumeCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public HighPassFilter_OperatorCalculator_AllVars(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase minFrequencyCalculator,
            OperatorCalculatorBase blobVolumeCalculator,
            double targetSamplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(new[] 
            {
                soundCalculator,
                minFrequencyCalculator,
                blobVolumeCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(soundCalculator, () => soundCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _soundCalculator = soundCalculator;
            _minFrequencyCalculator = minFrequencyCalculator ?? throw new NullException(() => minFrequencyCalculator);
            _blobVolumeCalculator = blobVolumeCalculator ?? throw new NullException(() => blobVolumeCalculator);
            _targetSamplingRate = targetSamplingRate;
            _samplesBetweenApplyFilterVariables = samplesBetweenApplyFilterVariables;
            _biQuadFilter = new BiQuadFilter();

            _nyquistFrequency = _targetSamplingRate / 2.0;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > _samplesBetweenApplyFilterVariables)
            {
                SetFilterVariables();
                _counter = 0;
            }

            double signal = _soundCalculator.Calculate();
            double result = _biQuadFilter.Transform(signal);

            _counter++;

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
            _counter = 0;
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

    internal class HighPassFilter_OperatorCalculator_ManyConsts
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly double _minFrequency;
        private readonly double _blobVolume;
        private readonly double _targetSamplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public HighPassFilter_OperatorCalculator_ManyConsts(
            OperatorCalculatorBase soundCalculator,
            double minFrequency,
            double blobVolume,
            double targetSamplingRate)
                : base(new[] { soundCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(soundCalculator, () => soundCalculator);
            OperatorCalculatorHelper.AssertFilterFrequency(minFrequency, targetSamplingRate);

            _soundCalculator = soundCalculator;
            _minFrequency = minFrequency;
            _blobVolume = blobVolume;
            _targetSamplingRate = targetSamplingRate;
            _biQuadFilter = new BiQuadFilter();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
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
            _biQuadFilter.SetHighPassFilterVariables(_targetSamplingRate, _minFrequency, _blobVolume);
            _biQuadFilter.ResetSamples();
        }
    }
}

using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LowPassFilter_OperatorCalculator_SoundVarOrConst_OtherInputsVar
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _maxFrequencyCalculator;
        private readonly OperatorCalculatorBase _blobVolumeCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public LowPassFilter_OperatorCalculator_SoundVarOrConst_OtherInputsVar(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase maxFrequencyCalculator,
            OperatorCalculatorBase blobVolumeCalculator,
            double targetSamplingRate,
            int samplesBetweenApplyFilterVariables)
                : base(new []
                {
                    soundCalculator,
                    maxFrequencyCalculator,
                    blobVolumeCalculator
                })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(soundCalculator, () => soundCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _soundCalculator = soundCalculator;
            _maxFrequencyCalculator = maxFrequencyCalculator ?? throw new NullException(() => maxFrequencyCalculator);
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

            double sound = _soundCalculator.Calculate();
            double result = _biQuadFilter.Transform(sound);

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
            double maxFrequency = _maxFrequencyCalculator.Calculate();
            double blobVolume = _blobVolumeCalculator.Calculate();

            if (maxFrequency > _nyquistFrequency) maxFrequency = _nyquistFrequency;

            _biQuadFilter.SetLowPassFilterVariables(_targetSamplingRate, maxFrequency, blobVolume);
        }
    }

    internal class LowPassFilter_OperatorCalculator_SoundVarOrConst_OtherInputsConst
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase soundCalculator;
        private readonly double _maxFrequency;
        private readonly double _width;
        private readonly double _samplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public LowPassFilter_OperatorCalculator_SoundVarOrConst_OtherInputsConst(
            OperatorCalculatorBase soundCalculator,
            double maxFrequency,
            double width,
            double samplingRate)
                : base(new[] { soundCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(soundCalculator, () => soundCalculator);
            OperatorCalculatorHelper.AssertFilterFrequency(maxFrequency, samplingRate);

            this.soundCalculator = soundCalculator;
            _maxFrequency = maxFrequency;
            _width = width;
            _samplingRate = samplingRate;
            _biQuadFilter = new BiQuadFilter();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double sound = soundCalculator.Calculate();
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
            _biQuadFilter.SetLowPassFilterVariables(_samplingRate, _maxFrequency, _width);
            _biQuadFilter.ResetSamples();
        }
    }
}

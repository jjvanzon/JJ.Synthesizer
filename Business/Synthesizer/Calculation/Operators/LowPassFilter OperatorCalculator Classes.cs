using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LowPassFilter_OperatorCalculator_AllVars
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _maxFrequencyCalculator;
        private readonly OperatorCalculatorBase _bandWidthCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public LowPassFilter_OperatorCalculator_AllVars(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase maxFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            double targetSamplingRate,
            int samplesBetweenApplyFilterVariables)
                : base(new []
                {
                    signalCalculator,
                    maxFrequencyCalculator,
                    bandWidthCalculator
                })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _signalCalculator = signalCalculator;
            _maxFrequencyCalculator = maxFrequencyCalculator ?? throw new NullException(() => maxFrequencyCalculator);
            _bandWidthCalculator = bandWidthCalculator ?? throw new NullException(() => bandWidthCalculator);
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

            double signal = _signalCalculator.Calculate();
            double value = _biQuadFilter.Transform(signal);

            _counter++;

            return value;
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
            double bandWidth = _bandWidthCalculator.Calculate();

            if (maxFrequency > _nyquistFrequency) maxFrequency = _nyquistFrequency;

            _biQuadFilter.SetLowPassFilterVariables(_targetSamplingRate, maxFrequency, bandWidth);
        }
    }

    internal class LowPassFilter_OperatorCalculator_ManyConsts
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _maxFrequency;
        private readonly double _bandWidth;
        private readonly double _samplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public LowPassFilter_OperatorCalculator_ManyConsts(
            OperatorCalculatorBase signalCalculator,
            double maxFrequency,
            double bandWidth,
            double samplingRate)
                : base(new[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertFilterFrequency(maxFrequency, samplingRate);

            _signalCalculator = signalCalculator;
            _maxFrequency = maxFrequency;
            _bandWidth = bandWidth;
            _samplingRate = samplingRate;
            _biQuadFilter = new BiQuadFilter();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double value = _biQuadFilter.Transform(signal);
            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
            _biQuadFilter.SetLowPassFilterVariables(_samplingRate, _maxFrequency, _bandWidth);
            _biQuadFilter.ResetSamples();
        }
    }
}

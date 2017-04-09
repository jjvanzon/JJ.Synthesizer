using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _centerFrequencyCalculator;
        private readonly OperatorCalculatorBase _bandWidthCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase centerFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            double targetSamplingRate,
            int samplesBetweenApplyFilterVariables)
                : base(new[]
                {
                    signalCalculator,
                    centerFrequencyCalculator,
                    bandWidthCalculator
                })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _signalCalculator = signalCalculator;
            _centerFrequencyCalculator = centerFrequencyCalculator ?? throw new NullException(() => centerFrequencyCalculator);
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
            double centerFrequency = _centerFrequencyCalculator.Calculate();
            double bandWidth = _bandWidthCalculator.Calculate();

            if (centerFrequency > _nyquistFrequency) centerFrequency = _nyquistFrequency;

            _biQuadFilter.SetBandPassFilterConstantSkirtGainVariables(_targetSamplingRate, centerFrequency, bandWidth);
        }
    }

    internal class BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _centerFrequency;
        private readonly double _bandWidth;
        private readonly double _targetSamplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double centerFrequency,
            double bandWidth,
            double targetSamplingRate)
            : base(new[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertFilterFrequency(centerFrequency, targetSamplingRate);

            _signalCalculator = signalCalculator;
            _centerFrequency = centerFrequency;
            _bandWidth = bandWidth;
            _targetSamplingRate = targetSamplingRate;
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
            _biQuadFilter.SetBandPassFilterConstantSkirtGainVariables(_targetSamplingRate, _centerFrequency, _bandWidth);
            _biQuadFilter.ResetSamples();
        }
    }
}
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighShelfFilter_OperatorCalculator_AllVars
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _transitionFrequencyCalculator;
        private readonly OperatorCalculatorBase _transitionSlopeCalculator;
        private readonly OperatorCalculatorBase _dbGainCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public HighShelfFilter_OperatorCalculator_AllVars(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase transitionFrequencyCalculator,
            OperatorCalculatorBase transitionSlopeCalculator,
            OperatorCalculatorBase dbGainCalculator,
            double targetSamplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(new[]
            {
                signalCalculator,
                transitionFrequencyCalculator,
                dbGainCalculator,
                transitionSlopeCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (transitionFrequencyCalculator == null) throw new NullException(() => transitionFrequencyCalculator);
            if (transitionSlopeCalculator == null) throw new NullException(() => transitionSlopeCalculator);
            if (dbGainCalculator == null) throw new NullException(() => dbGainCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _signalCalculator = signalCalculator;
            _transitionFrequencyCalculator = transitionFrequencyCalculator;
            _transitionSlopeCalculator = transitionSlopeCalculator;
            _dbGainCalculator = dbGainCalculator;
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
            double transitionFrequency = _transitionFrequencyCalculator.Calculate();
            double transitionSlope = _transitionSlopeCalculator.Calculate();
            double dbGain = _dbGainCalculator.Calculate();

            if (transitionFrequency > _nyquistFrequency) transitionFrequency = _nyquistFrequency;

            _biQuadFilter.SetHighShelfVariables(_targetSamplingRate, transitionFrequency, transitionSlope, dbGain);
        }
    }

    internal class HighShelfFilter_OperatorCalculator_ManyConsts
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _transitionFrequency;
        private readonly double _transitionSlope;
        private readonly double _dbGain;
        private readonly double _targetSamplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public HighShelfFilter_OperatorCalculator_ManyConsts(
            OperatorCalculatorBase signalCalculator,
            double transitionFrequency,
            double transitionSlope,
            double dbGain,
            double targetSamplingRate)
                : base(new[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertFilterFrequency(transitionFrequency, targetSamplingRate);

            _signalCalculator = signalCalculator;
            _transitionFrequency = transitionFrequency;
            _transitionSlope = transitionSlope;
            _dbGain = dbGain;
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
            _biQuadFilter.SetHighShelfVariables(_targetSamplingRate, _transitionFrequency, _transitionSlope, _dbGain);
            _biQuadFilter.ResetSamples();
        }
    }
}

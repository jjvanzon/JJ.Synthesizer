using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LowShelfFilter_OperatorCalculator_SoundVarOrConst_OtherInputsVar
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _transitionFrequencyCalculator;
        private readonly OperatorCalculatorBase _transitionSlopeCalculator;
        private readonly OperatorCalculatorBase _dbGainCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public LowShelfFilter_OperatorCalculator_SoundVarOrConst_OtherInputsVar(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase transitionFrequencyCalculator,
            OperatorCalculatorBase transitionSlopeCalculator,
            OperatorCalculatorBase dbGainCalculator,
            double targetSamplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(new[]
            {
                soundCalculator,
                transitionFrequencyCalculator,
                transitionSlopeCalculator,
                dbGainCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(soundCalculator, () => soundCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _soundCalculator = soundCalculator;
            _transitionFrequencyCalculator = transitionFrequencyCalculator ?? throw new NullException(() => transitionFrequencyCalculator);
            _transitionSlopeCalculator = transitionSlopeCalculator ?? throw new NullException(() => transitionSlopeCalculator);
            _dbGainCalculator = dbGainCalculator ?? throw new NullException(() => dbGainCalculator);
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
            double transitionFrequency = _transitionFrequencyCalculator.Calculate();
            double transitionSlope = _transitionSlopeCalculator.Calculate();
            double dbGain = _dbGainCalculator.Calculate();

            if (transitionFrequency > _nyquistFrequency) transitionFrequency = _nyquistFrequency;

            _biQuadFilter.SetLowShelfVariables(_targetSamplingRate, transitionFrequency, transitionSlope, dbGain);
        }
    }

    internal class LowShelfFilter_OperatorCalculator_SoundVarOrConst_OtherInputsConst 
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _transitionFrequency;
        private readonly double _transitionSlope;
        private readonly double _dbGain;
        private readonly double _samplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public LowShelfFilter_OperatorCalculator_SoundVarOrConst_OtherInputsConst(
            OperatorCalculatorBase signalCalculator,
            double transitionFrequency,
            double transitionSlope,
            double dbGain,
            double samplingRate)
                : base(new[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertFilterFrequency(transitionFrequency, samplingRate);

            _signalCalculator = signalCalculator;
            _transitionFrequency = transitionFrequency;
            _transitionSlope = transitionSlope;
            _dbGain = dbGain;
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
            _biQuadFilter.SetLowShelfVariables(_samplingRate, _transitionFrequency, _transitionSlope, _dbGain);
            _biQuadFilter.ResetSamples();
        }
    }
}

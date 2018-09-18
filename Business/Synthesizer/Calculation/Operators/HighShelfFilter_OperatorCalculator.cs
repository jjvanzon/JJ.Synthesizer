using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighShelfFilter_OperatorCalculator
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _transitionFrequencyCalculator;
        private readonly OperatorCalculatorBase _transitionSlopeCalculator;
        private readonly OperatorCalculatorBase _dbGainCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly BiQuadFilter _biQuadFilter;

        public HighShelfFilter_OperatorCalculator(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase transitionFrequencyCalculator,
            OperatorCalculatorBase transitionSlopeCalculator,
            OperatorCalculatorBase dbGainCalculator,
            double targetSamplingRate)
            : base(
                new[]
                {
                    soundCalculator,
                    transitionFrequencyCalculator,
                    dbGainCalculator,
                    transitionSlopeCalculator
                })
        {
            _soundCalculator = soundCalculator;
            _transitionFrequencyCalculator = transitionFrequencyCalculator;
            _transitionSlopeCalculator = transitionSlopeCalculator;
            _dbGainCalculator = dbGainCalculator;
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
            double transitionFrequency = _transitionFrequencyCalculator.Calculate();
            double transitionSlope = _transitionSlopeCalculator.Calculate();
            double dbGain = _dbGainCalculator.Calculate();

            if (transitionFrequency > _nyquistFrequency) transitionFrequency = _nyquistFrequency;

            _biQuadFilter.SetHighShelfVariables(_targetSamplingRate, transitionFrequency, transitionSlope, dbGain);
        }
    }
}
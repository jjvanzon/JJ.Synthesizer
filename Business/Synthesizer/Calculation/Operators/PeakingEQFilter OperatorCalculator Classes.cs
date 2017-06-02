using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class PeakingEQFilter_OperatorCalculator_AllVars
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _centerFrequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _dbGainCalculator;
        private readonly double _samplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public PeakingEQFilter_OperatorCalculator_AllVars(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase centerFrequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase dbGainCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(new[]
            {
                soundCalculator,
                centerFrequencyCalculator,
                widthCalculator,
                dbGainCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(soundCalculator, () => soundCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _soundCalculator = soundCalculator;
            _centerFrequencyCalculator = centerFrequencyCalculator ?? throw new NullException(() => centerFrequencyCalculator);
            _widthCalculator = widthCalculator ?? throw new NullException(() => widthCalculator);
            _dbGainCalculator = dbGainCalculator ?? throw new NullException(() => dbGainCalculator);
            _samplingRate = samplingRate;
            _samplesBetweenApplyFilterVariables = samplesBetweenApplyFilterVariables;
            _biQuadFilter = new BiQuadFilter();

            _nyquistFrequency = _samplingRate / 2.0;

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
            double centerFrequency = _centerFrequencyCalculator.Calculate();
            double width = _widthCalculator.Calculate();
            double dbGain = _dbGainCalculator.Calculate();

            if (centerFrequency > _nyquistFrequency) centerFrequency = _nyquistFrequency;

            _biQuadFilter.SetPeakingEQVariables(_samplingRate, centerFrequency, width, dbGain);
        }
    }

    internal class PeakingEQFilter_OperatorCalculator_ManyConsts : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly double _centerFrequency;
        private readonly double _width;
        private readonly double _dbGain;
        private readonly double _targetSamplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public PeakingEQFilter_OperatorCalculator_ManyConsts(
            OperatorCalculatorBase soundCalculator,
            double centerFrequency,
            double width,
            double dbGain,
            double targetSamplingRate)
                : base(new[] { soundCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(soundCalculator, () => soundCalculator);
            OperatorCalculatorHelper.AssertFilterFrequency(centerFrequency, targetSamplingRate);

            _soundCalculator = soundCalculator;
            _centerFrequency = centerFrequency;
            _width = width;
            _dbGain = dbGain;
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
            _biQuadFilter.SetPeakingEQVariables(_targetSamplingRate, _centerFrequency, _width, _dbGain);
            _biQuadFilter.ResetSamples();
        }
    }
}

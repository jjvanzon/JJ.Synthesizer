﻿using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AllPassFilter_OperatorCalculator
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _centerFrequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly double _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly BiQuadFilter _biQuadFilter;

        public AllPassFilter_OperatorCalculator(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase centerFrequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            double targetSamplingRate)
            : base(new[] 
            {
                soundCalculator,
                centerFrequencyCalculator,
                widthCalculator })
        {
            _soundCalculator = soundCalculator;
            _centerFrequencyCalculator = centerFrequencyCalculator;
            _widthCalculator = widthCalculator;
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
            double centerFrequency = _centerFrequencyCalculator.Calculate();
            double width = _widthCalculator.Calculate();

            if (centerFrequency > _nyquistFrequency) centerFrequency = _nyquistFrequency;

            _biQuadFilter.SetAllPassFilterVariables(_targetSamplingRate, centerFrequency, width);
        }
    }
}

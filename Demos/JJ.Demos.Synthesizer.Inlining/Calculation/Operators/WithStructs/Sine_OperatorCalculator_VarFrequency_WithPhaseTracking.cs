using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal struct Sine_OperatorCalculator_VarFrequency_WithPhaseTracking<TFrequencyCalculator> : IOperatorCalculator
        where TFrequencyCalculator : IOperatorCalculator
    {
        public TFrequencyCalculator _frequencyCalculator;
        public DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double position = _dimensionStack.Get();

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;
            double value = SineCalculator.Sin(_phase);

            _previousPosition = position;

            return value;
        }
    }
}

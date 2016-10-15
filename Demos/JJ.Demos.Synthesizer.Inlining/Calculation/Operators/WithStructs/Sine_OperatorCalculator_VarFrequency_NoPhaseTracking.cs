using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal struct Sine_OperatorCalculator_VarFrequency_NoPhaseTracking<TFrequencyCalculator>
        : ISine_OperatorCalculator_VarFrequency
        where TFrequencyCalculator : struct, IOperatorCalculator
    {
        public TFrequencyCalculator _frequencyCalculator;
        public DimensionStack _dimensionStack;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double position = _dimensionStack.Get();

            double frequency = _frequencyCalculator.Calculate();

            double phase = position * frequency;
            double value = SineCalculator.Sin(phase);

            return value;
        }

        DimensionStack ISine_OperatorCalculator_VarFrequency.DimensionStack
        {
            get { return _dimensionStack; }
            set { _dimensionStack = value; }
        }

        IOperatorCalculator ISine_OperatorCalculator_VarFrequency.FrequencyCalculator
        {
            get { return _frequencyCalculator; }
            set { _frequencyCalculator = (TFrequencyCalculator)value; }
        }
    }
}
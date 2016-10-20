using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Sine_OperatorCalculator_VarFrequency_NoPhaseTracking<TFrequencyCalculator>
        : ISine_OperatorCalculator_VarFrequency
        where TFrequencyCalculator : IOperatorCalculator
    {
        private TFrequencyCalculator _frequencyCalculator;
        public IOperatorCalculator FrequencyCalculator
        {
            get { return _frequencyCalculator; }
            set { _frequencyCalculator = (TFrequencyCalculator)value; }
        }

        private DimensionStack _dimensionStack;
        public DimensionStack DimensionStack
        {
            get { return _dimensionStack; }
            set { _dimensionStack = value; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double position = _dimensionStack.Get();

            double frequency = _frequencyCalculator.Calculate();

            double phase = position * frequency;
            double value = SineCalculator.Sin(phase);

            return value;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
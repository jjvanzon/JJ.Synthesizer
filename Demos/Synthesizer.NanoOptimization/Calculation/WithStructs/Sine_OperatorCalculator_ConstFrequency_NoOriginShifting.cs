using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Sine_OperatorCalculator_ConstFrequency_NoOriginShifting : IOperatorCalculator
    {
        private double _frequency;
        public double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
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
            double value = SineCalculator.Sin(position * _frequency);

            return value;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);

    }
}

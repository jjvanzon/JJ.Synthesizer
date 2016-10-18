using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Sine_OperatorCalculator_ConstFrequency_NoOriginShifting : IOperatorCalculator
    {
        public double _frequency;
        public DimensionStack _dimensionStack;

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{
    /// <summary>
    /// I still need some kind of polymorphism, because I need to call calculators
    /// abstractly from another calculator.
    /// </summary>
    internal interface IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Calculate();
    }
}

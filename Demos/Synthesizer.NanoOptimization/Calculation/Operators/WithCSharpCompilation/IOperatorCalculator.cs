using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithCSharpCompilation
{
    /// <summary> Public to be accessible to run-time generated assemblies. </summary>
    public interface IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Calculate();
    }
}

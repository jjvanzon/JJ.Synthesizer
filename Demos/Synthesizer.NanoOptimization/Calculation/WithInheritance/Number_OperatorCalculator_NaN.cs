using System;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithInheritance
{
    internal class Number_OperatorCalculator_NaN : OperatorCalculatorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return Double.NaN;
        }
    }
}

using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithInheritance
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

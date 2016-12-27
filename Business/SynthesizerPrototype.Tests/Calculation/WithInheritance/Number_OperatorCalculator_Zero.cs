using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithInheritance
{
    internal class Number_OperatorCalculator_Zero : OperatorCalculatorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return 0.0;
        }
    }
}

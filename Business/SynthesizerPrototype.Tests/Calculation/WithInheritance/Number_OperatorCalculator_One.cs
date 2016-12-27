using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithInheritance
{
    internal class Number_OperatorCalculator_One : OperatorCalculatorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return 1.0;
        }
    }
}

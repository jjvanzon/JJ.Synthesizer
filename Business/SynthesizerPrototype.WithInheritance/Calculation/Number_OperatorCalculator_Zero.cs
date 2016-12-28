using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    public class Number_OperatorCalculator_Zero : OperatorCalculatorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return 0.0;
        }
    }
}

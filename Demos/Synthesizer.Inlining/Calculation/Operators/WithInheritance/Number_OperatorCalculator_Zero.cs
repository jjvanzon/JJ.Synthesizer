using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance
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

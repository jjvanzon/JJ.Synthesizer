using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance
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

using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class Number_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _number;

        public Number_OperatorCalculator(double number) => _number = number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate() => _number;
    }
}

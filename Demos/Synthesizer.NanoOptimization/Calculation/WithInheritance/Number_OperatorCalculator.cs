using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithInheritance
{
    internal class Number_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _number;

        public Number_OperatorCalculator(double number)
        {
            _number = number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _number;
        }
    }
}

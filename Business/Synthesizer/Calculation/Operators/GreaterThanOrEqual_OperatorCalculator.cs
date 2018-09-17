using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class GreaterThanOrEqual_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorA;
        private readonly OperatorCalculatorBase _calculatorB;

        public GreaterThanOrEqual_OperatorCalculator(
            OperatorCalculatorBase calculatorA,
            OperatorCalculatorBase calculatorB)
            : base(new[] { calculatorA, calculatorB })
        {
            _calculatorA = calculatorA;
            _calculatorB = calculatorB;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _calculatorA.Calculate();
            double b = _calculatorB.Calculate();

            return a >= b ? 1.0 : 0.0;
        }
    }
}
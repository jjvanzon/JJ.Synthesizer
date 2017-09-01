using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

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
            _calculatorA = calculatorA ?? throw new NullException(() => calculatorA);
            _calculatorB = calculatorB ?? throw new NullException(() => calculatorB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _calculatorA.Calculate();
            double b = _calculatorB.Calculate();

            if (a >= b) return 1.0;
            else return 0.0;
        }
    }
}
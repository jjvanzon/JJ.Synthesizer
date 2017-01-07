using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LessThan_OperatorCalculator_VarA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorA;
        private readonly OperatorCalculatorBase _calculatorB;

        public LessThan_OperatorCalculator_VarA_VarB(
            OperatorCalculatorBase calculatorA,
            OperatorCalculatorBase calculatorB)
            : base(new OperatorCalculatorBase[] { calculatorA, calculatorB })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(calculatorA, () => calculatorA);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(calculatorB, () => calculatorB);

            _calculatorA = calculatorA;
            _calculatorB = calculatorB;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _calculatorA.Calculate();
            double b = _calculatorB.Calculate();

            if (a < b) return 1.0;
            else return 0.0;
        }
    }

    internal class LessThan_OperatorCalculator_VarA_ConstB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorA;
        private readonly double _b;

        public LessThan_OperatorCalculator_VarA_ConstB(OperatorCalculatorBase calculatorA, double b)
            : base(new OperatorCalculatorBase[] { calculatorA })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(calculatorA, () => calculatorA);

            _calculatorA = calculatorA;
            _b = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _calculatorA.Calculate();

            if (a < _b) return 1.0;
            else return 0.0;
        }
    }

    internal class LessThan_OperatorCalculator_ConstA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _calculatorB;

        public LessThan_OperatorCalculator_ConstA_VarB(double a, OperatorCalculatorBase calculatorB)
            : base(new OperatorCalculatorBase[] { calculatorB })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(calculatorB, () => calculatorB);

            _a = a;
            _calculatorB = calculatorB;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _calculatorB.Calculate();

            if (_a < b) return 1.0;
            else return 0.0;
        }
    }
}
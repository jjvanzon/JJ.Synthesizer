using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LessThanOrEqual_VarA_VarB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorA;
        private readonly OperatorCalculatorBase _calculatorB;

        public LessThanOrEqual_VarA_VarB_OperatorCalculator(
            OperatorCalculatorBase calculatorA,
            OperatorCalculatorBase calculatorB)
            : base(new OperatorCalculatorBase[] { calculatorA, calculatorB })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorA, () => calculatorA);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorB, () => calculatorB);

            _calculatorA = calculatorA;
            _calculatorB = calculatorB;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _calculatorA.Calculate();
            double b = _calculatorB.Calculate();

            if (a <= b) return 1.0;
            else return 0.0;
        }
    }

    internal class LessThanOrEqual_VarA_ConstB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorA;
        private readonly double _b;

        public LessThanOrEqual_VarA_ConstB_OperatorCalculator(OperatorCalculatorBase calculatorA, double b)
            : base(new OperatorCalculatorBase[] { calculatorA })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorA, () => calculatorA);

            _calculatorA = calculatorA;
            _b = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _calculatorA.Calculate();

            if (a <= _b) return 1.0;
            else return 0.0;
        }
    }

    internal class LessThanOrEqual_ConstA_VarB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _calculatorB;

        public LessThanOrEqual_ConstA_VarB_OperatorCalculator(double a, OperatorCalculatorBase calculatorB)
            : base(new OperatorCalculatorBase[] { calculatorB })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorB, () => calculatorB);

            _a = a;
            _calculatorB = calculatorB;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _calculatorB.Calculate();

            if (_a <= b) return 1.0;
            else return 0.0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class And_VarA_VarB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorA;
        private readonly OperatorCalculatorBase _calculatorB;

        public And_VarA_VarB_OperatorCalculator(
            OperatorCalculatorBase calculatorA,
            OperatorCalculatorBase calculatorB)
            : base(new OperatorCalculatorBase[] { calculatorA, calculatorB })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorA, () => calculatorA);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorB, () => calculatorB);

            _calculatorA = calculatorA;
            _calculatorB = calculatorB;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _calculatorA.Calculate(time, channelIndex);
            double b = _calculatorB.Calculate(time, channelIndex);

            bool aIsTrue = a != 0.0;
            bool bIsTrue = b != 0.0;

            if (aIsTrue && bIsTrue) return 1.0;
            else return 0.0;
        }
    }

    internal class And_VarA_ConstB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorA;
        private readonly bool _bIsTrue;

        public And_VarA_ConstB_OperatorCalculator(OperatorCalculatorBase calculatorA, double b)
            : base(new OperatorCalculatorBase[] { calculatorA })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorA, () => calculatorA);

            _calculatorA = calculatorA;
            _bIsTrue = b != 0.0;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _calculatorA.Calculate(time, channelIndex);
            bool aIsTrue = a != 0.0;

            if (aIsTrue && _bIsTrue) return 1.0;
            else return 0.0;
        }
    }

    internal class And_ConstA_VarB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly bool _aIsTrue;
        private readonly OperatorCalculatorBase _calculatorB;

        public And_ConstA_VarB_OperatorCalculator(double a, OperatorCalculatorBase calculatorB)
            : base(new OperatorCalculatorBase[] { calculatorB })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorB, () => calculatorB);

            _aIsTrue = a != 0.0;
            _calculatorB = calculatorB;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _calculatorB.Calculate(time, channelIndex);

            bool bIsTrue = b != 0.0;

            if (_aIsTrue == bIsTrue) return 1.0;
            else return 0.0;
        }
    }
}
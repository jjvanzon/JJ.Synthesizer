using System;
using System.Collections.Generic;
using System.Linq;

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

        public override double Calculate(double time, int channelIndex)
        {
            double a = _calculatorA.Calculate(time, channelIndex);
            double b = _calculatorB.Calculate(time, channelIndex);

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

        public override double Calculate(double time, int channelIndex)
        {
            double a = _calculatorA.Calculate(time, channelIndex);

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

        public override double Calculate(double time, int channelIndex)
        {
            double b = _calculatorB.Calculate(time, channelIndex);

            if (_a <= b) return 1.0;
            else return 0.0;
        }
    }
}
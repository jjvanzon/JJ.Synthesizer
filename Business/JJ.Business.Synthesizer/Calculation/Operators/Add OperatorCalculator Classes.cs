using System;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Add_OperatorCalculator_VarA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Add_OperatorCalculator_VarA_VarB(OperatorCalculatorBase aCalculator, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(aCalculator, () => aCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            return a + b;
        }
    }

    internal class Add_OperatorCalculator_ConstA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;

        public Add_OperatorCalculator_ConstA_VarB(double a, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { bCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);

            _a = a;

            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _bCalculator.Calculate();

            return _a + b;
        }
    }

    internal class Add_OperatorCalculator_WithOperandArray : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _operandCalculators;

        public Add_OperatorCalculator_WithOperandArray(OperatorCalculatorBase[] operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);

            _operandCalculators = operandCalculators;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double result = 0;

            for (int i = 0; i < _operandCalculators.Length; i++)
            {
                double result2 = _operandCalculators[i].Calculate();

                result += result2;
            }

            return result;
        }
    }
}

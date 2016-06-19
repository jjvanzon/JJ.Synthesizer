using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_OperatorCalculator_ConstA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private double _a;
        private OperatorCalculatorBase _bCalculator;

        public Multiply_OperatorCalculator_ConstA_VarB(double a, OperatorCalculatorBase bCalculator)
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
            return _a * b;
        }
    }

    internal class Multiply_OperatorCalculator_VarA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Multiply_OperatorCalculator_VarA_VarB(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
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
            return a * b;
        }
    }

    internal class Multiply_OperatorCalculator_WithOperandArray : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _firstOperandCalculator;
        private readonly OperatorCalculatorBase[] _remainingOperandCalculators;

        public Multiply_OperatorCalculator_WithOperandArray(IList<OperatorCalculatorBase> operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);
            if (operandCalculators.Count == 0) throw new CollectionEmptyException(() => operandCalculators);

            _firstOperandCalculator = operandCalculators.First();
            _remainingOperandCalculators = operandCalculators.Skip(1).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double product = _firstOperandCalculator.Calculate(); ;

            for (int i = 0; i < _remainingOperandCalculators.Length; i++)
            {
                double value = _remainingOperandCalculators[i].Calculate();

                product *= value;
            }

            return product;
        }
    }
}
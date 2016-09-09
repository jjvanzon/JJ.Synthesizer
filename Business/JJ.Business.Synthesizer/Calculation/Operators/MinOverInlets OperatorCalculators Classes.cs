using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // You could imagine many more optimized calculations, such as first operand is const and several,
    // that omit the loop, but future optimizations will just make that work obsolete again.

    internal class MinOverInlets_OperatorCalculator_WithConst_AndVarArray : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _constValue;
        private readonly OperatorCalculatorBase[] _varOperandCalculators;
        private readonly double _varOperandCalculatorsCount;

        public MinOverInlets_OperatorCalculator_WithConst_AndVarArray(double constValue, IList<OperatorCalculatorBase> operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);

            _constValue = constValue;
            _varOperandCalculators = operandCalculators.ToArray();
            _varOperandCalculatorsCount = _varOperandCalculators.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double min = _constValue;

            for (int i = 0; i < _varOperandCalculatorsCount; i++)
            {
                double value = _varOperandCalculators[i].Calculate();

                if (min > value)
                {
                    min = value;
                }
            }

            return min;
        }
    }

    internal class MinOverInlets_OperatorCalculator_AllVars : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _firstOperandCalculator;
        private readonly OperatorCalculatorBase[] _remainingOperandCalculators;
        private readonly double _remainingOperandCalculatorsCount;
        
        public MinOverInlets_OperatorCalculator_AllVars(IList<OperatorCalculatorBase> operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);
            if (operandCalculators.Count <= 2) throw new LessThanOrEqualException(() => operandCalculators.Count, 2);

            _firstOperandCalculator = operandCalculators.First();
            _remainingOperandCalculators = operandCalculators.Skip(1).ToArray();
            _remainingOperandCalculatorsCount = _remainingOperandCalculators.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double result = _firstOperandCalculator.Calculate();

            for (int i = 0; i < _remainingOperandCalculatorsCount; i++)
            {
                double result2 = _remainingOperandCalculators[i].Calculate();

                if (result2 < result)
                {
                    result = result2;
                }
            }

            return result;
        }
    }

    internal class MinOverInlets_OperatorCalculator_OneConst_OneVar : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _constValue;
        private readonly OperatorCalculatorBase _varCalculator;

        public MinOverInlets_OperatorCalculator_OneConst_OneVar(double constValue, OperatorCalculatorBase varCalculator)
            : base(new OperatorCalculatorBase[] { varCalculator })
        {
            if (varCalculator == null) throw new NullException(() => varCalculator);

            _constValue = constValue;
            _varCalculator = varCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double varValue = _varCalculator.Calculate();

            if (_constValue < varValue)
            {
                return _constValue;
            }
            else
            {
                return varValue;
            }
        }
    }

    internal class MinOverInlets_OperatorCalculator_TwoVars : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public MinOverInlets_OperatorCalculator_TwoVars(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MaxOverInlets_OperatorCalculator_Vars_1Const : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _varOperandCalculators;
        private readonly double _varOperandCalculatorsCount;
        private readonly double _constValue;

        public MaxOverInlets_OperatorCalculator_Vars_1Const(IList<OperatorCalculatorBase> operandCalculators, double constValue)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);

            _varOperandCalculators = operandCalculators.ToArray();
            _varOperandCalculatorsCount = _varOperandCalculators.Length;
            _constValue = constValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double max = _constValue;

            for (int i = 0; i < _varOperandCalculatorsCount; i++)
            {
                double value = _varOperandCalculators[i].Calculate();

                if (max < value)
                {
                    max = value;
                }
            }

            return max;
        }
    }

    internal class MaxOverInlets_OperatorCalculator_AllVars : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _firstOperandCalculator;
        private readonly OperatorCalculatorBase[] _remainingOperandCalculators;
        private readonly double _remainingOperandCalculatorsCount;

        public MaxOverInlets_OperatorCalculator_AllVars(IList<OperatorCalculatorBase> operandCalculators)
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
            double max = _firstOperandCalculator.Calculate();

            for (int i = 0; i < _remainingOperandCalculatorsCount; i++)
            {
                double value = _remainingOperandCalculators[i].Calculate();

                if (max < value)
                {
                    max = value;
                }
            }

            return max;
        }
    }

    internal class MaxOverInlets_OperatorCalculator_1Var_1Const : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _varCalculator;
        private readonly double _constValue;

        public MaxOverInlets_OperatorCalculator_1Var_1Const(OperatorCalculatorBase varCalculator, double constValue)
            : base(new OperatorCalculatorBase[] { varCalculator })
        {
            if (varCalculator == null) throw new NullException(() => varCalculator);

            _varCalculator = varCalculator;
            _constValue = constValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double varValue = _varCalculator.Calculate();

            if (_constValue > varValue)
            {
                return _constValue;
            }
            else
            {
                return varValue;
            }
        }
    }

    internal class MaxOverInlets_OperatorCalculator_2Vars : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public MaxOverInlets_OperatorCalculator_2Vars(
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

            if (a > b)
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
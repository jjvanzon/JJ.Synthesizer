using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Bundle_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly int _dimensionIndex;
        private readonly OperatorCalculatorBase[] _operands;
        private readonly double _operandCountDouble;
        private readonly DimensionStacks _dimensionStack;

        public Bundle_OperatorCalculator(
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack,
            IList<OperatorCalculatorBase> operands)
            : base(operands)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
            _operands = operands.ToArray();
            _operandCountDouble = operands.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double dimensionValue = _dimensionStack.PopAndGet(_dimensionIndex);

            double result;

            if (ConversionHelper.CanCastToNonNegativeInt32WithMax(dimensionValue, _operandCountDouble))
            {
                int dimensionValueInt = (int)dimensionValue;

                OperatorCalculatorBase operand = _operands[dimensionValueInt];

                result = operand.Calculate();
            }
            else
            {
                result = 0.0;
            }

            _dimensionStack.Push(_dimensionIndex, dimensionValue);

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Bundle_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly int _dimensionIndex;
        private readonly OperatorCalculatorBase[] _operands;
        private readonly double _operandCountDouble;

        public Bundle_OperatorCalculator(DimensionEnum dimensionEnum, IList<OperatorCalculatorBase> operands)
            : base(operands)
        {
            _dimensionIndex = (int)dimensionEnum;
            _operands = operands.ToArray();
            _operandCountDouble = operands.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate(DimensionStack dimensionStack)
        {
            double dimensionValue = dimensionStack.PopAndGet(_dimensionIndex);

            double result;

            if (ConversionHelper.CanCastToNonNegativeInt32WithMax(dimensionValue, _operandCountDouble))
            {
                int dimensionValueInt = (int)dimensionValue;

                OperatorCalculatorBase operand = _operands[dimensionValueInt];

                result = operand.Calculate(dimensionStack);
            }
            else
            {
                result = 0.0;
            }

            dimensionStack.Push(_dimensionIndex, dimensionValue);

            return result;
        }
    }
}

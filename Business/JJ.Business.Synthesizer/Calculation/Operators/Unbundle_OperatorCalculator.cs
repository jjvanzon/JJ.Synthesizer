using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Unbundle_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandCalculator;
        private readonly int _dimensionIndex;
        private readonly double _dimensionValue;
        private readonly DimensionStack _dimensionStack;

        public Unbundle_OperatorCalculator(
            OperatorCalculatorBase operandCalculator, 
            DimensionEnum dimensionEnum, 
            double dimensionValue,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { operandCalculator })
        {
            if (operandCalculator == null) throw new NullException(() => operandCalculator);
            // Do not call OperatorCalculatorHelper.AssertDimensionEnum, because Undefined is allowed to.
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _operandCalculator = operandCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionValue = dimensionValue;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            _dimensionStack.Push(_dimensionIndex, _dimensionValue);

            double result = _operandCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }
}

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

        public Unbundle_OperatorCalculator(
            OperatorCalculatorBase operandCalculator, 
            DimensionEnum dimensionEnum, 
            double dimensionValue)
            : base(new OperatorCalculatorBase[] { operandCalculator })
        {
            if (operandCalculator == null) throw new NullException(() => operandCalculator);

            _operandCalculator = operandCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionValue = dimensionValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate(DimensionStack dimensionStack)
        {
            dimensionStack.Push(_dimensionIndex, _dimensionValue);

            double result = _operandCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }
}

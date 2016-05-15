using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Bundle_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _operands;
        private readonly double _operandCountDouble;
        private readonly DimensionStack _dimensionStack;
        //private readonly int _dimensionLayer;

        public Bundle_OperatorCalculator(
            DimensionStack dimensionStack,
            IList<OperatorCalculatorBase> operands//,
            /*int dimensionLayer*/)
            : base(operands)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _dimensionStack = dimensionStack;
            _operands = operands.ToArray();
            _operandCountDouble = operands.Count;
            //_dimensionLayer = dimensionLayer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.PopAndGet();

            double result;

            if (ConversionHelper.CanCastToNonNegativeInt32WithMax(position, _operandCountDouble))
            {
                int positionInt = (int)position;

                OperatorCalculatorBase operand = _operands[positionInt];

                result = operand.Calculate();
            }
            else
            {
                result = 0.0;
            }

            _dimensionStack.Push(position);

            return result;
        }
    }
}

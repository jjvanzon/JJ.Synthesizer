using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class InletsToDimension_OperatorCalculator_Block : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _operandCalculators;
        private readonly OperatorCalculatorBase _positionCalculator;
        private readonly double _maxIndexDouble;

        [Obsolete(
            "Not obsolete, but make sure that you pass the positionCalculator " +
            "is passed to the operandCalculators. When they are, remove this obsolete attribute.")]
        public InletsToDimension_OperatorCalculator_Block(
            IList<OperatorCalculatorBase> operandCalculators,
            OperatorCalculatorBase positionCalculator)
            : base(operandCalculators)
        {
            _operandCalculators = operandCalculators.ToArray();
            _positionCalculator = positionCalculator;
            _maxIndexDouble = operandCalculators.Count - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();

            double result;

            if (ConversionHelper.CanCastToNonNegativeInt32WithMax(position, _maxIndexDouble))
            {
                int positionInt = (int)position;

                OperatorCalculatorBase operand = _operandCalculators[positionInt];

                result = operand.Calculate();
            }
            else
            {
                result = 0.0;
            }

            return result;
        }
    }
}

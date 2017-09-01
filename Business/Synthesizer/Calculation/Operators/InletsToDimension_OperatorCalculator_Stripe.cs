using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class InletsToDimension_OperatorCalculator_Stripe : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _operandCalculators;
        private readonly double _maxIndexDouble;
        private readonly OperatorCalculatorBase _positionInputCalculator;

        [Obsolete(
            "Not obsolete, but make sure that you pass the positionInputCalculator " +
            "is passed to the operandCalculators. When they are, remove this obsolete attribute.")]
        public InletsToDimension_OperatorCalculator_Stripe(
            IList<OperatorCalculatorBase> operandCalculators,
            OperatorCalculatorBase positionInputCalculator)
            : base(operandCalculators)
        {

            _positionInputCalculator = positionInputCalculator;
            _operandCalculators = operandCalculators.ToArray();
            _maxIndexDouble = operandCalculators.Count - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionInputCalculator.Calculate();
     
            // Correct position, to get stripe interpolation.
            position += 0.5;

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

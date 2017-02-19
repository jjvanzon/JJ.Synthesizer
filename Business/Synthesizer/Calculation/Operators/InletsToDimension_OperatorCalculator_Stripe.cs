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
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;
        
        public InletsToDimension_OperatorCalculator_Stripe(IList<OperatorCalculatorBase> operandCalculators, DimensionStack dimensionStack)
            : base(operandCalculators)
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _operandCalculators = operandCalculators.ToArray();
            _maxIndexDouble = operandCalculators.Count - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
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

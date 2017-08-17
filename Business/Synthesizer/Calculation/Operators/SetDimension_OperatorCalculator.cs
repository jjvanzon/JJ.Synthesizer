using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SetDimension_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _passThroughCalculator;
        private readonly OperatorCalculatorBase _numberCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SetDimension_OperatorCalculator(
            OperatorCalculatorBase passThroughCalculator,
            OperatorCalculatorBase numberCalculator,
            DimensionStack dimensionStack)
            : base(new[] { passThroughCalculator, numberCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(passThroughCalculator, () => passThroughCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(numberCalculator, () => numberCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _passThroughCalculator = passThroughCalculator;
            _numberCalculator = numberCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _numberCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(position);
#else
            _dimensionStack.Set(_dimensionStackIndex, position);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double outputValue = _passThroughCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return outputValue;
        }

        public override void Reset()
        {
            double position = _numberCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(position);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, position);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif
            base.Reset();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }
    }
}

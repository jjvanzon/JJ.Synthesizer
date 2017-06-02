using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SetDimension_OperatorCalculator_VarPassThrough_VarX : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _passThroughCalculator;
        private readonly OperatorCalculatorBase _xCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SetDimension_OperatorCalculator_VarPassThrough_VarX(
            OperatorCalculatorBase passThroughCalculator,
            OperatorCalculatorBase xCalculator,
            DimensionStack dimensionStack)
            : base(new[] { passThroughCalculator, xCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(passThroughCalculator, () => passThroughCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(xCalculator, () => xCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _passThroughCalculator = passThroughCalculator;
            _xCalculator = xCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _xCalculator.Calculate();

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
            double position = _xCalculator.Calculate();

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

    internal class SetDimension_OperatorCalculator_VarPassThrough_ConstX : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _passThroughCalculator;
        private readonly double _x;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SetDimension_OperatorCalculator_VarPassThrough_ConstX(
            OperatorCalculatorBase passThroughCalculator,
            double x,
            DimensionStack dimensionStack)
            : base(new[] { passThroughCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(passThroughCalculator, () => passThroughCalculator);

            _x = x;
            _passThroughCalculator = passThroughCalculator;
            _dimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack);
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            _dimensionStack.Push(_x);
#else
            _dimensionStack.Set(_dimensionStackIndex, _x);
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
    }
}

using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SetDimension_OperatorCalculator_VarPassThrough_VarValue : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _passThroughCalculator;
        private readonly OperatorCalculatorBase _valueCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SetDimension_OperatorCalculator_VarPassThrough_VarValue(
            OperatorCalculatorBase passThroughCalculator,
            OperatorCalculatorBase valueCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { passThroughCalculator, valueCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(passThroughCalculator, () => passThroughCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(valueCalculator, () => valueCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _passThroughCalculator = passThroughCalculator;
            _valueCalculator = valueCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _valueCalculator.Calculate();

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
            double position = _valueCalculator.Calculate();

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

    internal class SetDimension_OperatorCalculator_VarPassThrough_ConstValue : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _passThroughCalculator;
        private readonly double _value;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SetDimension_OperatorCalculator_VarPassThrough_ConstValue(
            OperatorCalculatorBase passThroughCalculator,
            double value,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { passThroughCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(passThroughCalculator, () => passThroughCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _value = value;
            _passThroughCalculator = passThroughCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            _dimensionStack.Push(_value);
#else
            _dimensionStack.Set(_dimensionStackIndex, _value);
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

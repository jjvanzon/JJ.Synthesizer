using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SetDimension_OperatorCalculator_ConstValue : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly double _value;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SetDimension_OperatorCalculator_ConstValue(
            OperatorCalculatorBase calculationCalculator,
            double value,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { calculationCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _value = value;
            _calculationCalculator = calculationCalculator;
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

            double outputValue = _calculationCalculator.Calculate();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return outputValue;
        }
    }

    internal class SetDimension_OperatorCalculator_VarValue : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly OperatorCalculatorBase _valueCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SetDimension_OperatorCalculator_VarValue(
            OperatorCalculatorBase calculationCalculator,
            OperatorCalculatorBase valueCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { calculationCalculator, valueCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(valueCalculator, () => valueCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _calculationCalculator = calculationCalculator;
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

            double outputValue = _calculationCalculator.Calculate();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return outputValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SetDimension_OperatorCalculator_ConstValue : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly double _value;
        private readonly int _dimensionEnumInt;
        private readonly DimensionStacks _dimensionStack;

        public SetDimension_OperatorCalculator_ConstValue(
            OperatorCalculatorBase calculationCalculator,
            double value,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { calculationCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _dimensionEnumInt = (int)dimensionEnum;
            _value = value;
            _calculationCalculator = calculationCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            _dimensionStack.Push(_dimensionEnumInt, _value);

            double outputValue = _calculationCalculator.Calculate();

            _dimensionStack.Pop(_dimensionEnumInt);

            return outputValue;
        }
    }

    internal class SetDimension_OperatorCalculator_VarValue : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly OperatorCalculatorBase _valueCalculator;
        private readonly int _dimensionEnumInt;
        private readonly DimensionStacks _dimensionStack;

        public SetDimension_OperatorCalculator_VarValue(
            OperatorCalculatorBase calculationCalculator,
            OperatorCalculatorBase valueCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { calculationCalculator, valueCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(valueCalculator, () => valueCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _dimensionEnumInt = (int)dimensionEnum;
            _calculationCalculator = calculationCalculator;
            _valueCalculator = valueCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double dimensionValue = _valueCalculator.Calculate();

            _dimensionStack.Push(_dimensionEnumInt, dimensionValue);

            double outputValue = _calculationCalculator.Calculate();

            _dimensionStack.Pop(_dimensionEnumInt);

            return outputValue;
        }
    }
}

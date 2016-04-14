using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SetDimension_OperatorCalculator_ConstValue : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly double _value;
        private readonly int _dimensionEnumInt;

        public SetDimension_OperatorCalculator_ConstValue(
            OperatorCalculatorBase calculationCalculator,
            double value,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { calculationCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _dimensionEnumInt = (int)dimensionEnum;
            _value = value;
            _calculationCalculator = calculationCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            dimensionStack.Push(_dimensionEnumInt, _value);

            double outputValue = _calculationCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionEnumInt);

            return outputValue;
        }
    }

    internal class SetDimension_OperatorCalculator_VarValue : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly OperatorCalculatorBase _valueCalculator;
        private readonly int _dimensionEnumInt;

        public SetDimension_OperatorCalculator_VarValue(
            OperatorCalculatorBase calculationCalculator,
            OperatorCalculatorBase valueCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { calculationCalculator, valueCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(valueCalculator, () => valueCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _dimensionEnumInt = (int)dimensionEnum;
            _calculationCalculator = calculationCalculator;
            _valueCalculator = valueCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double dimensionValue = _valueCalculator.Calculate(dimensionStack);

            dimensionStack.Push(_dimensionEnumInt, dimensionValue);

            double outputValue = _calculationCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionEnumInt);

            return outputValue;
        }
    }
}

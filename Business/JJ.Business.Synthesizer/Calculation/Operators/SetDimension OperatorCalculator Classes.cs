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
        private readonly DimensionEnum _dimensionEnum;

        public SetDimension_OperatorCalculator_ConstValue(
            OperatorCalculatorBase calculationCalculator,
            double value,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { calculationCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);

            _dimensionEnum = dimensionEnum;
            _value = value;
            _calculationCalculator = calculationCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Temporary implementation (2016-03-10), before we have more dimension values on the call stack.
            switch (_dimensionEnum)
            {
                case DimensionEnum.Time:
                    time = _value;
                    break;

                case DimensionEnum.Channel:
                    channelIndex = (int)_value;
                    break;
            }

            double outputValue = _calculationCalculator.Calculate(time, channelIndex);
            return outputValue;
        }
    }

    internal class SetDimension_OperatorCalculator_VarValue : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly OperatorCalculatorBase _valueCalculator;
        private readonly DimensionEnum _dimensionEnum;

        public SetDimension_OperatorCalculator_VarValue(
            OperatorCalculatorBase calculationCalculator,
            OperatorCalculatorBase valueCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { calculationCalculator, valueCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(valueCalculator, () => valueCalculator);

            _dimensionEnum = dimensionEnum;
            _calculationCalculator = calculationCalculator;
            _valueCalculator = valueCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dimensionValue = _valueCalculator.Calculate(time, channelIndex);

            // Temporary implementation (2016-03-10), before we have more dimension values on the call stack.
            switch (_dimensionEnum)
            {
                case DimensionEnum.Time:
                    time = dimensionValue;
                    break;

                case DimensionEnum.Channel:
                    channelIndex = (int)dimensionValue;
                    break;
            }

            double outputValue = _calculationCalculator.Calculate(time, channelIndex);
            return outputValue;
        }
    }

}

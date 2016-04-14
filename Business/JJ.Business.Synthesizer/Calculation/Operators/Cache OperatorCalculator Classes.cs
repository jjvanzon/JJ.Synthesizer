using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Cache_OperatorCalculator_SingleChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator _arrayCalculator;
        private readonly int _dimensionIndex;

        public Cache_OperatorCalculator_SingleChannel(
            TArrayCalculator arrayCalculator,
            DimensionEnum dimensionEnum)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _arrayCalculator = arrayCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(_dimensionIndex);

            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator[] _arrayCalculators;
        private readonly int _dimensionIndex;

        public Cache_OperatorCalculator_MultiChannel(
            IList<TArrayCalculator> arrayCalculators,
            DimensionEnum dimensionEnum)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _arrayCalculators = arrayCalculators.ToArray();
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double channel = dimensionStack.Get(DimensionEnum.Channel);
            double time = dimensionStack.Get(_dimensionIndex);

            // TODO: Cast to int can fail.
            int channelInt = (int)channel;

            return _arrayCalculators[channelInt].CalculateValue(time);
        }
    }
}
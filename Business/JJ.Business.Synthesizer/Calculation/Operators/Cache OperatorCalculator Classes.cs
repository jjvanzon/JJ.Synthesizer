using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Cache_OperatorCalculator_SingleChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator _arrayCalculator;
        private readonly DimensionStack _dimensionStack;

        public Cache_OperatorCalculator_SingleChannel(TArrayCalculator arrayCalculator, DimensionStack dimensionStack)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _arrayCalculator = arrayCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double time = _dimensionStack.Get();

            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator[] _arrayCalculators;
        private readonly int _arrayCalculatorsLength;
        private readonly DimensionStack _channelDimensionStack;
        private readonly DimensionStack _dimensionStack;

        public Cache_OperatorCalculator_MultiChannel(
            IList<TArrayCalculator> arrayCalculators,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (channelDimensionStack == null) throw new NullException(() => channelDimensionStack);

            _arrayCalculators = arrayCalculators.ToArray();
            _arrayCalculatorsLength = _arrayCalculators.Length;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double channelDouble = _channelDimensionStack.Get();
            double time = _dimensionStack.Get();

            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelDouble, _arrayCalculatorsLength))
            {
                return 0.0;
            }

            int channelInt = (int)channelDouble;

            return _arrayCalculators[channelInt].CalculateValue(time);
        }
    }
}
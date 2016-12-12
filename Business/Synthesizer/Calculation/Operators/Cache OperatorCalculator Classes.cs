using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Cache_OperatorCalculator_SingleChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator _arrayCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Cache_OperatorCalculator_SingleChannel(
            TArrayCalculator arrayCalculator, 
            DimensionStack dimensionStack)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _arrayCalculator = arrayCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double time = _dimensionStack.Get();
#else
            double time = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator[] _arrayCalculators;
        private readonly int _arrayCalculatorsMaxIndex;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;
        private readonly int _dimensionStackIndex;
        private readonly int _channelDimensionStackIndex;

        public Cache_OperatorCalculator_MultiChannel(
            IList<TArrayCalculator> arrayCalculators,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
            OperatorCalculatorHelper.AssertDimensionStack(channelDimensionStack);

            _arrayCalculators = arrayCalculators.ToArray();
            _arrayCalculatorsMaxIndex = _arrayCalculators.Length - 1;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _channelDimensionStackIndex = channelDimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double channelDouble = _channelDimensionStack.Get();
#else
            double channelDouble = _channelDimensionStack.Get(_channelDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_channelDimensionStack, _channelDimensionStackIndex);
#endif
            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelDouble, _arrayCalculatorsMaxIndex))
            {
                return 0.0;
            }
            int channelInt = (int)channelDouble;

#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            return _arrayCalculators[channelInt].CalculateValue(position);
        }
    }
}
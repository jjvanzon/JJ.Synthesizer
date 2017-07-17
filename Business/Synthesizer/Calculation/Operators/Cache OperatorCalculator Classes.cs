using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Cache_OperatorCalculator_SingleChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ICalculatorWithPosition
    {
        private readonly TArrayCalculator _arrayCalculator;
        private readonly DimensionStack _dimensionStack;

        public Cache_OperatorCalculator_SingleChannel(
            TArrayCalculator arrayCalculator, 
            DimensionStack dimensionStack)
        {
            if (arrayCalculator == null) throw new ArgumentNullException(nameof(arrayCalculator));

            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _arrayCalculator = arrayCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double time = _dimensionStack.Get();

            return _arrayCalculator.Calculate(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ICalculatorWithPosition
    {
        private readonly TArrayCalculator[] _arrayCalculators;
        private readonly double _arrayCalculatorsMaxIndexDouble;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;

        public Cache_OperatorCalculator_MultiChannel(
            IList<TArrayCalculator> arrayCalculators,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
            OperatorCalculatorHelper.AssertDimensionStack(channelDimensionStack);

            _arrayCalculators = arrayCalculators.ToArray();
            _arrayCalculatorsMaxIndexDouble = _arrayCalculators.Length - 1;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double channelDouble = _channelDimensionStack.Get();

            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelDouble, _arrayCalculatorsMaxIndexDouble))
            {
                return 0.0;
            }
            int channelInt = (int)channelDouble;

            double position = _dimensionStack.Get();

            return _arrayCalculators[channelInt].Calculate(position);
        }
    }
}
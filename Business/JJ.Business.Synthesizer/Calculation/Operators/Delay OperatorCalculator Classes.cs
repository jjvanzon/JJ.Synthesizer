using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Delay_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _positionDifferenceCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Delay_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase positionDifferenceCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                positionDifferenceCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(positionDifferenceCalculator, () => positionDifferenceCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _positionDifferenceCalculator = positionDifferenceCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            double positionDifference = _positionDifferenceCalculator.Calculate();

            // IMPORTANT: To add time to the output, you have subtract time from the input.
            double transformedPosition = position - positionDifference;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }
    }

    internal class Delay_VarSignal_ConstTimeDifference_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _positionDifference;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Delay_VarSignal_ConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double positionDifferenceValue,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _positionDifference = positionDifferenceValue;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = _dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To add time to the output, you have subtract time from the input.
            double transformedPosition = position - _positionDifference;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }
    }
}

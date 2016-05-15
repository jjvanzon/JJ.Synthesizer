using System;
using JJ.Framework.Reflection.Exceptions;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Earlier_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _positionDifferenceCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Earlier_OperatorCalculator(
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

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedPosition = position + positionDifference;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }
    }

    internal class Earlier_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _timeDifference;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Earlier_WithConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeDifference,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _timeDifference = timeDifference;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedPosition = position + _timeDifference;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }
    }
}

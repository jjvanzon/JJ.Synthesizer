using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Earlier_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeDifferenceCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Earlier_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase timeDifferenceCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                timeDifferenceCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(timeDifferenceCalculator, () => timeDifferenceCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _timeDifferenceCalculator = timeDifferenceCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double timeDifference = _timeDifferenceCalculator.Calculate();

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedPosition = position + timeDifference;

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    internal class Earlier_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _timeDifference;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Earlier_WithConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeDifference,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _timeDifference = timeDifference;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedPosition = position + _timeDifference;

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }
}

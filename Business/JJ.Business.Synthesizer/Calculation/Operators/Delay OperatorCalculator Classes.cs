using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Delay_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeDifferenceCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Delay_OperatorCalculator(
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

            // IMPORTANT: To add time to the output, you have subtract time from the input.
            double transformedPosition = position - timeDifference;

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    internal class Delay_VarSignal_ConstTimeDifference_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _timeDifferenceValue;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Delay_VarSignal_ConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeDifferenceValue,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _timeDifferenceValue = timeDifferenceValue;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To add time to the output, you have subtract time from the input.
            double transformedPosition = position - _timeDifferenceValue;

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }
}

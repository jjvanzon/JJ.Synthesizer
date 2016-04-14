using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Earlier_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeDifferenceCalculator;
        private readonly int _dimensionIndex;

        public Earlier_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase timeDifferenceCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                timeDifferenceCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(timeDifferenceCalculator, () => timeDifferenceCalculator);

            _signalCalculator = signalCalculator;
            _timeDifferenceCalculator = timeDifferenceCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double timeDifference = _timeDifferenceCalculator.Calculate(dimensionStack);

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedPosition = position + timeDifference;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    internal class Earlier_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _timeDifference;
        private readonly int _dimensionIndex;

        public Earlier_WithConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeDifference,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
            _timeDifference = timeDifference;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedPosition = position + _timeDifference;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }
}

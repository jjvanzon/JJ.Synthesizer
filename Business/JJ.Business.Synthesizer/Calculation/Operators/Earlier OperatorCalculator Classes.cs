using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Earlier_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeDifferenceCalculator;

        public Earlier_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDifferenceCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, timeDifferenceCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (timeDifferenceCalculator == null) throw new NullException(() => timeDifferenceCalculator);
            if (timeDifferenceCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => timeDifferenceCalculator);

            _signalCalculator = signalCalculator;
            _timeDifferenceCalculator = timeDifferenceCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double timeDifference = _timeDifferenceCalculator.Calculate(dimensionStack);

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedTime = time + timeDifference;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }

    internal class Earlier_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _timeDifferenceValue;

        public Earlier_WithConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeDifferenceValue)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _timeDifferenceValue = timeDifferenceValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedTime = time + _timeDifferenceValue;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }
}

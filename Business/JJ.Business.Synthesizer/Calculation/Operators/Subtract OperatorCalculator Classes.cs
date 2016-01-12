using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Subtract_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly OperatorCalculatorBase _operandBCalculator;

        public Subtract_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            OperatorCalculatorBase operandBCalculator)
            : base(new OperatorCalculatorBase[] { operandACalculator, operandBCalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return a - b;
        }
    }

    internal class Subtract_WithConstOperandA_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _operandAValue;
        private readonly OperatorCalculatorBase _operandBCalculator;

        public Subtract_WithConstOperandA_OperatorCalculator(double operandAValue, OperatorCalculatorBase operandBCalculator)
            : base(new OperatorCalculatorBase[] { operandBCalculator })
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

            _operandAValue = operandAValue;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return _operandAValue - b;
        }
    }

    internal class Subtract_WithConstOperandB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly double _operandBValue;

        public Subtract_WithConstOperandB_OperatorCalculator(OperatorCalculatorBase operandACalculator, double operandBValue)
            : base(new OperatorCalculatorBase[] { operandACalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);

            _operandACalculator = operandACalculator;
            _operandBValue = operandBValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            return a - _operandBValue;
        }
    }
}
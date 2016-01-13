using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Add_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly OperatorCalculatorBase _operandBCalculator;

        public Add_OperatorCalculator(OperatorCalculatorBase operandACalculator, OperatorCalculatorBase operandBCalculator)
            : base(new OperatorCalculatorBase[] { operandACalculator, operandBCalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            if (Double.IsNaN(a)) a = 0.0;
            if (Double.IsNaN(b)) b = 0.0;

            return a + b;
        }
    }

    internal class Add_WithConstOperandA_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _operandAValue;
        private readonly OperatorCalculatorBase _operandBCalculator;

        public Add_WithConstOperandA_OperatorCalculator(double operandAValue, OperatorCalculatorBase operandBCalculator)
            : base(new OperatorCalculatorBase[] { operandBCalculator })
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

            _operandAValue = operandAValue;

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            if (Double.IsNaN(_operandAValue)) _operandAValue = 0.0;

            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _operandBCalculator.Calculate(time, channelIndex);

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            if (Double.IsNaN(b))
            {
                return _operandAValue;
            }

            return _operandAValue + b;
        }
    }

    internal class Add_WithConstOperandB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly double _operandBValue;

        public Add_WithConstOperandB_OperatorCalculator(OperatorCalculatorBase operandACalculator, double operandBValue)
            : base(new OperatorCalculatorBase[] { operandACalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);

            _operandACalculator = operandACalculator;
            _operandBValue = operandBValue;

            if (Double.IsNaN(_operandBValue)) _operandBValue = 0.0;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            if (Double.IsNaN(a))
            {
                return _operandBValue;
            }

            return a + _operandBValue;
        }
    }
}
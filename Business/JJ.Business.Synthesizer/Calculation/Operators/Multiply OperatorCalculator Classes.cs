using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_WithConstOrigin_AndOperandA_OperatorCalculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private OperatorCalculatorBase _operandBCalculator;
        private double _originValue;

        public Multiply_WithConstOrigin_AndOperandA_OperatorCalculator(
            double operandAValue,
            OperatorCalculatorBase operandBCalculator,
            double originValue)
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");

            _operandAValue = operandAValue;
            _operandBCalculator = operandBCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return (_operandAValue - _originValue) * b + _originValue;
        }
    }

    internal class Multiply_WithConstOrigin_AndOperandB_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private double _operandBValue;
        private double _originValue;

        public Multiply_WithConstOrigin_AndOperandB_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            double operandBValue,
            double originValue)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");

            _operandACalculator = operandACalculator;
            _operandBValue = operandBValue;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            return (a - _originValue) * _operandBValue + _originValue;
        }
    }

    internal class Multiply_WithConstOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private OperatorCalculatorBase _operandBCalculator;
        private double _originValue;

        public Multiply_WithConstOrigin_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            OperatorCalculatorBase operandBCalculator,
            double originValue)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return (a - _originValue) * b + _originValue;
        }
    }

    internal class Multiply_WithOrigin_AndConstOperandA_AndOperandB_OperatorCalculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private double _operandBValue;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_AndConstOperandA_AndOperandB_OperatorCalculator(
            double operandAValue,
            double operandBValue,
            OperatorCalculatorBase originCalculator)
        {
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _operandAValue = operandAValue;
            _operandBValue = operandBValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            return (_operandAValue - origin) * _operandBValue + origin;
        }
    }

    internal class Multiply_WithOrigin_AndConstOperandA_OperatorCalculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private OperatorCalculatorBase _operandBCalculator;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_AndConstOperandA_OperatorCalculator(
            double operandAValue,
            OperatorCalculatorBase operandBCalculator,
            OperatorCalculatorBase originCalculator)
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _operandAValue = operandAValue;
            _operandBCalculator = operandBCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return (_operandAValue - origin) * b + origin;
        }
    }

    internal class Multiply_WithOrigin_AndConstOperandB_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private double _operandBValue;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_AndConstOperandB_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            double operandBValue,
            OperatorCalculatorBase originCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _operandACalculator = operandACalculator;
            _operandBValue = operandBValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            double a = _operandACalculator.Calculate(time, channelIndex);
            return (a - origin) * _operandBValue + origin;
        }
    }

    internal class Multiply_WithOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private OperatorCalculatorBase _operandBCalculator;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            OperatorCalculatorBase operandBCalculator,
            OperatorCalculatorBase originCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return (a - origin) * b + origin;
        }
    }

    internal class Multiply_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private OperatorCalculatorBase _operandBCalculator;

        public Multiply_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase operandACalculator, OperatorCalculatorBase operandBCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return a * b;
        }
    }

    internal class Multiply_WithoutOrigin_WithConstOperandA_OperatorCalculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private OperatorCalculatorBase _operandBCalculator;

        public Multiply_WithoutOrigin_WithConstOperandA_OperatorCalculator(double operandValue, OperatorCalculatorBase operandBCalculator)
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");

            _operandAValue = operandValue;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return _operandAValue * b;
        }
    }

    internal class Multiply_WithoutOrigin_WithConstOperandB_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private double _operandBValue;

        public Multiply_WithoutOrigin_WithConstOperandB_OperatorCalculator(OperatorCalculatorBase operandACalculator, double operandBValue)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");

            _operandACalculator = operandACalculator;
            _operandBValue = operandBValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            return a * _operandBValue;
        }
    }
}

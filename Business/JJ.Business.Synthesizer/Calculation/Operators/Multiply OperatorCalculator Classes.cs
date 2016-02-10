using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_ConstOperandA_VarOperandB_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _operandAValue;
        private readonly OperatorCalculatorBase _operandBCalculator;
        private readonly double _originValue;

        public Multiply_ConstOperandA_VarOperandB_ConstOrigin_OperatorCalculator(
            double operandAValue,
            OperatorCalculatorBase operandBCalculator,
            double originValue)
            : base(new OperatorCalculatorBase[] { operandBCalculator })
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

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

    internal class Multiply_VarOperandA_ConstOperandB_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly double _operandBValue;
        private readonly double _originValue;

        public Multiply_VarOperandA_ConstOperandB_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            double operandBValue,
            double originValue)
            : base(new OperatorCalculatorBase[] { operandACalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            // TODO: Enable this code line again after debugging the hacsk in Random_OperatorCalculator_OtherInterpolations's
            // constructor that creates an instance of Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator.
            // It that no longer happens in that constructor, you can enable this code line again.
            //if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);

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

    internal class Multiply_VarOperandA_VarOperandB_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly OperatorCalculatorBase _operandBCalculator;
        private readonly double _originValue;

        public Multiply_VarOperandA_VarOperandB_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            OperatorCalculatorBase operandBCalculator,
            double originValue)
            : base(new OperatorCalculatorBase[] { operandACalculator, operandBCalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

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

    internal class Multiply_ConstOperandA_ConstOperandB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _operandAValue;
        private readonly double _operandBValue;
        private readonly OperatorCalculatorBase _originCalculator;

        public Multiply_ConstOperandA_ConstOperandB_VarOrigin_OperatorCalculator(
            double operandAValue,
            double operandBValue,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { originCalculator })
        {
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

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

    internal class Multiply_ConstOperandA_VarOperandB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _operandAValue;
        private readonly OperatorCalculatorBase _operandBCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Multiply_ConstOperandA_VarOperandB_VarOrigin_OperatorCalculator(
            double operandAValue,
            OperatorCalculatorBase operandBCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { operandBCalculator, originCalculator })
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

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

    internal class Multiply_VarOperandA_ConstOperandB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly double _operandBValue;
        private readonly OperatorCalculatorBase _originCalculator;

        public Multiply_VarOperandA_ConstOperandB_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            double operandBValue,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { operandACalculator, originCalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

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

    internal class Multiply_VarOperandA_VarOperandB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly OperatorCalculatorBase _operandBCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Multiply_VarOperandA_VarOperandB_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase operandACalculator,
            OperatorCalculatorBase operandBCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { operandACalculator, operandBCalculator, originCalculator })
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

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

    internal class Multiply_VarOperandA_VarOperandB_NoOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly OperatorCalculatorBase _operandBCalculator;

        public Multiply_VarOperandA_VarOperandB_NoOrigin_OperatorCalculator(OperatorCalculatorBase operandACalculator, OperatorCalculatorBase operandBCalculator)
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
            return a * b;
        }
    }

    internal class Multiply_ConstOperandA_VarOperandB_NoOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private double _operandAValue;
        private OperatorCalculatorBase _operandBCalculator;

        public Multiply_ConstOperandA_VarOperandB_NoOrigin_OperatorCalculator(double operandValue, OperatorCalculatorBase operandBCalculator)
            : base(new OperatorCalculatorBase[] { operandBCalculator })
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

            _operandAValue = operandValue;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return _operandAValue * b;
        }
    }

    internal class Multiply_VarOperandA_ConstOperandB_NoOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandACalculator;
        private readonly double _operandBValue;

        public Multiply_VarOperandA_ConstOperandB_NoOrigin_OperatorCalculator(OperatorCalculatorBase operandACalculator, double operandBValue)
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
            return a * _operandBValue;
        }
    }
}

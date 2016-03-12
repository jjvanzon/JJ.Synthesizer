using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Subtract_VarA_VarB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Subtract_VarA_VarB_OperatorCalculator(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _aCalculator.Calculate(time, channelIndex);
            double b = _bCalculator.Calculate(time, channelIndex);
            return a - b;
        }
    }

    internal class Subtract_ConstA_VarB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;

        public Subtract_ConstA_VarB_OperatorCalculator(double a, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { bCalculator })
        {
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _a = a;
            _bCalculator = bCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _bCalculator.Calculate(time, channelIndex);
            return _a - b;
        }
    }

    internal class Subtract_VarA_ConstB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;

        public Subtract_VarA_ConstB_OperatorCalculator(OperatorCalculatorBase aCalculator, double b)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);

            _aCalculator = aCalculator;
            _b = b;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _aCalculator.Calculate(time, channelIndex);
            return a - _b;
        }
    }
}
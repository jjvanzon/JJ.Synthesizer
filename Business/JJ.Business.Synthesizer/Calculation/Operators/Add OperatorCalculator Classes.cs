using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Add_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Add_OperatorCalculator(OperatorCalculatorBase aCalculator, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double a = _aCalculator.Calculate(dimensionStack);
            double b = _bCalculator.Calculate(dimensionStack);

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            //if (Double.IsNaN(a)) a = 0.0;
            //if (Double.IsNaN(b)) b = 0.0;

            return a + b;
        }
    }

    internal class Add_ConstA_VarB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;

        public Add_ConstA_VarB_OperatorCalculator(double a, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { bCalculator })
        {
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _a = a;

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            //if (Double.IsNaN(_a)) _a = 0.0;

            _bCalculator = bCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double b = _bCalculator.Calculate(dimensionStack);

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            //if (Double.IsNaN(b))
            //{
            //    return _a;
            //}

            return _a + b;
        }
    }

    internal class Add_VarA_ConstB_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;

        public Add_VarA_ConstB_OperatorCalculator(OperatorCalculatorBase aCalculator, double b)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);

            _aCalculator = aCalculator;
            _b = b;

            if (Double.IsNaN(_b)) _b = 0.0;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double a = _aCalculator.Calculate(dimensionStack);

            // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
            //if (Double.IsNaN(a))
            //{
            //    return _b;
            //}

            return a + _b;
        }
    }
}
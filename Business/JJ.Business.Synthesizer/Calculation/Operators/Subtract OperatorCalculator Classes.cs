using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _bCalculator.Calculate();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            return a - _b;
        }
    }
}
using System;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Add_OperatorCalculator_VarA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Add_OperatorCalculator_VarA_VarB(OperatorCalculatorBase aCalculator, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(aCalculator, () => aCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            return a + b;
        }
    }

    internal class Add_OperatorCalculator_ConstA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;

        public Add_OperatorCalculator_ConstA_VarB(double a, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { bCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);

            _a = a;

            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _bCalculator.Calculate();

            return _a + b;
        }
    }

    internal class Add_OperatorCalculator_VarA_ConstB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;

        public Add_OperatorCalculator_VarA_ConstB(OperatorCalculatorBase aCalculator, double b)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(aCalculator, () => aCalculator);

            _aCalculator = aCalculator;
            _b = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();

            return a + _b;
        }
    }
}

using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly double _origin;

        public MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_ConstOrigin(
            double a,
            OperatorCalculatorBase bCalculator,
            double origin)
            : base(new OperatorCalculatorBase[] { bCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);

            _a = a;
            _bCalculator = bCalculator;
            _origin = origin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _bCalculator.Calculate();
            return (_a - _origin) * b + _origin;
        }
    }

    internal class MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;
        private readonly double _origin;

        public MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_ConstOrigin(
            OperatorCalculatorBase aCalculator,
            double b,
            double origin)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            // TODO: Enable this code line again after debugging the hacsk in Random_OperatorCalculator_OtherInterpolations's
            // constructor that creates an instance of Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator.
            // It that no longer happens in that constructor, you can enable this code line again.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(aCalculator, () => aCalculator);

            _aCalculator = aCalculator;
            _b = b;
            _origin = origin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            return (a - _origin) * _b + _origin;
        }
    }

    internal class MultiplyWithOrigin_OperatorCalculator_VarA_VarB_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly double _origin;

        public MultiplyWithOrigin_OperatorCalculator_VarA_VarB_ConstOrigin(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator,
            double origin)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(aCalculator, () => aCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
            _origin = origin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();
            return (a - _origin) * b + _origin;
        }
    }

    internal class MultiplyWithOrigin_OperatorCalculator_ConstA_ConstB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly double _b;
        private readonly OperatorCalculatorBase _originCalculator;

        public MultiplyWithOrigin_OperatorCalculator_ConstA_ConstB_VarOrigin(
            double a,
            double b,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { originCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);

            _a = a;
            _b = b;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double origin = _originCalculator.Calculate();
            return (_a - origin) * _b + origin;
        }
    }

    internal class MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_VarOrigin(
            double a,
            OperatorCalculatorBase bCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { bCalculator, originCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);

            _a = a;
            _bCalculator = bCalculator;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double origin = _originCalculator.Calculate();
            double b = _bCalculator.Calculate();
            return (_a - origin) * b + origin;
        }
    }

    internal class MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;
        private readonly OperatorCalculatorBase _originCalculator;

        public MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_VarOrigin(
            OperatorCalculatorBase aCalculator,
            double b,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, originCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(aCalculator, () => aCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);

            _aCalculator = aCalculator;
            _b = b;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double origin = _originCalculator.Calculate();
            double a = _aCalculator.Calculate();
            return (a - origin) * _b + origin;
        }
    }

    internal class MultiplyWithOrigin_OperatorCalculator_VarA_VarB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        
        public MultiplyWithOrigin_OperatorCalculator_VarA_VarB_VarOrigin(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator, originCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(aCalculator, () => aCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(bCalculator, () => bCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double origin = _originCalculator.Calculate();
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();
            return (a - origin) * b + origin;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_OperatorCalculator_VarA_ConstB_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;
        private readonly double _origin;

        public Divide_OperatorCalculator_VarA_ConstB_ConstOrigin(
            OperatorCalculatorBase aCalculator,
            double b,
            double origin)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (b == 0) throw new ZeroException(() => b);

            _aCalculator = aCalculator;
            _b = b;
            _origin = origin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            return (a - _origin) / _b + _origin;
        }
    }

    internal class Divide_OperatorCalculator_ConstA_VarB_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly double _origin;

        public Divide_OperatorCalculator_ConstA_VarB_ConstOrigin(
            double a,
            OperatorCalculatorBase bCalculator,
            double origin)
            : base(new OperatorCalculatorBase[] { bCalculator })
        {
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _a = a;
            _bCalculator = bCalculator;
            _origin = origin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _bCalculator.Calculate();
            return (_a - _origin) / b + _origin;
        }
    }

    internal class Divide_OperatorCalculator_VarA_VarB_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly double _origin;

        public Divide_OperatorCalculator_VarA_VarB_ConstOrigin(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator,
            double origin)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
            _origin = origin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            return (a - _origin) / b + _origin;
        }
    }

    internal class Divide_OperatorCalculator_VarA_ConstB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_OperatorCalculator_VarA_ConstB_VarOrigin(
            OperatorCalculatorBase aCalculator,
            double b,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, originCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (b == 0) throw new ZeroException(() => b);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _aCalculator = aCalculator;
            _b = b;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double origin = _originCalculator.Calculate();
            double a = _aCalculator.Calculate();
            return (a - origin) / _b + origin;
        }
    }

    internal class Divide_OperatorCalculator_ConstA_ConstB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly double _b;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_OperatorCalculator_ConstA_ConstB_VarOrigin(
            double a,
            double b,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { originCalculator })
        {
            if (b == 0) throw new ZeroException(() => b);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _a = a;
            _b = b;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double origin = _originCalculator.Calculate();
            return (_a - origin) / _b + origin;
        }
    }

    internal class Divide_OperatorCalculator_ConstA_VarB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_OperatorCalculator_ConstA_VarB_VarOrigin(
            double a,
            OperatorCalculatorBase bCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { bCalculator, originCalculator })
        {
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _a = a;
            _bCalculator = bCalculator;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double b = _bCalculator.Calculate();

            double origin = _originCalculator.Calculate();

            return (_a - origin) / b + origin;
        }
    }

    internal class Divide_OperatorCalculator_VarA_VarB_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_OperatorCalculator_VarA_VarB_VarOrigin(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator, originCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
            _originCalculator = originCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            double origin = _originCalculator.Calculate();

            return (a - origin) / b + origin;
        }
    }

    internal class Divide_OperatorCalculator_VarA_VarB_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Divide_OperatorCalculator_VarA_VarB_ZeroOrigin(OperatorCalculatorBase aCalculator, OperatorCalculatorBase bCalculator)
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

            return a / b;
        }
    }

    internal class Divide_OperatorCalculator_VarA_ConstB_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;

        public Divide_OperatorCalculator_VarA_ConstB_ZeroOrigin(OperatorCalculatorBase aCalculator, double b)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (b == 0) throw new ZeroException(() => b);

            _aCalculator = aCalculator;
            _b = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double numerator = _aCalculator.Calculate();
            return numerator / _b;
        }
    }

    internal class Divide_OperatorCalculator_ConstA_VarB_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;

        public Divide_OperatorCalculator_ConstA_VarB_ZeroOrigin(double a, OperatorCalculatorBase bCalculator)
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

            return _a / b;
        }
    }
}

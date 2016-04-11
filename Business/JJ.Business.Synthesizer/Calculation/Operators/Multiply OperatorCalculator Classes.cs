using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_ConstA_VarB_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly double _origin;

        public Multiply_ConstA_VarB_ConstOrigin_OperatorCalculator(
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

        public override double Calculate(DimensionStack dimensionStack)
        {
            double b = _bCalculator.Calculate(dimensionStack);
            return (_a - _origin) * b + _origin;
        }
    }

    internal class Multiply_VarA_ConstB_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;
        private readonly double _origin;

        public Multiply_VarA_ConstB_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase aCalculator,
            double b,
            double origin)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            // TODO: Enable this code line again after debugging the hacsk in Random_OperatorCalculator_OtherInterpolations's
            // constructor that creates an instance of Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator.
            // It that no longer happens in that constructor, you can enable this code line again.
            //if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);

            _aCalculator = aCalculator;
            _b = b;
            _origin = origin;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double a = _aCalculator.Calculate(dimensionStack);
            return (a - _origin) * _b + _origin;
        }
    }

    internal class Multiply_VarA_VarB_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly double _origin;

        public Multiply_VarA_VarB_ConstOrigin_OperatorCalculator(
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

        public override double Calculate(DimensionStack dimensionStack)
        {
            double a = _aCalculator.Calculate(dimensionStack);
            double b = _bCalculator.Calculate(dimensionStack);
            return (a - _origin) * b + _origin;
        }
    }

    internal class Multiply_ConstA_ConstB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly double _b;
        private readonly OperatorCalculatorBase _originCalculator;

        public Multiply_ConstA_ConstB_VarOrigin_OperatorCalculator(
            double a,
            double b,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { originCalculator })
        {
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _a = a;
            _b = b;
            _originCalculator = originCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double origin = _originCalculator.Calculate(dimensionStack);
            return (_a - origin) * _b + origin;
        }
    }

    internal class Multiply_ConstA_VarB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _a;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Multiply_ConstA_VarB_VarOrigin_OperatorCalculator(
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

        public override double Calculate(DimensionStack dimensionStack)
        {
            double origin = _originCalculator.Calculate(dimensionStack);
            double b = _bCalculator.Calculate(dimensionStack);
            return (_a - origin) * b + origin;
        }
    }

    internal class Multiply_VarA_ConstB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;
        private readonly OperatorCalculatorBase _originCalculator;

        public Multiply_VarA_ConstB_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase aCalculator,
            double b,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, originCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _aCalculator = aCalculator;
            _b = b;
            _originCalculator = originCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double origin = _originCalculator.Calculate(dimensionStack);
            double a = _aCalculator.Calculate(dimensionStack);
            return (a - origin) * _b + origin;
        }
    }

    internal class Multiply_VarA_VarB_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        
        public Multiply_VarA_VarB_VarOrigin_OperatorCalculator(
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

        internal OperatorCalculatorBase BCalculator
        {
            get
            {
                return _bCalculator;
            }
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double origin = _originCalculator.Calculate(dimensionStack);
            double a = _aCalculator.Calculate(dimensionStack);
            double b = _bCalculator.Calculate(dimensionStack);
            return (a - origin) * b + origin;
        }
    }

    internal class Multiply_VarA_VarB_NoOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Multiply_VarA_VarB_NoOrigin_OperatorCalculator(OperatorCalculatorBase aCalculator, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double a = _aCalculator.Calculate(dimensionStack);
            double b = _bCalculator.Calculate(dimensionStack);
            return a * b;
        }
    }

    internal class Multiply_ConstA_VarB_NoOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private double _a;
        private OperatorCalculatorBase _bCalculator;

        public Multiply_ConstA_VarB_NoOrigin_OperatorCalculator(double a, OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { bCalculator })
        {
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _a = a;
            _bCalculator = bCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double b = _bCalculator.Calculate(dimensionStack);
            return _a * b;
        }
    }

    internal class Multiply_VarA_ConstB_NoOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;

        public Multiply_VarA_ConstB_NoOrigin_OperatorCalculator(OperatorCalculatorBase aCalculator, double b)
            : base(new OperatorCalculatorBase[] { aCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);

            _aCalculator = aCalculator;
            _b = b;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double a = _aCalculator.Calculate(dimensionStack);
            return a * _b;
        }
    }
}

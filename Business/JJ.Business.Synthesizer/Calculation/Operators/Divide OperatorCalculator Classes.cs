using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_VarNumerator_ConstDenominator_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numeratorCalculator;
        private readonly double _denominatorValue;
        private readonly double _originValue;

        public Divide_VarNumerator_ConstDenominator_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase numeratorCalculator,
            double denominatorValue,
            double originValue)
            : base(new OperatorCalculatorBase[] { numeratorCalculator })
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => numeratorCalculator);
            if (denominatorValue == 0) throw new ZeroException(() => denominatorValue);

            _numeratorCalculator = numeratorCalculator;
            _denominatorValue = denominatorValue;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _numeratorCalculator.Calculate(time, channelIndex);
            return (a - _originValue) / _denominatorValue + _originValue;
        }
    }

    internal class Divide_ConstNumerator_VarDenominator_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _numeratorValue;
        private readonly OperatorCalculatorBase _denominatorCalculator;
        private readonly double _originValue;

        public Divide_ConstNumerator_VarDenominator_ConstOrigin_OperatorCalculator(
            double numeratorValue,
            OperatorCalculatorBase denominatorCalculator,
            double originValue)
            : base(new OperatorCalculatorBase[] { denominatorCalculator })
        {
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => denominatorCalculator);

            _numeratorValue = numeratorValue;
            _denominatorCalculator = denominatorCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return _numeratorValue;
            }

            return (_numeratorValue - _originValue) / denominator + _originValue;
        }
    }

    internal class Divide_VarNumerator_VarDenominator_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numeratorCalculator;
        private readonly OperatorCalculatorBase _denominatorCalculator;
        private readonly double _originValue;

        public Divide_VarNumerator_VarDenominator_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase numeratorCalculator,
            OperatorCalculatorBase denominatorCalculator,
            double originValue)
            : base(new OperatorCalculatorBase[] { numeratorCalculator, denominatorCalculator })
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => numeratorCalculator);
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => denominatorCalculator);

            _numeratorCalculator = numeratorCalculator;
            _denominatorCalculator = denominatorCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double numerator = _numeratorCalculator.Calculate(time, channelIndex);
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return numerator;
            }

            return (numerator - _originValue) / denominator + _originValue;
        }
    }

    internal class Divide_WithOrigin_AndConstDenominator_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numeratorCalculator;
        private readonly double _denominatorValue;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstDenominator_OperatorCalculator(
            OperatorCalculatorBase numeratorCalculator,
            double denominatorValue,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { numeratorCalculator, originCalculator })
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => numeratorCalculator);
            if (denominatorValue == 0) throw new ZeroException(() => denominatorValue);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _numeratorCalculator = numeratorCalculator;
            _denominatorValue = denominatorValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            double a = _numeratorCalculator.Calculate(time, channelIndex);
            return (a - origin) / _denominatorValue + origin;
        }
    }

    internal class Divide_WithOrigin_AndConstNumerator_AndDenominator_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _numeratorValue;
        private readonly double _denominatorValue;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstNumerator_AndDenominator_OperatorCalculator(
            double numeratorValue,
            double denominatorValue,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { originCalculator })
        {
            if (denominatorValue == 0) throw new ZeroException(() => denominatorValue);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _numeratorValue = numeratorValue;
            _denominatorValue = denominatorValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            return (_numeratorValue - origin) / _denominatorValue + origin;
        }
    }

    internal class Divide_WithOrigin_AndConstNumerator_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _numeratorValue;
        private readonly OperatorCalculatorBase _denominatorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstNumerator_OperatorCalculator(
            double numeratorValue,
            OperatorCalculatorBase denominatorCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { denominatorCalculator, originCalculator })
        {
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => denominatorCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _numeratorValue = numeratorValue;
            _denominatorCalculator = denominatorCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return _numeratorValue;
            }

            double origin = _originCalculator.Calculate(time, channelIndex);

            return (_numeratorValue - origin) / denominator + origin;
        }
    }

    internal class Divide_WithOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numeratorCalculator;
        private readonly OperatorCalculatorBase _denominatorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_OperatorCalculator(
            OperatorCalculatorBase numeratorCalculator,
            OperatorCalculatorBase denominatorCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { numeratorCalculator, denominatorCalculator, originCalculator })
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => numeratorCalculator);
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => denominatorCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _numeratorCalculator = numeratorCalculator;
            _denominatorCalculator = denominatorCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double numerator = _numeratorCalculator.Calculate(time, channelIndex);
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return numerator;
            }

            double origin = _originCalculator.Calculate(time, channelIndex);

            return (numerator - origin) / denominator + origin;
        }
    }

    internal class Divide_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numeratorCalculator;
        private readonly OperatorCalculatorBase _denominatorCalculator;

        public Divide_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase numeratorCalculator, OperatorCalculatorBase denominatorCalculator)
            : base(new OperatorCalculatorBase[] { numeratorCalculator, denominatorCalculator })
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => numeratorCalculator);
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => denominatorCalculator);

            _numeratorCalculator = numeratorCalculator;
            _denominatorCalculator = denominatorCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double numerator = _numeratorCalculator.Calculate(time, channelIndex);
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return numerator;
            }

            return numerator / denominator;
        }
    }

    internal class Divide_VarNumerator_ConstDenominator_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numeratorCalculator;
        private readonly double _denominatorValue;

        public Divide_VarNumerator_ConstDenominator_ZeroOrigin_OperatorCalculator(OperatorCalculatorBase numeratorCalculator, double denominatorValue)
            : base(new OperatorCalculatorBase[] { numeratorCalculator })
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => numeratorCalculator);
            if (denominatorValue == 0) throw new ZeroException(() => denominatorValue);

            _numeratorCalculator = numeratorCalculator;
            _denominatorValue = denominatorValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double numerator = _numeratorCalculator.Calculate(time, channelIndex);
            return numerator / _denominatorValue;
        }
    }

    internal class Divide_ConstNumerator_VarDenominator_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _numeratorValue;
        private readonly OperatorCalculatorBase _denominatorCalculator;

        public Divide_ConstNumerator_VarDenominator_ZeroOrigin_OperatorCalculator(double numeratorValue, OperatorCalculatorBase denominatorCalculator)
            : base(new OperatorCalculatorBase[] { denominatorCalculator })
        {
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            // TODO: Enable this code line again after debugging the hacsk in Random_OperatorCalculator_OtherInterpolations's
            // constructor that creates an instance of Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator.
            // It that no longer happens in that constructor, you can enable this code line again.
            //if (denominatorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => denominatorCalculator);

            _numeratorValue = numeratorValue;
            _denominatorCalculator = denominatorCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return _numeratorValue;
            }

            return _numeratorValue / denominator;
        }
    }
}

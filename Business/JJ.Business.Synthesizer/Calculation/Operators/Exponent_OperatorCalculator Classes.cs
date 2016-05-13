using System;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Exponent_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_OperatorCalculator(OperatorCalculatorBase lowCalculator, OperatorCalculatorBase highCalculator, OperatorCalculatorBase ratioCalculator)
            : base(new OperatorCalculatorBase[] { lowCalculator, highCalculator, ratioCalculator })
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);
            if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);
            if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _lowCalculator = lowCalculator;
            _highCalculator = highCalculator;
            _ratioCalculator = ratioCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double low = _lowCalculator.Calculate();
            double high = _highCalculator.Calculate();
            double ratio = _ratioCalculator.Calculate();
            
            double result = low * Math.Pow(high / low, ratio);
            return result;
        }
    }

    internal class Exponent_WithConstLow_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _low;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_WithConstLow_OperatorCalculator(double low, OperatorCalculatorBase highCalculator, OperatorCalculatorBase ratioCalculator)
            : base(new OperatorCalculatorBase[] { highCalculator, ratioCalculator })
        {
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);
            if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _low = low;
            _highCalculator = highCalculator;
            _ratioCalculator = ratioCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double high = _highCalculator.Calculate();
            double ratio = _ratioCalculator.Calculate();

            double result = _low * Math.Pow(high / _low, ratio);
            return result;
        }
    }

    internal class Exponent_WithConstHigh_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly double _high;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_WithConstHigh_OperatorCalculator(OperatorCalculatorBase lowCalculator, double high, OperatorCalculatorBase ratioCalculator)
            : base(new OperatorCalculatorBase[] { lowCalculator, ratioCalculator })
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);
            if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _lowCalculator = lowCalculator;
            _high = high;
            _ratioCalculator = ratioCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double low = _lowCalculator.Calculate();
            double ratio = _ratioCalculator.Calculate();

            // TODO: Low priority: Can you break up a fraction raised to a power
            // into two so that you can cache one power and prevent the division below?
            double result = low * Math.Pow(_high / low, ratio);
            return result;
        }
    }

    internal class Exponent_WithConstLowAndConstHigh_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _low;
#if DEBUG
        private readonly double _high;
#endif
        private readonly double _highDividedByLow;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_WithConstLowAndConstHigh_OperatorCalculator(double low, double high, OperatorCalculatorBase ratioCalculator)
            : base(new OperatorCalculatorBase[] { ratioCalculator })
        {
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _low = low;
#if DEBUG
            _high = high;
#endif
            _highDividedByLow = high / low;

            _ratioCalculator = ratioCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double ratio = _ratioCalculator.Calculate();

            double result = _low * Math.Pow(_highDividedByLow, ratio);
            return result;
        }
    }

    internal class Exponent_WithConstRatio_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly double _ratio;

        public Exponent_WithConstRatio_OperatorCalculator(OperatorCalculatorBase lowCalculator, OperatorCalculatorBase highCalculator, double ratio)
            : base(new OperatorCalculatorBase[] { lowCalculator, highCalculator })
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);
            if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);

            _lowCalculator = lowCalculator;
            _highCalculator = highCalculator;
            _ratio = ratio;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double low = _lowCalculator.Calculate();
            double high = _highCalculator.Calculate();

            double result = low * Math.Pow(high / low, _ratio);
            return result;
        }
    }

    internal class Exponent_WithConstLowAndConstRatio_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _low;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly double _ratio;

        public Exponent_WithConstLowAndConstRatio_OperatorCalculator(double low, OperatorCalculatorBase highCalculator, double ratio)
            : base(new OperatorCalculatorBase[] { highCalculator })
        {
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);

            _low = low;
            _highCalculator = highCalculator;
            _ratio = ratio;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double high = _highCalculator.Calculate();

            double result = _low * Math.Pow(high / _low, _ratio);
            return result;
        }
    }

    internal class Exponent_WithConstHighAndConstRatio_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly double _high;
        private readonly double _ratio;

        public Exponent_WithConstHighAndConstRatio_OperatorCalculator(OperatorCalculatorBase lowCalculator, double high, double ratio)
            : base(new OperatorCalculatorBase[] { lowCalculator })
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);

            _lowCalculator = lowCalculator;
            _high = high;
            _ratio = ratio;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double low = _lowCalculator.Calculate();

            double result = low * Math.Pow(_high / low, _ratio);
            return result;
        }
    }
}

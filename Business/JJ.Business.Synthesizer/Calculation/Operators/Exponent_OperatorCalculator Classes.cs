using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Exponent_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_OperatorCalculator(OperatorCalculatorBase lowCalculator, OperatorCalculatorBase highCalculator, OperatorCalculatorBase ratioCalculator)
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

        public override double Calculate(double time, int channelIndex)
        {
            double low = _lowCalculator.Calculate(time, channelIndex);
            double high = _highCalculator.Calculate(time, channelIndex);
            double ratio = _ratioCalculator.Calculate(time, channelIndex);
            
            double result = low * Math.Pow(high / low, ratio);
            return result;
        }
    }

    internal class Exponent_OperatorCalculator_WithConstLow : OperatorCalculatorBase
    {
        private readonly double _low;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_OperatorCalculator_WithConstLow(double low, OperatorCalculatorBase highCalculator, OperatorCalculatorBase ratioCalculator)
        {
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);
            if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _low = low;
            _highCalculator = highCalculator;
            _ratioCalculator = ratioCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double high = _highCalculator.Calculate(time, channelIndex);
            double ratio = _ratioCalculator.Calculate(time, channelIndex);

            double result = _low * Math.Pow(high / _low, ratio);
            return result;
        }
    }

    internal class Exponent_OperatorCalculator_WithConstHigh : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly double _high;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_OperatorCalculator_WithConstHigh(OperatorCalculatorBase lowCalculator, double high, OperatorCalculatorBase ratioCalculator)
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);
            if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _lowCalculator = lowCalculator;
            _high = high;
            _ratioCalculator = ratioCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double low = _lowCalculator.Calculate(time, channelIndex);
            double ratio = _ratioCalculator.Calculate(time, channelIndex);

            double result = low * Math.Pow(_high / low, ratio);
            return result;
        }
    }

    internal class Exponent_OperatorCalculator_WithConstHighAndConstLow : OperatorCalculatorBase
    {
        private readonly double _low;
        private readonly double _high;
        private readonly OperatorCalculatorBase _ratioCalculator;

        public Exponent_OperatorCalculator_WithConstHighAndConstLow(double low, double high, OperatorCalculatorBase ratioCalculator)
        {
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _low = low;
            _high = high;
            _ratioCalculator = ratioCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double ratio = _ratioCalculator.Calculate(time, channelIndex);

            double result = _low * Math.Pow(_high / _low, ratio);
            return result;
        }
    }

    internal class Exponent_OperatorCalculator_WithConstRatio : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly double _ratio;

        public Exponent_OperatorCalculator_WithConstRatio(OperatorCalculatorBase lowCalculator, OperatorCalculatorBase highCalculator, double ratio)
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);
            if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);

            _lowCalculator = lowCalculator;
            _highCalculator = highCalculator;
            _ratio = ratio;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double low = _lowCalculator.Calculate(time, channelIndex);
            double high = _highCalculator.Calculate(time, channelIndex);

            double result = low * Math.Pow(high / low, _ratio);
            return result;
        }
    }

    internal class Exponent_OperatorCalculator_WithConstLowAndConstRatio : OperatorCalculatorBase
    {
        private readonly double _low;
        private readonly OperatorCalculatorBase _highCalculator;
        private readonly double _ratio;

        public Exponent_OperatorCalculator_WithConstLowAndConstRatio(double low, OperatorCalculatorBase highCalculator, double ratio)
        {
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);

            _low = low;
            _highCalculator = highCalculator;
            _ratio = ratio;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double high = _highCalculator.Calculate(time, channelIndex);

            double result = _low * Math.Pow(high / _low, _ratio);
            return result;
        }
    }

    internal class Exponent_OperatorCalculator_WithConstHighAndConstRatio : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _lowCalculator;
        private readonly double _high;
        private readonly double _ratio;

        public Exponent_OperatorCalculator_WithConstHighAndConstRatio(OperatorCalculatorBase lowCalculator, double high, double ratio)
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);

            _lowCalculator = lowCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double low = _lowCalculator.Calculate(time, channelIndex);

            double result = low * Math.Pow(_high / low, _ratio);
            return result;
        }
    }
}

using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SpeedUp_WithConstOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;
        private double _originValue;

        public SpeedUp_WithConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeDividerCalculator,
            double originValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (timeDividerCalculator is Number_OperatorCalculator) throw new Exception("timeDividerCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = (time - _originValue) * timeDivider + _originValue;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }

    internal class SpeedUp_WithOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;
        private OperatorCalculatorBase _originCalculator;

        public SpeedUp_WithOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeDividerCalculator,
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (timeDividerCalculator is Number_OperatorCalculator) throw new Exception("timeDividerCalculator cannot be a Value_OperatorCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) * timeDivider + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }

    internal class SpeedUp_WithOrigin_WithConstTimeDivider_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeDividerValue;
        private OperatorCalculatorBase _originCalculator;

        public SpeedUp_WithOrigin_WithConstTimeDivider_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeDividerValue,
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signal cannot be a ValueCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeDividerValue = timeDividerValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) * _timeDividerValue + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }

    internal class SpeedUp_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;

        public SpeedUp_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDividerCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (timeDividerCalculator is Number_OperatorCalculator) throw new Exception("timeDividerCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * timeDivider;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }

    internal class SpeedUp_WithoutOrigin_WithConstTimeDivider_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeDividerValue;

        public SpeedUp_WithoutOrigin_WithConstTimeDivider_OperatorCalculator(OperatorCalculatorBase signalCalculator, double timeDividerValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeDividerValue == 0) throw new Exception("timeDividerValue cannot be 0.");

            _signalCalculator = signalCalculator;
            _timeDividerValue = timeDividerValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * _timeDividerValue;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}

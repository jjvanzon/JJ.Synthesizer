using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_WithConstOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;
        private double _originValue;

        public SlowDown_WithConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeMultiplierCalculator,
            double originValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (timeMultiplierCalculator is Number_OperatorCalculator) throw new Exception("timeMultiplierCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);

            if (timeMultiplier == 0)
            {
                double result = _signalCalculator.Calculate(time, channelIndex);
                return result;
            }
            else
            {
                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedTime = (time - _originValue) / timeMultiplier + _originValue;
                double result = _signalCalculator.Calculate(transformedTime, channelIndex);
                return result;
            }
        }
    }

    internal class SlowDown_WithOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;
        private OperatorCalculatorBase _originOutletCalculator;

        public SlowDown_WithOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeMultiplierCalculator, OperatorCalculatorBase originOutletCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (timeMultiplierCalculator is Number_OperatorCalculator) throw new Exception("timeMultiplierCalculator cannot be a Value_OperatorCalculator.");
            if (originOutletCalculator == null) throw new NullException(() => originOutletCalculator);
            if (originOutletCalculator is Number_OperatorCalculator) throw new Exception("originOutletCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
            _originOutletCalculator = originOutletCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);
            if (timeMultiplier == 0)
            {
                double result = _signalCalculator.Calculate(time, channelIndex);
                return result;
            }
            else
            {
                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double origin = _originOutletCalculator.Calculate(time, channelIndex);
                double transformedTime = (time - origin) / timeMultiplier + origin;
                double result = _signalCalculator.Calculate(transformedTime, channelIndex);
                return result;
            }
        }
    }

    internal class SlowDown_WithOrigin_WithConstTimeMultiplier_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeMultiplierValue;
        private OperatorCalculatorBase _originCalculator;

        public SlowDown_WithOrigin_WithConstTimeMultiplier_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeMultiplierValue,
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signal cannot be a ValueCalculator.");
            if (timeMultiplierValue == 0) throw new Exception("timeMultiplierValue cannot be 0.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierValue = timeMultiplierValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) / _timeMultiplierValue + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }

    internal class SlowDown_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;

        public SlowDown_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeMultiplierCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a ValueCalculator.");
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (timeMultiplierCalculator is Number_OperatorCalculator) throw new Exception("timeMultiplierCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);
            if (timeMultiplier == 0)
            {
                double result = _signalCalculator.Calculate(time, channelIndex);
                return result;
            }
            else
            {
                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedTime = time / timeMultiplier;
                double result = _signalCalculator.Calculate(transformedTime, channelIndex);
                return result;
            }
        }
    }

    internal class SlowDown_WithoutOrigin_WithConstTimeMultiplier_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeMultiplierValue;

        public SlowDown_WithoutOrigin_WithConstTimeMultiplier_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeMultiplierValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a ValueCalculator.");
            if (timeMultiplierValue == 0) throw new Exception("timeMultiplierValue cannot be 0.");

            _signalCalculator = signalCalculator;
            _timeMultiplierValue = timeMultiplierValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time / _timeMultiplierValue;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}

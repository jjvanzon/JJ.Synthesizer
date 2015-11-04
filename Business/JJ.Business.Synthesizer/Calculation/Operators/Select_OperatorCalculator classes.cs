using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Select_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeCalculator;

        public Select_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (timeCalculator == null) throw new NullException(() => timeCalculator);
            if (timeCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => timeCalculator);

            _signalCalculator = signalCalculator;
            _timeCalculator = timeCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double time2 = _timeCalculator.Calculate(time, channelIndex);
            double result = _signalCalculator.Calculate(time2, channelIndex);
            return result;
        }
    }

    internal class Select_WithConstTime_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _time2;

        public Select_WithConstTime_OperatorCalculator(OperatorCalculatorBase signalCalculator, double time2)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _time2 = time2;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = _signalCalculator.Calculate(_time2, channelIndex);
            return result;
        }
    }
}

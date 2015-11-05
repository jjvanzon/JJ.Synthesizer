//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Delay_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _signalCalculator;
//        private double _timeDifferenceValue;

//        public Delay_WithConstTimeDifference_OperatorCalculator(
//            OperatorCalculatorBase signalCalculator, 
//            double timeDifferenceValue)
//        {
//            if (signalCalculator == null) throw new NullException(() => signalCalculator);
//            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

//            _signalCalculator = signalCalculator;
//            _timeDifferenceValue = timeDifferenceValue;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            // IMPORTANT: To add time to the output, you have substract time from the input.
//            double transformedTime = time - _timeDifferenceValue;
//            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
//            return result;
//        }
//    }
//}

//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Earlier_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
//    {
//        private OperatorCalculatorBase _signalCalculator;
//        private OperatorCalculatorBase _timeDifferenceCalculator;

//        public Earlier_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDifferenceCalculator)
//            : base(new OperatorCalculatorBase[] { signalCalculator, timeDifferenceCalculator })
//        {
//            if (signalCalculator == null) throw new NullException(() => signalCalculator);
//            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
//            if (timeDifferenceCalculator == null) throw new NullException(() => timeDifferenceCalculator);
//            if (timeDifferenceCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => timeDifferenceCalculator);

//            _signalCalculator = signalCalculator;
//            _timeDifferenceCalculator = timeDifferenceCalculator;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double timeDifference = _timeDifferenceCalculator.Calculate(time, channelIndex);
//            // IMPORTANT: To subtract time from the output, you have add time to the input.
//            double transformedTime = time + timeDifference;
//            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
//            return result;
//        }


//        private const int DEFAULT_CHANNEL_INDEX = 0;

//        //public override void ResetPhase(double time)
//        //{
//        //    double timeDifference = _timeDifferenceCalculator.Calculate(time, DEFAULT_CHANNEL_INDEX); // TODO: ResetPhase may need a channelIndex parameter at one point.

//        //    // IMPORTANT: To subtract time from the output, you have add time to the input.
//        //    double transformedTime = time + timeDifference;

//        //    base.ResetPhase(transformedTime);
//        //}
//    }

//    internal class Earlier_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
//    {
//        private OperatorCalculatorBase _signalCalculator;
//        private double _timeDifferenceValue;

//        public Earlier_WithConstTimeDifference_OperatorCalculator(
//            OperatorCalculatorBase signalCalculator,
//            double timeDifferenceValue)
//            : base(new OperatorCalculatorBase[] { signalCalculator })
//        {
//            if (signalCalculator == null) throw new NullException(() => signalCalculator);
//            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

//            _signalCalculator = signalCalculator;
//            _timeDifferenceValue = timeDifferenceValue;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            // IMPORTANT: To subtract time from the output, you have add time to the input.
//            double transformedTime = time + _timeDifferenceValue;
//            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
//            return result;
//        }
//    }
//}

﻿//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class SpeedUp_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _signalCalculator;
//        private OperatorCalculatorBase _timeDividerCalculator;

//        public SpeedUp_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDividerCalculator)
//        {
//            if (signalCalculator == null) throw new NullException(() => signalCalculator);
//            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
//            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
//            if (timeDividerCalculator is Number_OperatorCalculator) throw new Exception("timeDividerCalculator cannot be a Value_OperatorCalculator.");

//            _signalCalculator = signalCalculator;
//            _timeDividerCalculator = timeDividerCalculator;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

//            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
//            double transformedTime = time * timeDivider;
//            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
//            return result;
//        }
//    }
//}

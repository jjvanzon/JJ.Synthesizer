//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Multiply_WithConstOrigin_AndOperandB_OperatorCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _operandACalculator;
//        private double _operandBValue;
//        private double _originValue;

//        public Multiply_WithConstOrigin_AndOperandB_OperatorCalculator(
//            OperatorCalculatorBase operandACalculator, 
//            double operandBValue, 
//            double originValue)
//        {
//            if (operandACalculator == null) throw new NullException(() => operandACalculator);
//            if (operandACalculator is Number_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");

//            _operandACalculator = operandACalculator;
//            _operandBValue = operandBValue;
//            _originValue = originValue;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double a = _operandACalculator.Calculate(time, channelIndex);
//            return (a - _originValue) * _operandBValue + _originValue;
//        }
//    }
//}
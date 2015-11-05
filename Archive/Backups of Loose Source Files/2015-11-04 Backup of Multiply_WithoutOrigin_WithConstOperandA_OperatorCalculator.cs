//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Multiply_WithoutOrigin_WithConstOperandA_OperatorCalculator : OperatorCalculatorBase
//    {
//        private double _operandAValue;
//        private OperatorCalculatorBase _operandBCalculator;

//        public Multiply_WithoutOrigin_WithConstOperandA_OperatorCalculator(double operandValue, OperatorCalculatorBase operandBCalculator)
//        {
//            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
//            if (operandBCalculator is Number_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");

//            _operandAValue = operandValue;
//            _operandBCalculator = operandBCalculator;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double b = _operandBCalculator.Calculate(time, channelIndex);
//            return _operandAValue * b;
//        }
//    }
//}

//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Divide_WithConstOrigin_AndDenominator_OperatorCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _numeratorCalculator;
//        private double _denominatorValue;
//        private double _originValue;

//        public Divide_WithConstOrigin_AndDenominator_OperatorCalculator(
//            OperatorCalculatorBase numeratorCalculator, 
//            double denominatorValue, 
//            double originValue)
//        {
//            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
//            if (numeratorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => numeratorCalculator);
//            if (denominatorValue == 0) throw new ZeroException(() => denominatorValue);

//            _numeratorCalculator = numeratorCalculator;
//            _denominatorValue = denominatorValue;
//            _originValue = originValue;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double a = _numeratorCalculator.Calculate(time, channelIndex);
//            return (a - _originValue) / _denominatorValue + _originValue;
//        }
//    }
//}
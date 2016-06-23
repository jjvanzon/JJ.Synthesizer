//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class MaxContinuous_OperatorCalculator : MinOrMaxContinuous_OperatorCalculatorBase
//    {
//        public MaxContinuous_OperatorCalculator(
//            OperatorCalculatorBase signalCalculator,
//            OperatorCalculatorBase fromCalculator,
//            OperatorCalculatorBase tillCalculator,
//            OperatorCalculatorBase stepCalculator,
//            DimensionStack dimensionStack)
//            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
//        { }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override bool MustOverwrite(double currentValue, double newValue)
//        {
//            return newValue > currentValue;
//        }
//    }
//}

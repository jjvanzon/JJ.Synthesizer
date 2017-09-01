//using System;
//using JJ.Business.Synthesizer.Calculation.Operators;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Calculation
//{
//    /// <summary>
//    /// A stub for making code OperatorCalculators able to work with a Position OperatorCalculator,
//    /// instead of a DimensionStack, without making a second copy of the OperatorCalculator's code.
//    /// </summary>
//    internal class DimensionStack_AroundOperatorCalculator : DimensionStack
//    {
//        private readonly OperatorCalculatorBase _operatorCalculator;

//        public DimensionStack_AroundOperatorCalculator(
//            OperatorCalculatorBase operatorCalculator,
//            DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName) : base(standardDimensionEnum, canonicalCustomDimensionName)
//        {
//            _operatorCalculator = operatorCalculator ?? throw new ArgumentNullException(nameof(operatorCalculator));
//        }

//        public DimensionStack_AroundOperatorCalculator(
//            OperatorCalculatorBase operatorCalculator,
//            DimensionEnum standardDimensionEnum) : base(standardDimensionEnum)
//        {
//            _operatorCalculator = operatorCalculator ?? throw new ArgumentNullException(nameof(operatorCalculator));
//        }

//        public DimensionStack_AroundOperatorCalculator(
//            OperatorCalculatorBase operatorCalculator,
//            string canonicalCustomDimensionName) : base(
//            canonicalCustomDimensionName)
//        {
//            _operatorCalculator = operatorCalculator ?? throw new ArgumentNullException(nameof(operatorCalculator));
//        }

//        public override double Get() => _operatorCalculator.Calculate();
//    }
//}
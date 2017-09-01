//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    [Obsolete("Use GetDimension_OperatorCalculator instead", true)]
//    internal class OperatorCalculator_AroundDimensionStack : OperatorCalculatorBase
//    {
//        private readonly DimensionStack _dimensionStack;

//        public OperatorCalculator_AroundDimensionStack(DimensionStack dimensionStack)
//        {
//            _dimensionStack = dimensionStack ?? throw new ArgumentNullException(nameof(dimensionStack));
//        }

//        public override double Calculate() => _dimensionStack.Get();
//    }
//}

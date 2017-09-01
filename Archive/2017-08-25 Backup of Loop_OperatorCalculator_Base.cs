//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        protected readonly DimensionStack _dimensionStack;

//        protected double _origin;

//        public Loop_OperatorCalculator_Base(
//            OperatorCalculatorBase signalCalculator,
//            DimensionStack dimensionStack,
//            IList<OperatorCalculatorBase> childOperatorCalculators)
//            : base(childOperatorCalculators)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
//            _dimensionStack = dimensionStack;

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected abstract double? GetTransformedPosition();

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double? transformedPosition = GetTransformedPosition();
//            if (!transformedPosition.HasValue)
//            {
//                return 0.0;
//            }

//            _dimensionStack.Push(transformedPosition.Value);

//            double value = _signalCalculator.Calculate();

//            _dimensionStack.Pop();

//            return value;
//        }

//        public override void Reset()
//        {
//            // First reset parent, then children,
//            // because unlike some other operators,
//            // child state is dependent transformed position,
//            // which is dependent on parent state.
//            ResetNonRecursive();

//            // Dimension Transformation
//            double? transformedPosition = GetTransformedPosition();
//            if (!transformedPosition.HasValue)
//            {
//                // TODO: There is no meaningful value to fall back to. What to do? Return instead?
//                //transformedPosition = _origin;
//                return;
//            }

//            _dimensionStack.Push(transformedPosition.Value);

//            base.Reset();

//            _dimensionStack.Pop();
//        }

//        private void ResetNonRecursive()
//        {
//            double position = _dimensionStack.Get();

//            _origin = position;
//        }
//    }
//}
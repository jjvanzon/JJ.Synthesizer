//using JJ.Framework.Reflection.Exceptions;
//using System;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    [Obsolete("Will be replaced by Squash operator.")]
//    internal class SpeedUp_OperatorCalculator_VarSignal_VarFactor_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly OperatorCalculatorBase _factorCalculator;
//        private readonly DimensionStack _dimensionStack;
//        private readonly int _nextDimensionStackIndex;
//        private readonly int _previousDimensionStackIndex;

//        public SpeedUp_OperatorCalculator_VarSignal_VarFactor_NoPhaseTracking(
//            OperatorCalculatorBase signalCalculator,
//            OperatorCalculatorBase factorCalculator,
//            DimensionStack dimensionStack)
//            : base(new OperatorCalculatorBase[] { signalCalculator, factorCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
//            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

//            _signalCalculator = signalCalculator;
//            _factorCalculator = factorCalculator;
//            _dimensionStack = dimensionStack;
//            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
//            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//#if !USE_INVAR_INDICES
//            double position = _dimensionStack.Get();
//#else
//            double position = _dimensionStack.Get(_previousDimensionStackIndex);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
//#endif

//            double transformedPosition = GetTransformedPosition(position);

//#if !USE_INVAR_INDICES
//            _dimensionStack.Push(transformedPosition);
//#else
//            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
//#endif

//            double result = _signalCalculator.Calculate();
//#if !USE_INVAR_INDICES
//            _dimensionStack.Pop();
//#endif
//            return result;
//        }

//        public override void Reset()
//        {
//#if !USE_INVAR_INDICES
//            double position = _dimensionStack.Get();
//#else
//            double position = _dimensionStack.Get(_previousDimensionStackIndex);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
//#endif

//            double transformedPosition = GetTransformedPosition(position);

//#if !USE_INVAR_INDICES
//            _dimensionStack.Push(transformedPosition);
//#else
//            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
//#endif

//            base.Reset();
//#if !USE_INVAR_INDICES
//            _dimensionStack.Pop();
//#endif
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetTransformedPosition(double position)
//        {
//            double factor = _factorCalculator.Calculate();

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = position * factor;

//            return transformedPosition;
//        }
//    }

//    [Obsolete("Will be replaced by Squash operator.")]
//    internal class SpeedUp_OperatorCalculator_VarSignal_ConstFactor_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly double _factor;
//        private readonly DimensionStack _dimensionStack;
//        private readonly int _nextDimensionStackIndex;
//        private readonly int _previousDimensionStackIndex;

//        public SpeedUp_OperatorCalculator_VarSignal_ConstFactor_NoOriginShifting(
//            OperatorCalculatorBase signalCalculator,
//            double factor,
//            DimensionStack dimensionStack)
//            : base(new OperatorCalculatorBase[] { signalCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
//            if (factor == 0) throw new ZeroException(() => factor);
//            if (factor == 1) throw new EqualException(() => factor, 1);
//            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
//            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
//            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

//            _signalCalculator = signalCalculator;
//            _factor = factor;
//            _dimensionStack = dimensionStack;
//            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
//            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double transformedPosition = GetTransformedPosition();

//#if !USE_INVAR_INDICES
//            _dimensionStack.Push(transformedPosition);
//#else
//            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
//#endif

//            double result = _signalCalculator.Calculate();

//#if !USE_INVAR_INDICES
//            _dimensionStack.Pop();
//#endif
//            return result;
//        }

//        public override void Reset()
//        {
//            double transformedPosition = GetTransformedPosition();

//#if !USE_INVAR_INDICES
//            _dimensionStack.Push(transformedPosition);
//#else
//            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
//#endif

//            base.Reset();

//#if !USE_INVAR_INDICES
//            _dimensionStack.Pop();
//#endif
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetTransformedPosition()
//        {
//#if !USE_INVAR_INDICES
//            double position = _dimensionStack.Get();
//#else
//            double position = _dimensionStack.Get(_previousDimensionStackIndex);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
//#endif

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = position * _factor;
//            return transformedPosition;
//        }
//    }
//}

using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Select_OperatorCalculator_VarPosition : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _positionCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;

        public Select_OperatorCalculator_VarPosition(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase positionCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                positionCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(positionCalculator, () => positionCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _positionCalculator = positionCalculator;
            _dimensionStack = dimensionStack;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(position);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, position);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            double result = _signalCalculator.Calculate();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return result;
        }

        public override void Reset()
        {
            double position = _positionCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(position);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, position);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }
    }

    internal class Select_OperatorCalculator_ConstPosition : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _position;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;

        public Select_OperatorCalculator_ConstPosition(
            OperatorCalculatorBase signalCalculator,
            double position,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _position = position;
            _dimensionStack = dimensionStack;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            _dimensionStack.Push(_position);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, _position);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            double result = _signalCalculator.Calculate();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return result;
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            _dimensionStack.Push(_position);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, _position);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }
    }
}

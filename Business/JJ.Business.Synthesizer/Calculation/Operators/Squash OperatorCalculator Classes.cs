using System;
using JJ.Framework.Reflection.Exceptions;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Const-Const-Zero does not exist.

    // Const-Var-Zero does not exist.

    // Var-Const-Zero

    internal class Squash_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Squash_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
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
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = position * _factor;
            return transformedPosition;
        }
    }

    // Var-Var-Zero

    internal class Squash_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Squash_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
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
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = position * factor;

            return transformedPosition;
        }
    }

    // Const-Const-Const does not exist.

    // Const-Const-Var does not exist.

    // Const-Var-Const does not exist.

    // Const-Var-Var does not exist.

    // Var-Const-Const

    internal class Squash_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly double _origin;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Squash_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(
            OperatorCalculatorBase signalCalculator,
            double factor,
            double origin,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            if (origin == 0) throw new ZeroException(() => origin);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _origin = origin;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
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
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - _origin) * _factor + _origin;

            return transformedPosition;
        }
    }

    // Var-Const-Var

    internal class Squash_OperatorCalculator_VarSignal_ConstFactor_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Squash_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(
            OperatorCalculatorBase signalCalculator,
            double factor,
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                originCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(originCalculator, () => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            double value = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return value;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            double origin = _originCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - origin) * _factor + origin;

            return transformedPosition;
        }
    }

    // Var-Var-Const

    internal class Squash_OperatorCalculator_VarSignal_VarFactor_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly double _origin;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Squash_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            double origin,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _origin = origin;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
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
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - _origin) * factor + _origin;

            return transformedPosition;
        }
    }

    // Var-Var-Var

    internal class Squash_OperatorCalculator_VarSignal_VarFactor_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Squash_OperatorCalculator_VarSignal_VarFactor_VarOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator,
                originCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(originCalculator, () => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
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
            double transformedPosition = GetTransformedPosition();

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            double factor = _factorCalculator.Calculate();
            double origin = _originCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - origin) * factor + origin;

            return transformedPosition;
        }
    }
}

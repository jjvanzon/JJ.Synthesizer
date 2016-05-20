using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Const-Const-Zero does not exist.

    // Const-Var-Zero does not exist.

    // Var-Const-Zero

    internal class Stretch_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = position / _factor;

            return transformedPosition;
        }
    }

    // Var-Var-Zero

    internal class Stretch_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = position / factor;

            return transformedPosition;
        }
    }

    // Const-Const-Const does not exist.

    // Const-Const-Var does not exist.

    // Const-Var-Const does not exist.

    // Const-Var-Var does not exist.

    // Var-Const-Const

    internal class Stretch_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly double _origin;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(
            OperatorCalculatorBase signalCalculator,
            double factor,
            double origin,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
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
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = (position - _origin) / _factor + _origin;

            return transformedPosition;
        }
    }

    // Var-Const-Var

    internal class Stretch_OperatorCalculator_VarSignal_ConstFactor_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(
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
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result2 = _signalCalculator.Calculate();

            return result2;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double origin = _originCalculator.Calculate();

            double transformedPosition = (position - origin) / _factor + origin;
            return transformedPosition;
        }
    }

    // Var-Var-Const

    internal class Stretch_OperatorCalculator_VarSignal_VarFactor_ConstOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly double _origin;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(
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
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            if (origin == 0) throw new ZeroException(() => origin);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _origin = origin;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double factor = _factorCalculator.Calculate();
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = (position - _origin) / factor + _origin;

            return transformedPosition;
        }
    }

    // Var-Var-Var

    internal class Stretch_OperatorCalculator_VarSignal_VarFactor_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_OperatorCalculator_VarSignal_VarFactor_VarOrigin(
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
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double factor = _factorCalculator.Calculate();
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
            double origin = _originCalculator.Calculate();

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = (position - origin) / factor + origin;
            return transformedPosition;
        }
    }
}
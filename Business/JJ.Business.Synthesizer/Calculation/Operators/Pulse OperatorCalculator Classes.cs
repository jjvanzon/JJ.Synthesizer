using System;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Pulse_OperatorCalculator_ConstFrequency_ConstWidth_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Pulse_OperatorCalculator_ConstFrequency_ConstWidth_WithOriginShifting(
            double frequency,
            double width,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _width = width;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double phase = (position - _origin) * _frequency;
            double relativePhase = phase % 1.0;
            if (relativePhase < _width)
            {
                return -1.0;
            }
            else
            {
                return 1.0;
            }
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            base.Reset();
        }
    }

    internal class Pulse_OperatorCalculator_ConstFrequency_VarWidth_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Pulse_OperatorCalculator_ConstFrequency_VarWidth_WithOriginShifting(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double width = _widthCalculator.Calculate();

            double phase = (position - _origin) * _frequency;
            double relativePhase = phase % 1.0;
            if (relativePhase < width)
            {
                return -1.0;
            }
            else
            {
                return 1.0;
            }
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            base.Reset();
        }
    }

    internal class Pulse_OperatorCalculator_VarFrequency_ConstWidth_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_OperatorCalculator_VarFrequency_ConstWidth_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value;
            double relativePhase = _phase % 1.0;
            if (relativePhase < _width)
            {
                value = -1.0;
            }
            else
            {
                value = 1.0;
            }

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            ResetNonRecursive();

            base.Reset();
        }

        private void ResetNonRecursive()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            _previousPosition = position;
            _phase = 0.0;
        }
    }

    internal class Pulse_OperatorCalculator_VarFrequency_VarWidth_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_OperatorCalculator_VarFrequency_VarWidth_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double frequency = _frequencyCalculator.Calculate();
            double width = _widthCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value;
            double relativePhase = _phase % 1.0;
            if (relativePhase < width)
            {
                value = -1.0;
            }
            else
            {
                value = 1.0;
            }

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            ResetNonRecursive();

            base.Reset();
        }

        private void ResetNonRecursive()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            _previousPosition = position;
            _phase = 0.0;
        }
    }

    // Copies with Origin Shifting and Phase Tracking removed

    internal class Pulse_OperatorCalculator_ConstFrequency_ConstWidth_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_ConstFrequency_ConstWidth_NoOriginShifting(
            double frequency,
            double width,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _width = width;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double phase = position * _frequency;
            double relativePhase = phase % 1.0;
            if (relativePhase < _width)
            {
                return -1.0;
            }
            else
            {
                return 1.0;
            }
        }
    }

    internal class Pulse_OperatorCalculator_ConstFrequency_VarWidth_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_ConstFrequency_VarWidth_NoOriginShifting(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double width = _widthCalculator.Calculate();

            double phase = position * _frequency;
            double relativePhase = phase % 1.0;
            if (relativePhase < width)
            {
                return -1.0;
            }
            else
            {
                return 1.0;
            }
        }
    }

    internal class Pulse_OperatorCalculator_VarFrequency_ConstWidth_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_VarFrequency_ConstWidth_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double frequency = _frequencyCalculator.Calculate();

            double phase = position * frequency;

            double value;
            double relativePhase = phase % 1.0;
            if (relativePhase < _width)
            {
                value = -1.0;
            }
            else
            {
                value = 1.0;
            }

            return value;
        }
    }

    internal class Pulse_OperatorCalculator_VarFrequency_VarWidth_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_VarFrequency_VarWidth_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double frequency = _frequencyCalculator.Calculate();
            double width = _widthCalculator.Calculate();

            double phase = position * frequency;

            double value;
            double relativePhase = phase % 1.0;

            if (relativePhase < width)
            {
                value = -1.0;
            }
            else
            {
                value = 1.0;
            }

            return value;
        }
    }
}

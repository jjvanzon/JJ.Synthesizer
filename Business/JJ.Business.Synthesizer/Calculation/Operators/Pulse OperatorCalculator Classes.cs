using System;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // TODO: Program variations without phase shift.
    // Phase shift checks are for that reason temporarily commented out.

    internal class Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_WithOriginShifting(
            double frequency,
            double width,
            double phaseShift,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _width = width;
            _phaseShift = phaseShift;
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

            double shiftedPhase = (position - _origin) * _frequency + _phaseShift;
            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_ConstFrequency_VarWidth_ConstPhaseShift_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Pulse_OperatorCalculator_ConstFrequency_VarWidth_ConstPhaseShift_WithOriginShifting(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShift = phaseShift;
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

            double shiftedPhase = (position - _origin) * _frequency + _phaseShift;
            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private int _dimensionStackIndex;

        private double _origin;

        public Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_WithOriginShifting(
            double frequency,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
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

            double phaseShift = _phaseShiftCalculator.Calculate();

            double shiftedPhase = (position - _origin) * _frequency + phaseShift;

            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_ConstFrequency_VarWidth_VarPhaseShift_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Pulse_OperatorCalculator_ConstFrequency_VarWidth_VarPhaseShift_WithOriginShifting(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
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
            double phaseShift = _phaseShiftCalculator.Calculate();

            double shiftedPhase = (position - _origin) * _frequency + phaseShift;

            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _width;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _shiftedPhase;
        private double _previousPosition;

        public Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phaseShift = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            _shiftedPhase = phaseShift;
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
            _shiftedPhase = _shiftedPhase + positionChange * frequency;

            double value;
            double relativePhase = _shiftedPhase % 1.0;
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
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            _previousPosition = position;
            _shiftedPhase = _phaseShift;

            base.Reset();
        }
    }

    internal class Pulse_OperatorCalculator_VarFrequency_VarWidth_ConstPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _shiftedPhase;
        private double _previousPosition;

        public Pulse_OperatorCalculator_VarFrequency_VarWidth_ConstPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _phaseShift = phaseShift;
            _dimensionStack = dimensionStack;

            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _shiftedPhase = phaseShift;
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
            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _shiftedPhase = _shiftedPhase + positionChange * frequency;

            double value;
            double relativePhase = _shiftedPhase % 1.0;
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
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            _previousPosition = position;
            _shiftedPhase = _phaseShift;

            base.Reset();
        }
    }

    internal class Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
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
            double phaseShift = _phaseShiftCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value;
            double shiftedPhase = _phase + phaseShift;
            double relativePhase = shiftedPhase % 1.0;
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

            base.Reset();
        }
    }

    internal class Pulse_OperatorCalculator_VarFrequency_VarWidth_VarPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_OperatorCalculator_VarFrequency_VarWidth_VarPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
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
            double phaseShift = _phaseShiftCalculator.Calculate();
            double width = _widthCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value;
            double shiftedPhase = _phase + phaseShift;
            double relativePhase = shiftedPhase % 1.0;

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

            base.Reset();
        }
    }

    // Copies with Origin Shifting and Phase Tracking removed

    internal class Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_NoOriginShifting(
            double frequency,
            double width,
            double phaseShift,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _width = width;
            _phaseShift = phaseShift;
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

            double shiftedPhase = position * _frequency + _phaseShift;
            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_ConstFrequency_VarWidth_ConstPhaseShift_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_ConstFrequency_VarWidth_ConstPhaseShift_NoOriginShifting(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShift = phaseShift;
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

            double shiftedPhase = position * _frequency + _phaseShift;
            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private int _dimensionStackIndex;

        public Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_NoOriginShifting(
            double frequency,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
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

            double phaseShift = _phaseShiftCalculator.Calculate();

            double shiftedPhase = position * _frequency + phaseShift;

            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_ConstFrequency_VarWidth_VarPhaseShift_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_ConstFrequency_VarWidth_VarPhaseShift_NoOriginShifting(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
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
            double phaseShift = _phaseShiftCalculator.Calculate();

            double shiftedPhase = position * _frequency + phaseShift;

            double relativePhase = shiftedPhase % 1.0;
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

    internal class Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _width;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phaseShift = phaseShift;
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

            double phase = position * frequency + _phaseShift;

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

    internal class Pulse_OperatorCalculator_VarFrequency_VarWidth_ConstPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_VarFrequency_VarWidth_ConstPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _phaseShift = phaseShift;
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
            double frequency = _frequencyCalculator.Calculate();

            double phase = position * frequency + _phaseShift;

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

    internal class Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
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
            double phaseShift = _phaseShiftCalculator.Calculate();

            double phase = position * frequency + phaseShift;

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

    internal class Pulse_OperatorCalculator_VarFrequency_VarWidth_VarPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_OperatorCalculator_VarFrequency_VarWidth_VarPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
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
            double phaseShift = _phaseShiftCalculator.Calculate();
            double width = _widthCalculator.Calculate();

            double phase = position * frequency + phaseShift;

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

using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sine_OperatorCalculator_VarFrequency_PhaseShiftZero_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Sine_OperatorCalculator_VarFrequency_PhaseShiftZero_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
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

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value = SineCalculator.Sin(_phase);

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

    internal class Sine_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _shiftedPhase;
        private double _previousPosition;

        public Sine_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
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
            double result = SineCalculator.Sin(_shiftedPhase);

            _previousPosition = position;

            return result;
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

    internal class Sine_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Sine_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                frequencyCalculator,
                phaseShiftCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
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
            double result = SineCalculator.Sin(_phase + phaseShift);

            _previousPosition = position;

            return result;
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

    internal class Sine_OperatorCalculator_ConstFrequency_PhaseShiftZero_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Sine_OperatorCalculator_ConstFrequency_PhaseShiftZero_WithOriginShifting(
            double frequency,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
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
            double value = SineCalculator.Sin(phase);

            return value;
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

    internal class Sine_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Sine_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting(
            double frequency,
            double phaseShift,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
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

            double phase = (position - _origin) * _frequency + _phaseShift;
            double result = SineCalculator.Sin(phase);

            return result;
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

    internal class Sine_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Sine_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
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

            double phase = (position - _origin) * _frequency + phaseShift;
            double result = SineCalculator.Sin(phase);

            return result;
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

    // No Phase Tracking or Origin Shifting

    internal class Sine_OperatorCalculator_VarFrequency_PhaseShiftZero_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sine_OperatorCalculator_VarFrequency_PhaseShiftZero_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
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
            double value = SineCalculator.Sin(phase);

            return value;
        }
    }

    internal class Sine_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sine_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
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
            double result = SineCalculator.Sin(phase);

            return result;
        }
    }

    internal class Sine_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sine_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                frequencyCalculator,
                phaseShiftCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
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
            double result = SineCalculator.Sin(phase);

            return result;
        }
    }

    internal class Sine_OperatorCalculator_ConstFrequency_PhaseShiftZero_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sine_OperatorCalculator_ConstFrequency_PhaseShiftZero_NoOriginShifting(
            double frequency,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
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

            double value = SineCalculator.Sin(position * _frequency);

            return value;
        }
    }

    internal class Sine_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sine_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting(
            double frequency,
            double phaseShift,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
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

            double phase = position * _frequency + _phaseShift;
            double result = SineCalculator.Sin(phase);

            return result;
        }
    }

    internal class Sine_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sine_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
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

            double result = SineCalculator.Sin(position * _frequency + phaseShift);

            return result;
        }
    }
}
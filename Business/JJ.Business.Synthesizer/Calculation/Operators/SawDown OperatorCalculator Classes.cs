using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // TODO: Program variations without phase shift.
    // Phase shift checks are for that reason temporarily commented out.

    internal class SawDown_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public SawDown_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting(
            double frequency,
            double phaseShift,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _phaseShift = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double shiftedPhase = (position - _origin) * _frequency + _phaseShift;
            double value = 1.0 - (2.0 * shiftedPhase % 2.0);
            return value;
        }

        public override void Reset()
        {
            _origin = _dimensionStack.Get(_dimensionStackIndex);

            base.Reset();
        }
    }

    internal class SawDown_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public SawDown_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double phaseShift = _phaseShiftCalculator.Calculate();

            double phase = (position - _origin) * _frequency + phaseShift;
            double value = 1 - (2 * phase % 2);

            return value;
        }

        public override void Reset()
        {
            _origin = _dimensionStack.Get(_dimensionStackIndex);

            base.Reset();
        }
    }

    internal class SawDown_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SawDown_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting(
            double frequency, 
            double phaseShift,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _phaseShift = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double phase = position * _frequency + _phaseShift;
            double value = 1.0 - (2.0 * phase % 2.0);
            return value;
        }
    }

    internal class SawDown_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SawDown_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double phaseShift = _phaseShiftCalculator.Calculate();

            double phase = position * _frequency + phaseShift;
            double value = 1 - (2 * phase % 2);

            return value;
        }
    }

    internal class SawDown_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _shiftedPhase;
        private double _previousPosition;

        public SawDown_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            _phaseShift = phaseShift;
            _shiftedPhase = phaseShift;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _shiftedPhase = _shiftedPhase + positionChange * frequency;

            double value = 1 - (2 * _shiftedPhase % 2);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            _previousPosition = position;
            _shiftedPhase = _phaseShift;

            base.Reset();
        }
    }

    internal class SawDown_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public SawDown_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double frequency = _frequencyCalculator.Calculate();
            double phaseShift = _phaseShiftCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;
            double shiftedPhase = _phase + phaseShift;
            double value = 1 - (2 * shiftedPhase % 2);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class SawDown_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SawDown_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            //OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShift = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double frequency = _frequencyCalculator.Calculate();

            double phase = position * frequency + _phaseShift;
            double value = 1 - (2 * phase % 2);

            return value;
        }
    }

    internal class SawDown_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SawDown_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double frequency = _frequencyCalculator.Calculate();
            double phaseShift = _phaseShiftCalculator.Calculate();

            double phase = position * frequency + phaseShift;
            double value = 1 - (2 * phase % 2);

            return value;
        }
    }
}

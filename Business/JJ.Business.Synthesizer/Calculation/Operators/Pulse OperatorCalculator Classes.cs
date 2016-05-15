using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(
            double frequency,
            double width,
            double phaseShift,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _width = width;
            _phaseShift = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double shiftedPhase = position * _frequency + _phaseShift;
            double relativePhase = shiftedPhase % 1.0;
            if (relativePhase < _width)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    internal class Pulse_ConstFrequency_VarWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly double _phaseShift;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_ConstFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShift = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double width = _widthCalculator.Calculate();

            double shiftedPhase = position * _frequency + _phaseShift;
            double relativePhase = shiftedPhase % 1.0;
            if (relativePhase < width)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    internal class Pulse_ConstFrequency_ConstWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private int _dimensionStackIndex;

        public Pulse_ConstFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(
            double frequency,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double phaseShift = _phaseShiftCalculator.Calculate();

            double shiftedPhase = position * _frequency + phaseShift;

            double relativePhase = shiftedPhase % 1;
            if (relativePhase < _width)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    internal class Pulse_ConstFrequency_VarWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Pulse_ConstFrequency_VarWidth_VarPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double width = _widthCalculator.Calculate();
            double phaseShift = _phaseShiftCalculator.Calculate();

            double shiftedPhase = position * _frequency + phaseShift;

            double relativePhase = shiftedPhase % 1;
            if (relativePhase < width)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    internal class Pulse_VarFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_VarFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phase = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value;
            double relativePhase = _phase % 1;
            if (relativePhase < _width)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

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

    internal class Pulse_VarFrequency_VarWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;
        private double _phase;
        private double _previousPosition;

        public Pulse_VarFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _phase = phaseShift;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double width = _widthCalculator.Calculate();
            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value;
            double relativePhase = _phase % 1;
            if (relativePhase < width)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

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

    internal class Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly double _width;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
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

            double value;
            double shiftedPhase = _phase + phaseShift;
            double relativePhase = shiftedPhase % 1;
            if (relativePhase < _width)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

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

    internal class Pulse_VarFrequency_VarWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_VarFrequency_VarWidth_VarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _widthCalculator = widthCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double frequency = _frequencyCalculator.Calculate();
            double phaseShift = _phaseShiftCalculator.Calculate();
            double width = _widthCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value;
            double shiftedPhase = _phase + phaseShift;
            double relativePhase = shiftedPhase % 1;

            if (relativePhase < width)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

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
}

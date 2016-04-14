using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly double _phaseShift;
        private readonly int _dimensionIndex;

        public Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(
            double frequency,
            double width,
            double phaseShift,
            DimensionEnum dimensionEnum)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequency = frequency;
            _width = width;
            _phaseShift = phaseShift;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

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
        private readonly int _dimensionIndex;

        public Pulse_ConstFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { widthCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShift = phaseShift;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double width = _widthCalculator.Calculate(dimensionStack);

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
        private readonly int _dimensionIndex;

        public Pulse_ConstFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(
            double frequency,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequency = frequency;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);

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
        private readonly int _dimensionIndex;

        public Pulse_ConstFrequency_VarWidth_VarPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double width = _widthCalculator.Calculate(dimensionStack);
            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);

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
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;
        
        public Pulse_VarFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            double phaseShift,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phase = phaseShift;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);

            double dt = position - _previousPosition;
            double phase = _phase + dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

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

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Pulse_VarFrequency_VarWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly DimensionEnum _dimensionIndex;
        private double _phase;
        private double _previousPosition;

        public Pulse_VarFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            double phaseShift,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _phase = phaseShift;
            _dimensionIndex = dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double width = _widthCalculator.Calculate(dimensionStack);
            double frequency = _frequencyCalculator.Calculate(dimensionStack);

            double dt = position - _previousPosition;
            double phase = _phase + dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

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

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly double _width;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);
            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);

            double dt = position - _previousPosition;
            double phase = _phase + dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

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

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Pulse_VarFrequency_VarWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public Pulse_VarFrequency_VarWidth_VarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _widthCalculator = widthCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);
            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);
            double width = _widthCalculator.Calculate(dimensionStack);

            double dt = position - _previousPosition;
            double phase = _phase + dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

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

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }
}

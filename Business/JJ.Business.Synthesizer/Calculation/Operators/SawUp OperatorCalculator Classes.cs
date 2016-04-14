using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawUp_WithConstFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _phaseShift;
        private readonly int _dimensionIndex;

        public SawUp_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(
            double frequency, 
            double phaseShift,
            DimensionEnum dimensionEnum)
        {
            if (frequency == 0) throw new ZeroException(() => frequency);

            _frequency = frequency;
            _phaseShift = phaseShift;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double shiftedPhase = position * _frequency + _phaseShift;
            double value = -1.0 + (2.0 * shiftedPhase % 2.0);
            return value;
        }
    }

    internal class SawUp_WithConstFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly int _dimensionIndex;

        public SawUp_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            if (frequency == 0) throw new ZeroException(() => frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequency = frequency;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);

            double phase = position * _frequency;

            double shiftedPhase = phase + phaseShift;
            double value = -1 + (2 * shiftedPhase % 2);

            return value;
        }
    }

    internal class SawUp_WithVarFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public SawUp_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);

            _frequencyCalculator = frequencyCalculator;
            _dimensionIndex = (int)dimensionEnum;

            // TODO: Assert so it does not become NaN.
            _phase = phaseShift;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double value = -1 + (2 * _phase % 2);

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

    internal class SawUp_WithVarFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public SawUp_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);
            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double shiftedPhase = _phase + phaseShift;
            double value = -1 + (2 * shiftedPhase % 2);

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

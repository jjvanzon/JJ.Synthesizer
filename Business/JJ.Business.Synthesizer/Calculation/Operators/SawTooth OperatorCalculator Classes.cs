using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawTooth_WithConstFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private double _phase;
        private double _previousTime;

        public SawTooth_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(double frequency, double phaseShift)
        {
            if (frequency == 0) throw new ZeroException(() => frequency);

            _frequency = frequency;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _frequency;

            double value = -1 + (2 * _phase % 2);

            _previousTime = time;

            return value;
        }
    }

    internal class SawTooth_WithConstFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private double _phase;
        private double _previousTime;

        public SawTooth_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator)
        {
            if (frequency == 0) throw new ZeroException(() => frequency);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

            _frequency = frequency;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * _frequency;

            double shiftedPhase = _phase + phaseShift;
            double value = -1 + (2 * shiftedPhase % 2);

            _previousTime = time;

            return value;
        }
    }

    internal class SawTooth_WithVarFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private double _phase;
        private double _previousTime;

        public SawTooth_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (frequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => frequencyCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

            double value = -1 + (2 * _phase % 2);

            _previousTime = time;

            return value;
        }
    }

    internal class SawTooth_WithVarFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _frequencyCalculator;
        private OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public SawTooth_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (frequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => frequencyCalculator);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

            double shiftedPhase = _phase + phaseShift;
            double value = -1 + (2 * shiftedPhase % 2);

            _previousTime = time;

            return value;
        }
    }
}

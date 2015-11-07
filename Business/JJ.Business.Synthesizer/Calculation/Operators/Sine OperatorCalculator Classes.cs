using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sine_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _frequencyCalculator;

        private double _phase;
        private double _previousTime;

        public Sine_WithoutPhaseShift_OperatorCalculator(OperatorCalculatorBase frequencyCalculator)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);

            _frequencyCalculator = frequencyCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + Maths.TWO_PI * dt * frequency;

            double value = Math.Sin(_phase);

            _previousTime = time;

            return value;
        }
    }

    internal class Sine_WithPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _frequencyCalculator;
        private OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public Sine_WithPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + Maths.TWO_PI * dt * frequency;

            double result = Math.Sin(_phase + Maths.TWO_PI * phaseShift);

            _previousTime = time;

            return result;
        }
    }
}

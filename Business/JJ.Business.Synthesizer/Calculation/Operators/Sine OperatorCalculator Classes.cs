using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sine_WithVarFrequency_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;

        private double _phase;
        private double _previousTime;

        public Sine_WithVarFrequency_WithoutPhaseShift_OperatorCalculator(OperatorCalculatorBase frequencyCalculator)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);

            _frequencyCalculator = frequencyCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            double phase = _phase + Maths.TWO_PI * dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double value = SineCalculator.Sin(_phase);

            _previousTime = time;

            return value;
        }

        public override void ResetState()
        {
            _phase = 0.0;
            _previousTime = 0.0;

            base.ResetState();
        }
    }

    internal class Sine_WithVarFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _phaseShiftTimesTwoPi;

        protected double _phase;
        protected double _previousTime;

        public Sine_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (phaseShift % 1.0 == 0.0) throw new Exception("phaseShift cannot be a multiple of 1.");

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftTimesTwoPi = phaseShift * Maths.TWO_PI;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            double phase = _phase + Maths.TWO_PI * dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double result = SineCalculator.Sin(_phase + _phaseShiftTimesTwoPi);

            _previousTime = time;

            return result;
        }

        public override void ResetState()
        {
            _phase = 0.0;
            _previousTime = 0.0;

            base.ResetState();
        }
    }

    internal class Sine_WithVarFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        protected double _phase;
        protected double _previousTime;

        public Sine_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
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
            double phase = _phase + Maths.TWO_PI * dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double result = SineCalculator.Sin(_phase + Maths.TWO_PI * phaseShift);

            _previousTime = time;

            return result;
        }

        public override void ResetState()
        {
            _phase = 0.0;
            _previousTime = 0.0;

            base.ResetState();
        }
    }

    internal class Sine_WithConstFrequency_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequencyTimesTwoPi;

        public Sine_WithConstFrequency_WithoutPhaseShift_OperatorCalculator(double frequency)
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            _frequencyTimesTwoPi = frequency * Maths.TWO_PI;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double value = SineCalculator.Sin(time * _frequencyTimesTwoPi);
            return value;
        }
    }

    internal class Sine_WithConstFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequencyTimesTwoPi;
        private readonly double _phaseShiftTimeTwoPi;

        public Sine_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(double frequency, double phaseShift)
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            if (phaseShift % 1.0 == 0.0) throw new Exception("phaseShift cannot be a multiple of 1.");

            _frequencyTimesTwoPi = frequency * Maths.TWO_PI;
            _phaseShiftTimeTwoPi = phaseShift * Maths.TWO_PI;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = SineCalculator.Sin(time * _frequencyTimesTwoPi + _phaseShiftTimeTwoPi);
            return result;
        }
    }

    internal class Sine_WithConstFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequencyTimesTwoPi;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        public Sine_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(double frequency, OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);

            _frequencyTimesTwoPi = frequency * Maths.TWO_PI;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // TODO: Not tested.
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);
            double result = SineCalculator.Sin(time * _frequencyTimesTwoPi + Maths.TWO_PI * phaseShift);
            return result;
        }
    }
}
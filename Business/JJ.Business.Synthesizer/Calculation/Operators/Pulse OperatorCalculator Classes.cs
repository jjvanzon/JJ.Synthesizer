using System;
using System.Linq.Expressions;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _width;
        private readonly double _phaseShift;

        public Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(
            double frequency,
            double width,
            double phaseShift)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequency = frequency;
            _width = width;
            _phaseShift = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double shiftedPhase = time * _frequency + _phaseShift;
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

        public Pulse_ConstFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            double phaseShift)
            : base(new OperatorCalculatorBase[] { widthCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShift = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double width = _widthCalculator.Calculate(time, channelIndex);

            double shiftedPhase = time * _frequency + _phaseShift;
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

        public Pulse_ConstFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(
            double frequency,
            double width,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequency = frequency;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double shiftedPhase = time * _frequency + phaseShift;

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

        public Pulse_ConstFrequency_VarWidth_VarPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequency = frequency;
            _widthCalculator = widthCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double width = _widthCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double shiftedPhase = time * _frequency + phaseShift;

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
        private double _phase;
        private double _previousTime;

        public Pulse_VarFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            double phaseShift)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
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

    internal class Pulse_VarFrequency_VarWidth_ConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private double _phase;
        private double _previousTime;

        public Pulse_VarFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            double phaseShift)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertPhaseShift(phaseShift);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double width = _widthCalculator.Calculate(time, channelIndex);
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
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

    internal class Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly double _width;

        private double _phase;
        private double _previousTime;

        public Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double width,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertWidth(width);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
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

    internal class Pulse_VarFrequency_VarWidth_VarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _widthCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public Pulse_VarFrequency_VarWidth_VarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase widthCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, widthCalculator, phaseShiftCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _widthCalculator = widthCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);
            double width = _widthCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
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
}

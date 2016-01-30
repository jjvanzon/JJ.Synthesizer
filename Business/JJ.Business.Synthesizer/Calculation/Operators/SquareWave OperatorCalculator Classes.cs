using System;
using System.Linq.Expressions;
using JJ.Framework.Reflection.Exceptions;

// TODO: Rename file from "SquareWave OperatorCalculator Classes.cs" to
// "Pulse OperatorCalculator Classes.cs"

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
            Pulse_OperatorCalculator_Helper.AssertFrequency(frequency);
            Pulse_OperatorCalculator_Helper.AssertWidth(width);
            Pulse_OperatorCalculator_Helper.AssertPhaseShift(phaseShift);

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
            Pulse_OperatorCalculator_Helper.AssertFrequency(frequency);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            Pulse_OperatorCalculator_Helper.AssertPhaseShift(phaseShift);

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
            Pulse_OperatorCalculator_Helper.AssertFrequency(frequency);
            Pulse_OperatorCalculator_Helper.AssertWidth(width);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

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
            Pulse_OperatorCalculator_Helper.AssertFrequency(frequency);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

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
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            Pulse_OperatorCalculator_Helper.AssertWidth(width);
            Pulse_OperatorCalculator_Helper.AssertPhaseShift(phaseShift);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

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
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            Pulse_OperatorCalculator_Helper.AssertPhaseShift(phaseShift);

            _frequencyCalculator = frequencyCalculator;
            _widthCalculator = widthCalculator;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double width = _widthCalculator.Calculate(time, channelIndex);
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

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
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            Pulse_OperatorCalculator_Helper.AssertWidth(width);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _width = width;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

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
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(widthCalculator, () => widthCalculator);
            Pulse_OperatorCalculator_Helper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

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
            _phase = _phase + dt * frequency;

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

    internal static class Pulse_OperatorCalculator_Helper
    {
        public static void AssertFrequency(double frequency)
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            if (Double.IsNaN(frequency)) throw new NaNException(() => frequency);
            if (Double.IsInfinity(frequency)) throw new InfinityException(() => frequency);
        }

        public static void AssertWidth(double width)
        {
            if (width == 0.0) throw new ZeroException(() => width);
            if (width >= 1.0) throw new GreaterThanOrEqualException(() => width, 1.0);
            if (Double.IsNaN(width)) throw new NaNException(() => width);
            if (Double.IsInfinity(width)) throw new InfinityException(() => width);
        }

        public static void AssertPhaseShift(double phaseShift)
        {
            if (phaseShift >= 1.0) throw new GreaterThanOrEqualException(() => phaseShift, 1.0);
            if (Double.IsNaN(phaseShift)) throw new NaNException(() => phaseShift);
            if (Double.IsInfinity(phaseShift)) throw new InfinityException(() => phaseShift);
        }

        public static void AssertOperatorCalculatorBase(
            OperatorCalculatorBase operatorCalculatorBase,
            Expression<Func<object>> expression)
        {
            if (operatorCalculatorBase == null) throw new NullException(expression);
            if (operatorCalculatorBase is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(expression);
        }
    }
}

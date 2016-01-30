using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
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
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            //if (frequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => frequencyCalculator);
            if (widthCalculator == null) throw new NullException(() => widthCalculator);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            //if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

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
}

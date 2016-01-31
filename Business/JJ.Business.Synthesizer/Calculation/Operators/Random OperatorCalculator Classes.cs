using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Random_VarFrequency_VarPhaseShift_BlockInterpolation_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly WhiteNoiseCalculator _whiteNoiseCalculator;
        private readonly double _whiteNoiseCalculatorOffset;
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public Random_VarFrequency_VarPhaseShift_BlockInterpolation_OperatorCalculator(
            WhiteNoiseCalculator whiteNoiseCalculator,
            double whiteNoiseCalculatorOffset,
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            if (whiteNoiseCalculator == null) throw new NullException(() => whiteNoiseCalculator);
            // TODO: Make assertion strict again, once you have more calculator variations.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _whiteNoiseCalculator = whiteNoiseCalculator;
            _whiteNoiseCalculatorOffset = whiteNoiseCalculatorOffset;
            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;

            _phase = _whiteNoiseCalculatorOffset;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            // Hack frequency, so WhiteNoiseCalculator will do what we want.
            frequency = frequency / 44100.0;

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

            double shiftedPhase = _phase + phaseShift;

            double value = _whiteNoiseCalculator.GetValue(_phase);

            // Hack value, so we get wat we want.
            value = (value + 1.0) / 2.0;

            _previousTime = time;

            return value;
        }

        public override void ResetState()
        {
            base.ResetState();

            _phase = _whiteNoiseCalculatorOffset;
            _previousTime = 0;
        }
    }
}

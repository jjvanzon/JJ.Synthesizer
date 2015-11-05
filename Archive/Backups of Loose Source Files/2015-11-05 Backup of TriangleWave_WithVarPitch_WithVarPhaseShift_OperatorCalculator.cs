//using JJ.Framework.Reflection.Exceptions;
//using System;
//using JJ.Framework.Mathematics;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class TriangleWave_WithVarPitch_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _pitchCalculator;
//        private readonly OperatorCalculatorBase _phaseShiftCalculator;
//        private double _phase;
//        private double _previousTime;

//        public TriangleWave_WithVarPitch_WithVarPhaseShift_OperatorCalculator(
//            OperatorCalculatorBase pitchCalculator,
//            OperatorCalculatorBase phaseShiftCalculator)
//        {
//            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
//            if (pitchCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => pitchCalculator);
//            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
//            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

//            _pitchCalculator = pitchCalculator;
//            _phaseShiftCalculator = phaseShiftCalculator;

//            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
//            _phase += 0.25;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double pitch = _pitchCalculator.Calculate(time, channelIndex);
//            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

//            double dt = time - _previousTime;
//            _phase = _phase + dt * pitch;

//            double shiftedPhase = _phase + phaseShift;
//            double relativePhase = shiftedPhase % 1.0;
//            double value;
//            if (relativePhase < 0.5)
//            {
//                // Starts going up at a rate of 2 up over 1/2 a cycle.
//                value = -1.0 + 4.0 * relativePhase;
//            }
//            else
//            {
//                // And then going down at phase 1/2.
//                // (Extending the line to x = 0 it ends up at y = 3.)
//                value = 3.0 - 4.0 * relativePhase;
//            }

//            _previousTime = time;

//            return value;
//        }
//    }
//}


//using JJ.Framework.Reflection.Exceptions;
//using System;
//using JJ.Framework.Mathematics;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class SawTooth_WithConstPitch_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
//    {
//        private readonly double _pitch;
//        private readonly OperatorCalculatorBase _phaseShiftCalculator;
//        private double _phase;
//        private double _previousTime;

//        public SawTooth_WithConstPitch_WithVarPhaseShift_OperatorCalculator(
//            double pitch,
//            OperatorCalculatorBase phaseShiftCalculator)
//        {
//            if (pitch == 0) throw new ZeroException(() => pitch);
//            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
//            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

//            _pitch = pitch;
//            _phaseShiftCalculator = phaseShiftCalculator;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

//            double dt = time - _previousTime;
//            _phase = _phase + dt * _pitch;

//            double shiftedPhase = _phase + phaseShift;
//            double value = -1 + (2 * shiftedPhase % 2);

//            _previousTime = time;

//            return value;
//        }
//    }
//}

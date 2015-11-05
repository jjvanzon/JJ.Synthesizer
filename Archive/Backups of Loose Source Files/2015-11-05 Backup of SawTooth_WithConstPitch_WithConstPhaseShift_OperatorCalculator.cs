//using JJ.Framework.Reflection.Exceptions;
//using System;
//using JJ.Framework.Mathematics;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class SawTooth_WithConstPitch_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
//    {
//        private readonly double _pitch;
//        private double _phase;
//        private double _previousTime;

//        public SawTooth_WithConstPitch_WithConstPhaseShift_OperatorCalculator(double pitch, double phaseShift)
//        {
//            if (pitch == 0) throw new ZeroException(() => pitch);

//            _pitch = pitch;
//            _phase = phaseShift;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double dt = time - _previousTime;
//            _phase = _phase + dt * _pitch;

//            double value = -1 + (2 * _phase % 2);

//            _previousTime = time;

//            return value;
//        }
//    }
//}

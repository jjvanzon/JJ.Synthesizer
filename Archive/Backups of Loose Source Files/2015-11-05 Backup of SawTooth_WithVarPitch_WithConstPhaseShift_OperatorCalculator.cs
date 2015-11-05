//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class SawTooth_WithVarPitch_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _pitchCalculator;
//        private double _phase;
//        private double _previousTime;

//        public SawTooth_WithVarPitch_WithConstPhaseShift_OperatorCalculator(
//            OperatorCalculatorBase pitchCalculator,
//            double phaseShift)
//        {
//            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
//            if (pitchCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => pitchCalculator);

//            _pitchCalculator = pitchCalculator;
//            _phase = phaseShift;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double pitch = _pitchCalculator.Calculate(time, channelIndex);

//            double dt = time - _previousTime;
//            _phase = _phase + dt * pitch;

//            double value = -1 + (2 * _phase % 2);

//            _previousTime = time;

//            return value;
//        }
//    }
//}

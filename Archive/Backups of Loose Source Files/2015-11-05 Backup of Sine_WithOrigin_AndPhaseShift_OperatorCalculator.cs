//using JJ.Framework.Reflection.Exceptions;
//using System;
//using JJ.Framework.Mathematics;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Sine_WithOrigin_AndPhaseShift_OperatorCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _volumeCalculator;
//        private OperatorCalculatorBase _pitchCalculator;
//        private OperatorCalculatorBase _originCalculator;
//        private OperatorCalculatorBase _phaseShiftCalculator;

//        private double _phase;
//        private double _previousTime;

//        public Sine_WithOrigin_AndPhaseShift_OperatorCalculator(
//            OperatorCalculatorBase volumeCalculator, 
//            OperatorCalculatorBase pitchCalculator, 
//            OperatorCalculatorBase originCalculator, 
//            OperatorCalculatorBase phaseShiftCalculator)
//        {
//            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
//            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
//            if (originCalculator == null) throw new NullException(() => originCalculator);
//            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);

//            _volumeCalculator = volumeCalculator;
//            _pitchCalculator = pitchCalculator;
//            _originCalculator = originCalculator;
//            _phaseShiftCalculator = phaseShiftCalculator;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double volume = _volumeCalculator.Calculate(time, channelIndex);
//            double pitch = _pitchCalculator.Calculate(time, channelIndex);
//            double origin = _originCalculator.Calculate(time, channelIndex);
//            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

//            double dt = time - _previousTime;
//            _phase = _phase + Maths.TWO_PI * dt * pitch;

//            double result = origin + volume * Math.Sin(_phase + Maths.TWO_PI * phaseShift);

//            _previousTime = time;

//            return result;
//        }
//    }
//}

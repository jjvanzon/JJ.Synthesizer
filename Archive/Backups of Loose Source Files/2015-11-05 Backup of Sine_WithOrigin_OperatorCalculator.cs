﻿//using JJ.Framework.Reflection.Exceptions;
//using System;
//using JJ.Framework.Mathematics;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Sine_WithOrigin_OperatorCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _volumeCalculator;
//        private OperatorCalculatorBase _pitchCalculator;
//        private OperatorCalculatorBase _originCalculator;

//        private double _phase;
//        private double _previousTime;

//        public Sine_WithOrigin_OperatorCalculator(
//            OperatorCalculatorBase volumeCalculator, 
//            OperatorCalculatorBase pitchCalculator, 
//            OperatorCalculatorBase originCalculator)
//        {
//            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
//            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
//            if (originCalculator == null) throw new NullException(() => originCalculator);

//            _volumeCalculator = volumeCalculator;
//            _pitchCalculator = pitchCalculator;
//            _originCalculator = originCalculator;
//        }
            
//        public override double Calculate(double time, int channelIndex)
//        {
//            double volume = _volumeCalculator.Calculate(time, channelIndex);
//            double pitch = _pitchCalculator.Calculate(time, channelIndex); 
//            double origin = _originCalculator.Calculate(time, channelIndex);

//            double dt = time - _previousTime;
//            _phase = _phase + Maths.TWO_PI * dt * pitch;

//            double value = origin + volume * Math.Sin(_phase);

//            _previousTime = time;

//            return value;
//        }
//    }
//}

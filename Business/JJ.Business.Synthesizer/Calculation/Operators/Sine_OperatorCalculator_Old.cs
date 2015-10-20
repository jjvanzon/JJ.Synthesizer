//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Sine_OperatorCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _volumeCalculator;
//        private OperatorCalculatorBase _pitchCalculator;

//        public Sine_OperatorCalculator(OperatorCalculatorBase volumeCalculator, OperatorCalculatorBase pitchCalculator)
//        {
//            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
//            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);

//            _volumeCalculator = volumeCalculator;
//            _pitchCalculator = pitchCalculator;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double volume = _volumeCalculator.Calculate(time, channelIndex);
//            double pitch = _pitchCalculator.Calculate(time, channelIndex);
//            return volume * Math.Sin(2 * Math.PI * pitch * time);
//        }
//    }
//}

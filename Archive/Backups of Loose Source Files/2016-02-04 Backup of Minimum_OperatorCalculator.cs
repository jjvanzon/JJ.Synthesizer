//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Minimum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly double _sampleDuration;
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly int _sampleCount;

//        private double _previousTime;
//        private double _passedSampleTime;
//        private int _sampleCounter;
//        private double _tempMinimum;
//        private double _minimum;

//        public Minimum_OperatorCalculator(
//            OperatorCalculatorBase signalCalculator,
//            double timeSliceDuration,
//            int sampleCount)
//            : base(new OperatorCalculatorBase[] { signalCalculator })
//        {
//            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
//            if (timeSliceDuration <= 0.0) throw new LessThanException(() => timeSliceDuration, 0.0);
//            if (sampleCount <= 0) throw new LessThanOrEqualException(() => sampleCount, 0);

//            _signalCalculator = signalCalculator;
//            _sampleDuration = timeSliceDuration / sampleCount;
//            _sampleCount = sampleCount;

//            ResetState();
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            // Update _passedSampleTime
//            double dt = time - _previousTime;
//            if (dt >= 0)
//            {
//                _passedSampleTime += dt;
//            }
//            else
//            {
//                // Substitute for Math.Abs().
//                // This makes it work for time that goes in reverse 
//                // and even time that quickly goes back and forth.
//                _passedSampleTime -= dt;
//            }

//            if (_passedSampleTime >= _sampleDuration)
//            {
//                _sampleCounter++;

//                double sample = _signalCalculator.Calculate(time, channelIndex);

//                if (_tempMinimum > sample)
//                {
//                    _tempMinimum = sample;
//                }

//                _passedSampleTime = 0.0;
//            }

//            if (_sampleCounter == _sampleCount)
//            {
//                _minimum = _tempMinimum;

//                _sampleCounter = 0;
//                _tempMinimum = CalculationHelper.VERY_HIGH_VALUE;
//            }

//            _previousTime = time;

//            return _minimum;
//        }

//        public override void ResetState()
//        {
//            _previousTime = 0.0;
//            _passedSampleTime = 0.0;
//            _sampleCounter = 0;
//            _tempMinimum = CalculationHelper.VERY_HIGH_VALUE;
//            _minimum = 0.0;

//            base.ResetState();
//        }
//    }
//}

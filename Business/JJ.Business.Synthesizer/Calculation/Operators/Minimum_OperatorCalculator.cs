using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Minimum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _sampleDuration;
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _timeSliceDuration;
        private readonly double _sampleCount;

        private int _index;
        private double[] _samples;

        private double _previousTime;
        private double _passedSampleTime;
        private double _minimum;

        public Minimum_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeSliceDuration,
            double sampleDuration)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (timeSliceDuration <= 0.0) throw new LessThanException(() => timeSliceDuration, 0.0);
            if (sampleDuration <= 0.0) throw new LessThanException(() => sampleDuration, 0.0);
            if (sampleDuration > timeSliceDuration) throw new GreaterThanException(() => sampleDuration, () => timeSliceDuration);

            _signalCalculator = signalCalculator;
            _timeSliceDuration = timeSliceDuration;
            _sampleDuration = sampleDuration;

            _sampleCount = _timeSliceDuration / _sampleDuration;

            // Check for overflow
            if (_sampleCount > Int32.MaxValue) throw new GreaterThanException(() => _sampleCount, Int32.MaxValue);

            // Make sample count a whole number
            _sampleCount = Math.Floor(_sampleCount);

            // Correct sample duration to make _sampleCount fit exactly in timeSliceDuration.
            _sampleDuration = _timeSliceDuration / _sampleCount;

            _samples = CreateArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Update _passedSampleTime
            double dt = time - _previousTime;
            if (dt >= 0)
            {
                _passedSampleTime += dt;
            }
            else
            {
                // Substitute for Math.Abs().
                // This makes it work for time that goes in reverse 
                // and even time that quickly goes back and forth.
                _passedSampleTime -= dt;
            }

            if (_passedSampleTime >= _sampleDuration)
            {
                double sample = _signalCalculator.Calculate(time, channelIndex);

                _samples[_index] = sample;

                _index++;

                _passedSampleTime = 0.0;
            }

            if (_index == _samples.Length)
            {
                _minimum = _samples[0];
                for (int i = 1; i < _samples.Length; i++)
                {
                    double sample = _samples[i];
                    if (_minimum > sample)
                    {
                        _minimum = sample;
                    }
                }
                _index = 0;
            }

            _previousTime = time;

            return _minimum;
        }

        public override void ResetState()
        {
            _index = 0;
            _previousTime = 0.0;
            _passedSampleTime = 0.0;
            _minimum = 0.0;

            base.ResetState();
        }

        private double[] CreateArray()
        {
            int sampleCountInt = (int)(_sampleCount);
            double[] array = new double[sampleCountInt];
            return array;
        }
    }
}

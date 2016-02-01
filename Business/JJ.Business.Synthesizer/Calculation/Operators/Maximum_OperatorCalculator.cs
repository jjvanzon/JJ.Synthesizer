using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Maximum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _sampleDuration;
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double[] _samples;

        private int _currentIndex;

        private double _previousTime;
        private double _passedSampleTime;
        private double _maximum;

        public Maximum_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeSliceDuration,
            int sampleCount)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (timeSliceDuration <= 0.0) throw new LessThanException(() => timeSliceDuration, 0.0);
            if (sampleCount <= 0) throw new LessThanOrEqualException(() => sampleCount, 0);

            _signalCalculator = signalCalculator;
            _sampleDuration = timeSliceDuration / sampleCount;
            _samples = new double[sampleCount];
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

                _samples[_currentIndex] = sample;

                _currentIndex++;

                _passedSampleTime = 0.0;
            }

            if (_currentIndex == _samples.Length)
            {
                _maximum = _samples[0];

                for (int i = 1; i < _samples.Length; i++)
                {
                    double sample = _samples[i];

                    if (_maximum < sample)
                    {
                        _maximum = sample;
                    }
                }

                _currentIndex = 0;
            }

            _previousTime = time;

            return _maximum;
        }

        public override void ResetState()
        {
            _currentIndex = 0;
            _previousTime = 0.0;
            _passedSampleTime = 0.0;
            _maximum = 0.0;

            base.ResetState();
        }
    }
}

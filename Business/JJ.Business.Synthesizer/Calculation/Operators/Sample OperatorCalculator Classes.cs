using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sample_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;
        private double _phase;
        private double _previousTime;

        public Sample_OperatorCalculator(ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase += dt;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousTime = time;

            return value;
        }
    }

    internal class Sample_WithConstFrequency_OperatorCalculator : OperatorCalculatorBase
    {
        private const double BASE_FREQUENCY = 440.0;

        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private double _phase;
        private double _previousTime;

        public Sample_WithConstFrequency_OperatorCalculator(ISampleCalculator sampleCalculator, double frequency)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;

            _rate = frequency / BASE_FREQUENCY;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _rate;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousTime = time;

            return value;
        }
    }

    internal class Sample_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;
        private double _phase;
        private double _previousTime;

        public Sample_MonoToStereo_OperatorCalculator(ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase += dt;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(_phase, 0);

            _previousTime = time;

            return value;
        }
    }

    internal class Sample_StereoToMono_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;
        private double _phase;
        private double _previousTime;

        public Sample_StereoToMono_OperatorCalculator(ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase += dt;

            double value1 = _sampleCalculator.CalculateValue(_phase, 0);
            double value2 = _sampleCalculator.CalculateValue(_phase, 1);
            double value = value1 + value2;

            _previousTime = time;

            return value;
        }
    }
}

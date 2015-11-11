using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal static class Sample_OperatorCalculator_Helper
    {
        public const double BASE_FREQUENCY = 440.0;
    }

    internal class Sample_WithVarFrequency_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private double _phase;
        private double _previousTime;

        public Sample_WithVarFrequency_OperatorCalculator(OperatorCalculatorBase frequencyCalculator, ISampleCalculator sampleCalculator)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            double dt = time - _previousTime;
            _phase = _phase + dt * rate;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousTime = time;

            return value;
        }
    }

    internal class Sample_WithConstFrequency_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private double _phase;
        private double _previousTime;

        public Sample_WithConstFrequency_OperatorCalculator(double frequency, ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
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

    internal class Sample_WithVarFrequency_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private double _phase;
        private double _previousTime;

        public Sample_WithVarFrequency_MonoToStereo_OperatorCalculator(OperatorCalculatorBase frequencyCalculator, ISampleCalculator sampleCalculator)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            double dt = time - _previousTime;
            _phase = _phase + dt * rate;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(_phase, 0);

            _previousTime = time;

            return value;
        }
    }

    internal class Sample_WithConstFrequency_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private double _phase;
        private double _previousTime;

        public Sample_WithConstFrequency_MonoToStereo_OperatorCalculator(double frequency, ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _rate;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(_phase, 0);

            _previousTime = time;

            return value;
        }
    }

    internal class Sample_WithVarFrequency_StereoToMono_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private double _phase;
        private double _previousTime;

        public Sample_WithVarFrequency_StereoToMono_OperatorCalculator(OperatorCalculatorBase frequencyCalculator, ISampleCalculator sampleCalculator)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            double dt = time - _previousTime;
            _phase = _phase + dt * rate;

            double value0 = _sampleCalculator.CalculateValue(_phase, 0);
            double value1 = _sampleCalculator.CalculateValue(_phase, 1);

            _previousTime = time;

            return value0 + value1;
        }
    }

    internal class Sample_WithConstFrequency_StereoToMono_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private double _phase;
        private double _previousTime;

        public Sample_WithConstFrequency_StereoToMono_OperatorCalculator(double frequency, ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _rate;

            double value0 = _sampleCalculator.CalculateValue(_phase, 0);
            double value1 = _sampleCalculator.CalculateValue(_phase, 1);

            _previousTime = time;

            return value0 + value1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Dsp;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighPassFilter_VarMinFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_MIN_FREQUENCY = 8;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly BiQuadFilter _biQuadFilter;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _minFrequencyCalculator;

        public HighPassFilter_VarMinFrequency_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase minFrequencyCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, minFrequencyCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (minFrequencyCalculator == null) throw new NullException(() => minFrequencyCalculator);
            if (minFrequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => minFrequencyCalculator);

            _signalCalculator = signalCalculator;
            _minFrequencyCalculator = minFrequencyCalculator;

            _biQuadFilter = BiQuadFilter.HighPassFilter(ASSUMED_SAMPLE_RATE, DEFAULT_MIN_FREQUENCY, DEFAULT_BAND_WIDTH);
        }

        public override double Calculate(double time, int channelIndex)
        {
            double minFrequency = _minFrequencyCalculator.Calculate(time, channelIndex);
            double signal = _signalCalculator.Calculate(time, channelIndex);

            _biQuadFilter.SetHighPassFilter(ASSUMED_SAMPLE_RATE, (float)minFrequency, DEFAULT_BAND_WIDTH);

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }
    }

    internal class HighPassFilter_ConstMinFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly BiQuadFilter _biQuadFilter;

        private readonly OperatorCalculatorBase _signalCalculator;

        public HighPassFilter_ConstMinFrequency_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double minFrequency)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;

            _biQuadFilter = BiQuadFilter.HighPassFilter(ASSUMED_SAMPLE_RATE, (float)minFrequency, DEFAULT_BAND_WIDTH);
        }

        public override double Calculate(double time, int channelIndex)
        {
            double signal = _signalCalculator.Calculate(time, channelIndex);

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }
    }

}

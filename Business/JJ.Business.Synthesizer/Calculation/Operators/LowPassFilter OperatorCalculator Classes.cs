using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Dsp;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LowPassFilter_VarMaxFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_MAX_FREQUENCY = 22050;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _maxFrequencyCalculator;

        private BiQuadFilter _biQuadFilter;

        public LowPassFilter_VarMaxFrequency_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase maxFrequencyCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, maxFrequencyCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (maxFrequencyCalculator == null) throw new NullException(() => maxFrequencyCalculator);
            if (maxFrequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => maxFrequencyCalculator);

            _signalCalculator = signalCalculator;
            _maxFrequencyCalculator = maxFrequencyCalculator;

            Reset(new DimensionStack());
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double maxFrequency = _maxFrequencyCalculator.Calculate(dimensionStack);
            double signal = _signalCalculator.Calculate(dimensionStack);

            _biQuadFilter.SetLowPassFilter(ASSUMED_SAMPLE_RATE, (float)maxFrequency, DEFAULT_BAND_WIDTH);

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            base.Reset(dimensionStack);

            _biQuadFilter = BiQuadFilter.LowPassFilter(ASSUMED_SAMPLE_RATE, DEFAULT_MAX_FREQUENCY, DEFAULT_BAND_WIDTH);
        }
    }

    internal class LowPassFilter_ConstMaxFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _maxFrequency;

        private BiQuadFilter _biQuadFilter;

        public LowPassFilter_ConstMaxFrequency_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double maxFrequency)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _maxFrequency = maxFrequency;

            Reset(new DimensionStack());
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double signal = _signalCalculator.Calculate(dimensionStack);

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            base.Reset(dimensionStack);

            _biQuadFilter = BiQuadFilter.LowPassFilter(ASSUMED_SAMPLE_RATE, (float)_maxFrequency, DEFAULT_BAND_WIDTH);
        }
    }
}

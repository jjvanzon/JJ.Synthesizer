using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Dsp;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighPassFilter_VarMinFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_MIN_FREQUENCY = 8;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _minFrequencyCalculator;

        private BiQuadFilter _biQuadFilter;

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

            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double minFrequency = _minFrequencyCalculator.Calculate();
            double signal = _signalCalculator.Calculate();

            _biQuadFilter.SetHighPassFilter(ASSUMED_SAMPLE_RATE, (float)minFrequency, DEFAULT_BAND_WIDTH);

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            _biQuadFilter = BiQuadFilter.HighPassFilter(ASSUMED_SAMPLE_RATE, DEFAULT_MIN_FREQUENCY, DEFAULT_BAND_WIDTH);
        }
    }

    internal class HighPassFilter_ConstMinFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _minFrequency;

        private BiQuadFilter _biQuadFilter;

        public HighPassFilter_ConstMinFrequency_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double minFrequency)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _minFrequency = minFrequency;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            _biQuadFilter = BiQuadFilter.HighPassFilter(ASSUMED_SAMPLE_RATE, (float)_minFrequency, DEFAULT_BAND_WIDTH);
        }
    }
}

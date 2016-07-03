using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CodeCopies.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighPassFilter_VarMinFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const int SAMPLES_PER_SET_FILTER = 100;
        private const double ASSUMED_SAMPLE_RATE = 44100.0;
        private const double DEFAULT_MIN_FREQUENCY = 8.0;
        private const double DEFAULT_BAND_WIDTH = 1.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _minFrequencyCalculator;

        private BiQuadFilter _biQuadFilter;
        private int _counter;

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

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > SAMPLES_PER_SET_FILTER)
            {
                double minFrequency = _minFrequencyCalculator.Calculate();
                _biQuadFilter.SetHighPassFilter(ASSUMED_SAMPLE_RATE, minFrequency, DEFAULT_BAND_WIDTH);

                _counter = 0;
            }

            double signal = _signalCalculator.Calculate();
            double value = _biQuadFilter.Transform(signal);

            _counter++;

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
            _biQuadFilter = BiQuadFilter.CreateHighPassFilter(ASSUMED_SAMPLE_RATE, DEFAULT_MIN_FREQUENCY, DEFAULT_BAND_WIDTH);
            _counter = 0;
        }
    }

    internal class HighPassFilter_ConstMinFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;
        private const double DEFAULT_BAND_WIDTH = 1.0;

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

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            double value = _biQuadFilter.Transform(signal);

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
            _biQuadFilter = BiQuadFilter.CreateHighPassFilter(ASSUMED_SAMPLE_RATE, _minFrequency, DEFAULT_BAND_WIDTH);
        }
    }
}

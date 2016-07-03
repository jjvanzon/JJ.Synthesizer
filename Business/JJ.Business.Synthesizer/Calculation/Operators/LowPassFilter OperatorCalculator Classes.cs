using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CodeCopies.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LowPassFilter_VarMaxFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const int SAMPLES_PER_SET_FILTER = 100;
        private const double ASSUMED_SAMPLE_RATE = 44100.0;
        private const double DEFAULT_MAX_FREQUENCY = 22050.0;
        private const double DEFAULT_BAND_WIDTH = 1.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _maxFrequencyCalculator;

        private BiQuadFilter _biQuadFilter;
        private int _counter;

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

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > SAMPLES_PER_SET_FILTER)
            {
                double maxFrequency = _maxFrequencyCalculator.Calculate();
                _biQuadFilter.SetLowPassFilter(ASSUMED_SAMPLE_RATE, maxFrequency, DEFAULT_BAND_WIDTH);

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
            _biQuadFilter = BiQuadFilter.CreateLowPassFilter(ASSUMED_SAMPLE_RATE, DEFAULT_MAX_FREQUENCY, DEFAULT_BAND_WIDTH);
            _counter = 0;
        }
    }

    internal class LowPassFilter_ConstMaxFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;
        private const double DEFAULT_BAND_WIDTH = 1.0;

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
            _biQuadFilter = BiQuadFilter.CreateLowPassFilter(ASSUMED_SAMPLE_RATE, _maxFrequency, DEFAULT_BAND_WIDTH);
        }
    }
}

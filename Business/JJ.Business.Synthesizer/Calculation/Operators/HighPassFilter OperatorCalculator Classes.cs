using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighPassFilter_OperatorCalculator_VarMinFrequency_VarBandWidth : OperatorCalculatorBase_WithChildCalculators
    {
        private const int SAMPLES_PER_SET_FILTER_CALL = 100;
        private const double ASSUMED_SAMPLE_RATE = 44100.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _minFrequencyCalculator;
        private readonly OperatorCalculatorBase _bandWidthCalculator;

        private BiQuadFilter _biQuadFilter;
        private int _counter;

        public HighPassFilter_OperatorCalculator_VarMinFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase minFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, minFrequencyCalculator, bandWidthCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (minFrequencyCalculator == null) throw new NullException(() => minFrequencyCalculator);
            if (bandWidthCalculator == null) throw new NullException(() => bandWidthCalculator);

            _signalCalculator = signalCalculator;
            _minFrequencyCalculator = minFrequencyCalculator;
            _bandWidthCalculator = bandWidthCalculator;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > SAMPLES_PER_SET_FILTER_CALL)
            {
                double minFrequency = _minFrequencyCalculator.Calculate();
                double bandWidth = _bandWidthCalculator.Calculate();

                _biQuadFilter.SetHighPassFilter(ASSUMED_SAMPLE_RATE, minFrequency, bandWidth);

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
            double minFrequency = _minFrequencyCalculator.Calculate();
            double bandWidth = _bandWidthCalculator.Calculate();

            _biQuadFilter = BiQuadFilter.CreateHighPassFilter(ASSUMED_SAMPLE_RATE, minFrequency, bandWidth);

            _counter = 0;
        }
    }

    internal class HighPassFilter_OperatorCalculator_ConstMinFrequency_ConstBandWidth : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _minFrequency;
        private readonly double _bandWidth;

        private BiQuadFilter _biQuadFilter;

        public HighPassFilter_OperatorCalculator_ConstMinFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double minFrequency,
            double bandWidth)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
            _minFrequency = minFrequency;
            _bandWidth = bandWidth;

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
            _biQuadFilter = BiQuadFilter.CreateHighPassFilter(ASSUMED_SAMPLE_RATE, _minFrequency, _bandWidth);
        }
    }
}

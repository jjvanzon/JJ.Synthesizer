using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AllPassFilter_OperatorCalculator_VarFrequency_VarBandWidth : OperatorCalculatorBase_WithChildCalculators
    {
        private const int SAMPLES_PER_SET_FILTER_CALL = 100;
        private const double ASSUMED_SAMPLE_RATE = 44100.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _bandWidthCalculator;

        private BiQuadFilter _biQuadFilter;
        private int _counter;

        public AllPassFilter_OperatorCalculator_VarFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, frequencyCalculator, bandWidthCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (bandWidthCalculator == null) throw new NullException(() => bandWidthCalculator);

            _signalCalculator = signalCalculator;
            _frequencyCalculator = frequencyCalculator;
            _bandWidthCalculator = bandWidthCalculator;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > SAMPLES_PER_SET_FILTER_CALL)
            {
                double frequency = _frequencyCalculator.Calculate();
                double bandWidth = _bandWidthCalculator.Calculate();

                _biQuadFilter.SetAllPassFilter(ASSUMED_SAMPLE_RATE, frequency, bandWidth);

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
            double frequency = _frequencyCalculator.Calculate();
            double bandWidth = _bandWidthCalculator.Calculate();
            _biQuadFilter = BiQuadFilter.CreateAllPassFilter(ASSUMED_SAMPLE_RATE, frequency, bandWidth);

            _counter = 0;
        }
    }

    internal class AllPassFilter_OperatorCalculator_ConstFrequency_ConstBandWidth : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _frequency;
        private readonly double _bandWidth;

        private BiQuadFilter _biQuadFilter;

        public AllPassFilter_OperatorCalculator_ConstFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double frequency,
            double bandWidth)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _frequency = frequency;
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
            _biQuadFilter = BiQuadFilter.CreateAllPassFilter(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Dsp;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class BandPassFilterConstantTransitionGain_ManyConstants_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _frequency;
        private readonly double _bandWidth;

        private BiQuadFilter _biQuadFilter;

        public BandPassFilterConstantTransitionGain_ManyConstants_OperatorCalculator(
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

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
            _biQuadFilter = BiQuadFilter.BandPassFilterConstantSkirtGain(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth);
        }
    }
}
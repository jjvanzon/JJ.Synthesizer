using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class PeakingEQFilter_ManyConstants_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _centerFrequency;
        private readonly double _bandWidth;
        private readonly double _dbGain;

        private BiQuadFilter _biQuadFilter;

        public PeakingEQFilter_ManyConstants_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double centerFrequency,
            double bandWidth,
            double dbGain)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _centerFrequency = centerFrequency;
            _bandWidth = bandWidth;
            _dbGain = dbGain;

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
            _biQuadFilter = BiQuadFilter.CreatePeakingEQ(ASSUMED_SAMPLE_RATE, _centerFrequency, _bandWidth, _dbGain);
        }
    }
}
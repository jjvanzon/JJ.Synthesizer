using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighShelfFilter_ManyConstants_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _shelfFrequency;
        private readonly double _dbGain;
        private readonly double _shelfSlope;

        private BiQuadFilter _biQuadFilter;

        public HighShelfFilter_ManyConstants_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double shelfFrequency,
            double dbGain,
            double shelfSlope)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
            _shelfFrequency = shelfFrequency;
            _dbGain = dbGain;
            _shelfSlope = shelfSlope;

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
            _biQuadFilter = BiQuadFilter.CreateHighShelf(ASSUMED_SAMPLE_RATE, _shelfFrequency, _shelfSlope, _dbGain);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_Filter_ConstFrequency_ConstBandWidth 
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        protected readonly double _frequency;
        protected readonly double _bandWidth;
        protected readonly double _samplingRate;

        protected BiQuadFilter _biQuadFilter;

        public OperatorCalculatorBase_Filter_ConstFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double frequency,
            double bandWidth,
            double samplingRate)
                : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
            _frequency = frequency;
            _bandWidth = bandWidth;
            _samplingRate = samplingRate;

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
            CreateBiQuadFilter();
        }

        protected abstract void CreateBiQuadFilter();
    }
}
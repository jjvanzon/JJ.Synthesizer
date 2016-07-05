using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    [Obsolete("Put implementation directly inside derived classes.", true)]
    internal abstract class OperatorCalculatorBase_Filter_ConstFrequency_ConstBandWidth 
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _frequency;
        private readonly double _bandWidth;
        private readonly double _samplingRate;
        private readonly BiQuadFilter _biQuadFilter;

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
            _biQuadFilter = new BiQuadFilter();

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
            //_biQuadFilter...
        }
    }
}
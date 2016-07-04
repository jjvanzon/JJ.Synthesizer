using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LowPassFilter_OperatorCalculator_VarMaxFrequency_VarBandWidth 
        : OperatorCalculatorBase_Filter_VarFrequency_VarBandWidth
    {
        public LowPassFilter_OperatorCalculator_VarMaxFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase maxFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(
                  signalCalculator,
                  maxFrequencyCalculator,
                  bandWidthCalculator,
                  samplingRate,
                  samplesBetweenApplyFilterVariables)
        { }

        protected override void CreateBiQuadFilter(double frequency, double bandWidth)
        {
            _biQuadFilter = BiQuadFilter.CreateLowPassFilter(_samplingRate, frequency, bandWidth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetBiQuadFilterVariables(double frequency, double bandWidth)
        {
            _biQuadFilter.SetLowPassFilterVariables(_samplingRate, frequency, bandWidth);
        }
    }

    internal class LowPassFilter_OperatorCalculator_ConstMaxFrequency_ConstBandWidth 
        : OperatorCalculatorBase_Filter_ConstFrequency_ConstBandWidth
    {
        public LowPassFilter_OperatorCalculator_ConstMaxFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double maxFrequency,
            double bandWidth,
            double samplingRate)
            : base(signalCalculator, maxFrequency, bandWidth, samplingRate)
        { }

        protected override void CreateBiQuadFilter()
        {
            _biQuadFilter = BiQuadFilter.CreateLowPassFilter(_samplingRate, _frequency, _bandWidth);
        }
    }
}

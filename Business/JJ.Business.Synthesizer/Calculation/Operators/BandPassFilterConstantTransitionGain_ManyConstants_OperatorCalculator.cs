using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth
        : OperatorCalculatorBase_Filter_VarFrequency_VarBandWidth
    {
        public BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase centerFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(
                  signalCalculator,
                  centerFrequencyCalculator,
                  bandWidthCalculator,
                  samplingRate,
                  samplesBetweenApplyFilterVariables)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetBiQuadFilterVariables(double frequency, double bandWidth)
        {
            _biQuadFilter.SetBandPassFilterConstantSkirtGainVariables(_samplingRate, frequency, bandWidth);
        }

        protected override void CreateBiQuadFilter(double frequency, double bandWidth)
        {
            _biQuadFilter = BiQuadFilter.CreateBandPassFilterConstantSkirtGain(_samplingRate, frequency, bandWidth);
        }
    }

    internal class BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth
        : OperatorCalculatorBase_Filter_ConstFrequency_ConstBandWidth
    {
        public BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double centerFrequency,
            double bandWidth,
            double samplingRate)
            : base(signalCalculator, centerFrequency, bandWidth, samplingRate)
        { }

        protected override void CreateBiQuadFilter()
        {
            _biQuadFilter = BiQuadFilter.CreateBandPassFilterConstantSkirtGain(_samplingRate, _frequency, _bandWidth);
        }
    }
}
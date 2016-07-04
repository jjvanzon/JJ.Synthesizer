using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class BandPassFilterConstantPeakGain_OperatorCalculator_VarCenterFrequency_VarBandWidth
        : OperatorCalculatorBase_Filter_VarFrequency_VarBandWidth
    {
        public BandPassFilterConstantPeakGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(
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
            _biQuadFilter.SetBandPassFilterConstantPeakGainVariables(_samplingRate, frequency, bandWidth);
        }

        protected override void CreateBiQuadFilter(double frequency, double bandWidth)
        {
            _biQuadFilter = BiQuadFilter.CreateBandPassFilterConstantPeakGain(_samplingRate, frequency, bandWidth);
        }
    }

    internal class BandPassFilterConstantPeakGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth 
        : OperatorCalculatorBase_Filter_ConstFrequency_ConstBandWidth
    {
        public BandPassFilterConstantPeakGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double centerFrequency,
            double bandWidth,
            double samplingRate)
            : base(signalCalculator, centerFrequency, bandWidth, samplingRate)
        { }

        protected override void CreateBiQuadFilter()
        {
            _biQuadFilter = BiQuadFilter.CreateBandPassFilterConstantPeakGain(_samplingRate, _frequency, _bandWidth);
        }
    }
}
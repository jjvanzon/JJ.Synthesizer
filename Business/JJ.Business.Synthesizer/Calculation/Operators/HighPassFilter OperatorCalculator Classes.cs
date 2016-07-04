using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class HighPassFilter_OperatorCalculator_VarMinFrequency_VarBandWidth 
        : OperatorCalculatorBase_Filter_VarFrequency_VarBandWidth
    {
        public HighPassFilter_OperatorCalculator_VarMinFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase minFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(
                  signalCalculator, 
                  minFrequencyCalculator, 
                  bandWidthCalculator, 
                  samplingRate, 
                  samplesBetweenApplyFilterVariables)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetBiQuadFilterVariables(double frequency, double bandWidth)
        {
            _biQuadFilter.SetHighPassFilterVariables(_samplingRate, frequency, bandWidth);
        }

        protected override void CreateBiQuadFilter(double frequency, double bandWidth)
        {
            _biQuadFilter = BiQuadFilter.CreateHighPassFilter(_samplingRate, frequency, bandWidth);
        }
    }

    internal class HighPassFilter_OperatorCalculator_ConstMinFrequency_ConstBandWidth
        : OperatorCalculatorBase_Filter_ConstFrequency_ConstBandWidth
    {
        public HighPassFilter_OperatorCalculator_ConstMinFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double minFrequency,
            double bandWidth,
            double samplingRate)
            : base(signalCalculator, minFrequency, bandWidth, samplingRate)
        { }

        protected override void CreateBiQuadFilter()
        {
            _biQuadFilter = BiQuadFilter.CreateHighPassFilter(_samplingRate, _frequency, _bandWidth);
        }
    }
}

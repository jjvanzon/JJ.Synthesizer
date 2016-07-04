using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

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

        protected override BiQuadFilter CreateBiQuadFilter(double samplingRate, double frequency, double bandWidth)
        {
            BiQuadFilter biQuadFilter = BiQuadFilter.CreateHighPassFilter(samplingRate, frequency, bandWidth);
            return biQuadFilter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetBiQuadFilter(
            BiQuadFilter biQuadFilter, 
            double samplingRate, 
            double frequency, 
            double bandWidth)
        {
            biQuadFilter.SetHighPassFilter(samplingRate, frequency, bandWidth);
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

        protected override BiQuadFilter CreateBiQuadFilter(double samplingRate, double frequency, double bandWidth)
        {
            BiQuadFilter biQuadFilter = BiQuadFilter.CreateHighPassFilter(samplingRate, frequency, bandWidth);
            return biQuadFilter;
        }
    }
}

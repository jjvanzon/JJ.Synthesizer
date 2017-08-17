using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitorBase_AfterMathSimplification : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        protected sealed override IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Curve_OperatorDto_NoCurve(Curve_OperatorDto_NoCurve dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Loop_OperatorDto_ConstSignal(Loop_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Sample_OperatorDto_NoSample(Sample_OperatorDto_NoSample dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Sample_OperatorDto_ZeroFrequency(Sample_OperatorDto_ZeroFrequency dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Sine_OperatorDto_ZeroFrequency(Sine_OperatorDto_ZeroFrequency dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal(Squash_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal(Stretch_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Triangle_OperatorDto_ZeroFrequency(Triangle_OperatorDto_ZeroFrequency dto) => throw new NotSupportedException();
    }
}

using System;
using JJ.Business.Synthesizer.Dto.Operators;

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterClassSpecialization : OperatorDtoVisitorBase
    {
        protected sealed override IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Cache_OperatorDto(Cache_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Random_OperatorDto(Random_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SampleWithRate1_OperatorDto(SampleWithRate1_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumFollower_OperatorDto(SumFollower_OperatorDto dto) => throw new NotSupportedException();
    }
}
using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterClassSpecialization : OperatorDtoVisitorBase
    {
        protected sealed override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Cache_OperatorDto(Cache_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Loop_OperatorDto(Loop_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Pulse_OperatorDto(Pulse_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Random_OperatorDto(Random_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Sample_OperatorDto(Sample_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Square_OperatorDto(Square_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Stretch_OperatorDto(Stretch_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumFollower_OperatorDto(SumFollower_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Triangle_OperatorDto(Triangle_OperatorDto dto) => throw new NotSupportedException();
    }
}
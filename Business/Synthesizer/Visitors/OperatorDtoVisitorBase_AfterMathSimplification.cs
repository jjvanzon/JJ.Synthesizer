using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitorBase_AfterMathSimplification : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        protected sealed override IOperatorDto Visit_AverageFollower_OperatorDto_ConstSignal(AverageFollower_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_AverageOverDimension_OperatorDto_ConstSignal(AverageOverDimension_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(ClosestOverInlets_OperatorDto_ConstInput_ConstItems dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Hold_OperatorDto_ConstSignal(Hold_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Loop_OperatorDto_ConstSignal(Loop_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MaxFollower_OperatorDto_ConstSignal(MaxFollower_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MinFollower_OperatorDto_ConstSignal(MinFollower_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ZeroStep(RangeOverOutlets_Outlet_OperatorDto_ZeroStep dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_ConstNumber(SetDimension_OperatorDto_ConstPassThrough_ConstNumber dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_VarNumber(SetDimension_OperatorDto_ConstPassThrough_VarNumber dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto) => throw new NotSupportedException();
    }
}

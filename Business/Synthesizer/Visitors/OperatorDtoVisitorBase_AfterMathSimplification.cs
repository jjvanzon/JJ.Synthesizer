using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitorBase_AfterMathSimplification : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto) => throw new NotSupportedException();
        protected sealed override IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto) => throw new NotSupportedException();
        protected override IOperatorDto Visit_Curve_OperatorDto_NoCurve(Curve_OperatorDto_NoCurve dto) => throw new NotSupportedException();

    }
}

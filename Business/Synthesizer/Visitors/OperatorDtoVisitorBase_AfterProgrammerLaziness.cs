using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitorBase_AfterProgrammerLaziness : OperatorDtoVisitorBase_AfterMathSimplification
    {
        protected sealed override OperatorDtoBase Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
        {
            throw new NotSupportedException();
        }
    }
}

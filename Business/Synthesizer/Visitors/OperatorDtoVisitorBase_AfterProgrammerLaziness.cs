using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterProgrammerLaziness : OperatorDtoVisitorBase_AfterRewiring
    {
        protected sealed override OperatorDtoBase Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
        {
            throw new NotSupportedException();
        }
    }
}

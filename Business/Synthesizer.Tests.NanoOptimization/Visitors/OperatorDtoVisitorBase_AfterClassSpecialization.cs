using System;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterClassSpecialization : OperatorDtoVisitorBase
    {
        protected sealed override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            throw new NotSupportedException();
        }
    }
}
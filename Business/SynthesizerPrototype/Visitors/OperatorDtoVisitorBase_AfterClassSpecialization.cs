using System;
using JJ.Business.SynthesizerPrototype.Dto;

namespace JJ.Business.SynthesizerPrototype.Visitors
{
    public abstract class OperatorDtoVisitorBase_AfterClassSpecialization : OperatorDtoVisitorBase
    {
        protected sealed override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            throw new NotSupportedException();
        }
    }
}
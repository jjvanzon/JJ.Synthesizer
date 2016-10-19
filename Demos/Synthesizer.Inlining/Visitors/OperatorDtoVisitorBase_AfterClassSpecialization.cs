using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterClassSpecialization : OperatorDtoVisitorBase
    {
        protected sealed override OperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDto Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            throw new NotSupportedException();
        }
    }
}
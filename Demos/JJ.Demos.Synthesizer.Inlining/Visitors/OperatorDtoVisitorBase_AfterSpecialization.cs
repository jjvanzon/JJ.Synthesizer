using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterSpecialization : OperatorDtoVisitorBase
    {
        protected sealed override OperatorDto Visit_Add_OperatorDto_Concrete(Add_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDto Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDto Visit_Number_OperatorDto_Concrete(Number_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDto Visit_Shift_OperatorDto_Concrete(Shift_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDto Visit_Sine_OperatorDto_Concrete(Sine_OperatorDto dto)
        {
            throw new NotSupportedException();
        }
    }
}
using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_SpecialVisitation : OperatorDtoVisitorBase
    {
        /// <summary>
        /// Special visitation means writing out the underlying patches of custom operators,
        /// handling multiple outlets and other special things like enumerating variable inputs and reset operators.
        /// </summary>
        public OperatorDtoVisitor_SpecialVisitation()
        { }

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_Reset_OperatorDto(dto);
        }
    }
}

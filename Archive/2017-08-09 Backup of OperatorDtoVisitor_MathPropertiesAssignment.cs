//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_MathPropertiesAssignment : OperatorDtoVisitorBase
//    {
//        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
//        {
//            base.Visit_Absolute_OperatorDto(dto);

//            dto.Number = InputDtoFactory.CreateInputDto(dto.Number.Var);

//            return dto;
//        }
//    }
//}

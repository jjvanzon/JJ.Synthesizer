using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class ClassSpecialization_OperatorDtoVisitor : OperatorDtoVisitorBase
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            IList<OperatorDtoBase> operatorDtos = dto.InputOperatorDtos;

            IList<OperatorDtoBase> constOperatorDtos = operatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst).ToArray();
            IList<OperatorDtoBase> varOperatorDtos = operatorDtos.Except(constOperatorDtos).ToArray();
            IList<double> consts = constOperatorDtos.Select(x => MathPropertiesHelper.GetMathPropertiesDto(x).Value).ToArray();

            bool hasVars = varOperatorDtos.Any();
            bool hasConsts = constOperatorDtos.Any();

            if (hasVars && hasConsts)
            {
                return new Add_OperatorDto_Vars_Consts { Vars = varOperatorDtos, Consts = consts };
            }
            else if (hasVars && !hasConsts)
            {
                return new Add_OperatorDto_Vars_NoConsts { Vars = varOperatorDtos };
            }
            else if (!hasVars && hasConsts)
            {
                return new Add_OperatorDto_NoVars_Consts { Consts = consts };
            }
            else if (!hasVars && !hasConsts)
            {
                return new Add_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }
    }
}

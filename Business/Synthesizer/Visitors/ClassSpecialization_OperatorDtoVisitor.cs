using System;
using System.Collections.Generic;
using System.Linq;
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

            IList<OperatorDtoBase> inputOperatorDtos = dto.InputOperatorDtos;

            inputOperatorDtos = TruncateOperatorDtoList(inputOperatorDtos, x => x.Sum());

            OperatorDtoBase constInputOperatorDto = inputOperatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst).SingleOrDefault();
            if (constInputOperatorDto == null)
            {
                OperatorDtoBase dto2 = new Add_OperatorDto_Vars { Vars = inputOperatorDtos };
                return dto2;
            }
            else
            {
                IList<OperatorDtoBase> varInputOperatorDtos = inputOperatorDtos.Except(constInputOperatorDto).ToArray();
                double constValue = MathPropertiesHelper.GetMathPropertiesDto(constInputOperatorDto).Value;
                OperatorDtoBase dto2 = new Add_OperatorDto_Vars_1Const { Vars = varInputOperatorDtos, ConstValue = constValue };
                return dto2;
            }
        }

        // Helpers

        /// <summary> Collapses invariant operands to one and gets rid of nulls. </summary>
        /// <param name="constantsCombiningDelegate">The method that combines multiple invariant doubles to one.</param>
        private static IList<OperatorDtoBase> TruncateOperatorDtoList(
            IList<OperatorDtoBase> operatorDtos,
            Func<IEnumerable<double>, double> constantsCombiningDelegate)
        {
            IList<OperatorDtoBase> constOperatorDtos = operatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst).ToArray();
            IList<OperatorDtoBase> varOperatorDtos = operatorDtos.Except(constOperatorDtos).ToArray();

            OperatorDtoBase aggregatedConstOperatorDto = null;
            if (constOperatorDtos.Count != 0)
            {
                IEnumerable<double> consts = constOperatorDtos.Select(x => MathPropertiesHelper.GetMathPropertiesDto(x).Value);
                double aggregatedConsts = constantsCombiningDelegate(consts);
                aggregatedConstOperatorDto = new Number_OperatorDto { Number = aggregatedConsts };
            }

            IList<OperatorDtoBase> truncatedOperatorDtoList = varOperatorDtos.Union(aggregatedConstOperatorDto)
                                                                             .Where(x => x != null)
                                                                             .ToList();
            return truncatedOperatorDtoList;
        }
    }
}

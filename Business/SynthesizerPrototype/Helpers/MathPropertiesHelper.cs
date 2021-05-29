using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Framework.Common;

namespace JJ.Business.SynthesizerPrototype.Helpers
{
    internal static class MathPropertiesHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathPropertiesDto GetMathPropertiesDto(IOperatorDto operatorDto)
        {
            if (operatorDto is Number_OperatorDto number_OperatorDto)
            {
                double value = number_OperatorDto.Number;
                MathPropertiesDto mathPropertiesDto = GetMathPropertiesDto(value);
                return mathPropertiesDto;
            }
            else
            {
                var mathPropertiesDto = new MathPropertiesDto { IsVar = true };
                return mathPropertiesDto;
            }
        }

        // ReSharper disable CompareOfFloatsByEqualityOperator
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathPropertiesDto GetMathPropertiesDto(double value)
        {
            var mathPropertiesDto = new MathPropertiesDto
            {
                IsConst = true,
                IsVar = false,
                ConstValue = value,
                IsConstZero = value == 0.0,
                IsConstOne = value == 1.0,
                IsConstSpecialValue = DoubleHelper.IsSpecialValue(value),
                IsConstNonZero = value != 0.0 && !DoubleHelper.IsSpecialValue(value)
            };

            return mathPropertiesDto;
        }

        public static VarsConsts_MathPropertiesDto Get_VarsConsts_MathPropertiesDto(IList<IOperatorDto> operatorDtos)
        {
            IList<IOperatorDto> constOperatorDtos = operatorDtos.Where(x => GetMathPropertiesDto(x).IsConst).ToArray();

            IList<IOperatorDto> varOperatorDtos = operatorDtos.Except(constOperatorDtos).ToArray();
            IList<double> consts = constOperatorDtos.Select(x => GetMathPropertiesDto(x).ConstValue).ToArray();

            bool hasVars = varOperatorDtos.Any();
            bool hasConsts = constOperatorDtos.Any();

            var varsConsts_MathPropertiesDto = new VarsConsts_MathPropertiesDto
            {
                Vars = varOperatorDtos,
                Consts = consts,
                HasConsts = hasConsts,
                HasVars = hasVars,
                AllAreConst = !hasVars,
                AllAreVar = !hasConsts
            };

            return varsConsts_MathPropertiesDto;
        }
    }
}

using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers
{
    internal static class MathPropertiesHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathPropertiesDto GetMathPropertiesDto(OperatorDtoBase operatorDto)
        {
            var number_OperatorDto = operatorDto as Number_OperatorDto;
            if (number_OperatorDto != null)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathPropertiesDto GetMathPropertiesDto(double value)
        {
            var mathPropertiesDto = new MathPropertiesDto
            {
                IsConst = true,
                IsVar = false,
                Value = value
            };

            if (value == 0.0)
            {
                mathPropertiesDto.IsConstZero = true;
            }
            else if (value == 1.0)
            {
                mathPropertiesDto.IsConstZero = true;
            }
            else if (DoubleHelper.IsSpecialValue(value))
            {
                mathPropertiesDto.IsConstSpecialValue = true;
            }

            return mathPropertiesDto;
        }
    }
}

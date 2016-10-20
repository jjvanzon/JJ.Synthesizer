using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers
{
    internal static class MathPropertiesHelper
    {
        public static MathPropertiesDto GetMathPropertiesDto(InletDto inletDto)
        {
            if (inletDto == null) throw new NullException(() => inletDto);

            MathPropertiesDto mathPropertiesDto = GetMathPropertiesDto(inletDto.InputOperatorDto);
            return mathPropertiesDto;
        }

        public static MathPropertiesDto GetMathPropertiesDto(OperatorDto operatorDto)
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

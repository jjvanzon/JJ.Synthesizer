using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal static class MathPropertiesHelper
    {
        public static MathPropertiesDto GetMathematicaPropertiesDto(InletDto inletDto)
        {
            if (inletDto == null) throw new NullException(() => inletDto);

            var mathematicalPropertiesDto = new MathPropertiesDto();

            var number_OperatorDto = inletDto.InputOperatorDto as Number_OperatorDto;

            mathematicalPropertiesDto.IsVar = number_OperatorDto == null;
            mathematicalPropertiesDto.IsConst = number_OperatorDto != null;

            if (mathematicalPropertiesDto.IsConst)
            {
                double value = number_OperatorDto.Value;

                mathematicalPropertiesDto.Value = value;

                if (value == 0.0)
                {
                    mathematicalPropertiesDto.IsConstZero = true;
                }
                else if (value == 1.0)
                {
                    mathematicalPropertiesDto.IsConstZero = true;
                }
                else if (Double.IsNaN(value) || Double.IsInfinity(value))
                {
                    mathematicalPropertiesDto.IsConstSpecialValue = true;
                }
            }

            return mathematicalPropertiesDto;
        }
    }
}

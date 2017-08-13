using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Common;
// ReSharper disable CompareOfFloatsByEqualityOperator
#pragma warning disable 618

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class InputDtoFactory
    {
        /// <param name="operatorDto">nullable</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InputDto TryCreateInputDto(IOperatorDto operatorDto)
        {
            if (operatorDto == null)
            {
                return null;
            }
            else
            {
                return CreateInputDto(operatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InputDto CreateInputDto(IOperatorDto operatorDto)
        {
            if (operatorDto == null) throw new ArgumentNullException(nameof(operatorDto));

            if (operatorDto is Number_OperatorDto number_OperatorDto)
            {
                double value = number_OperatorDto.Number;
                InputDto inputDto = CreateInputDto(value);
                return inputDto;
            }
            else
            {
                var inputDto = new InputDto(true, operatorDto);
                return inputDto;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InputDto CreateInputDto(double @const)
        {
            bool isConstZero = @const == 0.0;
            bool isConstOne = @const == 1.0;
            bool isConstSpecialValue = DoubleHelper.IsSpecialValue(@const);
            bool isConstNonZero = @const != 0.0 && !DoubleHelper.IsSpecialValue(@const);

            var inputDto = new InputDto(true, isConstZero, isConstOne, isConstNonZero, isConstSpecialValue, @const);

            return inputDto;
        }

        public static VarsConsts_InputDto Get_VarsConsts_InputDto(IEnumerable<InputDto> inputDtos)
        {
            IList<InputDto> constInputDtos = inputDtos.Where(x => x.IsConst).ToArray();

            IList<InputDto> varInputDtos = inputDtos.Except(constInputDtos).ToArray();

            bool hasVars = varInputDtos.Any();
            bool hasConsts = constInputDtos.Any();

            var varsConsts_InputDto = new VarsConsts_InputDto
            {
                Vars = varInputDtos,
                Consts = constInputDtos,
                HasConsts = hasConsts,
                HasVars = hasVars,
                AllAreConst = !hasVars,
                AllAreVar = !hasConsts
            };

            if (constInputDtos.Count == 1)
            {
                varsConsts_InputDto.Const = constInputDtos.Single().Const;
                varsConsts_InputDto.ConstIsZero = varsConsts_InputDto.Const.Const == 0.0;
                varsConsts_InputDto.ConstIsOne = varsConsts_InputDto.Const.Const == 1.0;
            }

            return varsConsts_InputDto;
        }
    }
}

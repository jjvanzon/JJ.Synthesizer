using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
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
            => operatorDto == null ? null : CreateInputDto(operatorDto);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InputDto CreateInputDto(IOperatorDto operatorDto)
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
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
            bool isConstNonZero = @const != 0.0 && !DoubleHelper.IsSpecialValue(@const);

            var inputDto = new InputDto(true, isConstZero, isConstOne, isConstNonZero, @const);

            return inputDto;
        }
    }
}
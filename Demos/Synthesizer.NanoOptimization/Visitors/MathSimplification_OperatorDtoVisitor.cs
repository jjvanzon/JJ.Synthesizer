using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class MathSimplification_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        // Multiply

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_ConstB(dto);

            // Pre-Calculate
            var dto2 = new Number_OperatorDto(dto.A * dto.B);

            OperatorDtoBase dto3 = Visit_Number_OperatorDto_Concrete(dto2);

            return dto3;
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_VarB(dto);

            // Switch A and B
            var dto2 = new Multiply_OperatorDto_VarA_ConstB(dto.BOperatorDto, dto.A);

            OperatorDtoBase dto3 = Visit_Multiply_OperatorDto_VarA_ConstB(dto2);

            return dto3;
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            // 0
            if (bMathPropertiesDto.IsConstZero)
            {
                var dto2 = new Number_OperatorDto(0.0);

                OperatorDtoBase dto3 = Visit_Number_OperatorDto_Concrete(dto2);
            }

            // 1 is identity
            if (bMathPropertiesDto.IsConstOne)
            {
                return dto.AOperatorDto;
            }

            return dto;
        }

        // Shift

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_ConstSignal_ConstDistance(dto);

            var dto2 = new Number_OperatorDto(dto.SignalValue);

            OperatorDtoBase dto3 = Visit_Number_OperatorDto_Concrete(dto2);

            return dto3;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            base.Visit_Shift_OperatorDto_ConstSignal_VarDistance(dto);

            var dto2 = new Number_OperatorDto(dto.SignalValue);

            OperatorDtoBase dto3 = Visit_Number_OperatorDto_Concrete(dto2);

            return dto3;
        }

        // Sine

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.Frequency);

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                var dto2 = new Number_OperatorDto(0.0);

                OperatorDtoBase dto3 = Visit_Number_OperatorDto_Concrete(dto2);

                return dto3;
            }

            return dto;
        }
    }
}
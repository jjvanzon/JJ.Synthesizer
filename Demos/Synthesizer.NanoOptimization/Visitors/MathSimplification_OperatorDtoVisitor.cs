﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class MathSimplification_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        public OperatorDto Execute(OperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        // Multiply

        protected override OperatorDto Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_ConstB(dto);

            // Pre-Calculate
            var dto2 = new Number_OperatorDto(dto.A * dto.B);

            OperatorDto dto3 = Visit_Number_OperatorDto_Concrete(dto2);

            return dto3;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_VarB(dto);

            // Switch A and B
            var dto2 = new Multiply_OperatorDto_VarA_ConstB(dto.BInletDto, dto.A);

            OperatorDto dto3 = Visit_Multiply_OperatorDto_VarA_ConstB(dto2);

            return dto3;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            // 0
            if (bMathPropertiesDto.IsConstZero)
            {
                var dto2 = new Number_OperatorDto(0.0);

                OperatorDto dto3 = Visit_Number_OperatorDto_Concrete(dto2);
            }

            // 1 is identity
            if (bMathPropertiesDto.IsConstOne)
            {
                return dto.AInletDto.InputOperatorDto;
            }

            return dto;
        }

        // Shift

        protected override OperatorDto Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_ConstSignal_ConstDistance(dto);

            var dto2 = new Number_OperatorDto(dto.SignalValue);

            OperatorDto dto3 = Visit_Number_OperatorDto_Concrete(dto2);

            return dto3;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            base.Visit_Shift_OperatorDto_ConstSignal_VarDistance(dto);

            var dto2 = new Number_OperatorDto(dto.SignalValue);

            OperatorDto dto3 = Visit_Number_OperatorDto_Concrete(dto2);

            return dto3;
        }

        // Sine

        protected override OperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.Frequency);

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                var dto2 = new Number_OperatorDto(0.0);

                OperatorDto dto3 = Visit_Number_OperatorDto_Concrete(dto2);

                return dto3;
            }

            return dto;
        }
    }
}
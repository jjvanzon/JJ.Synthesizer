using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal class MathSimplification_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        // Add

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_NoVars_NoConsts(dto);

            var dto2 = new Number_OperatorDto_Zero();

            return dto2;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            base.Visit_Add_OperatorDto_NoVars_Consts(dto);

            double number = dto.Consts.Sum();

            var dto2 = new Number_OperatorDto { Number = number };

            return dto2;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_Vars_NoConsts(dto);

            switch (dto.Vars.Count)
            {
                case 0:
                    {
                        OperatorDtoBase dto2 = new Number_OperatorDto_Zero();
                        return dto2;
                    }
                case 1:
                    {
                        OperatorDtoBase dto2 = dto.Vars[0];
                        return dto2;
                    }
                default:
                    {
                        return dto;
                    }
            }
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
        {
            base.Visit_Add_OperatorDto_Vars_Consts(dto);

            double constValue = dto.Consts.Sum();

            var dto2 = new Add_OperatorDto_Vars_1Const { Vars = dto.Vars, ConstValue = constValue };

            OperatorDtoBase dto3 = Visit_Add_OperatorDto_Vars_1Const(dto2);

            return dto3;
        }

        /// <summary> TODO: This method seems unnecessary. It seems all of this has already been handled. </summary>
        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            if (dto.Vars.Count == 0)
            {
                OperatorDtoBase dto2 = new Number_OperatorDto { Number = dto.ConstValue };
                return dto2;
            }

            if (dto.ConstValue == 0.0)
            {
                OperatorDtoBase dto2 = new Add_OperatorDto_Vars_NoConsts { Vars = dto.Vars };
                return dto2;
            }

            return dto;
        }

        // Multiply

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_ConstB(dto);

            // Pre-Calculate
            var dto2 = new Number_OperatorDto { Number = dto.A * dto.B };

            OperatorDtoBase dto3 = Visit_Number_OperatorDto(dto2);

            return dto3;
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_VarB(dto);

            // Switch A and B
            var dto2 = new Multiply_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };

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
                var dto2 = new Number_OperatorDto_Zero();

                OperatorDtoBase dto3 = Visit_Number_OperatorDto(dto2);
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

            var dto2 = new Number_OperatorDto { Number = dto.Signal };

            OperatorDtoBase dto3 = Visit_Number_OperatorDto(dto2);

            return dto3;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            base.Visit_Shift_OperatorDto_ConstSignal_VarDistance(dto);

            var dto2 = new Number_OperatorDto { Number = dto.Signal };

            OperatorDtoBase dto3 = Visit_Number_OperatorDto(dto2);

            return dto3;
        }

        // Sine

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.Frequency);

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                var dto2 = new Number_OperatorDto_Zero();

                OperatorDtoBase dto3 = Visit_Number_OperatorDto(dto2);

                return dto3;
            }

            return dto;
        }
    }
}
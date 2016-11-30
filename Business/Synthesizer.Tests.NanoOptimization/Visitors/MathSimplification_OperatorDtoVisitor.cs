using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal class MathSimplification_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        // Add

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            base.Visit_Add_OperatorDto_NoVars_Consts(dto);

            double result = dto.Consts.Sum();

            return new Number_OperatorDto { Number = result };
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_NoVars_NoConsts(dto);

            return new Number_OperatorDto_Zero();
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_Vars_NoConsts(dto);

            switch (dto.Vars.Count)
            {
                case 0:
                    return new Number_OperatorDto_Zero();

                case 1:
                    return dto.Vars[0];

                default:
                    return dto;
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

        ///// <summary>
        ///// In practice this method is not fired, because the previously run visitors do not produce this DTO,
        ///// but if it ended up in the structure for some reason, it is handled here.
        ///// </summary>
        //protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        //{
        //    base.Visit_Add_OperatorDto_Vars_1Const(dto);

        //    if (dto.Vars.Count == 0)
        //    {
        //        return new Number_OperatorDto { Number = dto.ConstValue };
        //    }

        //    if (dto.ConstValue == 0.0)
        //    {
        //        return new Add_OperatorDto_Vars_NoConsts { Vars = dto.Vars };
        //    }

        //    return dto;
        //}

        // Multiply

        /// <summary> Pre-calculate </summary>
        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_ConstB(dto);

            var dto2 = new Number_OperatorDto { Number = dto.A * dto.B };

            OperatorDtoBase dto3 = Visit_Number_OperatorDto(dto2);

            return dto3;
        }

        /// <summary> Switch A and B </summary>
        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_VarB(dto);

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
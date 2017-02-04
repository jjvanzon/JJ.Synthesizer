using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_MachineOptimization : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(dto);

            if (dto.Items.Count == 2)
            {
                return new ClosestOverInlets_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = dto.Items[0], Item2 = dto.Items[1] };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(dto);

            if (dto.Items.Count == 2)
            {
                return new ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = dto.Items[0], Item2 = dto.Items[1] };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            base.Visit_MaxOverInlets_OperatorDto_Vars_1Const(dto);

            if (dto.Vars.Count == 1)
            {
                return new MaxOverInlets_OperatorDto_1Var_1Const { AOperatorDto = dto.Vars[0], B = dto.ConstValue };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(dto);

            if (dto.Vars.Count == 2)
            {
                return new MaxOverInlets_OperatorDto_2Vars { AOperatorDto = dto.Vars[0], BOperatorDto = dto.Vars[1] };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            base.Visit_MinOverInlets_OperatorDto_Vars_1Const(dto);

            if (dto.Vars.Count == 1)
            {
                return new MinOverInlets_OperatorDto_1Var_1Const { AOperatorDto = dto.Vars[0], B = dto.ConstValue };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_MinOverInlets_OperatorDto_Vars_NoConsts(dto);

            if (dto.Vars.Count == 2)
            {
                return new MinOverInlets_OperatorDto_2Vars { AOperatorDto = dto.Vars[0], BOperatorDto = dto.Vars[1] };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto(dto);

            double value = dto.Number;

            if (DoubleHelper.IsSpecialValue(value))
            {
                return new Number_OperatorDto_NaN();
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (value == 1.0)
            {
                return new Number_OperatorDto_One();
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (value == 0.0)
            {
                return new Number_OperatorDto_Zero();
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            base.Visit_Power_OperatorDto_VarBase_ConstExponent(dto);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (dto.Exponent == 2.0)
            {
                return new Power_OperatorDto_VarBase_Exponent2 { BaseOperatorDto = dto.BaseOperatorDto };
            }
            // ReSharper disable once RedundantIfElseBlock
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            else if (dto.Exponent == 3.0)
            {
                return new Power_OperatorDto_VarBase_Exponent3 { BaseOperatorDto = dto.BaseOperatorDto };
            }
            // ReSharper disable once RedundantIfElseBlock
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            else if (dto.Exponent == 4.0)
            {
                return new Power_OperatorDto_VarBase_Exponent4 { BaseOperatorDto = dto.BaseOperatorDto };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
        {
            base.Visit_RangeOverDimension_OperatorDto_OnlyConsts(dto);

            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.Step);

            // ReSharper disable once InvertIf
            if (stepMathPropertiesDto.IsConstOne)
            {
                var dto2 = new RangeOverDimension_OperatorDto_WithConsts_AndStepOne { From = dto.From, Till = dto.Till };
                DtoCloner.Clone_DimensionProperties(dto, dto2);

                return dto2;
            }

            return dto;
        }
    }
}
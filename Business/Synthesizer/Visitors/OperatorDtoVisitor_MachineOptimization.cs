using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_MachineOptimization : OperatorDtoVisitorBase_AfterMathSimplification
    {
        public IOperatorDto Execute(IOperatorDto dto) => Visit_OperatorDto_Polymorphic(dto);

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(dto);

            IOperatorDto dto2;

            if (dto.Items.Count == 2)
            {
                dto2 = new ClosestOverInlets_OperatorDto_VarInput_2ConstItems();
            }
            else
            {
                dto2 = dto;
            }

            DtoCloner.CloneProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(dto);

            IOperatorDto dto2;

            if (dto.Items.Count == 2)
            {
                dto2 = new ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems();
            }
            else
            {
                dto2 = dto;
            }

            DtoCloner.CloneProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto(dto);

            double value = dto.Number;

            if (DoubleHelper.IsSpecialValue(value))
            {
                return new Number_OperatorDto_NaN();
            }

            if (value == 1.0)
            {
                return new Number_OperatorDto_One();
            }

            if (value == 0.0)
            {
                return new Number_OperatorDto_Zero();
            }

            return dto;
        }

        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            base.Visit_Power_OperatorDto(dto);

            switch (dto.Exponent.Const)
            {
                case 2.0:
                    return new Multiply_OperatorDto { Inputs = new[] { dto.Base, dto.Base } };

                case 3.0:
                    return new Multiply_OperatorDto { Inputs = new[] { dto.Base, dto.Base, dto.Base } };

                case 4.0:
                    var dto2 = new Multiply_OperatorDto { Inputs = new[] { dto.Base, dto.Base } };
                    var dto3 = new Multiply_OperatorDto { Inputs = new[] { dto2, dto.Base } };
                    return dto3;

                default:
                    return dto;
            }
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
        {
            base.Visit_RangeOverDimension_OperatorDto_OnlyConsts(dto);

            IOperatorDto dto2;

            // ReSharper disable once InvertIf
            if (dto.Step.IsConstOne)
            {
                dto2 = new RangeOverDimension_OperatorDto_WithConsts_AndStepOne();

            }
            else
            {
                dto2 = dto;
            }

            DtoCloner.CloneProperties(dto, dto2);

            return dto2;
        }
    }
}
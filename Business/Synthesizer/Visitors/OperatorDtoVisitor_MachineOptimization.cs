﻿using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Helpers;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_MachineOptimization : OperatorDtoVisitorBase_AfterMathSimplification
    {
        public IOperatorDto Execute(IOperatorDto dto) => Visit_OperatorDto_Polymorphic(dto);

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto) => WithAlreadyProcessedCheck(dto, () => base.Visit_OperatorDto_Polymorphic(dto));

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
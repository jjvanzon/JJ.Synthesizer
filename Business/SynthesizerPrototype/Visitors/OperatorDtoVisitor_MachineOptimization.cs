﻿using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Framework.Common;

namespace JJ.Business.SynthesizerPrototype.Visitors
{
    internal class OperatorDtoVisitor_MachineOptimization : OperatorDtoVisitorBase_AfterMathSimplification
    {
        public IOperatorDto Execute(IOperatorDto dto) => Visit_OperatorDto_Polymorphic(dto);

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto(dto);

            double value = dto.Number;

            if (DoubleHelper.IsSpecialValue(value))
            {
                return new Number_OperatorDto_NaN();
            }

            switch (value)
            {
                case 1.0: return new Number_OperatorDto_One();
                case 0.0: return new Number_OperatorDto_Zero();
            }

            return dto;
        }
    }
}
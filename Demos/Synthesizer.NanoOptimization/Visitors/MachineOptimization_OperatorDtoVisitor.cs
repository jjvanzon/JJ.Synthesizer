using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Common;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class MachineOptimization_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        public OperatorDto Execute(OperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDto Visit_Number_OperatorDto_Concrete(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto_Concrete(dto);

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
    }
}

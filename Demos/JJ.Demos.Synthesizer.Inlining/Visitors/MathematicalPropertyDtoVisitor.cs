using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class MathematicalPropertyDtoVisitor : OperatorDtoVisitorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(OperatorDto operatorDto)
        {
            if (operatorDto == null) throw new NullException(() => operatorDto);

            Visit_OperatorDto_Polymorphic(operatorDto);
        }

        protected override void VisitInletDto(InletDto inletDto)
        {
            var number_OperatorDto = inletDto.InputOperatorDto as Number_OperatorDto;

            inletDto.IsConst = number_OperatorDto != null;

            if (inletDto.IsConst)
            {
                double value = number_OperatorDto.Value;

                inletDto.Value = value;

                if (value == 0.0)
                {
                    inletDto.IsConstZero = true;
                }
                else if (value == 1.0)
                {
                    inletDto.IsConstZero = true;
                }
                else if (Double.IsNaN(value) || Double.IsInfinity(value))
                {
                    inletDto.IsConstSpecialValue = true;
                }
            }

            base.VisitInletDto(inletDto);
        }
    }
}

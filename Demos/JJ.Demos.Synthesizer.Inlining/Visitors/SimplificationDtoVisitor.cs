using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class SimplificationDtoVisitor : OperatorDtoVisitorBase
    {
        public OperatorDto Execute(OperatorDto operatorDto)
        {
            return Visit_OperatorDto_Polymorphic(operatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_OperatorDto_Polymorphic(OperatorDto operatorDto)
        {
            operatorDto = base.Visit_OperatorDto_Polymorphic(operatorDto);

            IList<InletDto> inletDtos = operatorDto.InletDtos;
            int inletDtoCount = inletDtos.Count;

            for (int i = 0; i < inletDtoCount; i++)
            {
                InletDto inletDto = inletDtos[i];
                MathPropertiesDto inletMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(inletDto);

                if (inletMathPropertiesDto.IsConstSpecialValue)
                {
                    return new Number_OperatorDto_NaN();
                }
            }

            return operatorDto;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            base.Visit_Add_OperatorDto_Concrete(add_OperatorDto);

            InletDto aInletDto = add_OperatorDto.AInletDto;
            InletDto bInletDto = add_OperatorDto.BInletDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(aInletDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(bInletDto);

            if (aMathPropertiesDto.IsConstZero)
            {
                return bInletDto.InputOperatorDto;
            }

            if (bMathPropertiesDto.IsConstZero)
            {
                return aInletDto.InputOperatorDto;
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Number_OperatorDto(aMathPropertiesDto.Value + bMathPropertiesDto.Value);
            }

            // Switch A and B.
            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Add_OperatorDto_VarA_ConstB(bInletDto, aInletDto, aMathPropertiesDto.Value);
            }

            return add_OperatorDto;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto multiply_OperatorDto)
        {
            base.Visit_Multiply_OperatorDto_Concrete(multiply_OperatorDto);

            InletDto aInletDto = multiply_OperatorDto.AInletDto;
            InletDto bInletDto = multiply_OperatorDto.BInletDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(aInletDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(bInletDto);

            if (aMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            if (bMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            if (aMathPropertiesDto.IsConstOne)
            {
                return bInletDto.InputOperatorDto;
            }

            if (bMathPropertiesDto.IsConstOne)
            {
                return aInletDto.InputOperatorDto;
            }

            // Switch A and B.
            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_VarA_ConstB(bInletDto, aInletDto, aMathPropertiesDto.Value);
            }

            return multiply_OperatorDto;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_Shift_OperatorDto_Concrete(Shift_OperatorDto shift_OperatorDto)
        {
            base.Visit_Shift_OperatorDto_Concrete(shift_OperatorDto);

            InletDto signalInletDto = shift_OperatorDto.SignalInletDto;
            InletDto distanceInletDto = shift_OperatorDto.DistanceInletDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(signalInletDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(signalInletDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalInletDto.InputOperatorDto;
            }

            if (distanceMathPropertiesDto.IsConstZero)
            {
                return signalInletDto.InputOperatorDto;
            }

            return shift_OperatorDto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_Concrete(Sine_OperatorDto sine_OperatorDto)
        {
            base.Visit_Sine_OperatorDto_Concrete(sine_OperatorDto);

            InletDto frequencyInletDto = sine_OperatorDto.FrequencyInletDto;
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(frequencyInletDto);

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            return sine_OperatorDto;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class Simplification_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterSpecialization
    {
        public OperatorDto Execute(OperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDto Visit_OperatorDto_Polymorphic(OperatorDto dto)
        {
            dto = base.Visit_OperatorDto_Polymorphic(dto);

            IList<InletDto> inletDtos = dto.InletDtos;
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

            return dto;
        }

        protected override OperatorDto Visit_Add_OperatorDto_Base(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto_Base(dto);

            InletDto aInletDto = dto.AInletDto;
            InletDto bInletDto = dto.BInletDto;

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

            return dto;
        }

        protected override OperatorDto Visit_Add_OperatorDto_ConstA_ConstB(Add_OperatorDto_ConstA_ConstB dto)
        {
            var newDto = new Number_OperatorDto(dto.A + dto.B);
            return newDto;
        }

        protected override OperatorDto Visit_Add_OperatorDto_ConstA_VarB(Add_OperatorDto_ConstA_VarB dto)
        {
            // Switch A and B.
            var newDto = new Add_OperatorDto_VarA_ConstB(dto.BInletDto, dto.AInletDto, dto.A);
            return newDto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_Base(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto_Base(dto);

            InletDto aInletDto = dto.AInletDto;
            InletDto bInletDto = dto.BInletDto;

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

            return dto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            var newDto = new Number_OperatorDto(dto.A * dto.B);
            return newDto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            // Switch A and B.
            var newDto = new Multiply_OperatorDto_VarA_ConstB(dto.BInletDto, dto.AInletDto, dto.A);
            return newDto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_Base(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto_Base(dto);

            InletDto signalInletDto = dto.SignalInletDto;
            InletDto distanceInletDto = dto.DistanceInletDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(signalInletDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(signalInletDto);

            if (distanceMathPropertiesDto.IsConstZero)
            {
                return signalInletDto.InputOperatorDto;
            }

            return dto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            return dto.SignalInletDto.InputOperatorDto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            return dto.SignalInletDto.InputOperatorDto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_Base(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto_Base(dto);

            InletDto frequencyInletDto = dto.FrequencyInletDto;
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(frequencyInletDto);

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            return dto;
        }
    }
}
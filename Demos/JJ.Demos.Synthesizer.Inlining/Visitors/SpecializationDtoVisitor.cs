using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Demos.Synthesizer.Inlining.Helpers;
using JJ.Framework.Common;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class SpecializationDtoVisitor : OperatorDtoVisitorBase
    {
        public OperatorDto Execute(OperatorDto operatorDto)
        {
            return Visit_OperatorDto_Polymorphic(operatorDto);
        }

        protected override OperatorDto Visit_Add_OperatorDto_Concrete(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto_Concrete(dto);

            InletDto aInletDto = dto.AInletDto;
            InletDto bInletDto = dto.BInletDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(aInletDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(bInletDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Add_OperatorDto_ConstA_ConstB(aInletDto, bInletDto, aMathPropertiesDto.Value, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Add_OperatorDto_VarA_ConstB(aInletDto, bInletDto, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Add_OperatorDto_ConstA_VarB(aInletDto, bInletDto, aMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Add_OperatorDto_VarA_VarB(aInletDto, bInletDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto_Concrete(dto);

            InletDto aInletDto = dto.AInletDto;
            InletDto bInletDto = dto.BInletDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(aInletDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(bInletDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Number_OperatorDto(aMathPropertiesDto.Value * bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Multiply_OperatorDto_VarA_ConstB(aInletDto, bInletDto, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_ConstA_VarB(bInletDto, aInletDto, aMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_VarA_VarB(bInletDto, aInletDto);

            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Number_OperatorDto_Concrete(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto_Concrete(dto);

            double value = dto.Value;

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

        protected override OperatorDto Visit_Shift_OperatorDto_Concrete(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto_Concrete(dto);

            InletDto signalInletDto = dto.SignalInletDto;
            InletDto distanceInletDto = dto.DistanceInletDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(signalInletDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(signalInletDto);

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_ConstSignal_ConstDistance(signalInletDto, distanceInletDto, signalMathPropertiesDto.Value, distanceMathPropertiesDto.Value);
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_VarSignal_ConstDistance(signalInletDto, distanceInletDto, distanceMathPropertiesDto.Value);
            }

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_ConstSignal_VarDistance(signalInletDto, distanceInletDto, signalMathPropertiesDto.Value);
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_VarSignal_VarDistance(signalInletDto, distanceInletDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Sine_OperatorDto_Concrete(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto_Concrete(dto);

            InletDto frequencyInletDto = dto.FrequencyInletDto;
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(frequencyInletDto);

            if (frequencyMathPropertiesDto.IsConst)
            {
                return new Sine_OperatorDto_ConstFrequency_NoOriginShifting(frequencyInletDto, frequencyMathPropertiesDto.Value);
            }

            if (frequencyMathPropertiesDto.IsVar)
            {
                return new Sine_OperatorDto_VarFrequency_WithPhaseTracking(frequencyInletDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }
    }
}
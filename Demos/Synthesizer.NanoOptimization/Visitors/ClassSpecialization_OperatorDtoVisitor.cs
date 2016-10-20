using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;
using JJ.Framework.Common;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class ClassSpecialization_OperatorDtoVisitor : OperatorDtoVisitorBase
    {
        public OperatorDto Execute(OperatorDto operatorDto)
        {
            return Visit_OperatorDto_Polymorphic(operatorDto);
        }

        protected override OperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            InletDto aInletDto = dto.AInletDto;
            InletDto bInletDto = dto.BInletDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aInletDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bInletDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Add_OperatorDto_ConstA_ConstB(aMathPropertiesDto.Value, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Add_OperatorDto_VarA_ConstB(aInletDto, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Add_OperatorDto_ConstA_VarB(aMathPropertiesDto.Value, bInletDto);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Add_OperatorDto_VarA_VarB(aInletDto, bInletDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto(dto);

            InletDto aInletDto = dto.AInletDto;
            InletDto bInletDto = dto.BInletDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aInletDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bInletDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Multiply_OperatorDto_ConstA_ConstB(aMathPropertiesDto.Value, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Multiply_OperatorDto_VarA_ConstB(aInletDto, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_ConstA_VarB(aMathPropertiesDto.Value, bInletDto);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_VarA_VarB(bInletDto, aInletDto);

            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto(dto);

            InletDto signalInletDto = dto.SignalInletDto;
            InletDto distanceInletDto = dto.DistanceInletDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalInletDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(distanceInletDto);

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_ConstSignal_ConstDistance(signalMathPropertiesDto.Value, distanceMathPropertiesDto.Value);
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_VarSignal_ConstDistance(signalInletDto, distanceMathPropertiesDto.Value);
            }

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_ConstSignal_VarDistance(signalMathPropertiesDto.Value, distanceInletDto);
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_VarSignal_VarDistance(signalInletDto, distanceInletDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto(dto);

            InletDto frequencyInletDto = dto.FrequencyInletDto;
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(frequencyInletDto);

            if (frequencyMathPropertiesDto.IsConst)
            {
                return new Sine_OperatorDto_ConstFrequency_NoOriginShifting(frequencyMathPropertiesDto.Value);
            }

            if (frequencyMathPropertiesDto.IsVar)
            {
                return new Sine_OperatorDto_VarFrequency_WithPhaseTracking(frequencyInletDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }
    }
}
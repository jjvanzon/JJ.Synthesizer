using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal class ClassSpecialization_OperatorDtoVisitor : OperatorDtoVisitorBase
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            IList<OperatorDtoBase> operatorDtos = dto.InputOperatorDtos;

            IList<OperatorDtoBase> constOperatorDtos = operatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst).ToArray();
            IList<OperatorDtoBase> varOperatorDtos = operatorDtos.Except(constOperatorDtos).ToArray();
            IList<double> consts = constOperatorDtos.Select(x => MathPropertiesHelper.GetMathPropertiesDto(x).ConstValue).ToArray();

            bool hasVars = varOperatorDtos.Any();
            bool hasConsts = constOperatorDtos.Any();

            if (hasVars && hasConsts)
            {
                return new Add_OperatorDto_Vars_Consts { Vars = varOperatorDtos, Consts = consts };
            }
            else if (hasVars && !hasConsts)
            {
                return new Add_OperatorDto_Vars_NoConsts { Vars = varOperatorDtos };
            }
            else if (!hasVars && hasConsts)
            {
                return new Add_OperatorDto_NoVars_Consts { Consts = consts };
            }
            else if (!hasVars && !hasConsts)
            {
                return new Add_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Multiply_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Multiply_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase distanceOperatorDto = dto.DistanceOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(distanceOperatorDto);

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_ConstSignal_ConstDistance { Signal = signalMathPropertiesDto.ConstValue, Distance = distanceMathPropertiesDto.ConstValue };
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_VarSignal_ConstDistance { SignalOperatorDto = signalOperatorDto, Distance = distanceMathPropertiesDto.ConstValue };
            }

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_ConstSignal_VarDistance { Signal = signalMathPropertiesDto.ConstValue, DistanceOperatorDto = distanceOperatorDto };
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_VarSignal_VarDistance { SignalOperatorDto = signalOperatorDto, DistanceOperatorDto = distanceOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto(dto);

            OperatorDtoBase frequencyOperatorDto = dto.FrequencyOperatorDto;
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(frequencyOperatorDto);

            if (frequencyMathPropertiesDto.IsConst)
            {
                return new Sine_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }

            if (frequencyMathPropertiesDto.IsVar)
            {
                return new Sine_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = frequencyOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }
   }
}
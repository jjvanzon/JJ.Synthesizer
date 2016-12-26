//using System.Reflection;
//using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
//using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

//namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
//{
//    internal class OperatorDtoVisitor_ClassSpecialization : OperatorDtoVisitorBase
//    {
//        public OperatorDtoBase Execute(OperatorDtoBase dto)
//        {
//            return Visit_OperatorDto_Polymorphic(dto);
//        }

//        //protected override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
//        //{
//        //    base.Visit_Add_OperatorDto(dto);

//        //    VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

//        //    if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
//        //    {
//        //        return new Add_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
//        //    }
//        //    else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
//        //    {
//        //        return new Add_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
//        //    }
//        //    else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
//        //    {
//        //        return new Add_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
//        //    }
//        //    else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
//        //    {
//        //        return new Add_OperatorDto_NoVars_NoConsts();
//        //    }

//        //    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
//        //}

//        //protected override OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
//        //{
//        //    base.Visit_Multiply_OperatorDto(dto);

//        //    OperatorDtoBase aOperatorDto = dto.AOperatorDto;
//        //    OperatorDtoBase bOperatorDto = dto.BOperatorDto;

//        //    MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
//        //    MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

//        //    if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
//        //    {
//        //        return new Multiply_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
//        //    }
//        //    else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
//        //    {
//        //        return new Multiply_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
//        //    }
//        //    else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
//        //    {
//        //        return new Multiply_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
//        //    }
//        //    else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
//        //    {
//        //        return new Multiply_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
//        //    }

//        //    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
//        //}

//        //protected override OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
//        //{
//        //    base.Visit_Shift_OperatorDto(dto);

//        //    OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
//        //    OperatorDtoBase distanceOperatorDto = dto.DistanceOperatorDto;

//        //    MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
//        //    MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(distanceOperatorDto);

//        //    if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
//        //    {
//        //        return new Shift_OperatorDto_ConstSignal_ConstDistance { Signal = signalMathPropertiesDto.ConstValue, Distance = distanceMathPropertiesDto.ConstValue };
//        //    }
//        //    else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
//        //    {
//        //        return new Shift_OperatorDto_VarSignal_ConstDistance { SignalOperatorDto = signalOperatorDto, Distance = distanceMathPropertiesDto.ConstValue };
//        //    }
//        //    else if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
//        //    {
//        //        return new Shift_OperatorDto_ConstSignal_VarDistance { Signal = signalMathPropertiesDto.ConstValue, DistanceOperatorDto = distanceOperatorDto };
//        //    }
//        //    else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
//        //    {
//        //        return new Shift_OperatorDto_VarSignal_VarDistance { SignalOperatorDto = signalOperatorDto, DistanceOperatorDto = distanceOperatorDto };
//        //    }

//        //    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
//        //}

//        //protected override OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
//        //{
//        //    base.Visit_Sine_OperatorDto(dto);

//        //    OperatorDtoBase frequencyOperatorDto = dto.FrequencyOperatorDto;
//        //    MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(frequencyOperatorDto);

//        //    if (frequencyMathPropertiesDto.IsConstZero)
//        //    {
//        //        return new Sine_OperatorDto_ZeroFrequency();
//        //    }
//        //    else if (frequencyMathPropertiesDto.IsConst)
//        //    {
//        //        return new Sine_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
//        //    }
//        //    else if (frequencyMathPropertiesDto.IsVar)
//        //    {
//        //        return new Sine_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = frequencyOperatorDto };
//        //    }

//        //    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
//        //}
//    }
//}
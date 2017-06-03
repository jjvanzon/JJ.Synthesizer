using System.Reflection;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
// ReSharper disable RedundantIfElseBlock
// ReSharper disable ConvertIfStatementToSwitchStatement

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoVisitor_ClassSpecializationBase : OperatorDtoVisitorBase
    {
        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
        {
            base.Visit_Absolute_OperatorDto(dto);

            MathPropertiesDto numberMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.NumberOperatorDto);

            IOperatorDto dto2;

            if (numberMathPropertiesDto.IsConst)
            {
                dto2 = new Absolute_OperatorDto_ConstNumber { Number = numberMathPropertiesDto.ConstValue };
            }
            else if (numberMathPropertiesDto.IsVar)
            {
                dto2 = new Absolute_OperatorDto_VarNumber { NumberOperatorDto = dto.NumberOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            IOperatorDto dto2;

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new Add_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new Add_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new Add_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new Add_OperatorDto_NoVars_NoConsts();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto)
        {
            base.Visit_AllPassFilter_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto widthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.WidthOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new AllPassFilter_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst)
            {
                dto2 = new AllPassFilter_OperatorDto_ManyConsts { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new AllPassFilter_OperatorDto_AllVars { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
        {
            base.Visit_And_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new And_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new And_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new And_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new And_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto)
        {
            base.Visit_AverageOverDimension_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new AverageOverDimension_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
            {
                dto2 = new AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
            {
                dto2 = new AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto)
        {
            base.Visit_AverageFollower_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new AverageFollower_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new AverageFollower_OperatorDto_AllVars();
            }

            DtoCloner.TryClone_AggregateFollowerProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
        {
            base.Visit_AverageOverInlets_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            IOperatorDto dto2;

            if (mathPropertiesDto.AllAreConst)
            {
                dto2 = new AverageOverInlets_OperatorDto_AllConsts { Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars)
            {
                dto2 = new AverageOverInlets_OperatorDto_Vars { Vars = dto.Vars };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto)
        {
            base.Visit_BandPassFilterConstantPeakGain_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto widthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.WidthOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto)
        {
            base.Visit_BandPassFilterConstantTransitionGain_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto widthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.WidthOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Cache_OperatorDto(Cache_OperatorDto dto)
        {
            base.Visit_Cache_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new Cache_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Block)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_Block();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_Cubic();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_Hermite();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_Line();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_Stripe();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Block)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_Block();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_Cubic();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_Hermite();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_Line();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_Stripe();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_CacheOperatorProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto)
        {
            base.Visit_ChangeTrigger_OperatorDto(dto);

            MathPropertiesDto passThroughMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PassThroughInputOperatorDto);
            MathPropertiesDto resetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ResetOperatorDto);

            IOperatorDto dto2;

            if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsConst)
            {
                dto2 = new ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThrough = passThroughMathPropertiesDto.ConstValue, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsVar)
            {
                dto2 = new ChangeTrigger_OperatorDto_ConstPassThrough_VarReset { PassThrough = passThroughMathPropertiesDto.ConstValue, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsConst)
            {
                dto2 = new ChangeTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsVar)
            {
                dto2 = new ChangeTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto)
        {
            base.Visit_ClosestOverDimension_OperatorDto(dto);

            ClosestOverDimension_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            DtoCloner.Clone_ClosestOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto)
        {
            base.Visit_ClosestOverDimensionExp_OperatorDto(dto);

            ClosestOverDimensionExp_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            DtoCloner.Clone_ClosestOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto(dto);

            MathPropertiesDto inputMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.InputOperatorDto);
            VarsConsts_MathPropertiesDto itemsMathProperties = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.ItemOperatorDtos);

            IOperatorDto dto2;

            if (inputMathProperties.IsConst && itemsMathProperties.AllAreConst)
            {
                dto2 = new ClosestOverInlets_OperatorDto_ConstInput_ConstItems { Input = inputMathProperties.ConstValue, Items = itemsMathProperties.Consts };
            }
            else if (inputMathProperties.IsVar && itemsMathProperties.AllAreConst)
            {
                dto2 = new ClosestOverInlets_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = itemsMathProperties.Consts };
            }
            else
            {
                dto2 = new ClosestOverInlets_OperatorDto_VarInput_VarItems { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto(dto);

            MathPropertiesDto inputMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.InputOperatorDto);
            VarsConsts_MathPropertiesDto itemsMathProperties = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.ItemOperatorDtos);

            IOperatorDto dto2;

            if (inputMathProperties.IsConst && itemsMathProperties.AllAreConst)
            {
                dto2 = new ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems { Input = inputMathProperties.ConstValue, Items = itemsMathProperties.Consts };
            }
            else if (inputMathProperties.IsVar && itemsMathProperties.AllAreConst)
            {
                dto2 = new ClosestOverInletsExp_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = itemsMathProperties.Consts };
            }
            else
            {
                dto2 = new ClosestOverInletsExp_OperatorDto_VarInput_VarItems { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto)
        {
            base.Visit_Curve_OperatorDto(dto);

            IOperatorDto dto2;

            if (!dto.CurveID.HasValue)
            {
                dto2 = new Curve_OperatorDto_NoCurve();
            }
            else
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                bool hasMinX = dto.MinX != 0.0;

                if (!hasMinX && dto.StandardDimensionEnum == DimensionEnum.Time)
                {
                    dto2 = new Curve_OperatorDto_MinXZero_WithOriginShifting();
                }
                else if (!hasMinX && dto.StandardDimensionEnum != DimensionEnum.Time)
                {
                    dto2 = new Curve_OperatorDto_MinXZero_NoOriginShifting();
                }
                else if (hasMinX && dto.StandardDimensionEnum == DimensionEnum.Time)
                {
                    dto2 = new Curve_OperatorDto_MinX_WithOriginShifting();
                }
                else if (hasMinX && dto.StandardDimensionEnum != DimensionEnum.Time)
                {
                    dto2 = new Curve_OperatorDto_MinX_NoOriginShifting();
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }
            }

            DtoCloner.TryClone_CurveProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
        {
            base.Visit_Divide_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OriginOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                dto2 = new Divide_OperatorDto_VarA_VarB_VarOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new Divide_OperatorDto_VarA_VarB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                dto2 = new Divide_OperatorDto_VarA_VarB_ConstOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                dto2 = new Divide_OperatorDto_VarA_ConstB_VarOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new Divide_OperatorDto_VarA_ConstB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                dto2 = new Divide_OperatorDto_VarA_ConstB_ConstOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                dto2 = new Divide_OperatorDto_ConstA_VarB_VarOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new Divide_OperatorDto_ConstA_VarB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                dto2 = new Divide_OperatorDto_ConstA_VarB_ConstOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                dto2 = new Divide_OperatorDto_ConstA_ConstB_VarOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new Divide_OperatorDto_ConstA_ConstB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                dto2 = new Divide_OperatorDto_ConstA_ConstB_ConstOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
        {
            base.Visit_Equal_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new Equal_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new Equal_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new Equal_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new Equal_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto(Exponent_OperatorDto dto)
        {
            base.Visit_Exponent_OperatorDto(dto);

            MathPropertiesDto lowMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.LowOperatorDto);
            MathPropertiesDto highMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.HighOperatorDto);
            MathPropertiesDto ratioMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.RatioOperatorDto);

            IOperatorDto dto2;

            if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsVar)
            {
                dto2 = new Exponent_OperatorDto_VarLow_VarHigh_VarRatio { LowOperatorDto = dto.LowOperatorDto, HighOperatorDto = dto.HighOperatorDto, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsConst)
            {
                dto2 = new Exponent_OperatorDto_VarLow_VarHigh_ConstRatio { LowOperatorDto = dto.LowOperatorDto, HighOperatorDto = dto.HighOperatorDto, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsVar)
            {
                dto2 = new Exponent_OperatorDto_VarLow_ConstHigh_VarRatio { LowOperatorDto = dto.LowOperatorDto, High = highMathPropertiesDto.ConstValue, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsConst)
            {
                dto2 = new Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio { LowOperatorDto = dto.LowOperatorDto, High = highMathPropertiesDto.ConstValue, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsVar)
            {
                dto2 = new Exponent_OperatorDto_ConstLow_VarHigh_VarRatio { Low = lowMathPropertiesDto.ConstValue, HighOperatorDto = dto.HighOperatorDto, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsConst)
            {
                dto2 = new Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio { Low = lowMathPropertiesDto.ConstValue, HighOperatorDto = dto.HighOperatorDto, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsVar)
            {
                dto2 = new Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio { Low = lowMathPropertiesDto.ConstValue, High = highMathPropertiesDto.ConstValue, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsConst)
            {
                dto2 = new Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio { Low = lowMathPropertiesDto.ConstValue, High = highMathPropertiesDto.ConstValue, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
        {
            base.Visit_GreaterThan_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new GreaterThan_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new GreaterThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new GreaterThan_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new GreaterThan_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
        {
            base.Visit_GreaterThanOrEqual_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new GreaterThanOrEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new GreaterThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new GreaterThanOrEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new GreaterThanOrEqual_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
        {
            base.Visit_HighPassFilter_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto minFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.MinFrequencyOperatorDto);
            MathPropertiesDto blobVolumeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BlobVolumeOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new HighPassFilter_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (minFrequencyMathPropertiesDto.IsConst && blobVolumeMathPropertiesDto.IsConst)
            {
                dto2 = new HighPassFilter_OperatorDto_ManyConsts { MinFrequency = minFrequencyMathPropertiesDto.ConstValue, BlobVolume = blobVolumeMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new HighPassFilter_OperatorDto_AllVars { MinFrequencyOperatorDto = dto.MinFrequencyOperatorDto, BlobVolumeOperatorDto = dto.BlobVolumeOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
        {
            base.Visit_HighShelfFilter_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto transitionFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionFrequencyOperatorDto);
            MathPropertiesDto transitionSlopeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionSlopeOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DBGainOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new HighShelfFilter_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (transitionFrequencyMathPropertiesDto.IsConst && transitionSlopeMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                dto2 = new HighShelfFilter_OperatorDto_ManyConsts { TransitionFrequency = transitionFrequencyMathPropertiesDto.ConstValue, TransitionSlope = transitionSlopeMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new HighShelfFilter_OperatorDto_AllVars { TransitionFrequencyOperatorDto = dto.TransitionFrequencyOperatorDto, TransitionSlopeOperatorDto = dto.TransitionSlopeOperatorDto, DBGainOperatorDto = dto.DBGainOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto)
        {
            base.Visit_Hold_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new Hold_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new Hold_OperatorDto_VarSignal { InputOperatorDtos = dto.InputOperatorDtos, SignalOperatorDto = dto.SignalOperatorDto };
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
        {
            base.Visit_If_OperatorDto(dto);

            MathPropertiesDto conditionMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ConditionOperatorDto);
            MathPropertiesDto thenMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ThenOperatorDto);
            MathPropertiesDto elseMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ElseOperatorDto);

            IOperatorDto dto2;

            if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsVar)
            {
                dto2 = new If_OperatorDto_VarCondition_VarThen_VarElse { ConditionOperatorDto = dto.ConditionOperatorDto, ThenOperatorDto = dto.ThenOperatorDto, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsConst)
            {
                dto2 = new If_OperatorDto_VarCondition_VarThen_ConstElse { ConditionOperatorDto = dto.ConditionOperatorDto, ThenOperatorDto = dto.ThenOperatorDto, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsVar)
            {
                dto2 = new If_OperatorDto_VarCondition_ConstThen_VarElse { ConditionOperatorDto = dto.ConditionOperatorDto, Then = thenMathPropertiesDto.ConstValue, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsConst)
            {
                dto2 = new If_OperatorDto_VarCondition_ConstThen_ConstElse { ConditionOperatorDto = dto.ConditionOperatorDto, Then = thenMathPropertiesDto.ConstValue, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsVar)
            {
                dto2 = new If_OperatorDto_ConstCondition_VarThen_VarElse { Condition = conditionMathPropertiesDto.ConstValue, ThenOperatorDto = dto.ThenOperatorDto, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsConst)
            {
                dto2 = new If_OperatorDto_ConstCondition_VarThen_ConstElse { Condition = conditionMathPropertiesDto.ConstValue, ThenOperatorDto = dto.ThenOperatorDto, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsVar)
            {
                dto2 = new If_OperatorDto_ConstCondition_ConstThen_VarElse { Condition = conditionMathPropertiesDto.ConstValue, Then = thenMathPropertiesDto.ConstValue, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsConst)
            {
                dto2 = new If_OperatorDto_ConstCondition_ConstThen_ConstElse { Condition = conditionMathPropertiesDto.ConstValue, Then = thenMathPropertiesDto.ConstValue, Else = elseMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto)
        {
            base.Visit_InletsToDimension_OperatorDto(dto);

            IOperatorDto dto2;

            if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
            {
                dto2 = new InletsToDimension_OperatorDto_Block { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
            {
                dto2 = new InletsToDimension_OperatorDto_Stripe_LagBehind { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line)
            {
                dto2 = new InletsToDimension_OperatorDto_Line { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
            {
                dto2 = new InletsToDimension_OperatorDto_CubicEquidistant { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
            {
                dto2 = new InletsToDimension_OperatorDto_CubicAbruptSlope { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
            {
                dto2 = new InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
            {
                dto2 = new InletsToDimension_OperatorDto_Hermite_LagBehind { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto)
        {
            base.Visit_Interpolate_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto samplingRateMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SamplingRateOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new Interpolate_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
            {
                dto2 = new Interpolate_OperatorDto_Block { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
            {
                dto2 = new Interpolate_OperatorDto_Stripe_LagBehind { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && samplingRateMathPropertiesDto.IsConst)
            {
                dto2 = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate { SamplingRate = samplingRateMathPropertiesDto.ConstValue };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && samplingRateMathPropertiesDto.IsVar)
            {
                dto2 = new Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
            {
                dto2 = new Interpolate_OperatorDto_CubicEquidistant { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
            {
                dto2 = new Interpolate_OperatorDto_CubicAbruptSlope { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
            {
                dto2 = new Interpolate_OperatorDto_CubicSmoothSlope_LagBehind { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
            {
                dto2 = new Interpolate_OperatorDto_Hermite_LagBehind { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_InterpolateOperatorProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
        {
            base.Visit_LessThan_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new LessThan_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new LessThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new LessThan_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new LessThan_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
        {
            base.Visit_LessThanOrEqual_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new LessThanOrEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new LessThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new LessThanOrEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new LessThanOrEqual_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Loop_OperatorDto(Loop_OperatorDto dto)
        {
            base.Visit_Loop_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto skipMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SkipOperatorDto);
            MathPropertiesDto loopStartMarkerMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.LoopStartMarkerOperatorDto);
            MathPropertiesDto loopEndMarkerMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.LoopEndMarkerOperatorDto);
            MathPropertiesDto releaseEndMarkerMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ReleaseEndMarkerOperatorDto);
            MathPropertiesDto noteDurationMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.NoteDurationOperatorDto);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool skipEqualsLoopStartMarker = skipMathPropertiesDto.IsConst && loopStartMarkerMathPropertiesDto.IsConst && skipMathPropertiesDto.ConstValue == loopStartMarkerMathPropertiesDto.ConstValue;
            bool noNoteDuration = noteDurationMathPropertiesDto.IsConst && noteDurationMathPropertiesDto.ConstValue >= CalculationHelper.VERY_HIGH_VALUE;
            bool noReleaseEndMarker = releaseEndMarkerMathPropertiesDto.IsConst && releaseEndMarkerMathPropertiesDto.ConstValue >= CalculationHelper.VERY_HIGH_VALUE;
            bool noSkip = skipMathPropertiesDto.IsConstZero;

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new Loop_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (noSkip && noReleaseEndMarker && loopStartMarkerMathPropertiesDto.IsConst && loopEndMarkerMathPropertiesDto.IsConst)
            {
                dto2 = new Loop_OperatorDto_NoSkipOrRelease_ManyConstants
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    LoopStartMarker = loopStartMarkerMathPropertiesDto.ConstValue,
                    LoopEndMarker = loopEndMarkerMathPropertiesDto.ConstValue,
                    NoteDurationOperatorDto = dto.NoteDurationOperatorDto
                };
            }
            else if (skipMathPropertiesDto.IsConst && loopStartMarkerMathPropertiesDto.IsConst && loopEndMarkerMathPropertiesDto.IsConst && releaseEndMarkerMathPropertiesDto.IsConst)
            {
                dto2 = new Loop_OperatorDto_ManyConstants
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    Skip = skipMathPropertiesDto.ConstValue,
                    LoopStartMarker = loopStartMarkerMathPropertiesDto.ConstValue,
                    LoopEndMarker = loopEndMarkerMathPropertiesDto.ConstValue,
                    ReleaseEndMarker = releaseEndMarkerMathPropertiesDto.ConstValue,
                    NoteDurationOperatorDto = dto.NoteDurationOperatorDto
                };
            }
            else if (skipMathPropertiesDto.IsConst && skipEqualsLoopStartMarker && loopEndMarkerMathPropertiesDto.IsConst && noNoteDuration)
            {
                dto2 = new Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SkipAndLoopStartMarker = skipMathPropertiesDto.ConstValue,
                    LoopEndMarker = loopEndMarkerMathPropertiesDto.ConstValue,
                    ReleaseEndMarkerOperatorDto = dto.ReleaseEndMarkerOperatorDto
                };
            }
            else if (skipMathPropertiesDto.IsConst && skipEqualsLoopStartMarker && loopEndMarkerMathPropertiesDto.IsVar && noNoteDuration)
            {
                dto2 = new Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SkipAndLoopStartMarker = skipMathPropertiesDto.ConstValue,
                    LoopEndMarkerOperatorDto = dto.LoopEndMarkerOperatorDto,
                    ReleaseEndMarkerOperatorDto = dto.ReleaseEndMarkerOperatorDto
                };
            }
            else if (noSkip && noReleaseEndMarker)
            {
                dto2 = new Loop_OperatorDto_NoSkipOrRelease
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    LoopStartMarkerOperatorDto = dto.LoopStartMarkerOperatorDto,
                    LoopEndMarkerOperatorDto = dto.LoopEndMarkerOperatorDto,
                    NoteDurationOperatorDto = dto.NoteDurationOperatorDto
                };
            }
            else
            {
                dto2 = new Loop_OperatorDto_AllVars
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SkipOperatorDto = dto.SkipOperatorDto,
                    LoopStartMarkerOperatorDto = dto.LoopStartMarkerOperatorDto,
                    LoopEndMarkerOperatorDto = dto.LoopEndMarkerOperatorDto,
                    ReleaseEndMarkerOperatorDto = dto.ReleaseEndMarkerOperatorDto,
                    NoteDurationOperatorDto = dto.NoteDurationOperatorDto
                };
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
        {
            base.Visit_LowPassFilter_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto maxFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.MaxFrequencyOperatorDto);
            MathPropertiesDto blobVolumeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BlobVolumeOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new LowPassFilter_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (maxFrequencyMathPropertiesDto.IsConst && blobVolumeMathPropertiesDto.IsConst)
            {
                dto2 = new LowPassFilter_OperatorDto_ManyConsts { MaxFrequency = maxFrequencyMathPropertiesDto.ConstValue, BlobVolume = blobVolumeMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new LowPassFilter_OperatorDto_AllVars { MaxFrequencyOperatorDto = dto.MaxFrequencyOperatorDto, BlobVolumeOperatorDto = dto.BlobVolumeOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
        {
            base.Visit_LowShelfFilter_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto transitionFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionSlopeOperatorDto);
            MathPropertiesDto transitionSlopeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionSlopeOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DBGainOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new LowShelfFilter_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (transitionFrequencyMathPropertiesDto.IsConst && transitionSlopeMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                dto2 = new LowShelfFilter_OperatorDto_ManyConsts { TransitionFrequency = transitionFrequencyMathPropertiesDto.ConstValue, TransitionSlope = transitionSlopeMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new LowShelfFilter_OperatorDto_AllVars { TransitionFrequencyOperatorDto = dto.TransitionSlopeOperatorDto, TransitionSlopeOperatorDto = dto.TransitionSlopeOperatorDto, DBGainOperatorDto = dto.DBGainOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto)
        {
            base.Visit_MaxOverDimension_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new MaxOverDimension_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
            {
                dto2 = new MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
            {
                dto2 = new MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto)
        {
            base.Visit_MaxFollower_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new MaxFollower_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new MaxFollower_OperatorDto_AllVars();
            }

            DtoCloner.TryClone_AggregateFollowerProperties(dto, dto2);

            return dto;
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
        {
            base.Visit_MaxOverInlets_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            IOperatorDto dto2;

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new MaxOverInlets_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new MaxOverInlets_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new MaxOverInlets_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new MaxOverInlets_OperatorDto_NoVars_NoConsts();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto)
        {
            base.Visit_MinOverDimension_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new MinOverDimension_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
            {
                dto2 = new MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
            {
                dto2 = new MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto)
        {
            base.Visit_MinFollower_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new MinFollower_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new MinFollower_OperatorDto_AllVars();
            }

            DtoCloner.TryClone_AggregateFollowerProperties(dto, dto2);

            return dto;
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
        {
            base.Visit_MinOverInlets_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            IOperatorDto dto2;

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new MinOverInlets_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new MinOverInlets_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new MinOverInlets_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new MinOverInlets_OperatorDto_NoVars_NoConsts();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            IOperatorDto dto2;

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new Multiply_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new Multiply_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                dto2 = new Multiply_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                dto2 = new Multiply_OperatorDto_NoVars_NoConsts();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto(MultiplyWithOrigin_OperatorDto dto)
        {
            base.Visit_MultiplyWithOrigin_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OriginOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                dto2 = new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
        {
            base.Visit_Negative_OperatorDto(dto);

            MathPropertiesDto numberMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.NumberOperatorDto);

            IOperatorDto dto2;

            if (numberMathPropertiesDto.IsConst)
            {
                dto2 = new Negative_OperatorDto_ConstNumber { Number = numberMathPropertiesDto.ConstValue };
            }
            else if (numberMathPropertiesDto.IsVar)
            {
                dto2 = new Negative_OperatorDto_VarNumber { NumberOperatorDto = dto.NumberOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
        {
            base.Visit_Not_OperatorDto(dto);

            MathPropertiesDto numberMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.NumberOperatorDto);

            IOperatorDto dto2;

            if (numberMathPropertiesDto.IsConst)
            {
                dto2 = new Not_OperatorDto_ConstNumber { Number = numberMathPropertiesDto.ConstValue };
            }
            else if (numberMathPropertiesDto.IsVar)
            {
                dto2 = new Not_OperatorDto_VarNumber { NumberOperatorDto = dto.NumberOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
        {
            base.Visit_NotchFilter_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto widthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.WidthOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new NotchFilter_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst)
            {
                dto2 = new NotchFilter_OperatorDto_ManyConsts { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new NotchFilter_OperatorDto_AllVars { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
        {
            base.Visit_NotEqual_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new NotEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new NotEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new NotEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new NotEqual_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_OneOverX_OperatorDto(OneOverX_OperatorDto dto)
        {
            base.Visit_OneOverX_OperatorDto(dto);

            MathPropertiesDto numberMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.NumberOperatorDto);

            IOperatorDto dto2;

            if (numberMathPropertiesDto.IsConst)
            {
                dto2 = new OneOverX_OperatorDto_ConstNumber { Number = numberMathPropertiesDto.ConstValue };
            }
            else if (numberMathPropertiesDto.IsVar)
            {
                dto2 = new OneOverX_OperatorDto_VarNumber { NumberOperatorDto = dto.NumberOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
        {
            base.Visit_Or_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new Or_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new Or_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new Or_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new Or_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
        {
            base.Visit_PeakingEQFilter_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto widthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.WidthOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DBGainOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new PeakingEQFilter_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                dto2 = new PeakingEQFilter_OperatorDto_ManyConsts { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 =new PeakingEQFilter_OperatorDto_AllVars { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto, DBGainOperatorDto = dto.DBGainOperatorDto };
            }

            DtoCloner.TryClone_FilterProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            base.Visit_Power_OperatorDto(dto);

            MathPropertiesDto baseMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BaseOperatorDto);
            MathPropertiesDto exponentMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ExponentOperatorDto);

            IOperatorDto dto2;

            if (baseMathPropertiesDto.IsConst && exponentMathPropertiesDto.IsConst)
            {
                dto2 = new Power_OperatorDto_ConstBase_ConstExponent { Base = baseMathPropertiesDto.ConstValue, Exponent = exponentMathPropertiesDto.ConstValue };
            }
            else if (baseMathPropertiesDto.IsVar && exponentMathPropertiesDto.IsConst)
            {
                dto2 = new Power_OperatorDto_VarBase_ConstExponent { BaseOperatorDto = dto.BaseOperatorDto, Exponent = exponentMathPropertiesDto.ConstValue };
            }
            else if (baseMathPropertiesDto.IsConst && exponentMathPropertiesDto.IsVar)
            {
                dto2 = new Power_OperatorDto_ConstBase_VarExponent { Base = baseMathPropertiesDto.ConstValue, ExponentOperatorDto = dto.ExponentOperatorDto };
            }
            else if (baseMathPropertiesDto.IsVar && exponentMathPropertiesDto.IsVar)
            {
                dto2 = new Power_OperatorDto_VarBase_VarExponent { BaseOperatorDto = dto.BaseOperatorDto, ExponentOperatorDto = dto.ExponentOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto(Pulse_OperatorDto dto)
        {
            base.Visit_Pulse_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);
            MathPropertiesDto widthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.WidthOperatorDto);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool isHalfWidth = widthMathPropertiesDto.IsConst && widthMathPropertiesDto.ConstValue == 0.5;

            IOperatorDto dto2;

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                dto2 = new Pulse_OperatorDto_ZeroFrequency();
            }
            else if (frequencyMathPropertiesDto.IsConst && isHalfWidth && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, WidthOperatorDto = dto.WidthOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && isHalfWidth && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && isHalfWidth && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, WidthOperatorDto = dto.WidthOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && isHalfWidth && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
        {
            base.Visit_PulseTrigger_OperatorDto(dto);

            MathPropertiesDto passThroughMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PassThroughInputOperatorDto);
            MathPropertiesDto resetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ResetOperatorDto);

            IOperatorDto dto2;

            if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsConst)
            {
                dto2 = new PulseTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThrough = passThroughMathPropertiesDto.ConstValue, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsVar)
            {
                dto2 = new PulseTrigger_OperatorDto_ConstPassThrough_VarReset { PassThrough = passThroughMathPropertiesDto.ConstValue, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsConst)
            {
                dto2 = new PulseTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsVar)
            {
                dto2 = new PulseTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Random_OperatorDto(Random_OperatorDto dto)
        {
            base.Visit_Random_OperatorDto(dto);

            MathPropertiesDto rateMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.RateOperatorDto);

            IOperatorDto dto2;

            if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
            {
                dto2 = new Random_OperatorDto_Block { RateOperatorDto = dto.RateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
            {
                dto2 = new Random_OperatorDto_Stripe_LagBehind { RateOperatorDto = dto.RateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && rateMathPropertiesDto.IsConst)
            {
                dto2 = new Random_OperatorDto_Line_LagBehind_ConstRate { Rate = rateMathPropertiesDto.ConstValue };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && rateMathPropertiesDto.IsVar)
            {
                dto2 = new Random_OperatorDto_Line_LagBehind_VarRate { RateOperatorDto = dto.RateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
            {
                dto2 = new Random_OperatorDto_CubicEquidistant { RateOperatorDto = dto.RateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
            {
                dto2 = new Random_OperatorDto_CubicAbruptSlope { RateOperatorDto = dto.RateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
            {
                dto2 = new Random_OperatorDto_CubicSmoothSlope_LagBehind { RateOperatorDto = dto.RateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
            {
                dto2 = new Random_OperatorDto_Hermite_LagBehind { RateOperatorDto = dto.RateOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_RandomOperatorProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto)
        {
            base.Visit_RangeOverDimension_OperatorDto(dto);

            MathPropertiesDto fromMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FromOperatorDto);
            MathPropertiesDto tillMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TillOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);

            IOperatorDto dto2;

            if (fromMathPropertiesDto.IsConst && tillMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst)
            {
                dto2 = new RangeOverDimension_OperatorDto_OnlyConsts
                {
                    From = fromMathPropertiesDto.ConstValue,
                    Till = tillMathPropertiesDto.ConstValue,
                    Step = stepMathPropertiesDto.ConstValue,
                    StandardDimensionEnum = dto.StandardDimensionEnum,
                    CanonicalCustomDimensionName = dto.CanonicalCustomDimensionName
                };
            }
            else
            {
                dto2 = new RangeOverDimension_OperatorDto_OnlyVars
                {
                    FromOperatorDto = dto.FromOperatorDto,
                    TillOperatorDto = dto.TillOperatorDto,
                    StepOperatorDto = dto.StepOperatorDto,
                    StandardDimensionEnum = dto.StandardDimensionEnum,
                    CanonicalCustomDimensionName = dto.CanonicalCustomDimensionName
                };
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto)
        {
            base.Visit_RangeOverOutlets_Outlet_OperatorDto(dto);

            MathPropertiesDto fromMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FromOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);

            IOperatorDto dto2;

            if (stepMathPropertiesDto.IsConstZero)
            {
                dto2 = new RangeOverOutlets_Outlet_OperatorDto_ZeroStep { FromOperatorDto = dto.FromOperatorDto };
            }
            else if (fromMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst)
            {
                dto2 = new RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep { From = fromMathPropertiesDto.ConstValue, Step = stepMathPropertiesDto.ConstValue, OutletListIndex = dto.OutletListIndex };
            }
            else if (fromMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst)
            {
                dto2 = new RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep { FromOperatorDto = dto.FromOperatorDto, Step = stepMathPropertiesDto.ConstValue, OutletListIndex = dto.OutletListIndex };
            }
            else if (fromMathPropertiesDto.IsConst && stepMathPropertiesDto.IsVar)
            {
                dto2 = new RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep { From = fromMathPropertiesDto.ConstValue, StepOperatorDto = dto.StepOperatorDto, OutletListIndex = dto.OutletListIndex };
            }
            else if (fromMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar)
            {
                dto2 = new RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep { FromOperatorDto = dto.FromOperatorDto, StepOperatorDto = dto.StepOperatorDto, OutletListIndex = dto.OutletListIndex };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto(Reverse_OperatorDto dto)
        {
            base.Visit_Reverse_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto factorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FactorOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new Reverse_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (factorMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDto_VarFactor_WithPhaseTracking { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto };
            }
            else if (factorMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDto_VarFactor_NoPhaseTracking { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto };
            }
            else if (factorMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDto_ConstFactor_WithOriginShifting { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
            }
            else if (factorMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDto_ConstFactor_NoOriginShifting { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto)
        {
            base.Visit_Round_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);
            MathPropertiesDto offsetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OffsetOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConst)
            {
                dto2 = new Round_OperatorDto_AllConsts { Signal = signalMathPropertiesDto.ConstValue, Step = stepMathPropertiesDto.ConstValue, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new Round_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue, StepOperatorDto = dto.StepOperatorDto, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConstOne && offsetMathPropertiesDto.IsConstZero)
            {
                dto2 = new Round_OperatorDto_VarSignal_StepOne_OffsetZero { SignalOperatorDto = dto.SignalOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsVar)
            {
                dto2 = new Round_OperatorDto_VarSignal_VarStep_VarOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsConstZero)
            {
                dto2 = new Round_OperatorDto_VarSignal_VarStep_ZeroOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsConst)
            {
                dto2 = new Round_OperatorDto_VarSignal_VarStep_ConstOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsVar)
            {
                dto2 = new Round_OperatorDto_VarSignal_ConstStep_VarOffset { SignalOperatorDto = dto.SignalOperatorDto, Step = stepMathPropertiesDto.ConstValue, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConstZero)
            {
                dto2 = new Round_OperatorDto_VarSignal_ConstStep_ZeroOffset { SignalOperatorDto = dto.SignalOperatorDto, Step = stepMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConst)
            {
                dto2 = new Round_OperatorDto_VarSignal_ConstStep_ConstOffset { SignalOperatorDto = dto.SignalOperatorDto, Step = stepMathPropertiesDto.ConstValue, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Sample_OperatorDto(Sample_OperatorDto dto)
        {
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            int sampleChannelCount = dto.SampleChannelCount;
            bool hasTargetChannelCount = sampleChannelCount == dto.TargetChannelCount;
            bool isFromMonoToStereo = sampleChannelCount == 1 && dto.TargetChannelCount == 2;
            bool isFromStereoToMono = sampleChannelCount == 2 && dto.TargetChannelCount == 1;

            IOperatorDto dto2;

            if (!dto.SampleID.HasValue)
            {
                dto2 = new Sample_OperatorDto_NoSample();
            }
            else if (frequencyMathPropertiesDto.IsConstZero)
            {
                dto2 = new Sample_OperatorDto_ZeroFrequency();
            }
            else if (hasTargetChannelCount && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (hasTargetChannelCount && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (hasTargetChannelCount && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (hasTargetChannelCount && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_SampleProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto(SawDown_OperatorDto dto)
        {
            base.Visit_SawDown_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            IOperatorDto dto2;

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                dto2 = new SawDown_OperatorDto_ZeroFrequency();
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto(SawUp_OperatorDto dto)
        {
            base.Visit_SawUp_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            IOperatorDto dto2;

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                dto2 = new SawUp_OperatorDto_ZeroFrequency();
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Scaler_OperatorDto(Scaler_OperatorDto dto)
        {
            base.Visit_Scaler_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto sourceValueAMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SourceValueAOperatorDto);
            MathPropertiesDto sourceValueBMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SourceValueBOperatorDto);
            MathPropertiesDto targetValueAMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TargetValueAOperatorDto);
            MathPropertiesDto targetValueBMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TargetValueBOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst &&
                sourceValueAMathPropertiesDto.IsConst &&
                sourceValueBMathPropertiesDto.IsConst &&
                targetValueAMathPropertiesDto.IsConst &&
                targetValueBMathPropertiesDto.IsConst)
            {
                dto2 = new Scaler_OperatorDto_AllConsts
                {
                    Signal = signalMathPropertiesDto.ConstValue,
                    SourceValueA = sourceValueAMathPropertiesDto.ConstValue,
                    SourceValueB = sourceValueBMathPropertiesDto.ConstValue,
                    TargetValueA = targetValueAMathPropertiesDto.ConstValue,
                    TargetValueB = targetValueBMathPropertiesDto.ConstValue
                };
            }
            else if (sourceValueAMathPropertiesDto.IsConst &&
                sourceValueBMathPropertiesDto.IsConst &&
                targetValueAMathPropertiesDto.IsConst &&
                targetValueBMathPropertiesDto.IsConst)
            {
                dto2 = new Scaler_OperatorDto_ManyConsts
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SourceValueA = sourceValueAMathPropertiesDto.ConstValue,
                    SourceValueB = sourceValueBMathPropertiesDto.ConstValue,
                    TargetValueA = targetValueAMathPropertiesDto.ConstValue,
                    TargetValueB = targetValueBMathPropertiesDto.ConstValue
                };
            }
            else
            {
                dto2 = new Scaler_OperatorDto_AllVars
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SourceValueAOperatorDto = dto.SourceValueAOperatorDto,
                    SourceValueBOperatorDto = dto.SourceValueBOperatorDto,
                    TargetValueAOperatorDto = dto.TargetValueAOperatorDto,
                    TargetValueBOperatorDto = dto.TargetValueBOperatorDto
                };
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto)
        {
            base.Visit_SetDimension_OperatorDto(dto);

            MathPropertiesDto passThroughMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PassThroughInputOperatorDto);
            MathPropertiesDto numberMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.NumberOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (passThroughMathPropertiesDto.IsConst && numberMathPropertiesDto.IsConst)
            {
                dto2 = new SetDimension_OperatorDto_ConstPassThrough_ConstNumber { PassThrough = passThroughMathPropertiesDto.ConstValue, Number = numberMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsVar && numberMathPropertiesDto.IsConst)
            {
                dto2 = new SetDimension_OperatorDto_VarPassThrough_ConstNumber { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Number = numberMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsConst && numberMathPropertiesDto.IsVar)
            {
                dto2 = new SetDimension_OperatorDto_ConstPassThrough_VarNumber { PassThrough = passThroughMathPropertiesDto.ConstValue, NumberOperatorDto = dto.NumberOperatorDto };
            }
            else if (passThroughMathPropertiesDto.IsVar && numberMathPropertiesDto.IsVar)
            {
                dto2 = new SetDimension_OperatorDto_VarPassThrough_VarNumber { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, NumberOperatorDto = dto.NumberOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DistanceOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
            {
                dto2 = new Shift_OperatorDto_ConstSignal_ConstDistance { Signal = signalMathPropertiesDto.ConstValue, Distance = distanceMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
            {
                dto2 = new Shift_OperatorDto_VarSignal_ConstDistance { SignalOperatorDto = dto.SignalOperatorDto, Distance = distanceMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
            {
                dto2 = new Shift_OperatorDto_ConstSignal_VarDistance { Signal = signalMathPropertiesDto.ConstValue, DistanceOperatorDto = dto.DistanceOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
            {
                dto2 = new Shift_OperatorDto_VarSignal_VarDistance { SignalOperatorDto = dto.SignalOperatorDto, DistanceOperatorDto = dto.DistanceOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            IOperatorDto dto2;

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                dto2 = new Sine_OperatorDto_ZeroFrequency();
            } 
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto)
        {
            base.Visit_SortOverDimension_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new SortOverDimension_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
            {
                dto2 = new SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
            {
                dto2 = new SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto)
        {
            base.Visit_Spectrum_OperatorDto(dto);

            MathPropertiesDto soundMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SoundOperatorDto);

            IOperatorDto dto2;

            if (soundMathPropertiesDto.IsConst)
            {
                dto2 = new Spectrum_OperatorDto_ConstSound { Sound = soundMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new Spectrum_OperatorDto_AllVars
                {
                    SoundOperatorDto = dto.SoundOperatorDto,
                    StartOperatorDto = dto.StartOperatorDto,
                    EndOperatorDto = dto.EndOperatorDto,
                    FrequencyCountOperatorDto = dto.FrequencyCountOperatorDto
                };
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Square_OperatorDto(Square_OperatorDto dto)
        {
            base.Visit_Square_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            IOperatorDto dto2;

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                dto2 = new Square_OperatorDto_ZeroFrequency();
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto)
        {
            base.Visit_Squash_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto factorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FactorOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OriginOperatorDto);

            IOperatorDto dto2;

            if (dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }
            }
            else
            {
                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_VarOrigin { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto(Stretch_OperatorDto dto)
        {
            base.Visit_Stretch_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto factorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FactorOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OriginOperatorDto);

            IOperatorDto dto2;

            if (dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }
            }
            else
            {
                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin { SignalOperatorDto = dto.SignalOperatorDto, FactorOperatorDto = dto.FactorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin { SignalOperatorDto = dto.SignalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = dto.FactorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
        {
            base.Visit_Subtract_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            IOperatorDto dto2;

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                dto2 = new Subtract_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                dto2 = new Subtract_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                dto2 = new Subtract_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                dto2 = new Subtract_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto)
        {
            base.Visit_SumOverDimension_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto fromMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FromOperatorDto);
            MathPropertiesDto tillMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TillOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst && fromMathPropertiesDto.IsConst && tillMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst)
            {
                dto2 = new SumOverDimension_OperatorDto_AllConsts { Signal = signalMathPropertiesDto.ConstValue, From = fromMathPropertiesDto.ConstValue, Till = tillMathPropertiesDto.ConstValue, Step = stepMathPropertiesDto.ConstValue };
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
            {
                dto2 = new SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
            }
            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
            {
                dto2 = new SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto(SumFollower_OperatorDto dto)
        {
            base.Visit_SumFollower_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto sampleCountMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SampleCountOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst && sampleCountMathPropertiesDto.IsConst)
            {
                dto2 = new SumFollower_OperatorDto_ConstSignal_ConstSampleCount { Signal = signalMathPropertiesDto.ConstValue, SampleCount = sampleCountMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsConst && sampleCountMathPropertiesDto.IsVar)
            {
                dto2 = new SumFollower_OperatorDto_ConstSignal_VarSampleCount { Signal = signalMathPropertiesDto.ConstValue, SampleCountOperatorDto = dto.SampleCountOperatorDto };
            }
            else
            {
                dto2 = new SumFollower_OperatorDto_AllVars { SignalOperatorDto = dto.SignalOperatorDto, SliceLengthOperatorDto = dto.SliceLengthOperatorDto, SampleCountOperatorDto = dto.SampleCountOperatorDto };
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_TimePower_OperatorDto(TimePower_OperatorDto dto)
        {
            base.Visit_TimePower_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OriginOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new TimePower_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (originMathPropertiesDto.IsConstZero)
            {
                dto2 = new TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin { SignalOperatorDto = dto.SignalOperatorDto, ExponentOperatorDto = dto.ExponentOperatorDto };
            }
            else
            {
                dto2 = new TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin { SignalOperatorDto = dto.SignalOperatorDto, ExponentOperatorDto = dto.ExponentOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
        {
            base.Visit_ToggleTrigger_OperatorDto(dto);

            MathPropertiesDto passThroughMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PassThroughInputOperatorDto);
            MathPropertiesDto resetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ResetOperatorDto);

            IOperatorDto dto2;

            if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsConst)
            {
                dto2 = new ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThrough = passThroughMathPropertiesDto.ConstValue, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsVar)
            {
                dto2 = new ToggleTrigger_OperatorDto_ConstPassThrough_VarReset { PassThrough = passThroughMathPropertiesDto.ConstValue, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsConst)
            {
                dto2 = new ToggleTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsVar)
            {
                dto2 = new ToggleTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto(Triangle_OperatorDto dto)
        {
            base.Visit_Triangle_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            IOperatorDto dto2;

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                dto2 = new Triangle_OperatorDto_ZeroFrequency();
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

            return dto2;
        }
    }
}

using System.Reflection;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoVisitor_ClassSpecializationBase : OperatorDtoVisitorBase
    {
        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
        {
            base.Visit_Absolute_OperatorDto(dto);

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.XOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new Absolute_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else if (xMathPropertiesDto.IsVar)
            {
                return new Absolute_OperatorDto_VarX { XOperatorDto = dto.XOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Add_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new Add_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Add_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new Add_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto)
        {
            base.Visit_AllPassFilter_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BandWidthOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new AllPassFilter_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                dto2 = new AllPassFilter_OperatorDto_ManyConsts { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new AllPassFilter_OperatorDto_AllVars { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, BandWidthOperatorDto = dto.BandWidthOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
        {
            base.Visit_And_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new And_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new And_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new And_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new And_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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

            return dto;
        }

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
        {
            base.Visit_AverageOverInlets_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.AllAreConst)
            {
                return new AverageOverInlets_OperatorDto_AllConsts { Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars)
            {
                return new AverageOverInlets_OperatorDto_Vars { Vars = dto.Vars };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto)
        {
            base.Visit_BandPassFilterConstantPeakGain_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BandWidthOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, BandWidthOperatorDto = dto.BandWidthOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto)
        {
            base.Visit_BandPassFilterConstantTransitionGain_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BandWidthOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, BandWidthOperatorDto = dto.BandWidthOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

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
                dto2 = new Cache_OperatorDto_SingleChannel_BlockInterpolation();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_CubicInterpolation();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_HermiteInterpolation();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_LineInterpolation();
            }
            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
            {
                dto2 = new Cache_OperatorDto_SingleChannel_StripeInterpolation();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Block)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_BlockInterpolation();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_CubicInterpolation();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_HermiteInterpolation();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_LineInterpolation();
            }
            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
            {
                dto2 = new Cache_OperatorDto_MultiChannel_StripeInterpolation();
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

            if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsConst)
            {
                return new ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThrough = passThroughMathPropertiesDto.ConstValue, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsVar)
            {
                return new ChangeTrigger_OperatorDto_ConstPassThrough_VarReset { PassThrough = passThroughMathPropertiesDto.ConstValue, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsConst)
            {
                return new ChangeTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsVar)
            {
                return new ChangeTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
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

            if (inputMathProperties.IsConst && itemsMathProperties.AllAreConst)
            {
                return new ClosestOverInlets_OperatorDto_ConstInput_ConstItems { Input = inputMathProperties.ConstValue, Items = itemsMathProperties.Consts };
            }
            else if (inputMathProperties.IsVar && itemsMathProperties.AllAreConst)
            {
                return new ClosestOverInlets_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = itemsMathProperties.Consts };
            }
            else
            {
                return new ClosestOverInlets_OperatorDto_VarInput_VarItems { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto(dto);

            MathPropertiesDto inputMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.InputOperatorDto);
            VarsConsts_MathPropertiesDto itemsMathProperties = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.ItemOperatorDtos);

            if (inputMathProperties.IsConst && itemsMathProperties.AllAreConst)
            {
                return new ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems { Input = inputMathProperties.ConstValue, Items = itemsMathProperties.Consts };
            }
            else if (inputMathProperties.IsVar && itemsMathProperties.AllAreConst)
            {
                return new ClosestOverInletsExp_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = itemsMathProperties.Consts };
            }
            else
            {
                return new ClosestOverInletsExp_OperatorDto_VarInput_VarItems { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }
        }

        protected override IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto)
        {
            base.Visit_Curve_OperatorDto(dto);

            if (dto.CurveID == null)
            {
                return new Curve_OperatorDto_NoCurve();
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool hasMinX = dto.MinX != 0.0;

            IOperatorDto dto2;

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

            DtoCloner.TryClone_CurveProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
        {
            base.Visit_Divide_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OriginOperatorDto);

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_VarA_VarB_VarOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_VarA_VarB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_VarA_VarB_ConstOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_VarA_ConstB_VarOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_VarA_ConstB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_VarA_ConstB_ConstOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_ConstA_VarB_VarOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_ConstA_VarB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_ConstA_VarB_ConstOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_ConstA_ConstB_VarOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_ConstA_ConstB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_ConstA_ConstB_ConstOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
        {
            base.Visit_Equal_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Equal_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Equal_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Equal_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Equal_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto(Exponent_OperatorDto dto)
        {
            base.Visit_Exponent_OperatorDto(dto);

            MathPropertiesDto lowMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.LowOperatorDto);
            MathPropertiesDto highMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.HighOperatorDto);
            MathPropertiesDto ratioMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.RatioOperatorDto);

            if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_VarLow_VarHigh_VarRatio { LowOperatorDto = dto.LowOperatorDto, HighOperatorDto = dto.HighOperatorDto, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_VarLow_VarHigh_ConstRatio { LowOperatorDto = dto.LowOperatorDto, HighOperatorDto = dto.HighOperatorDto, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_VarLow_ConstHigh_VarRatio { LowOperatorDto = dto.LowOperatorDto, High = highMathPropertiesDto.ConstValue, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio { LowOperatorDto = dto.LowOperatorDto, High = highMathPropertiesDto.ConstValue, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_ConstLow_VarHigh_VarRatio { Low = lowMathPropertiesDto.ConstValue, HighOperatorDto = dto.HighOperatorDto, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio { Low = lowMathPropertiesDto.ConstValue, HighOperatorDto = dto.HighOperatorDto, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio { Low = lowMathPropertiesDto.ConstValue, High = highMathPropertiesDto.ConstValue, RatioOperatorDto = dto.RatioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio { Low = lowMathPropertiesDto.ConstValue, High = highMathPropertiesDto.ConstValue, Ratio = ratioMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
        {
            base.Visit_GreaterThan_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new GreaterThan_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new GreaterThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new GreaterThan_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new GreaterThan_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
        {
            base.Visit_GreaterThanOrEqual_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new GreaterThanOrEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new GreaterThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new GreaterThanOrEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new GreaterThanOrEqual_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
        {
            base.Visit_HighPassFilter_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto minFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.MinFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BandWidthOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new HighPassFilter_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (minFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                dto2 = new HighPassFilter_OperatorDto_ManyConsts { MinFrequency = minFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new HighPassFilter_OperatorDto_AllVars { MinFrequencyOperatorDto = dto.MinFrequencyOperatorDto, BandWidthOperatorDto = dto.BandWidthOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
        {
            base.Visit_HighShelfFilter_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto transitionFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionFrequencyOperatorDto);
            MathPropertiesDto transitionSlopeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionSlopeOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DBGainOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new HighShelfFilter_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (transitionFrequencyMathPropertiesDto.IsConst && transitionSlopeMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                dto2 = new HighShelfFilter_OperatorDto_ManyConsts { TransitionFrequency = transitionFrequencyMathPropertiesDto.ConstValue, TransitionSlope = transitionSlopeMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new HighShelfFilter_OperatorDto_AllVars { TransitionFrequencyOperatorDto = dto.TransitionFrequencyOperatorDto, TransitionSlopeOperatorDto = dto.TransitionSlopeOperatorDto, DBGainOperatorDto = dto.DBGainOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto)
        {
            base.Visit_Hold_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return new Hold_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else
            {
                return new Hold_OperatorDto_VarSignal { InputOperatorDtos = dto.InputOperatorDtos, SignalOperatorDto = dto.SignalOperatorDto };
            }
        }

        protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
        {
            base.Visit_If_OperatorDto(dto);

            MathPropertiesDto conditionMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ConditionOperatorDto);
            MathPropertiesDto thenMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ThenOperatorDto);
            MathPropertiesDto elseMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ElseOperatorDto);

            if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_VarCondition_VarThen_VarElse { ConditionOperatorDto = dto.ConditionOperatorDto, ThenOperatorDto = dto.ThenOperatorDto, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_VarCondition_VarThen_ConstElse { ConditionOperatorDto = dto.ConditionOperatorDto, ThenOperatorDto = dto.ThenOperatorDto, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_VarCondition_ConstThen_VarElse { ConditionOperatorDto = dto.ConditionOperatorDto, Then = thenMathPropertiesDto.ConstValue, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_VarCondition_ConstThen_ConstElse { ConditionOperatorDto = dto.ConditionOperatorDto, Then = thenMathPropertiesDto.ConstValue, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_ConstCondition_VarThen_VarElse { Condition = conditionMathPropertiesDto.ConstValue, ThenOperatorDto = dto.ThenOperatorDto, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_ConstCondition_VarThen_ConstElse { Condition = conditionMathPropertiesDto.ConstValue, ThenOperatorDto = dto.ThenOperatorDto, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_ConstCondition_ConstThen_VarElse { Condition = conditionMathPropertiesDto.ConstValue, Then = thenMathPropertiesDto.ConstValue, ElseOperatorDto = dto.ElseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_ConstCondition_ConstThen_ConstElse { Condition = conditionMathPropertiesDto.ConstValue, Then = thenMathPropertiesDto.ConstValue, Else = elseMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new LessThan_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new LessThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new LessThan_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new LessThan_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
        {
            base.Visit_LessThanOrEqual_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new LessThanOrEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new LessThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new LessThanOrEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new LessThanOrEqual_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
        {
            base.Visit_LowPassFilter_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto maxFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.MaxFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BandWidthOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new LowPassFilter_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (maxFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                dto2 = new LowPassFilter_OperatorDto_ManyConsts { MaxFrequency = maxFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new LowPassFilter_OperatorDto_AllVars { MaxFrequencyOperatorDto = dto.MaxFrequencyOperatorDto, BandWidthOperatorDto = dto.BandWidthOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
        {
            base.Visit_LowShelfFilter_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto transitionFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionSlopeOperatorDto);
            MathPropertiesDto transitionSlopeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TransitionSlopeOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DBGainOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new LowShelfFilter_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (transitionFrequencyMathPropertiesDto.IsConst && transitionSlopeMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                dto2 = new LowShelfFilter_OperatorDto_ManyConsts { TransitionFrequency = transitionFrequencyMathPropertiesDto.ConstValue, TransitionSlope = transitionSlopeMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new LowShelfFilter_OperatorDto_AllVars { TransitionFrequencyOperatorDto = dto.TransitionSlopeOperatorDto, TransitionSlopeOperatorDto = dto.TransitionSlopeOperatorDto, DBGainOperatorDto = dto.DBGainOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

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

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto(dto);

            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto(MultiplyWithOrigin_OperatorDto dto)
        {
            base.Visit_MultiplyWithOrigin_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OriginOperatorDto);

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
        {
            base.Visit_Negative_OperatorDto(dto);

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.XOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new Negative_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else if (xMathPropertiesDto.IsVar)
            {
                return new Negative_OperatorDto_VarX { XOperatorDto = dto.XOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
        {
            base.Visit_Not_OperatorDto(dto);

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.XOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new Not_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else if (xMathPropertiesDto.IsVar)
            {
                return new Not_OperatorDto_VarX { XOperatorDto = dto.XOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
        {
            base.Visit_NotchFilter_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BandWidthOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new NotchFilter_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                dto2 = new NotchFilter_OperatorDto_ManyConsts { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 = new NotchFilter_OperatorDto_AllVars { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, BandWidthOperatorDto = dto.BandWidthOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
        {
            base.Visit_NotEqual_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new NotEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new NotEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new NotEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new NotEqual_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_OneOverX_OperatorDto(OneOverX_OperatorDto dto)
        {
            base.Visit_OneOverX_OperatorDto(dto);

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.XOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new OneOverX_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else if (xMathPropertiesDto.IsVar)
            {
                return new OneOverX_OperatorDto_VarX { XOperatorDto = dto.XOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
        {
            base.Visit_Or_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Or_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Or_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Or_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Or_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
        {
            base.Visit_PeakingEQFilter_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.CenterFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BandWidthOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DBGainOperatorDto);

            IOperatorDto dto2;

            if (signalMathPropertiesDto.IsConst)
            {
                dto2 = new PeakingEQFilter_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                dto2 = new PeakingEQFilter_OperatorDto_ManyConsts { CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                dto2 =new PeakingEQFilter_OperatorDto_AllVars { CenterFrequencyOperatorDto = dto.CenterFrequencyOperatorDto, BandWidthOperatorDto = dto.BandWidthOperatorDto, DBGainOperatorDto = dto.DBGainOperatorDto };
            }

            DtoCloner.TryClone_FilterVarSignalProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            base.Visit_Power_OperatorDto(dto);

            MathPropertiesDto baseMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BaseOperatorDto);
            MathPropertiesDto exponentMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ExponentOperatorDto);

            if (baseMathPropertiesDto.IsConst && exponentMathPropertiesDto.IsConst)
            {
                return new Power_OperatorDto_ConstBase_ConstExponent { Base = baseMathPropertiesDto.ConstValue, Exponent = exponentMathPropertiesDto.ConstValue };
            }
            else if (baseMathPropertiesDto.IsVar && exponentMathPropertiesDto.IsConst)
            {
                return new Power_OperatorDto_VarBase_ConstExponent { BaseOperatorDto = dto.BaseOperatorDto, Exponent = exponentMathPropertiesDto.ConstValue };
            }
            else if (baseMathPropertiesDto.IsConst && exponentMathPropertiesDto.IsVar)
            {
                return new Power_OperatorDto_ConstBase_VarExponent { Base = baseMathPropertiesDto.ConstValue, ExponentOperatorDto = dto.ExponentOperatorDto };
            }
            else if (baseMathPropertiesDto.IsVar && exponentMathPropertiesDto.IsVar)
            {
                return new Power_OperatorDto_VarBase_VarExponent { BaseOperatorDto = dto.BaseOperatorDto, ExponentOperatorDto = dto.ExponentOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
        {
            base.Visit_PulseTrigger_OperatorDto(dto);

            MathPropertiesDto passThroughMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PassThroughInputOperatorDto);
            MathPropertiesDto resetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ResetOperatorDto);

            if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsConst)
            {
                return new PulseTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThrough = passThroughMathPropertiesDto.ConstValue, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsVar)
            {
                return new PulseTrigger_OperatorDto_ConstPassThrough_VarReset { PassThrough = passThroughMathPropertiesDto.ConstValue, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsConst)
            {
                return new PulseTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsVar)
            {
                return new PulseTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
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

            if (fromMathPropertiesDto.IsConst && tillMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst)
            {
                return new RangeOverDimension_OperatorDto_OnlyConsts
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
                return new RangeOverDimension_OperatorDto_OnlyVars
                {
                    FromOperatorDto = dto.FromOperatorDto,
                    TillOperatorDto = dto.TillOperatorDto,
                    StepOperatorDto = dto.StepOperatorDto,
                    StandardDimensionEnum = dto.StandardDimensionEnum,
                    CanonicalCustomDimensionName = dto.CanonicalCustomDimensionName
                };
            }
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto)
        {
            base.Visit_RangeOverOutlets_Outlet_OperatorDto(dto);

            MathPropertiesDto fromMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FromOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);

            if (stepMathPropertiesDto.IsConstZero)
            {
                return new RangeOverOutlets_Outlet_OperatorDto_ZeroStep { FromOperatorDto = dto.FromOperatorDto };
            }
            else if (fromMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst)
            {
                return new RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep { From = fromMathPropertiesDto.ConstValue, Step = stepMathPropertiesDto.ConstValue, OutletListIndex = dto.OutletListIndex };
            }
            else if (fromMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst)
            {
                return new RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep { FromOperatorDto = dto.FromOperatorDto, Step = stepMathPropertiesDto.ConstValue, OutletListIndex = dto.OutletListIndex };
            }
            else if (fromMathPropertiesDto.IsConst && stepMathPropertiesDto.IsVar)
            {
                return new RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep { From = fromMathPropertiesDto.ConstValue, StepOperatorDto = dto.StepOperatorDto, OutletListIndex = dto.OutletListIndex };
            }
            else if (fromMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar)
            {
                return new RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep { FromOperatorDto = dto.FromOperatorDto, StepOperatorDto = dto.StepOperatorDto, OutletListIndex = dto.OutletListIndex };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto)
        {
            base.Visit_Round_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);
            MathPropertiesDto offsetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OffsetOperatorDto);

            if (signalMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConst)
            {
                return new Round_OperatorDto_AllConsts { Signal = signalMathPropertiesDto.ConstValue, Step = stepMathPropertiesDto.ConstValue, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsConst)
            {
                return new Round_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue, StepOperatorDto = dto.StepOperatorDto, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConstOne && offsetMathPropertiesDto.IsConstZero)
            {
                return new Round_OperatorDto_VarSignal_StepOne_OffsetZero { SignalOperatorDto = dto.SignalOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsVar)
            {
                return new Round_OperatorDto_VarSignal_VarStep_VarOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsConstZero)
            {
                return new Round_OperatorDto_VarSignal_VarStep_ZeroOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsConst)
            {
                return new Round_OperatorDto_VarSignal_VarStep_ConstOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsVar)
            {
                return new Round_OperatorDto_VarSignal_ConstStep_VarOffset { SignalOperatorDto = dto.SignalOperatorDto, Step = stepMathPropertiesDto.ConstValue, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConstZero)
            {
                return new Round_OperatorDto_VarSignal_ConstStep_ZeroOffset { SignalOperatorDto = dto.SignalOperatorDto, Step = stepMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConst)
            {
                return new Round_OperatorDto_VarSignal_ConstStep_ConstOffset { SignalOperatorDto = dto.SignalOperatorDto, Step = stepMathPropertiesDto.ConstValue, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            if (signalMathPropertiesDto.IsConst &&
                sourceValueAMathPropertiesDto.IsConst &&
                sourceValueBMathPropertiesDto.IsConst &&
                targetValueAMathPropertiesDto.IsConst &&
                targetValueBMathPropertiesDto.IsConst)
            {
                return new Scaler_OperatorDto_AllConsts
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
                return new Scaler_OperatorDto_ManyConsts
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
                return new Scaler_OperatorDto_AllVars
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SourceValueAOperatorDto = dto.SourceValueAOperatorDto,
                    SourceValueBOperatorDto = dto.SourceValueBOperatorDto,
                    TargetValueAOperatorDto = dto.TargetValueAOperatorDto,
                    TargetValueBOperatorDto = dto.TargetValueBOperatorDto
                };
            }
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto)
        {
            base.Visit_SetDimension_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PassThroughInputOperatorDto);
            MathPropertiesDto positionMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ValueOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (signalMathPropertiesDto.IsConst && positionMathPropertiesDto.IsConst)
            {
                dto2 = new SetDimension_OperatorDto_ConstPassThrough_ConstValue { PassThrough = signalMathPropertiesDto.ConstValue, Value = positionMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && positionMathPropertiesDto.IsConst)
            {
                dto2 = new SetDimension_OperatorDto_VarPassThrough_ConstValue { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Value = positionMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsConst && positionMathPropertiesDto.IsVar)
            {
                dto2 = new SetDimension_OperatorDto_ConstPassThrough_VarValue { PassThrough = signalMathPropertiesDto.ConstValue, ValueOperatorDto = dto.ValueOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && positionMathPropertiesDto.IsVar)
            {
                dto2 = new SetDimension_OperatorDto_VarPassThrough_VarValue { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ValueOperatorDto = dto.ValueOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            DtoCloner.Clone_DimensionProperties(dto, dto2);

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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return new Spectrum_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue };
            }
            else
            {
                var dto2 = new Spectrum_OperatorDto_AllVars
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    StartOperatorDto = dto.StartOperatorDto,
                    EndOperatorDto = dto.EndOperatorDto,
                    FrequencyCountOperatorDto = dto.FrequencyCountOperatorDto
                };

                DtoCloner.Clone_DimensionProperties(dto, dto2);

                return dto2;
            }
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
        {
            base.Visit_Subtract_OperatorDto(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.AOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Subtract_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Subtract_OperatorDto_VarA_ConstB { AOperatorDto = dto.AOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Subtract_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = dto.BOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Subtract_OperatorDto_VarA_VarB { AOperatorDto = dto.AOperatorDto, BOperatorDto = dto.BOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
        {
            base.Visit_ToggleTrigger_OperatorDto(dto);

            MathPropertiesDto passThroughMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PassThroughInputOperatorDto);
            MathPropertiesDto resetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ResetOperatorDto);

            if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsConst)
            {
                return new ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThrough = passThroughMathPropertiesDto.ConstValue, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsConst && resetMathPropertiesDto.IsVar)
            {
                return new ToggleTrigger_OperatorDto_ConstPassThrough_VarReset { PassThrough = passThroughMathPropertiesDto.ConstValue, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsConst)
            {
                return new ToggleTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Reset = resetMathPropertiesDto.ConstValue };
            }
            else if (passThroughMathPropertiesDto.IsVar && resetMathPropertiesDto.IsVar)
            {
                return new ToggleTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ResetOperatorDto = dto.ResetOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
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

            DtoCloner.TryClone_DimensionProperties(dto, dto2);

            return dto2;
        }
    }
}

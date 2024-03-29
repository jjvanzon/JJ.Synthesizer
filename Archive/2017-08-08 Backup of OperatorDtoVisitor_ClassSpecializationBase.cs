﻿//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Exceptions;
//// ReSharper disable RedundantIfElseBlock
//// ReSharper disable ConvertIfStatementToSwitchStatement

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal abstract class OperatorDtoVisitor_ClassSpecializationBase : OperatorDtoVisitorBase
//    {
//        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
//        {
//            base.Visit_Absolute_OperatorDto(dto);

//            dto.Number = InputDtoFactory.CreateInputDto(dto.Number.Var);

//            return dto;
//        }

//        protected override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
//        {
//            base.Visit_Add_OperatorDto(dto);

//            VarsConsts_InputDto inputDto = InputDtoFactory.Get_VarsConsts_InputDto(dto.Inputs);

//            IOperatorDto dto2;

//            if (inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new Add_OperatorDto_Vars_Consts { Vars = inputDto.Vars, Consts = inputDto.Consts };
//            }
//            else if (inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new Add_OperatorDto_Vars_NoConsts { Vars = inputDto.Vars };
//            }
//            else if (!inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new Add_OperatorDto_NoVars_Consts { Consts = inputDto.Consts };
//            }
//            else if (!inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new Add_OperatorDto_NoVars_NoConsts();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto)
//        {
//            base.Visit_AllPassFilter_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new AllPassFilter_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
//            {
//                dto2 = new AllPassFilter_OperatorDto_ManyConsts { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }
//            else
//            {
//                dto2 = new AllPassFilter_OperatorDto_AllVars { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
//        {
//            base.Visit_And_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new And_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new And_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new And_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new And_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto)
//        {
//            base.Visit_AverageOverDimension_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new AverageOverDimension_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
//            {
//                dto2 = new AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
//            {
//                dto2 = new AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto)
//        {
//            base.Visit_AverageFollower_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new AverageFollower_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else
//            {
//                dto2 = new AverageFollower_OperatorDto_AllVars();
//            }

//            DtoCloner.TryClone_AggregateFollowerProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
//        {
//            base.Visit_AverageOverInlets_OperatorDto(dto);

//            VarsConsts_InputDto inputDto = InputDtoFactory.Get_VarsConsts_InputDto(dto.Inputs);

//            IOperatorDto dto2;

//            if (inputDto.AllAreConst)
//            {
//                dto2 = new AverageOverInlets_OperatorDto_AllConsts { Consts = inputDto.Consts };
//            }
//            else if (inputDto.HasVars)
//            {
//                dto2 = new AverageOverInlets_OperatorDto_Vars { Vars = dto.Vars };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto)
//        {
//            base.Visit_BandPassFilterConstantPeakGain_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
//            {
//                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }
//            else
//            {
//                dto2 = new BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto)
//        {
//            base.Visit_BandPassFilterConstantTransitionGain_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
//            {
//                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }
//            else
//            {
//                dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto(Cache_OperatorDto dto)
//        {
//            base.Visit_Cache_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new Cache_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Block)
//            {
//                dto2 = new Cache_OperatorDto_SingleChannel_Block();
//            }
//            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
//            {
//                dto2 = new Cache_OperatorDto_SingleChannel_Cubic();
//            }
//            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
//            {
//                dto2 = new Cache_OperatorDto_SingleChannel_Hermite();
//            }
//            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
//            {
//                dto2 = new Cache_OperatorDto_SingleChannel_Line();
//            }
//            else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
//            {
//                dto2 = new Cache_OperatorDto_SingleChannel_Stripe();
//            }
//            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Block)
//            {
//                dto2 = new Cache_OperatorDto_MultiChannel_Block();
//            }
//            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
//            {
//                dto2 = new Cache_OperatorDto_MultiChannel_Cubic();
//            }
//            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
//            {
//                dto2 = new Cache_OperatorDto_MultiChannel_Hermite();
//            }
//            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
//            {
//                dto2 = new Cache_OperatorDto_MultiChannel_Line();
//            }
//            else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
//            {
//                dto2 = new Cache_OperatorDto_MultiChannel_Stripe();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_CacheOperatorProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto)
//        {
//            base.Visit_ChangeTrigger_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.PassThroughInput.IsConst && dto.Reset.IsConst)
//            {
//                dto2 = new ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsConst && dto.Reset.IsVar)
//            {
//                dto2 = new ChangeTrigger_OperatorDto_ConstPassThrough_VarReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsVar && dto.Reset.IsConst)
//            {
//                dto2 = new ChangeTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsVar && dto.Reset.IsVar)
//            {
//                dto2 = new ChangeTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto)
//        {
//            base.Visit_ClosestOverDimension_OperatorDto(dto);

//            ClosestOverDimension_OperatorDto dto2;

//            switch (dto.CollectionRecalculationEnum)
//            {
//                case CollectionRecalculationEnum.Continuous:
//                    dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous();
//                    break;

//                case CollectionRecalculationEnum.UponReset:
//                    dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset();
//                    break;

//                default:
//                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
//            }

//            DtoCloner.Clone_ClosestOverDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto)
//        {
//            base.Visit_ClosestOverDimensionExp_OperatorDto(dto);

//            ClosestOverDimensionExp_OperatorDto dto2;

//            switch (dto.CollectionRecalculationEnum)
//            {
//                case CollectionRecalculationEnum.Continuous:
//                    dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous();
//                    break;

//                case CollectionRecalculationEnum.UponReset:
//                    dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset();
//                    break;

//                default:
//                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
//            }

//            DtoCloner.Clone_ClosestOverDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
//        {
//            base.Visit_ClosestOverInlets_OperatorDto(dto);

//            VarsConsts_InputDto itemsMathProperties = InputDtoFactory.Get_VarsConsts_InputDto(dto.Items);

//            IOperatorDto dto2;

//            if (dto.Input.IsConst && itemsMathProperties.AllAreConst)
//            {
//                dto2 = new ClosestOverInlets_OperatorDto_ConstInput_ConstItems { Input = dto.Input, Items = itemsMathProperties.Consts };
//            }
//            else if (dto.Input.IsVar && itemsMathProperties.AllAreConst)
//            {
//                dto2 = new ClosestOverInlets_OperatorDto_VarInput_ConstItems { Input = dto.Input, Items = itemsMathProperties.Consts };
//            }
//            else
//            {
//                dto2 = new ClosestOverInlets_OperatorDto_VarInput_VarItems { Input = dto.Input, Items = dto.Items };
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
//        {
//            base.Visit_ClosestOverInletsExp_OperatorDto(dto);

//            VarsConsts_InputDto itemsMathProperties = InputDtoFactory.Get_VarsConsts_InputDto(dto.Items);

//            IOperatorDto dto2;

//            if (dto.Input.IsConst && itemsMathProperties.AllAreConst)
//            {
//                dto2 = new ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems { Input = dto.Input, Items = itemsMathProperties.Consts };
//            }
//            else if (dto.Input.IsVar && itemsMathProperties.AllAreConst)
//            {
//                dto2 = new ClosestOverInletsExp_OperatorDto_VarInput_ConstItems { Input = dto.Input, Items = itemsMathProperties.Consts };
//            }
//            else
//            {
//                dto2 = new ClosestOverInletsExp_OperatorDto_VarInput_VarItems { Input = dto.Input, Items = dto.Items };
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto)
//        {
//            base.Visit_Curve_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.CurveID == 0)
//            {
//                dto2 = new Curve_OperatorDto_NoCurve();
//            }
//            else
//            {
//                // ReSharper disable once CompareOfFloatsByEqualityOperator
//                bool hasMinX = dto.MinX != 0.0;

//                if (!hasMinX && dto.StandardDimensionEnum == DimensionEnum.Time)
//                {
//                    dto2 = new Curve_OperatorDto_MinXZero_WithOriginShifting();
//                }
//                else if (!hasMinX && dto.StandardDimensionEnum != DimensionEnum.Time)
//                {
//                    dto2 = new Curve_OperatorDto_MinXZero_NoOriginShifting();
//                }
//                else if (hasMinX && dto.StandardDimensionEnum == DimensionEnum.Time)
//                {
//                    dto2 = new Curve_OperatorDto_MinX_WithOriginShifting();
//                }
//                else if (hasMinX && dto.StandardDimensionEnum != DimensionEnum.Time)
//                {
//                    dto2 = new Curve_OperatorDto_MinX_NoOriginShifting();
//                }
//                else
//                {
//                    throw new VisitationCannotBeHandledException();
//                }
//            }

//            DtoCloner.TryClone_CurveProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
//        {
//            base.Visit_Divide_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new Divide_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new Divide_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new Divide_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new Divide_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
//        {
//            base.Visit_Equal_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new Equal_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new Equal_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new Equal_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new Equal_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
//        {
//            base.Visit_GreaterThan_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new GreaterThan_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new GreaterThan_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new GreaterThan_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new GreaterThan_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
//        {
//            base.Visit_GreaterThanOrEqual_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new GreaterThanOrEqual_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new GreaterThanOrEqual_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new GreaterThanOrEqual_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new GreaterThanOrEqual_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
//        {
//            base.Visit_HighPassFilter_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new HighPassFilter_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.MinFrequency.IsConst && dto.BlobVolume.IsConst)
//            {
//                dto2 = new HighPassFilter_OperatorDto_ManyConsts { MinFrequency = dto.MinFrequency, BlobVolume = dto.BlobVolume };
//            }
//            else
//            {
//                dto2 = new HighPassFilter_OperatorDto_AllVars { MinFrequency = dto.MinFrequency, BlobVolume = dto.BlobVolume };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
//        {
//            base.Visit_HighShelfFilter_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new HighShelfFilter_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.TransitionFrequency.IsConst && dto.TransitionSlope.IsConst & dto.DBGain.IsConst)
//            {
//                dto2 = new HighShelfFilter_OperatorDto_ManyConsts { TransitionFrequency = dto.TransitionFrequency, TransitionSlope = dto.TransitionSlope, DBGain = dto.DBGain };
//            }
//            else
//            {
//                dto2 = new HighShelfFilter_OperatorDto_AllVars { TransitionFrequency = dto.TransitionFrequency, TransitionSlope = dto.TransitionSlope, DBGain = dto.DBGain };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto)
//        {
//            base.Visit_Hold_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new Hold_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else
//            {
//                dto2 = new Hold_OperatorDto_VarSignal { Signal = dto.Signal };
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
//        {
//            base.Visit_If_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Condition.IsVar && dto.Then.IsVar && dto.Else.IsVar)
//            {
//                dto2 = new If_OperatorDto_VarCondition_VarThen_VarElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else if (dto.Condition.IsVar && dto.Then.IsVar && dto.Else.IsConst)
//            {
//                dto2 = new If_OperatorDto_VarCondition_VarThen_ConstElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else if (dto.Condition.IsVar && dto.Then.IsConst && dto.Else.IsVar)
//            {
//                dto2 = new If_OperatorDto_VarCondition_ConstThen_VarElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else if (dto.Condition.IsVar && dto.Then.IsConst && dto.Else.IsConst)
//            {
//                dto2 = new If_OperatorDto_VarCondition_ConstThen_ConstElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else if (dto.Condition.IsConst && dto.Then.IsVar && dto.Else.IsVar)
//            {
//                dto2 = new If_OperatorDto_ConstCondition_VarThen_VarElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else if (dto.Condition.IsConst && dto.Then.IsVar && dto.Else.IsConst)
//            {
//                dto2 = new If_OperatorDto_ConstCondition_VarThen_ConstElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else if (dto.Condition.IsConst && dto.Then.IsConst && dto.Else.IsVar)
//            {
//                dto2 = new If_OperatorDto_ConstCondition_ConstThen_VarElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else if (dto.Condition.IsConst && dto.Then.IsConst && dto.Else.IsConst)
//            {
//                dto2 = new If_OperatorDto_ConstCondition_ConstThen_ConstElse { Condition = dto.Condition, Then = dto.Then, Else = dto.Else };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto)
//        {
//            base.Visit_InletsToDimension_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
//            {
//                dto2 = new InletsToDimension_OperatorDto_Block { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
//            {
//                dto2 = new InletsToDimension_OperatorDto_Stripe_LagBehind { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line)
//            {
//                dto2 = new InletsToDimension_OperatorDto_Line { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
//            {
//                dto2 = new InletsToDimension_OperatorDto_CubicEquidistant { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
//            {
//                dto2 = new InletsToDimension_OperatorDto_CubicAbruptSlope { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
//            {
//                dto2 = new InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
//            {
//                dto2 = new InletsToDimension_OperatorDto_Hermite_LagBehind { Vars = dto.Vars, ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto)
//        {
//            base.Visit_Interpolate_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new Interpolate_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
//            {
//                dto2 = new Interpolate_OperatorDto_Block { SamplingRate = dto.SamplingRate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
//            {
//                dto2 = new Interpolate_OperatorDto_Stripe_LagBehind { SamplingRate = dto.SamplingRate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.SamplingRate.IsConst)
//            {
//                dto2 = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate { SamplingRate = dto.SamplingRate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.SamplingRate.IsVar)
//            {
//                dto2 = new Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate { SamplingRate = dto.SamplingRate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
//            {
//                dto2 = new Interpolate_OperatorDto_CubicEquidistant { SamplingRate = dto.SamplingRate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
//            {
//                dto2 = new Interpolate_OperatorDto_CubicAbruptSlope { SamplingRate = dto.SamplingRate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
//            {
//                dto2 = new Interpolate_OperatorDto_CubicSmoothSlope_LagBehind { SamplingRate = dto.SamplingRate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
//            {
//                dto2 = new Interpolate_OperatorDto_Hermite_LagBehind { SamplingRate = dto.SamplingRate };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_InterpolateOperatorProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
//        {
//            base.Visit_LessThan_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new LessThan_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new LessThan_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new LessThan_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new LessThan_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
//        {
//            base.Visit_LessThanOrEqual_OperatorDto(dto);


//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new LessThanOrEqual_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new LessThanOrEqual_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new LessThanOrEqual_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new LessThanOrEqual_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Loop_OperatorDto(Loop_OperatorDto dto)
//        {
//            base.Visit_Loop_OperatorDto(dto);

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool skipEqualsLoopStartMarker = dto.Skip.IsConst && dto.LoopStartMarker.IsConst && dto.Skip.Const == dto.LoopStartMarker.Const;
//            bool noNoteDuration = dto.NoteDuration.IsConst && dto.NoteDuration.Const >= CalculationHelper.VERY_HIGH_VALUE;
//            bool noReleaseEndMarker = dto.ReleaseEndMarker.IsConst && dto.ReleaseEndMarker.Const >= CalculationHelper.VERY_HIGH_VALUE;
//            bool noSkip = dto.Skip.IsConstZero;

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new Loop_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (noSkip && noReleaseEndMarker && dto.LoopStartMarker.IsConst && dto.LoopEndMarker.IsConst)
//            {
//                dto2 = new Loop_OperatorDto_NoSkipOrRelease_ManyConstants
//                {
//                    Signal = dto.Signal,
//                    Skip = dto.Skip,
//                    LoopStartMarker = dto.LoopStartMarker,
//                    LoopEndMarker = dto.LoopEndMarker,
//                    ReleaseEndMarker = dto.ReleaseEndMarker,
//                    NoteDuration = dto.NoteDuration
//                };
//            }
//            else if (dto.Skip.IsConst && dto.LoopStartMarker.IsConst && dto.LoopEndMarker.IsConst && dto.ReleaseEndMarker.IsConst)
//            {
//                dto2 = new Loop_OperatorDto_ManyConstants
//                {
//                    Signal = dto.Signal,
//                    Skip = dto.Skip,
//                    LoopStartMarker = dto.LoopStartMarker,
//                    LoopEndMarker = dto.LoopEndMarker,
//                    ReleaseEndMarker = dto.ReleaseEndMarker,
//                    NoteDuration = dto.NoteDuration
//                };
//            }
//            else if (dto.Skip.IsConst && skipEqualsLoopStartMarker && dto.LoopEndMarker.IsConst && noNoteDuration)
//            {
//                dto2 = new Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration
//                {
//                    Signal = dto.Signal,
//                    Skip = dto.Skip,
//                    LoopStartMarker = dto.LoopStartMarker,
//                    LoopEndMarker = dto.LoopEndMarker,
//                    ReleaseEndMarker = dto.ReleaseEndMarker,
//                    NoteDuration = dto.NoteDuration
//                };
//            }
//            else if (dto.Skip.IsConst && skipEqualsLoopStartMarker && dto.LoopEndMarker.IsVar && noNoteDuration)
//            {
//                dto2 = new Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration
//                {
//                    Signal = dto.Signal,
//                    Skip = dto.Skip,
//                    LoopStartMarker = dto.LoopStartMarker,
//                    LoopEndMarker = dto.LoopEndMarker,
//                    ReleaseEndMarker = dto.ReleaseEndMarker,
//                    NoteDuration = dto.NoteDuration
//                };
//            }
//            else if (noSkip && noReleaseEndMarker)
//            {
//                dto2 = new Loop_OperatorDto_NoSkipOrRelease
//                {
//                    Signal = dto.Signal,
//                    Skip = dto.Skip,
//                    LoopStartMarker = dto.LoopStartMarker,
//                    LoopEndMarker = dto.LoopEndMarker,
//                    ReleaseEndMarker = dto.ReleaseEndMarker,
//                    NoteDuration = dto.NoteDuration
//                };
//            }
//            else
//            {
//                dto2 = new Loop_OperatorDto_AllVars
//                {
//                    Signal = dto.Signal,
//                    Skip = dto.Skip,
//                    LoopStartMarker = dto.LoopStartMarker,
//                    LoopEndMarker = dto.LoopEndMarker,
//                    ReleaseEndMarker = dto.ReleaseEndMarker,
//                    NoteDuration = dto.NoteDuration
//                };
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
//        {
//            base.Visit_LowPassFilter_OperatorDto(dto);


//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new LowPassFilter_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.MaxFrequency.IsConst && dto.BlobVolume.IsConst)
//            {
//                dto2 = new LowPassFilter_OperatorDto_ManyConsts { MaxFrequency = dto.MaxFrequency, BlobVolume = dto.BlobVolume };
//            }
//            else
//            {
//                dto2 = new LowPassFilter_OperatorDto_AllVars { MaxFrequency = dto.MaxFrequency, BlobVolume = dto.BlobVolume };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
//        {
//            base.Visit_LowShelfFilter_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new LowShelfFilter_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.TransitionSlope.IsConst && dto.TransitionSlope.IsConst & dto.DBGain.IsConst)
//            {
//                dto2 = new LowShelfFilter_OperatorDto_ManyConsts { TransitionFrequency = dto.TransitionSlope, TransitionSlope = dto.TransitionSlope, DBGain = dto.DBGain };
//            }
//            else
//            {
//                dto2 = new LowShelfFilter_OperatorDto_AllVars { TransitionFrequency = dto.TransitionSlope, TransitionSlope = dto.TransitionSlope, DBGain = dto.DBGain };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto)
//        {
//            base.Visit_MaxOverDimension_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new MaxOverDimension_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
//            {
//                dto2 = new MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
//            {
//                dto2 = new MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto)
//        {
//            base.Visit_MaxFollower_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new MaxFollower_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else
//            {
//                dto2 = new MaxFollower_OperatorDto_AllVars();
//            }

//            DtoCloner.TryClone_AggregateFollowerProperties(dto, dto2);

//            return dto;
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
//        {
//            base.Visit_MaxOverInlets_OperatorDto(dto);

//            VarsConsts_InputDto inputDto = InputDtoFactory.Get_VarsConsts_InputDto(dto.Inputs);

//            IOperatorDto dto2;

//            if (inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new MaxOverInlets_OperatorDto_Vars_Consts { Vars = inputDto.Vars, Consts = inputDto.Consts };
//            }
//            else if (inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new MaxOverInlets_OperatorDto_Vars_NoConsts { Vars = inputDto.Vars };
//            }
//            else if (!inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new MaxOverInlets_OperatorDto_NoVars_Consts { Consts = inputDto.Consts };
//            }
//            else if (!inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new MaxOverInlets_OperatorDto_NoVars_NoConsts();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto)
//        {
//            base.Visit_MinOverDimension_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new MinOverDimension_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
//            {
//                dto2 = new MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
//            {
//                dto2 = new MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto)
//        {
//            base.Visit_MinFollower_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new MinFollower_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else
//            {
//                dto2 = new MinFollower_OperatorDto_AllVars();
//            }

//            DtoCloner.TryClone_AggregateFollowerProperties(dto, dto2);

//            return dto;
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
//        {
//            base.Visit_MinOverInlets_OperatorDto(dto);

//            VarsConsts_InputDto inputDto = InputDtoFactory.Get_VarsConsts_InputDto(dto.Vars);

//            IOperatorDto dto2;

//            if (inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new MinOverInlets_OperatorDto_Vars_Consts { Vars = inputDto.Vars, Consts = inputDto.Consts };
//            }
//            else if (inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new MinOverInlets_OperatorDto_Vars_NoConsts { Vars = inputDto.Vars };
//            }
//            else if (!inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new MinOverInlets_OperatorDto_NoVars_Consts { Consts = inputDto.Consts };
//            }
//            else if (!inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new MinOverInlets_OperatorDto_NoVars_NoConsts();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
//        {
//            base.Visit_Multiply_OperatorDto(dto);

//            VarsConsts_InputDto inputDto = InputDtoFactory.Get_VarsConsts_InputDto(dto.Inputs);

//            IOperatorDto dto2;

//            if (inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new Multiply_OperatorDto_Vars_Consts { Vars = inputDto.Vars, Consts = inputDto.Consts };
//            }
//            else if (inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new Multiply_OperatorDto_Vars_NoConsts { Vars = inputDto.Vars };
//            }
//            else if (!inputDto.HasVars && inputDto.HasConsts)
//            {
//                dto2 = new Multiply_OperatorDto_NoVars_Consts { Consts = inputDto.Consts };
//            }
//            else if (!inputDto.HasVars && !inputDto.HasConsts)
//            {
//                dto2 = new Multiply_OperatorDto_NoVars_NoConsts();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
//        {
//            base.Visit_Negative_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Number.IsConst)
//            {
//                dto2 = new Negative_OperatorDto_ConstNumber { Number = dto.Number };
//            }
//            else if (dto.Number.IsVar)
//            {
//                dto2 = new Negative_OperatorDto_VarNumber { Number = dto.Number };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
//        {
//            base.Visit_Not_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Number.IsConst)
//            {
//                dto2 = new Not_OperatorDto_ConstNumber { Number = dto.Number };
//            }
//            else if (dto.Number.IsVar)
//            {
//                dto2 = new Not_OperatorDto_VarNumber { Number = dto.Number };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
//        {
//            base.Visit_NotchFilter_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new NotchFilter_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
//            {
//                dto2 = new NotchFilter_OperatorDto_ManyConsts { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }
//            else
//            {
//                dto2 = new NotchFilter_OperatorDto_AllVars { CenterFrequency = dto.CenterFrequency, Width = dto.Width };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
//        {
//            base.Visit_NotEqual_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new NotEqual_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new NotEqual_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new NotEqual_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new NotEqual_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
//        {
//            base.Visit_Or_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new Or_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new Or_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new Or_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new Or_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
//        {
//            base.Visit_PeakingEQFilter_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new PeakingEQFilter_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else if (dto.CenterFrequency.IsConst && dto.Width.IsConst & dto.DBGain.IsConst)
//            {
//                dto2 = new PeakingEQFilter_OperatorDto_ManyConsts { CenterFrequency = dto.CenterFrequency, Width = dto.Width, DBGain = dto.DBGain };
//            }
//            else
//            {
//                dto2 =new PeakingEQFilter_OperatorDto_AllVars { CenterFrequency = dto.CenterFrequency, Width = dto.Width, DBGain = dto.DBGain };
//            }

//            DtoCloner.TryClone_FilterProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
//        {
//            base.Visit_Power_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Base.IsConst && dto.Exponent.IsConst)
//            {
//                dto2 = new Power_OperatorDto_ConstBase_ConstExponent { Base = dto.Base, Exponent = dto.Exponent };
//            }
//            else if (dto.Base.IsVar && dto.Exponent.IsConst)
//            {
//                dto2 = new Power_OperatorDto_VarBase_ConstExponent { Base = dto.Base, Exponent = dto.Exponent };
//            }
//            else if (dto.Base.IsConst && dto.Exponent.IsVar)
//            {
//                dto2 = new Power_OperatorDto_ConstBase_VarExponent { Base = dto.Base, Exponent = dto.Exponent };
//            }
//            else if (dto.Base.IsVar && dto.Exponent.IsVar)
//            {
//                dto2 = new Power_OperatorDto_VarBase_VarExponent { Base = dto.Base, Exponent = dto.Exponent };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto(Pulse_OperatorDto dto)
//        {
//            base.Visit_Pulse_OperatorDto(dto);

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isHalfWidth = dto.Width.IsConst && dto.Width.Const == 0.5;

//            IOperatorDto dto2;

//            if (dto.Frequency.IsConstZero)
//            {
//                dto2 = new Pulse_OperatorDto_ZeroFrequency();
//            }
//            else if (dto.Frequency.IsConst && isHalfWidth && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.Width.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else if (dto.Frequency.IsConst && dto.Width.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else if (dto.Frequency.IsVar && isHalfWidth && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsVar && dto.Width.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else if (dto.Frequency.IsVar && dto.Width.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else if (dto.Frequency.IsConst && isHalfWidth && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.Width.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else if (dto.Frequency.IsConst && dto.Width.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else if (dto.Frequency.IsVar && isHalfWidth && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsVar && dto.Width.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else if (dto.Frequency.IsVar && dto.Width.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking { Frequency = dto.Frequency, Width = dto.Width };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
//        {
//            base.Visit_PulseTrigger_OperatorDto(dto);


//            IOperatorDto dto2;

//            if (dto.PassThroughInput.IsConst && dto.Reset.IsConst)
//            {
//                dto2 = new PulseTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsConst && dto.Reset.IsVar)
//            {
//                dto2 = new PulseTrigger_OperatorDto_ConstPassThrough_VarReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsVar && dto.Reset.IsConst)
//            {
//                dto2 = new PulseTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsVar && dto.Reset.IsVar)
//            {
//                dto2 = new PulseTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto(Random_OperatorDto dto)
//        {
//            base.Visit_Random_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
//            {
//                dto2 = new Random_OperatorDto_Block { Rate = dto.Rate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
//            {
//                dto2 = new Random_OperatorDto_Stripe_LagBehind { Rate = dto.Rate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.Rate.IsConst)
//            {
//                dto2 = new Random_OperatorDto_Line_LagBehind_ConstRate { Rate = dto.Rate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.Rate.IsVar)
//            {
//                dto2 = new Random_OperatorDto_Line_LagBehind_VarRate { Rate = dto.Rate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
//            {
//                dto2 = new Random_OperatorDto_CubicEquidistant { Rate = dto.Rate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
//            {
//                dto2 = new Random_OperatorDto_CubicAbruptSlope { Rate = dto.Rate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
//            {
//                dto2 = new Random_OperatorDto_CubicSmoothSlope_LagBehind { Rate = dto.Rate };
//            }
//            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
//            {
//                dto2 = new Random_OperatorDto_Hermite_LagBehind { Rate = dto.Rate };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_RandomOperatorProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto)
//        {
//            base.Visit_RangeOverDimension_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.From.IsConst && dto.Till.IsConst && dto.Step.IsConst)
//            {
//                dto2 = new RangeOverDimension_OperatorDto_OnlyConsts
//                {
//                    From = dto.From,
//                    Till = dto.Till,
//                    Step = dto.Step,
//                    StandardDimensionEnum = dto.StandardDimensionEnum,
//                    CanonicalCustomDimensionName = dto.CanonicalCustomDimensionName
//                };
//            }
//            else
//            {
//                dto2 = new RangeOverDimension_OperatorDto_OnlyVars
//                {
//                    From = dto.From,
//                    Till = dto.Till,
//                    Step = dto.Step,
//                    StandardDimensionEnum = dto.StandardDimensionEnum,
//                    CanonicalCustomDimensionName = dto.CanonicalCustomDimensionName
//                };
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto)
//        {
//            base.Visit_RangeOverOutlets_Outlet_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Step.IsConstZero)
//            {
//                dto2 = new RangeOverOutlets_Outlet_OperatorDto_ZeroStep { From = dto.From };
//            }
//            else if (dto.From.IsConst && dto.Step.IsConst)
//            {
//                dto2 = new RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep { From = dto.From, Step = dto.Step, OutletPosition = dto.OutletPosition };
//            }
//            else if (dto.From.IsVar && dto.Step.IsConst)
//            {
//                dto2 = new RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep { From = dto.From, Step = dto.Step, OutletPosition = dto.OutletPosition };
//            }
//            else if (dto.From.IsConst && dto.Step.IsVar)
//            {
//                dto2 = new RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep { From = dto.From, Step = dto.Step, OutletPosition = dto.OutletPosition };
//            }
//            else if (dto.From.IsVar && dto.Step.IsVar)
//            {
//                dto2 = new RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep { From = dto.From, Step = dto.Step, OutletPosition = dto.OutletPosition };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Reverse_OperatorDto(Reverse_OperatorDto dto)
//        {
//            base.Visit_Reverse_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new Reverse_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (dto.Factor.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Reverse_OperatorDto_VarFactor_WithPhaseTracking { Signal = dto.Signal, Factor = dto.Factor };
//            }
//            else if (dto.Factor.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Reverse_OperatorDto_VarFactor_NoPhaseTracking { Signal = dto.Signal, Factor = dto.Factor };
//            }
//            else if (dto.Factor.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Reverse_OperatorDto_ConstFactor_WithOriginShifting { Signal = dto.Signal, Factor = dto.Factor };
//            }
//            else if (dto.Factor.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Reverse_OperatorDto_ConstFactor_NoOriginShifting { Signal = dto.Signal, Factor = dto.Factor };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto)
//        {
//            base.Visit_Round_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst && dto.Step.IsConst && dto.Offset.IsConst)
//            {
//                dto2 = new Round_OperatorDto_AllConsts { Signal = dto.Signal, Step = dto.Step, Offset = dto.Offset };
//            }
//            else if (dto.Signal.IsConst)
//            {
//                dto2 = new Round_OperatorDto_ConstSignal { Signal = dto.Signal, Step = dto.Step, Offset = dto.Offset };
//            }
//            else if (dto.Signal.IsVar && dto.Step.IsConstOne && dto.Offset.IsConstZero)
//            {
//                dto2 = new Round_OperatorDto_VarSignal_StepOne_OffsetZero { Signal = dto.Signal };
//            }
//            else if (dto.Signal.IsVar && dto.Step.IsVar && dto.Offset.IsVar)
//            {
//                dto2 = new Round_OperatorDto_VarSignal_VarStep_VarOffset { Signal = dto.Signal, Step = dto.Step, Offset = dto.Offset };
//            }
//            else if (dto.Signal.IsVar && dto.Step.IsVar && dto.Offset.IsConstZero)
//            {
//                dto2 = new Round_OperatorDto_VarSignal_VarStep_ZeroOffset { Signal = dto.Signal, Step = dto.Step };
//            }
//            else if (dto.Signal.IsVar && dto.Step.IsVar && dto.Offset.IsConst)
//            {
//                dto2 = new Round_OperatorDto_VarSignal_VarStep_ConstOffset { Signal = dto.Signal, Step = dto.Step, Offset = dto.Offset };
//            }
//            else if (dto.Signal.IsVar && dto.Step.IsConst && dto.Offset.IsVar)
//            {
//                dto2 = new Round_OperatorDto_VarSignal_ConstStep_VarOffset { Signal = dto.Signal, Step = dto.Step, Offset = dto.Offset };
//            }
//            else if (dto.Signal.IsVar && dto.Step.IsConst && dto.Offset.IsConstZero)
//            {
//                dto2 = new Round_OperatorDto_VarSignal_ConstStep_ZeroOffset { Signal = dto.Signal, Step = dto.Step };
//            }
//            else if (dto.Signal.IsVar && dto.Step.IsConst && dto.Offset.IsConst)
//            {
//                dto2 = new Round_OperatorDto_VarSignal_ConstStep_ConstOffset { Signal = dto.Signal, Step = dto.Step, Offset = dto.Offset };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto(Sample_OperatorDto dto)
//        {
//            int sampleChannelCount = dto.SampleChannelCount;
//            bool hasTargetChannelCount = sampleChannelCount == dto.TargetChannelCount;
//            bool isFromMonoToStereo = sampleChannelCount == 1 && dto.TargetChannelCount == 2;
//            bool isFromStereoToMono = sampleChannelCount == 2 && dto.TargetChannelCount == 1;

//            IOperatorDto dto2;

//            if (dto.SampleID == 0)
//            {
//                dto2 = new Sample_OperatorDto_NoSample();
//            }
//            else if (dto.Frequency.IsConstZero)
//            {
//                dto2 = new Sample_OperatorDto_ZeroFrequency();
//            }
//            else if (hasTargetChannelCount && dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (hasTargetChannelCount && dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (hasTargetChannelCount && dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (hasTargetChannelCount && dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (isFromMonoToStereo && dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (isFromMonoToStereo && dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (isFromMonoToStereo && dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (isFromMonoToStereo && dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (isFromStereoToMono && dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (isFromStereoToMono && dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (isFromStereoToMono && dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (isFromStereoToMono && dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_SampleProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_SawDown_OperatorDto(SawDown_OperatorDto dto)
//        {
//            base.Visit_SawDown_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Frequency.IsConstZero)
//            {
//                dto2 = new SawDown_OperatorDto_ZeroFrequency();
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new SawDown_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new SawDown_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new SawDown_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new SawDown_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_SawUp_OperatorDto(SawUp_OperatorDto dto)
//        {
//            base.Visit_SawUp_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Frequency.IsConstZero)
//            {
//                dto2 = new SawUp_OperatorDto_ZeroFrequency();
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new SawUp_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new SawUp_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new SawUp_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new SawUp_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }
        
//        protected override IOperatorDto Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto)
//        {
//            base.Visit_SetDimension_OperatorDto(dto);

//            OperatorDtoBase_WithDimension dto2;

//            if (dto.PassThrough.IsConst && dto.Number.IsConst)
//            {
//                dto2 = new SetDimension_OperatorDto_ConstPassThrough_ConstNumber { PassThrough = dto.PassThrough, Number = dto.Number };
//            }
//            else if (dto.PassThrough.IsVar && dto.Number.IsConst)
//            {
//                dto2 = new SetDimension_OperatorDto_VarPassThrough_ConstNumber { PassThrough = dto.PassThrough, Number = dto.Number };
//            }
//            else if (dto.PassThrough.IsConst && dto.Number.IsVar)
//            {
//                dto2 = new SetDimension_OperatorDto_ConstPassThrough_VarNumber { PassThrough = dto.PassThrough, Number = dto.Number };
//            }
//            else if (dto.PassThrough.IsVar && dto.Number.IsVar)
//            {
//                dto2 = new SetDimension_OperatorDto_VarPassThrough_VarNumber { PassThrough = dto.PassThrough, Number = dto.Number };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto)
//        {
//            base.Visit_Sine_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Frequency.IsConstZero)
//            {
//                dto2 = new Sine_OperatorDto_ZeroFrequency();
//            } 
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sine_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sine_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Sine_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Sine_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto)
//        {
//            base.Visit_SortOverDimension_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst)
//            {
//                dto2 = new SortOverDimension_OperatorDto_ConstSignal { Signal = dto.Signal };
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
//            {
//                dto2 = new SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
//            {
//                dto2 = new SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto)
//        {
//            base.Visit_Spectrum_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Sound.IsConst)
//            {
//                dto2 = new Spectrum_OperatorDto_ConstSound { Sound = dto.Sound };
//            }
//            else
//            {
//                dto2 = new Spectrum_OperatorDto_AllVars
//                {
//                    Sound = dto.Sound,
//                    Start = dto.Start,
//                    End = dto.End,
//                    FrequencyCount = dto.FrequencyCount
//                };
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Square_OperatorDto(Square_OperatorDto dto)
//        {
//            base.Visit_Square_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Frequency.IsConstZero)
//            {
//                dto2 = new Square_OperatorDto_ZeroFrequency();
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Square_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Square_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Square_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Square_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto)
//        {
//            base.Visit_Squash_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                if (dto.Signal.IsVar && dto.Factor.IsVar)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else
//                {
//                    throw new VisitationCannotBeHandledException();
//                }
//            }
//            else
//            {
//                if (dto.Signal.IsVar && dto.Factor.IsVar && dto.Origin.IsVar)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsVar && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsVar && dto.Origin.IsConst)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst && dto.Origin.IsVar)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst && dto.Origin.IsConst)
//                {
//                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar && dto.Origin.IsVar)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar && dto.Origin.IsConst)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst && dto.Origin.IsVar)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst && dto.Origin.IsConst)
//                {
//                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else
//                {
//                    throw new VisitationCannotBeHandledException();
//                }
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto(Stretch_OperatorDto dto)
//        {
//            base.Visit_Stretch_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                if (dto.Signal.IsVar && dto.Factor.IsVar)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else
//                {
//                    throw new VisitationCannotBeHandledException();
//                }
//            }
//            else
//            {
//                if (dto.Signal.IsVar && dto.Factor.IsVar && dto.Origin.IsVar)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsVar && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsVar && dto.Origin.IsConst)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst && dto.Origin.IsVar)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsVar && dto.Factor.IsConst && dto.Origin.IsConst)
//                {
//                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar && dto.Origin.IsVar)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsVar && dto.Origin.IsConst)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst && dto.Origin.IsVar)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst && dto.Origin.IsConstZero)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin { Signal = dto.Signal, Factor = dto.Factor };
//                }
//                else if (dto.Signal.IsConst && dto.Factor.IsConst && dto.Origin.IsConst)
//                {
//                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin { Signal = dto.Signal, Factor = dto.Factor, Origin = dto.Origin };
//                }
//                else
//                {
//                    throw new VisitationCannotBeHandledException();
//                }
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
//        {
//            base.Visit_Subtract_OperatorDto(dto);


//            IOperatorDto dto2;

//            if (dto.A.IsConst && dto.B.IsConst)
//            {
//                dto2 = new Subtract_OperatorDto_ConstA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsConst)
//            {
//                dto2 = new Subtract_OperatorDto_VarA_ConstB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsConst && dto.B.IsVar)
//            {
//                dto2 = new Subtract_OperatorDto_ConstA_VarB { A = dto.A, B = dto.B };
//            }
//            else if (dto.A.IsVar && dto.B.IsVar)
//            {
//                dto2 = new Subtract_OperatorDto_VarA_VarB { A = dto.A, B = dto.B };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto)
//        {
//            base.Visit_SumOverDimension_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst && dto.From.IsConst && dto.Till.IsConst && dto.Step.IsConst)
//            {
//                dto2 = new SumOverDimension_OperatorDto_AllConsts { Signal = dto.Signal, From = dto.From, Till = dto.Till, Step = dto.Step };
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
//            {
//                dto2 = new SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous();
//            }
//            else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
//            {
//                dto2 = new SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset();
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_AggregateOverDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_SumFollower_OperatorDto(SumFollower_OperatorDto dto)
//        {
//            base.Visit_SumFollower_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Signal.IsConst && dto.SampleCount.IsConst)
//            {
//                dto2 = new SumFollower_OperatorDto_ConstSignal_ConstSampleCount { Signal = dto.Signal, SampleCount = dto.SampleCount };
//            }
//            else if (dto.Signal.IsConst && dto.SampleCount.IsVar)
//            {
//                dto2 = new SumFollower_OperatorDto_ConstSignal_VarSampleCount { Signal = dto.Signal, SampleCount = dto.SampleCount };
//            }
//            else
//            {
//                dto2 = new SumFollower_OperatorDto_AllVars { Signal = dto.Signal, SliceLength = dto.SliceLength, SampleCount = dto.SampleCount };
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
//        {
//            base.Visit_ToggleTrigger_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.PassThroughInput.IsConst && dto.Reset.IsConst)
//            {
//                dto2 = new ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsConst && dto.Reset.IsVar)
//            {
//                dto2 = new ToggleTrigger_OperatorDto_ConstPassThrough_VarReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsVar && dto.Reset.IsConst)
//            {
//                dto2 = new ToggleTrigger_OperatorDto_VarPassThrough_ConstReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else if (dto.PassThroughInput.IsVar && dto.Reset.IsVar)
//            {
//                dto2 = new ToggleTrigger_OperatorDto_VarPassThrough_VarReset { PassThroughInput = dto.PassThroughInput, Reset = dto.Reset };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Triangle_OperatorDto(Triangle_OperatorDto dto)
//        {
//            base.Visit_Triangle_OperatorDto(dto);

//            IOperatorDto dto2;

//            if (dto.Frequency.IsConstZero)
//            {
//                dto2 = new Triangle_OperatorDto_ZeroFrequency();
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Triangle_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Triangle_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
//            {
//                dto2 = new Triangle_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = dto.Frequency };
//            }
//            else if (dto.Frequency.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
//            {
//                dto2 = new Triangle_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = dto.Frequency };
//            }
//            else
//            {
//                throw new VisitationCannotBeHandledException();
//            }

//            DtoCloner.TryClone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }
//    }
//}

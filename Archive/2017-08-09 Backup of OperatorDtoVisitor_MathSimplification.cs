﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Calculation;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Collections;
//using JJ.Framework.Mathematics;
//// ReSharper disable RedundantIfElseBlock

//namespace JJ.Business.Synthesizer.Visitors
//{
//    /// <summary>
//    /// Also takes care of the ClassSpecialization, since it derives from OperatorDtoVisitor_ClassSpecializationBase.
//    /// </summary>
//    internal class OperatorDtoVisitor_MathSimplification : OperatorDtoVisitor_ClassSpecializationBase
//    {
//        private delegate void SetFilterParametersWithWidthOrBlobVolumeDelegate(
//            double samplingRate, double limitedFrequency, double widthOrBlobVolume, 
//            out double a0, out double a1, out double a2, out double a3, out double a4);

//        private delegate void SetShelfFilterParametersDelegate(
//            double samplingRate, double limitedFrequency, double transitionSlope, double dbGain,
//            out double a0, out double a1, out double a2, out double a3, out double a4);

//        // General

//        public IOperatorDto Execute(IOperatorDto dto) => Visit_OperatorDto_Polymorphic(dto);

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            // NaN / Infinity

//            // Depth-first, so deeply pre-calculated NaN's can be picked up.
//            IOperatorDto dto2 = base.Visit_OperatorDto_Polymorphic(dto);

//            bool anyInputsHaveSpecialValue = dto2.InputOperatorDtos.Any(x => InputDtoFactory.CreateInputDto(x).IsConstSpecialValue);
//            if (anyInputsHaveSpecialValue)
//            {
//                return new Number_OperatorDto_NaN();
//            }

//            return dto2;
//        }

//        // Absolute

//        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
//        {
//            base.Visit_Absolute_OperatorDto(dto);

//            if (dto.Number.IsConst)
//            {
//                // Pre-calculate
//                return new Number_OperatorDto { Number = Math.Abs(dto.Number.Const) };
//            }

//            return dto;
//        }

//        // Add

//        protected override IOperatorDto Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
//        {
//            return Process_NoVars_Consts(dto, Enumerable.Sum);
//        }

//        protected override IOperatorDto Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
//        {
//            return Process_NoVars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
//        {
//            return Process_Vars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
//        {
//            base.Visit_Add_OperatorDto_Vars_Consts(dto);

//            // Pre-calculate
//            double constValue = dto.Consts.Select(x => x.Const).Sum();

//            return new Add_OperatorDto_Vars_1Const { Vars = dto.Vars, Const = InputDtoFactory.CreateInputDto(constValue), OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
//        {
//            base.Visit_Add_OperatorDto_Vars_1Const(dto);

//            InputDto constMathProperties = InputDtoFactory.CreateInputDto(dto.Const.Const);

//            // Identity
//            if (constMathProperties.IsConstZero)
//            {
//                return new Add_OperatorDto_Vars_NoConsts { Vars = dto.Vars, OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        // AllPassFilter

//        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_AllVars(AllPassFilter_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_ConstSound(AllPassFilter_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_ManyConsts(AllPassFilter_OperatorDto_ManyConsts dto)
//        {
//            return Process_Filter_ManyConsts_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetAllPassFilterVariables);
//        }

//        // And

//        protected override IOperatorDto Visit_And_OperatorDto_ConstA_ConstB(And_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_And_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            if (dto.A.IsConstNonZero && dto.B.IsConstNonZero)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else if (dto.A.IsConstZero || dto.B.IsConstZero)
//            {
//                return new Number_OperatorDto_Zero();
//            }

//            throw new VisitationCannotBeHandledException();
//        }

//        protected override IOperatorDto Visit_And_OperatorDto_ConstA_VarB(And_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_And_OperatorDto_ConstA_VarB(dto);

//            // Commute
//            return new And_OperatorDto_VarA_ConstB { A = dto.B, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_And_OperatorDto_VarA_ConstB(And_OperatorDto_VarA_ConstB dto)
//        {
//            base.Visit_And_OperatorDto_VarA_ConstB(dto);

//            if (dto.B.IsConstZero)
//            {
//                // 0
//                return new Number_OperatorDto_Zero();
//            }
//            else if (dto.B.IsConstNonZero)
//            {
//                // Identity
//                return dto.A.Var;
//            }

//            throw new VisitationCannotBeHandledException();
//        }

//        protected override IOperatorDto Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // AverageFollower

//        protected override IOperatorDto Visit_AverageFollower_OperatorDto_AllVars(AverageFollower_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_AverageFollower_OperatorDto_ConstSignal(AverageFollower_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // AverageOverDimension

//        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_ConstSignal(AverageOverDimension_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // AverageOverInlets

//        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto_AllConsts(AverageOverInlets_OperatorDto_AllConsts dto)
//        {
//            return Process_NoVars_Consts(dto, Enumerable.Average);
//        }

//        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto_Vars(AverageOverInlets_OperatorDto_Vars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // BandPassFilterConstantPeakGain

//        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth(BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth dto)
//        {
//            return Process_Filter_ManyConsts_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetBandPassFilterConstantPeakGainVariables);
//        }

//        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstSound(BandPassFilterConstantPeakGain_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth(BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // BandPassFilterConstantTransitionGain

//        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstSound(BandPassFilterConstantTransitionGain_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth(BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth dto)
//        {
//            return Process_Filter_ManyConsts_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetBandPassFilterConstantTransitionGainVariables);
//        }

//        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth(BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Cache

//        protected override IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Block(Cache_OperatorDto_MultiChannel_Block dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Cubic(Cache_OperatorDto_MultiChannel_Cubic dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Hermite(Cache_OperatorDto_MultiChannel_Hermite dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Line(Cache_OperatorDto_MultiChannel_Line dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Stripe(Cache_OperatorDto_MultiChannel_Stripe dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Block(Cache_OperatorDto_SingleChannel_Block dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Cubic(Cache_OperatorDto_SingleChannel_Cubic dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Hermite(Cache_OperatorDto_SingleChannel_Hermite dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Line(Cache_OperatorDto_SingleChannel_Line dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Stripe(Cache_OperatorDto_SingleChannel_Stripe dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // ChangeTrigger

//        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset(ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
//        {
//            return Process_Trigger_ConstPassThrough_ConstReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto_ConstPassThrough_VarReset(ChangeTrigger_OperatorDto_ConstPassThrough_VarReset dto)
//        {
//            return Process_Trigger_ConstPassThrough_VarReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto_VarPassThrough_ConstReset(ChangeTrigger_OperatorDto_VarPassThrough_ConstReset dto)
//        {
//            return Process_Trigger_VarPassThrough_ConstReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto_VarPassThrough_VarReset(ChangeTrigger_OperatorDto_VarPassThrough_VarReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // ClosestOverDimension

//        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // ClosestOverDimensionExp

//        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // ClosestOverInletsExp

//        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems dto)
//        {
//            base.Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(dto);

//            // Pre-calculate
//            double result = AggregateCalculator.ClosestExp(dto.Input, dto.Items);
//            return new Number_OperatorDto { Number = result };
//        }

//        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
//        {
//            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(dto);

//            if (dto.Items.Count == 0)
//            {
//                // 0
//                return new Number_OperatorDto_Zero();
//            }
//            else if (dto.Items.Count == 1)
//            {
//                // Identity
//                return new Number_OperatorDto { Number = dto.Items[0] };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // ClosestOverInlets

//        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(ClosestOverInlets_OperatorDto_ConstInput_ConstItems dto)
//        {
//            base.Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(dto);

//            // Pre-calculate
//            double result = AggregateCalculator.Closest(dto.Input, dto.Items);
//            return new Number_OperatorDto { Number = result };
//        }

//        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
//        {
//            base.Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(dto);

//            if (dto.Items.Count == 0)
//            {
//                // 0
//                return new Number_OperatorDto_Zero();
//            }
//            if (dto.Items.Count == 1)
//            {
//                // Identity
//                return new Number_OperatorDto { Number = dto.Items[0] };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Curve

//        protected override IOperatorDto Visit_Curve_OperatorDto_NoCurve(Curve_OperatorDto_NoCurve dto)
//        {
//            base.Visit_Curve_OperatorDto_NoCurve(dto);

//            // 0
//            return new Number_OperatorDto_Zero();
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // DimensionToOutlets

//        protected override IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Divide

//        protected override IOperatorDto Visit_Divide_OperatorDto_ConstA_ConstB(Divide_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_Divide_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            return new Number_OperatorDto { Number = dto.A.Const / dto.B.Const };
//        }

//        protected override IOperatorDto Visit_Divide_OperatorDto_ConstA_VarB(Divide_OperatorDto_ConstA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_ConstB(Divide_OperatorDto_VarA_ConstB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_VarB(Divide_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Equal

//        protected override IOperatorDto Visit_Equal_OperatorDto_ConstA_ConstB(Equal_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_Equal_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            if (dto.A == dto.B)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else
//            {
//                return new Number_OperatorDto_Zero();
//            }
//        }

//        protected override IOperatorDto Visit_Equal_OperatorDto_ConstA_VarB(Equal_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_Equal_OperatorDto_ConstA_VarB(dto);

//            // Commute
//            return new Equal_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_Equal_OperatorDto_VarA_ConstB(Equal_OperatorDto_VarA_ConstB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Equal_OperatorDto_VarA_VarB(Equal_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // GetDimension

//        protected override IOperatorDto Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
//        {
//            switch (dto.StandardDimensionEnum)
//            {
//                case DimensionEnum.SamplingRate:
//                    return new Number_OperatorDto { Number = dto.SamplingRate, OperatorID = dto.OperatorID };

//                case DimensionEnum.HighestFrequency:
//                    return new Number_OperatorDto { Number = dto.SamplingRate / 2.0, OperatorID = dto.OperatorID };
//            }

//            return Process_Nothing(dto);
//        }

//        // GreaterThanOrEqual

//        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_ConstA_ConstB(GreaterThanOrEqual_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_GreaterThanOrEqual_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            if (dto.A >= dto.B)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else
//            {
//                return new Number_OperatorDto_Zero();
//            }
//        }

//        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_ConstA_VarB(GreaterThanOrEqual_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_GreaterThanOrEqual_OperatorDto_ConstA_VarB(dto);

//            // Commute, switch sign
//            return new LessThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(GreaterThanOrEqual_OperatorDto_VarA_ConstB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(GreaterThanOrEqual_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // GreaterThan

//        protected override IOperatorDto Visit_GreaterThan_OperatorDto_ConstA_ConstB(GreaterThan_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_GreaterThan_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            if (dto.A > dto.B)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else
//            {
//                return new Number_OperatorDto_Zero();
//            }
//        }

//        protected override IOperatorDto Visit_GreaterThan_OperatorDto_ConstA_VarB(GreaterThan_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_GreaterThan_OperatorDto_ConstA_VarB(dto);

//            // Commute, switch sign
//            return new LessThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_GreaterThan_OperatorDto_VarA_ConstB(GreaterThan_OperatorDto_VarA_ConstB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_GreaterThan_OperatorDto_VarA_VarB(GreaterThan_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // HighPassFilter

//        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_AllVars(HighPassFilter_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_ConstSound(HighPassFilter_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_ManyConsts(HighPassFilter_OperatorDto_ManyConsts dto)
//        {
//            return Process_Filter_ManyConsts_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetHighPassFilterVariables);
//        }

//        // HighShelfFilter

//        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_AllVars(HighShelfFilter_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_ConstSound(HighShelfFilter_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_ManyConsts(HighShelfFilter_OperatorDto_ManyConsts dto)
//        {
//            return ProcessShelfFilter_ManyConsts(dto, BiQuadFilterWithoutFields.SetHighShelfFilterVariables);
//        }

//        // Hold

//        protected override IOperatorDto Visit_Hold_OperatorDto_VarSignal(Hold_OperatorDto_VarSignal dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Hold_OperatorDto_ConstSignal(Hold_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // If

//        protected override IOperatorDto Visit_If_OperatorDto_ConstCondition_ConstThen_ConstElse(If_OperatorDto_ConstCondition_ConstThen_ConstElse dto)
//        {
//            base.Visit_If_OperatorDto_ConstCondition_ConstThen_ConstElse(dto);

//            // Pre-calculate
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = dto.Condition != 0.0;
//            if (isTrue)
//            {
//                return new Number_OperatorDto { Number = dto.Then };
//            }
//            else
//            {
//                return new Number_OperatorDto { Number = dto.Else };
//            }
//        }

//        protected override IOperatorDto Visit_If_OperatorDto_ConstCondition_ConstThen_VarElse(If_OperatorDto_ConstCondition_ConstThen_VarElse dto)
//        {
//            base.Visit_If_OperatorDto_ConstCondition_ConstThen_VarElse(dto);

//            // Pre-calculate
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = dto.Condition != 0.0;
//            if (isTrue)
//            {
//                return new Number_OperatorDto { Number = dto.Then };
//            }
//            else
//            {
//                return dto.ElseOperatorDto;
//            }
//        }

//        protected override IOperatorDto Visit_If_OperatorDto_ConstCondition_VarThen_ConstElse(If_OperatorDto_ConstCondition_VarThen_ConstElse dto)
//        {
//            base.Visit_If_OperatorDto_ConstCondition_VarThen_ConstElse(dto);

//            // Pre-calculate
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = dto.Condition != 0.0;
//            if (isTrue)
//            {
//                return dto.ThenOperatorDto;
//            }
//            else
//            {
//                return new Number_OperatorDto { Number = dto.Else };
//            }
//        }

//        protected override IOperatorDto Visit_If_OperatorDto_ConstCondition_VarThen_VarElse(If_OperatorDto_ConstCondition_VarThen_VarElse dto)
//        {
//            base.Visit_If_OperatorDto_ConstCondition_VarThen_VarElse(dto);

//            // Pre-calculate
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = dto.Condition != 0.0;
//            if (isTrue)
//            {
//                return dto.ThenOperatorDto;
//            }
//            else
//            {
//                return dto.ElseOperatorDto;
//            }
//        }

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
//        {
//            base.Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(dto);

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            if (dto.Then == dto.Else)
//            {
//                // Identity
//                return new Number_OperatorDto { Number = dto.Then };
//            }
//            else
//            {
//                return dto;
//            }
//        }

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // InletsToDimension

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicAbruptSlope(InletsToDimension_OperatorDto_CubicAbruptSlope dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicEquidistant(InletsToDimension_OperatorDto_CubicEquidistant dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind(InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Hermite_LagBehind(InletsToDimension_OperatorDto_Hermite_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Interpolate

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // LessThanOrEqual

//        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_ConstA_ConstB(LessThanOrEqual_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_LessThanOrEqual_OperatorDto_ConstA_ConstB(dto);

//            if (dto.A <= dto.B)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else
//            {
//                return new Number_OperatorDto_Zero();
//            }
//        }

//        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_ConstA_VarB(LessThanOrEqual_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_LessThanOrEqual_OperatorDto_ConstA_VarB(dto);

//            // Commute, switch sign
//            return new GreaterThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(LessThanOrEqual_OperatorDto_VarA_ConstB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_VarA_VarB(LessThanOrEqual_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // LessThan

//        protected override IOperatorDto Visit_LessThan_OperatorDto_ConstA_ConstB(LessThan_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_LessThan_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            if (dto.A < dto.B)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else
//            {
//                return new Number_OperatorDto_Zero();
//            }
//        }

//        protected override IOperatorDto Visit_LessThan_OperatorDto_ConstA_VarB(LessThan_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_LessThan_OperatorDto_ConstA_VarB(dto);

//            // Commute, switch sign
//            return new GreaterThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_LessThan_OperatorDto_VarA_ConstB(LessThan_OperatorDto_VarA_ConstB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_LessThan_OperatorDto_VarA_VarB(LessThan_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Loop

//        protected override IOperatorDto Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSignal(Loop_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // LowPassFilter

//        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_ConstSound(LowPassFilter_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_AllVars(LowPassFilter_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_ManyConsts(LowPassFilter_OperatorDto_ManyConsts dto)
//        {
//            return Process_Filter_ManyConsts_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetLowPassFilterVariables);
//        }

//        // LowShelfFilter

//        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_ConstSound(LowShelfFilter_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_AllVars(LowShelfFilter_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_ManyConsts(LowShelfFilter_OperatorDto_ManyConsts dto)
//        {
//            double limitedFrequency = LimitFrequency(dto.Frequency, dto.NyquistFrequency);

//            BiQuadFilterWithoutFields.SetLowShelfFilterVariables(
//                dto.SamplingRate, limitedFrequency, dto.TransitionSlope, dto.DBGain,
//                out double a0, out double a1, out double a2, out double a3, out double a4);

//            dto.A0 = a0;
//            dto.A1 = a1;
//            dto.A2 = a2;
//            dto.A3 = a3;
//            dto.A4 = a4;

//            return dto;
//        }

//        // MaxFollower

//        protected override IOperatorDto Visit_MaxFollower_OperatorDto_AllVars(MaxFollower_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MaxFollower_OperatorDto_ConstSignal(MaxFollower_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // MaxOverDimension

//        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // MaxOverInlets

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_Consts(MaxOverInlets_OperatorDto_NoVars_Consts dto)
//        {
//            return Process_NoVars_Consts(dto, Enumerable.Max);
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts(MaxOverInlets_OperatorDto_NoVars_NoConsts dto)
//        {
//            return Process_NoVars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_Consts(MaxOverInlets_OperatorDto_Vars_Consts dto)
//        {
//            base.Visit_MaxOverInlets_OperatorDto_Vars_Consts(dto);

//            // Pre-calculate
//            double constValue = dto.Consts.Select(x => x.Const).Max();

//            return new MaxOverInlets_OperatorDto_Vars_1Const { Vars = dto.Vars, Const = InputDtoFactory.CreateInputDto(constValue), OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
//        {
//            return Process_Vars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // MinFollower

//        protected override IOperatorDto Visit_MinFollower_OperatorDto_AllVars(MinFollower_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MinFollower_OperatorDto_ConstSignal(MinFollower_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // MinOverDimension

//        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // MinOverInlets

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_NoVars_Consts(MinOverInlets_OperatorDto_NoVars_Consts dto)
//        {
//            return Process_NoVars_Consts(dto, Enumerable.Min);
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_NoVars_NoConsts(MinOverInlets_OperatorDto_NoVars_NoConsts dto)
//        {
//            return Process_NoVars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_Consts(MinOverInlets_OperatorDto_Vars_Consts dto)
//        {
//            base.Visit_MinOverInlets_OperatorDto_Vars_Consts(dto);

//            // Pre-calculate
//            double constValue = dto.Consts.Select(x => x.Const).Min();

//            return new MinOverInlets_OperatorDto_Vars_1Const { Vars = dto.Vars, Const = InputDtoFactory.CreateInputDto(constValue), OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
//        {
//            return Process_Vars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_1Var_1Const(MinOverInlets_OperatorDto_1Var_1Const dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_2Vars(MinOverInlets_OperatorDto_2Vars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Multiply

//        protected override IOperatorDto Visit_Multiply_OperatorDto_NoVars_Consts(Multiply_OperatorDto_NoVars_Consts dto)
//        {
//            return Process_NoVars_Consts(dto, CollectionExtensions.Product);
//        }

//        protected override IOperatorDto Visit_Multiply_OperatorDto_NoVars_NoConsts(Multiply_OperatorDto_NoVars_NoConsts dto)
//        {
//            return Process_NoVars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
//        {
//            return Process_Vars_NoConsts(dto);
//        }

//        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_Consts(Multiply_OperatorDto_Vars_Consts dto)
//        {
//            base.Visit_Multiply_OperatorDto_Vars_Consts(dto);

//            // Pre-calculate
//            double constValue = dto.Consts.Select(x => x.Const).Product();

//            return new Multiply_OperatorDto_Vars_1Const { Vars = dto.Vars, Const = InputDtoFactory.CreateInputDto(constValue), OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
//        {
//            InputDto constMathProperties = InputDtoFactory.CreateInputDto(dto.Const.Const);

//            // Identity
//            if (constMathProperties.IsConstOne)
//            {
//                return new Multiply_OperatorDto_Vars_NoConsts { Vars = dto.Vars, OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        // Negative

//        protected override IOperatorDto Visit_Negative_OperatorDto_ConstNumber(Negative_OperatorDto_ConstNumber dto)
//        {
//            base.Visit_Negative_OperatorDto_ConstNumber(dto);

//            // Pre-calculate
//            return new Number_OperatorDto { Number = -dto.Number };
//        }

//        protected override IOperatorDto Visit_Negative_OperatorDto_VarNumber(Negative_OperatorDto_VarNumber dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Noise

//        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // NotchFilter

//        protected override IOperatorDto Visit_NotchFilter_OperatorDto_AllVars(NotchFilter_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_NotchFilter_OperatorDto_ConstSound(NotchFilter_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_NotchFilter_OperatorDto_ManyConsts(NotchFilter_OperatorDto_ManyConsts dto)
//        {
//            return Process_Filter_ManyConsts_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetNotchFilterVariables);
//        }

//        // NotEqual

//        protected override IOperatorDto Visit_NotEqual_OperatorDto_ConstA_ConstB(NotEqual_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_NotEqual_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            if (dto.A != dto.B)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else
//            {
//                return new Number_OperatorDto_Zero();
//            }
//        }

//        protected override IOperatorDto Visit_NotEqual_OperatorDto_ConstA_VarB(NotEqual_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_NotEqual_OperatorDto_ConstA_VarB(dto);

//            // Commute
//            return new NotEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_NotEqual_OperatorDto_VarA_ConstB(NotEqual_OperatorDto_VarA_ConstB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_NotEqual_OperatorDto_VarA_VarB(NotEqual_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Not

//        protected override IOperatorDto Visit_Not_OperatorDto_ConstNumber(Not_OperatorDto_ConstNumber dto)
//        {
//            base.Visit_Not_OperatorDto_ConstNumber(dto);

//            // Pre-calculate
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isFalse = dto.Number == 0.0;
//            if (isFalse)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else
//            {
//                return new Number_OperatorDto_Zero();
//            }
//        }

//        protected override IOperatorDto Visit_Not_OperatorDto_VarNumber(Not_OperatorDto_VarNumber dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Number

//        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Or

//        protected override IOperatorDto Visit_Or_OperatorDto_ConstA_ConstB(Or_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_Or_OperatorDto_ConstA_ConstB(dto);

//            InputDto aInputDto = InputDtoFactory.CreateInputDto(dto.A);
//            InputDto bInputDto = InputDtoFactory.CreateInputDto(dto.B);

//            // Pre-calculate
//            if (aInputDto.IsConstNonZero || bInputDto.IsConstNonZero)
//            {
//                return new Number_OperatorDto_One();
//            }
//            else if (aInputDto.IsConstZero && bInputDto.IsConstZero)
//            {
//                return new Number_OperatorDto_Zero();
//            }

//            throw new VisitationCannotBeHandledException();
//        }

//        protected override IOperatorDto Visit_Or_OperatorDto_ConstA_VarB(Or_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_Or_OperatorDto_ConstA_VarB(dto);

//            // Commute
//            return new Or_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A, OperatorID = dto.OperatorID };
//        }

//        protected override IOperatorDto Visit_Or_OperatorDto_VarA_ConstB(Or_OperatorDto_VarA_ConstB dto)
//        {
//            base.Visit_Or_OperatorDto_VarA_ConstB(dto);

//            InputDto bInputDto = InputDtoFactory.CreateInputDto(dto.B);

//            if (bInputDto.IsConstNonZero)
//            {
//                // Simplify
//                return new Number_OperatorDto_One();
//            }
//            else if (bInputDto.IsConstZero)
//            {
//                // Identity
//                return dto.AOperatorDto;
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // PeakingEQFilter

//        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_AllVars(PeakingEQFilter_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_ConstSound(PeakingEQFilter_OperatorDto_ConstSound dto)
//        {
//            return Process_ConstSound_Identity(dto.Sound);
//        }

//        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_ManyConsts(PeakingEQFilter_OperatorDto_ManyConsts dto)
//        {
//            double limitedFrequency = LimitFrequency(dto.Frequency, dto.NyquistFrequency);

//            BiQuadFilterWithoutFields.SetPeakingEQFilterVariables(
//                dto.SamplingRate, limitedFrequency, dto.Width, dto.DBGain,
//                out double a0, out double a1, out double a2, out double a3, out double a4);

//            dto.A0 = a0;
//            dto.A1 = a1;
//            dto.A2 = a2;
//            dto.A3 = a3;
//            dto.A4 = a4;

//            return dto;
//        }

//        // Power

//        protected override IOperatorDto Visit_Power_OperatorDto_ConstBase_ConstExponent(Power_OperatorDto_ConstBase_ConstExponent dto)
//        {
//            base.Visit_Power_OperatorDto_ConstBase_ConstExponent(dto);

//            // Pre-calculate
//            return new Number_OperatorDto { Number = Math.Pow(dto.Base, dto.Exponent) };
//        }

//        protected override IOperatorDto Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
//        {
//            base.Visit_Power_OperatorDto_ConstBase_VarExponent(dto);

//            InputDto baseInputDto = InputDtoFactory.CreateInputDto(dto.Base);

//            if (baseInputDto.IsConstZero)
//            {
//                // 0
//                return new Number_OperatorDto_Zero();
//            }
//            else if (baseInputDto.IsConstOne)
//            {
//                // 1
//                return new Number_OperatorDto_One();
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
//        {
//            base.Visit_Power_OperatorDto_VarBase_ConstExponent(dto);

//            InputDto exponentInputDto = InputDtoFactory.CreateInputDto(dto.Exponent);

//            if (exponentInputDto.IsConstZero)
//            {
//                // 1
//                return new Number_OperatorDto_One();
//            }
//            else if (exponentInputDto.IsConstOne)
//            {
//                // Identity
//                return dto.Base;
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // PulseTrigger

//        protected override IOperatorDto Visit_PulseTrigger_OperatorDto_ConstPassThrough_ConstReset(PulseTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
//        {
//            return Process_Trigger_ConstPassThrough_ConstReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_PulseTrigger_OperatorDto_ConstPassThrough_VarReset(PulseTrigger_OperatorDto_ConstPassThrough_VarReset dto)
//        {
//            return Process_Trigger_ConstPassThrough_VarReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_PulseTrigger_OperatorDto_VarPassThrough_ConstReset(PulseTrigger_OperatorDto_VarPassThrough_ConstReset dto)
//        {
//            return Process_Trigger_VarPassThrough_ConstReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_PulseTrigger_OperatorDto_VarPassThrough_VarReset(PulseTrigger_OperatorDto_VarPassThrough_VarReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Pulse

//        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting dto)
//        {
//            base.Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting(dto);

//            // Simplify
//            var dto2 = new Square_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = dto.Frequency };
//            DtoCloner.Clone_WithDimensionProperties(dto, dto2);
//            return dto2;
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting dto)
//        {
//            base.Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting(dto);

//            // Simplify
//            var dto2 = new Square_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = dto.Frequency };
//            DtoCloner.Clone_WithDimensionProperties(dto, dto2);
//            return dto2;
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking dto)
//        {
//            base.Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking(dto);

//            // Simplify
//            var dto2 = new Square_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
//            DtoCloner.Clone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking dto)
//        {
//            base.Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking(dto);

//            // Simplify
//            var dto2 = new Square_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
//            DtoCloner.Clone_WithDimensionProperties(dto, dto2);

//            return dto2;
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Pulse_OperatorDto_ZeroFrequency(Pulse_OperatorDto_ZeroFrequency dto)
//        {
//            return Process_ZeroFrequency(dto);
//        }

//        // Random

//        protected override IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_CubicAbruptSlope(Random_OperatorDto_CubicAbruptSlope dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_CubicEquidistant(Random_OperatorDto_CubicEquidistant dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_CubicSmoothSlope_LagBehind(Random_OperatorDto_CubicSmoothSlope_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_Hermite_LagBehind(Random_OperatorDto_Hermite_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_ConstRate(Random_OperatorDto_Line_LagBehind_ConstRate dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_VarRate(Random_OperatorDto_Line_LagBehind_VarRate dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // RangeOverDimension

//        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto)
//        {
//            // Done in MachineOptimization_OperatorDtoVisitor instead.
//            return Process_Nothing(dto);
//        }

//        // RangeOverOutlets

//        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep dto)
//        {
//            base.Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep(dto);

//            // Pre-Calculate
//            double result = dto.From + dto.Step * dto.OutletPosition;
//            return new Number_OperatorDto { Number = result };
//        }

//        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep dto)
//        {
//            base.Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(dto);

//            // Simplify
//            double stepTimesPosition = dto.Step * dto.OutletPosition;
//            var dto2 = new Add_OperatorDto_Vars_1Const
//            {
//                Const = InputDtoFactory.CreateInputDto(stepTimesPosition),
//                Vars = new[] { InputDtoFactory.CreateInputDto(dto.From) }
//            };
//            return dto2;
//        }

//        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ZeroStep(RangeOverOutlets_Outlet_OperatorDto_ZeroStep dto)
//        {
//            // Identity
//            return dto.From;
//        }

//        // Reset

//        protected override IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto)
//        {
//            // Requires special visitation
//            return Process_Nothing(dto);
//        }

//        // Reverse

//        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstSignal(Reverse_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_NoOriginShifting(Reverse_OperatorDto_ConstFactor_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_WithOriginShifting(Reverse_OperatorDto_ConstFactor_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_NoPhaseTracking(Reverse_OperatorDto_VarFactor_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_WithPhaseTracking(Reverse_OperatorDto_VarFactor_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Round

//        protected override IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto)
//        {
//            base.Visit_Round_OperatorDto_AllConsts(dto);

//            // Pre-calculate
//            double result = MathHelper.RoundWithStep(dto.Signal, dto.Step, dto.Offset);
//            return new Number_OperatorDto { Number = result };
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Sample

//        protected override IOperatorDto Visit_Sample_OperatorDto_NoSample(Sample_OperatorDto_NoSample dto)
//        {
//            base.Visit_Sample_OperatorDto_NoSample(dto);

//            // 0
//            return new Number_OperatorDto_Zero();
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ZeroFrequency(Sample_OperatorDto_ZeroFrequency dto)
//        {
//            return Process_ZeroFrequency(dto);
//        }

//        // SawDown

//        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawDown_OperatorDto_ZeroFrequency(SawDown_OperatorDto_ZeroFrequency dto)
//        {
//            return Process_ZeroFrequency(dto);
//        }

//        // SawUp

//        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SawUp_OperatorDto_ZeroFrequency(SawUp_OperatorDto_ZeroFrequency dto)
//        {
//            return Process_ZeroFrequency(dto);
//        }

//        // SetDimension

//        protected override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_ConstNumber(SetDimension_OperatorDto_ConstPassThrough_ConstNumber dto)
//        {
//            return Process_ConstNumber_Identity(dto.PassThrough);
//        }

//        protected override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_VarNumber(SetDimension_OperatorDto_ConstPassThrough_VarNumber dto)
//        {
//            return Process_ConstNumber_Identity(dto.PassThrough);
//        }

//        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_ConstNumber(SetDimension_OperatorDto_VarPassThrough_ConstNumber dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_VarNumber(SetDimension_OperatorDto_VarPassThrough_VarNumber dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Sine

//        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Sine_OperatorDto_ZeroFrequency(Sine_OperatorDto_ZeroFrequency dto)
//        {
//            return Process_ZeroFrequency(dto);
//        }

//        // SortOverDimension

//        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // SortOverInlets

//        protected override IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Spectrum

//        protected override IOperatorDto Visit_Spectrum_OperatorDto_AllVars(Spectrum_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Spectrum_OperatorDto_ConstSound(Spectrum_OperatorDto_ConstSound dto)
//        {
//            base.Visit_Spectrum_OperatorDto_ConstSound(dto);

//            // 0
//            return new Number_OperatorDto_Zero();
//        }

//        // Square

//        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Square_OperatorDto_ZeroFrequency(Square_OperatorDto_ZeroFrequency dto)
//        {
//            return Process_ZeroFrequency(dto);
//        }

//        // Squash

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin(Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // Stretch

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
//        {
//            return Process_ConstSignal_Identity(dto.Signal);
//        }

//        // Subtract

//        protected override IOperatorDto Visit_Subtract_OperatorDto_ConstA_ConstB(Subtract_OperatorDto_ConstA_ConstB dto)
//        {
//            base.Visit_Subtract_OperatorDto_ConstA_ConstB(dto);

//            // Pre-calculate
//            return new Number_OperatorDto { Number = dto.A - dto.B };
//        }

//        protected override IOperatorDto Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
//        {
//            base.Visit_Subtract_OperatorDto_ConstA_VarB(dto);

//            InputDto aInputDto = InputDtoFactory.CreateInputDto(dto.A);

//            if (aInputDto.IsConstZero)
//            {
//                // Identity, switch sign
//                return new Negative_OperatorDto_VarNumber { Number = dto.BOperatorDto, OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
//        {
//            base.Visit_Subtract_OperatorDto_VarA_ConstB(dto);

//            InputDto bMathProperties = InputDtoFactory.CreateInputDto(dto.B);

//            if (bMathProperties.IsConstZero)
//            {
//                // Identity
//                return dto.AOperatorDto;
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // SumFollower

//        protected override IOperatorDto Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto)
//        {
//            base.Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(dto);

//            // Pre-calculate
//            return new Number_OperatorDto { Number = dto.Signal * dto.SampleCount };
//        }

//        protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // SumOverDimension

//        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto)
//        {
//            base.Visit_SumOverDimension_OperatorDto_AllConsts(dto);

//            // Pre-calculate
//            int sampleCount = (int)(dto.Till - dto.From / dto.Step);
//            double result = dto.Signal * sampleCount;
//            return new Number_OperatorDto { Number = result };
//        }

//        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // ToggleTrigger

//        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset(ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
//        {
//            return Process_Trigger_ConstPassThrough_ConstReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto_ConstPassThrough_VarReset(ToggleTrigger_OperatorDto_ConstPassThrough_VarReset dto)
//        {
//            return Process_Trigger_ConstPassThrough_VarReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto_VarPassThrough_ConstReset(ToggleTrigger_OperatorDto_VarPassThrough_ConstReset dto)
//        {
//            return Process_Trigger_VarPassThrough_ConstReset_Identity(dto);
//        }

//        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto_VarPassThrough_VarReset(ToggleTrigger_OperatorDto_VarPassThrough_VarReset dto)
//        {
//            return Process_Nothing(dto);
//        }

//        // Triangle

//        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
//        {
//            return Process_Nothing(dto);
//        }

//        protected override IOperatorDto Visit_Triangle_OperatorDto_ZeroFrequency(Triangle_OperatorDto_ZeroFrequency dto)
//        {
//            return Process_ZeroFrequency(dto);
//        }

//        // Helpers

//        private IOperatorDto Process_ConstNumber_Identity(double number)
//        {
//            // Identity
//            return new Number_OperatorDto { Number = number };
//        }

//        private IOperatorDto Process_ConstSignal_Identity(double signal)
//        {
//            // Identity
//            return new Number_OperatorDto { Number = signal };
//        }

//        private IOperatorDto Process_ConstSound_Identity(double sound)
//        {
//            // Identity
//            return new Number_OperatorDto { Number = sound };
//        }

//        private IOperatorDto Process_Filter_ManyConsts_WithWidthOrBlobVolume(
//            OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume dto,
//            SetFilterParametersWithWidthOrBlobVolumeDelegate setFilterParametersDelegate)
//        {
//            double limitedFrequency = LimitFrequency(dto.Frequency, dto.NyquistFrequency);

//            setFilterParametersDelegate(
//                dto.SamplingRate, limitedFrequency, dto.WidthOrBlobVolume,
//                out double a0, out double a1, out double a2, out double a3, out double a4);

//            dto.A0 = a0;
//            dto.A1 = a1;
//            dto.A2 = a2;
//            dto.A3 = a3;
//            dto.A4 = a4;

//            return dto;
//        }

//        private double LimitFrequency(double frequency, double nyquistFrequency)
//        {
//            double limitedFrequency = frequency;
//            if (limitedFrequency > nyquistFrequency)
//            {
//                limitedFrequency = nyquistFrequency;
//            }

//            return limitedFrequency;
//        }

//        /// <summary> 
//        /// For overrides that do not add any processing. 
//        /// They are overridden for maintainability purposes,
//        /// so only new virtual methods show up when typing 'override'.
//        /// </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private IOperatorDto Process_Nothing(IOperatorDto dto)
//        {
//            return Visit_OperatorDto_Base(dto);
//        }

//        private IOperatorDto Process_NoVars_Consts(
//            OperatorDtoBase_Consts dto, 
//            Func<IEnumerable<double>, double> aggregationDelegate)
//        {
//            base.Visit_OperatorDto_Base(dto);

//            // Pre-calculate
//            double result = aggregationDelegate(dto.Consts.Select(x => x.Const));

//            return new Number_OperatorDto { Number = result };
//        }

//        private IOperatorDto Process_NoVars_NoConsts(IOperatorDto dto)
//        {
//            base.Visit_OperatorDto_Base(dto);

//            // 0
//            return new Number_OperatorDto_Zero();
//        }

//        private IOperatorDto ProcessShelfFilter_ManyConsts(
//            OperatorDtoBase_ShelfFilter_ManyConsts dto,
//            SetShelfFilterParametersDelegate setFilterParametersDelegate)
//        {
//            double limitedFrequency = LimitFrequency(dto.Frequency, dto.NyquistFrequency);
//            setFilterParametersDelegate(
//                dto.SamplingRate, limitedFrequency, dto.TransitionSlope, dto.DBGain,
//                out double a0, out double a1, out double a2, out double a3, out double a4);

//            dto.A0 = a0;
//            dto.A1 = a1;
//            dto.A2 = a2;
//            dto.A3 = a3;
//            dto.A4 = a4;

//            return dto;
//        }

//        private IOperatorDto Process_Trigger_ConstPassThrough_ConstReset_Identity(OperatorDtoBase_Trigger dto)
//        {
//            // Identity
//            return new Number_OperatorDto { Number = dto.PassThrough };
//        }

//        private IOperatorDto Process_Trigger_ConstPassThrough_VarReset_Identity(OperatorDtoBase_Trigger dto)
//        {
//            // Identity
//            return new Number_OperatorDto { Number = dto.PassThrough };
//        }

//        private IOperatorDto Process_Trigger_VarPassThrough_ConstReset_Identity(OperatorDtoBase_Trigger dto)
//        {
//            // Identity
//            return dto.PassThroughInputOperatorDto;
//        }

//        private IOperatorDto Process_Vars_NoConsts(OperatorDtoBase_Vars dto)
//        {
//            base.Visit_OperatorDto_Base(dto);

//            switch (dto.Vars.Count)
//            {
//                case 0:
//                    // 0
//                    return new Number_OperatorDto_Zero();

//                case 1:
//                    return dto.Vars[0].Var;

//                default:
//                    return dto;
//            }
//        }

//        private IOperatorDto Process_ZeroFrequency(IOperatorDto dto)
//        {
//            base.Visit_OperatorDto_Base(dto);

//            // 0
//            return new Number_OperatorDto_Zero();
//        }
//    }
//}

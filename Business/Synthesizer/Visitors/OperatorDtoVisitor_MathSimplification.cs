using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;
using JJ.Framework.Mathematics;
// ReSharper disable RedundantIfElseBlock
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary>
    /// Also takes care of the ClassSpecialization, since it derives from OperatorDtoVisitor_ClassSpecializationBase.
    /// </summary>
    internal class OperatorDtoVisitor_MathSimplification : OperatorDtoVisitor_ClassSpecializationBase
    {
        private delegate void SetFilterParametersWithWidthOrBlobVolumeDelegate(
            double samplingRate, double limitedFrequency, double widthOrBlobVolume, 
            out double a0, out double a1, out double a2, out double a3, out double a4);

        private delegate void SetShelfFilterParametersDelegate(
            double samplingRate, double limitedFrequency, double transitionSlope, double dbGain,
            out double a0, out double a1, out double a2, out double a3, out double a4);

        // General

        public IOperatorDto Execute(IOperatorDto dto) => Visit_OperatorDto_Polymorphic(dto);

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            // NaN / Infinity

            // Depth-first, so deeply pre-calculated NaN's can be picked up.
            IOperatorDto dto2 = base.Visit_OperatorDto_Polymorphic(dto);

            bool anyInputsHaveSpecialValue = dto2.Inputs.Any(x => x.IsConstSpecialValue);
            if (anyInputsHaveSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            return dto2;
        }

        // Absolute

        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
        {
            base.Visit_Absolute_OperatorDto(dto);

            if (dto.Number.IsConst)
            {
                // Pre-calculate
                return new Number_OperatorDto { Number = Math.Abs(dto.Number.Const) };
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        // Add

        protected override IOperatorDto Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            return Process_NoVars_Consts(dto, Enumerable.Sum);
        }

        protected override IOperatorDto Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_NoVars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            return Process_Vars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
        {
            base.Visit_Add_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            var dto2 = new Add_OperatorDto_Vars_1Const();
            DtoCloner.CloneProperties(dto, dto2);

            dto2.Vars = dto.Vars;
            dto2.Const = dto.Consts.Select(x => x.Const).Sum();

            return dto2;
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            // Identity
            if (dto.Const.IsConstZero)
            {
                var dto2 = new Add_OperatorDto_Vars_NoConsts();
                DtoCloner.CloneProperties(dto, dto2);

                dto2.Vars = dto.Vars;

                return dto2;
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        // AllPassFilter

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetAllPassFilterVariables);
        }

        // And

        protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
        {
            base.Visit_And_OperatorDto(dto);

            if (dto.A.IsConstNonZero && dto.B.IsConstNonZero)
            {
                // Pre-calculate
                return new Number_OperatorDto_One();
            }
            else if (dto.A.IsConstZero || dto.B.IsConstZero)
            {
                // Pre-calculate
                return new Number_OperatorDto_Zero();
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute
                return Commute(dto);
            }
            else if (dto.A.IsVar && dto.B.IsConstZero)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }
            else if (dto.A.IsVar && dto.B.IsConstNonZero)
            {
                // Identity
                return dto.A.Var;
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        // AverageFollower

        protected override IOperatorDto Visit_AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_AverageFollower_OperatorDto_ConstSignal(AverageFollower_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // AverageOverDimension

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_ConstSignal(AverageOverDimension_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // AverageOverInlets

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto_AllConsts(AverageOverInlets_OperatorDto_AllConsts dto)
        {
            return Process_NoVars_Consts(dto, Enumerable.Average);
        }

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto_Vars(AverageOverInlets_OperatorDto_Vars dto)
        {
            return Process_Nothing(dto);
        }

        // BandPassFilterConstantPeakGain

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetBandPassFilterConstantPeakGainVariables);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        // BandPassFilterConstantTransitionGain

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetBandPassFilterConstantTransitionGainVariables);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        // Cache

        protected override IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Block(Cache_OperatorDto_MultiChannel_Block dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Cubic(Cache_OperatorDto_MultiChannel_Cubic dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Hermite(Cache_OperatorDto_MultiChannel_Hermite dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Line(Cache_OperatorDto_MultiChannel_Line dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Stripe(Cache_OperatorDto_MultiChannel_Stripe dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Block(Cache_OperatorDto_SingleChannel_Block dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Cubic(Cache_OperatorDto_SingleChannel_Cubic dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Hermite(Cache_OperatorDto_SingleChannel_Hermite dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Line(Cache_OperatorDto_SingleChannel_Line dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Stripe(Cache_OperatorDto_SingleChannel_Stripe dto)
        {
            return Process_Nothing(dto);
        }

        // ChangeTrigger

        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto)
        {
            return Process_Trigger(dto);
        }

        // ClosestOverDimension

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return Process_Nothing(dto);
        }

        // ClosestOverDimensionExp

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return Process_Nothing(dto);
        }

        // ClosestOverInletsExp

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(dto);

            // Pre-calculate
            double result = AggregateCalculator.ClosestExp(dto.Input.Const, dto.Items.Select(x => x.Const).ToArray());
            return new Number_OperatorDto { Number = result };
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(dto);

            if (dto.Items.Count == 0)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }
            else if (dto.Items.Count == 1)
            {
                // Identity
                return new Number_OperatorDto { Number = dto.Items[0].Const };
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto)
        {
            return Process_Nothing(dto);
        }

        // ClosestOverInlets

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(ClosestOverInlets_OperatorDto_ConstInput_ConstItems dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(dto);

            // Pre-calculate
            double result = AggregateCalculator.Closest(dto.Input.Const, dto.Items.Select(x => x.Const).ToArray());
            return new Number_OperatorDto { Number = result };
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(dto);

            if (dto.Items.Count == 0)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }
            if (dto.Items.Count == 1)
            {
                // Identity
                return new Number_OperatorDto { Number = dto.Items[0].Const };
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
        {
            return Process_Nothing(dto);
        }

        // Curve

        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
        {
            return Process_Curve(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
        {
            return Process_Curve(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
        {
            return Process_Curve(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
        {
            return Process_Curve(dto);
        }

        private IOperatorDto Process_Curve(Curve_OperatorDtoBase_WithoutMinX dto)
        {
            if (dto.CurveID == 0)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }

            return dto;
        }

        // DimensionToOutlets

        protected override IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto)
        {
            return Process_Nothing(dto);
        }

        // Divide

        protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
        {
            base.Visit_Divide_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                return new Number_OperatorDto { Number = dto.A.Const / dto.B.Const };
            }
            else if (dto.A.IsConstZero)
            {
                // Identity
                return new Number_OperatorDto { Number = dto.A.Const };
            }
            else if (dto.B.IsConstOne)
            {
                // Identity
                return dto.A.Var;
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        // Equal

        protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
        {
            base.Visit_Equal_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                if (dto.A == dto.B)
                {
                    return new Number_OperatorDto_One();
                }
                else
                {
                    return new Number_OperatorDto_Zero();
                }
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute
                return Commute(dto);
            }
            else if (dto.A.IsVar && dto.B.IsConst)
            {
                return Process_Nothing(dto);
            }
            else if (dto.A.IsVar && dto.B.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // GetDimension

        protected override IOperatorDto Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            switch (dto.StandardDimensionEnum)
            {
                case DimensionEnum.SamplingRate:
                {
                    var dto2 = new Number_OperatorDto();
                    DtoCloner.CloneProperties(dto, dto2);
                    dto2.Number = dto.SamplingRate;
                    return dto2;
                }

                case DimensionEnum.HighestFrequency:
                {
                    var dto2 = new Number_OperatorDto();
                    DtoCloner.CloneProperties(dto, dto2);
                    dto2.Number = dto.SamplingRate / 2.0;
                    return dto2;
                }
            }

            return Process_Nothing(dto);
        }

        // GreaterThan

        protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
        {
            base.Visit_GreaterThan_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                if (dto.A.Const > dto.B.Const)
                {
                    return new Number_OperatorDto_One();
                }
                else
                {
                    return new Number_OperatorDto_Zero();
                }
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute, switch sign
                var dto2 = new LessThan_OperatorDto();
                DtoCloner.CloneProperties(dto, dto2);
                dto2.A = dto.B;
                dto2.B = dto.A;
                return dto2;
            }
            else if (dto.A.IsVar && dto.B.IsConst)
            {
                return Process_Nothing(dto);
            }
            else if (dto.A.IsVar && dto.B.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // GreaterThanOrEqual

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
        {
            base.Visit_GreaterThanOrEqual_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                if (dto.A.Const >= dto.B.Const)
                {
                    return new Number_OperatorDto_One();
                }
                else
                {
                    return new Number_OperatorDto_Zero();
                }
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute, switch sign
                var dto2 = new LessThanOrEqual_OperatorDto();
                DtoCloner.CloneProperties(dto, dto2);
                dto2.A = dto.B;
                dto2.B = dto.A;
                return dto2;
            }
            else if (dto.A.IsVar && dto.B.IsConst)
            {
                return Process_Nothing(dto);
            }
            else if (dto.A.IsVar && dto.B.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // HighPassFilter

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetHighPassFilterVariables);
        }

        // HighShelfFilter

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_ShelfFilter_SoundVarOrConst_OtherInputsConst(dto, BiQuadFilterWithoutFields.SetHighShelfFilterVariables);
        }

        // Hold

        protected override IOperatorDto Visit_Hold_OperatorDto_VarSignal(Hold_OperatorDto_VarSignal dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Hold_OperatorDto_ConstSignal(Hold_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // If

        protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
        {
            base.Visit_If_OperatorDto(dto);

            if (dto.Condition.IsConst && dto.Then.IsConst && dto.Else.IsConst)
            {
                // Pre-calculate
                bool isTrue = dto.Condition.Const != 0.0;
                if (isTrue)
                {
                    return new Number_OperatorDto { Number = dto.Then.Const };
                }
                else
                {
                    return new Number_OperatorDto { Number = dto.Else.Const };
                }
            }
            else if (dto.Condition.IsConst && dto.Then.IsConst && dto.Else.IsVar)
            {
                bool isTrue = dto.Condition.Const != 0.0;
                if (isTrue)
                {
                    // Identity
                    return new Number_OperatorDto { Number = dto.Then.Const };
                }
                else
                {
                    // Identity
                    return dto.Else.Var;
                }
            }
            else if (dto.Condition.IsConst && dto.Then.IsVar && dto.Else.IsConst)
            {
                bool isTrue = dto.Condition.Const != 0.0;
                if (isTrue)
                {
                    // Identity
                    return dto.Then.Var;
                }
                else
                {
                    // Identity
                    return new Number_OperatorDto { Number = dto.Else.Const };
                }
            }
            else if (dto.Condition.IsConst && dto.Then.IsVar && dto.Else.IsVar)
            {
                bool isTrue = dto.Condition.Const != 0.0;
                if (isTrue)
                {
                    // Identity
                    return dto.Then.Var;
                }
                else
                {
                    // Identity
                    return dto.Else.Var;
                }
            }
            else if (dto.Condition.IsVar && dto.Then.IsConst && dto.Else.IsConst)
            {
                if (dto.Then == dto.Else)
                {
                    // Identity
                    return new Number_OperatorDto { Number = dto.Then.Const };
                }
            }

            return Process_Nothing(dto);
        }

        // InletsToDimension

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicAbruptSlope(InletsToDimension_OperatorDto_CubicAbruptSlope dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicEquidistant(InletsToDimension_OperatorDto_CubicEquidistant dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind(InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Hermite_LagBehind(InletsToDimension_OperatorDto_Hermite_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        // Interpolate

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        // LessThan

        protected override IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
        {
            base.Visit_LessThan_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                if (dto.A.Const < dto.B.Const)
                {
                    return new Number_OperatorDto_One();
                }
                else
                {
                    return new Number_OperatorDto_Zero();
                }
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute, switch sign
                var dto2 = new GreaterThan_OperatorDto();
                DtoCloner.CloneProperties(dto, dto2);
                dto2.A = dto.B;
                dto2.B = dto.A;
                return dto2;
            }
            else if (dto.A.IsVar && dto.B.IsConst)
            {
                return Process_Nothing(dto);
            }
            else if (dto.A.IsVar && dto.B.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // LessThanOrEqual

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
        {
            base.Visit_LessThanOrEqual_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                if (dto.A.Const <= dto.B.Const)
                {
                    return new Number_OperatorDto_One();
                }
                else
                {
                    return new Number_OperatorDto_Zero();
                }
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute, switch sign
                var dto2 = new GreaterThanOrEqual_OperatorDto();
                DtoCloner.CloneProperties(dto, dto2);
                dto2.A = dto.B;
                dto2.B = dto.A;
                return dto2;
            }
            else if (dto.A.IsVar && dto.B.IsConst)
            {
                return Process_Nothing(dto);
            }
            else if (dto.A.IsVar && dto.B.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // Loop

        protected override IOperatorDto Visit_Loop_OperatorDto_SignalVarOrConst_OtherInputsVar(Loop_OperatorDto_SignalVarOrConst_OtherInputsVar dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSignal(Loop_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto)
        {
            return Process_Nothing(dto);
        }

        // LowPassFilter

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetLowPassFilterVariables);
        }

        // LowShelfFilter

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_ShelfFilter_SoundVarOrConst_OtherInputsConst(dto, BiQuadFilterWithoutFields.SetHighShelfFilterVariables);
        }

        // MaxFollower

        protected override IOperatorDto Visit_MaxFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(MaxFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MaxFollower_OperatorDto_ConstSignal(MaxFollower_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // MaxOverDimension

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // MaxOverInlets

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_Consts(MaxOverInlets_OperatorDto_NoVars_Consts dto)
        {
            return Process_NoVars_Consts(dto, Enumerable.Max);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts(MaxOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_NoVars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_Consts(MaxOverInlets_OperatorDto_Vars_Consts dto)
        {
            base.Visit_MaxOverInlets_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            var dto2 = new MaxOverInlets_OperatorDto_Vars_1Const();
            DtoCloner.CloneProperties(dto, dto2);

            dto2.Vars = dto.Vars;
            dto2.Const = dto.Consts.Select(x => x.Const).Max();

            return dto2;
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return Process_Vars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            return Process_Nothing(dto);
        }

        // MinFollower

        protected override IOperatorDto Visit_MinFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(MinFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MinFollower_OperatorDto_ConstSignal(MinFollower_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // MinOverDimension

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // MinOverInlets

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_NoVars_Consts(MinOverInlets_OperatorDto_NoVars_Consts dto)
        {
            return Process_NoVars_Consts(dto, Enumerable.Min);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_NoVars_NoConsts(MinOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_NoVars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_Consts(MinOverInlets_OperatorDto_Vars_Consts dto)
        {
            base.Visit_MinOverInlets_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            var dto2 = new MinOverInlets_OperatorDto_Vars_1Const();
            DtoCloner.CloneProperties(dto, dto2);

            dto2.Vars = dto.Vars;
            dto2.Const = dto.Consts.Select(x => x.Const).Min();

            return dto2;
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return Process_Vars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_1Var_1Const(MinOverInlets_OperatorDto_1Var_1Const dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_2Vars(MinOverInlets_OperatorDto_2Vars dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            return Process_Nothing(dto);
        }

        // Multiply

        protected override IOperatorDto Visit_Multiply_OperatorDto_NoVars_Consts(Multiply_OperatorDto_NoVars_Consts dto)
        {
            return Process_NoVars_Consts(dto, CollectionExtensions.Product);
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_NoVars_NoConsts(Multiply_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_NoVars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            return Process_Vars_NoConsts(dto);
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_Consts(Multiply_OperatorDto_Vars_Consts dto)
        {
            base.Visit_Multiply_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            var dto2 = new Multiply_OperatorDto_Vars_1Const();
            DtoCloner.CloneProperties(dto, dto2);

            dto2.Vars = dto.Vars;
            dto2.Const = dto.Consts.Select(x => x.Const).Product();

            return dto2;
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            // Identity
            if (dto.Const.IsConstOne)
            {
                var dto2 = new Multiply_OperatorDto_Vars_NoConsts();
                DtoCloner.CloneProperties(dto, dto2);
                dto2.Vars = dto.Vars;
                return dto2;
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        // Negative

        protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
        {
            base.Visit_Negative_OperatorDto(dto);

            if (dto.Number.IsConst)
            {
                // Pre-calculate
                return new Number_OperatorDto { Number = -dto.Number.Const };
            }
            else if (dto.Number.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // Noise

        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            return Process_Nothing(dto);
        }

        // NotchFilter

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetNotchFilterVariables);
        }

        // NotEqual

        protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
        {
            base.Visit_NotEqual_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (dto.A != dto.B)
                {
                    return new Number_OperatorDto_One();
                }
                else
                {
                    return new Number_OperatorDto_Zero();
                }
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute
                return Commute(dto);
            }
            else if (dto.A.IsVar && dto.B.IsConst)
            {
                return Process_Nothing(dto);
            }
            else if (dto.A.IsVar && dto.B.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // Not

        protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
        {
            base.Visit_Not_OperatorDto(dto);

            if (dto.Number.IsConst)
            {
                // Pre-calculate
                bool isFalse = dto.Number.Const == 0.0;
                if (isFalse)
                {
                    return new Number_OperatorDto_One();
                }
                else
                {
                    return new Number_OperatorDto_Zero();
                }
            }
            else if (dto.Number.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // Number

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            return Process_Nothing(dto);
        }

        // Or

        protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
        {
            base.Visit_Or_OperatorDto(dto);

            if (dto.A.IsConstNonZero || dto.B.IsConstNonZero)
            {
                // Pre-calculate
                return new Number_OperatorDto_One();
            }
            else if (dto.A.IsConstZero && dto.B.IsConstZero)
            {
                // Pre-calculate
                return new Number_OperatorDto_Zero();
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                // Commute
                return Commute(dto);
            }
            else if (dto.A.IsVar && dto.B.IsConstNonZero)
            {
                // Simplify
                return new Number_OperatorDto_One();
            }
            else if (dto.A.IsVar && dto.B.IsConstZero)
            {
                // Identity
                return dto.A.Var;
            }
            else if (dto.A.IsVar && dto.B.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        // PeakingEQFilter

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_SoundVarOrConst_OtherInputsVar(dto);
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            if (dto.Sound.IsConst)
            {
                return Process_ConstSound_Identity(dto.Sound.Const);
            }

            double limitedFrequency = LimitFrequency(dto.Frequency.Const, dto.NyquistFrequency);

            BiQuadFilterWithoutFields.SetPeakingEQFilterVariables(
                dto.TargetSamplingRate, limitedFrequency, dto.Width.Const, dto.DBGain.Const,
                out double a0, out double a1, out double a2, out double a3, out double a4);

            dto.A0 = a0;
            dto.A1 = a1;
            dto.A2 = a2;
            dto.A3 = a3;
            dto.A4 = a4;

            return dto;
        }

        // Power

        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            base.Visit_Power_OperatorDto(dto);

            if (dto.Base.IsConst && dto.Exponent.IsConst)
            {
                // Pre-calculate
                return new Number_OperatorDto { Number = Math.Pow(dto.Base.Const, dto.Exponent.Const) };
            }
            else if (dto.Base.IsConstZero)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }
            else if (dto.Base.IsConstOne)
            {
                // 1
                return new Number_OperatorDto_One();
            }
            if (dto.Exponent.IsConstZero)
            {
                // 1
                return new Number_OperatorDto_One();
            }
            else if (dto.Exponent.IsConstOne)
            {
                // Identity
                return dto.Base.Var;
            }
            else 
            {
                return Process_Nothing(dto);
            }
        }

        // PulseTrigger

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
        {
            return Process_Trigger(dto);
        }

        // Pulse

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            bool isHalfWidth = IsHalfWidth(dto.Width);
            if (isHalfWidth)
            {
                // Simplify
                var dto2 = new Square_OperatorDto_ConstFrequency_NoOriginShifting();
                DtoCloner.CloneProperties(dto, dto2);
                return dto2;
            }

            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            bool isHalfWidth = IsHalfWidth(dto.Width);
            if (isHalfWidth)
            {
                // Simplify
                var dto2 = new Square_OperatorDto_ConstFrequency_WithOriginShifting();
                DtoCloner.CloneProperties(dto, dto2);
                return dto2;
            }

            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            bool isHalfWidth = IsHalfWidth(dto.Width);
            if (isHalfWidth)
            {
                // Simplify
                var dto2 = new Square_OperatorDto_VarFrequency_NoPhaseTracking { Frequency = dto.Frequency };
                DtoCloner.CloneProperties(dto, dto2);
                return dto2;
            }

            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            bool isHalfWidth = IsHalfWidth(dto.Width);
            if (isHalfWidth)
            {
                // Simplify
                var dto2 = new Square_OperatorDto_VarFrequency_WithPhaseTracking { Frequency = dto.Frequency };
                DtoCloner.CloneProperties(dto, dto2);
                return dto2;
            }

            return Process_WithFrequency(dto);
        }

        private static bool IsHalfWidth(InputDto inputDto) => inputDto.Const == 0.5;

        // Random

        protected override IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_CubicAbruptSlope(Random_OperatorDto_CubicAbruptSlope dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_CubicEquidistant(Random_OperatorDto_CubicEquidistant dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_CubicSmoothSlope_LagBehind(Random_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Hermite_LagBehind(Random_OperatorDto_Hermite_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_ConstRate(Random_OperatorDto_Line_LagBehind_ConstRate dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_VarRate(Random_OperatorDto_Line_LagBehind_VarRate dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto)
        {
            return Process_Nothing(dto);
        }

        // RangeOverDimension

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto)
        {
            // Done in MachineOptimization_OperatorDtoVisitor instead.
            return Process_Nothing(dto);
        }

        // RangeOverOutlets

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep dto)
        {
            base.Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep(dto);

            // Pre-Calculate
            double result = dto.From.Const + dto.Step.Const * dto.OutletPosition;
            return new Number_OperatorDto { Number = result };
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep dto)
        {
            base.Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(dto);

            // Simplify
            double stepTimesPosition = dto.Step.Const * dto.OutletPosition;

            var dto2 = new Add_OperatorDto_Vars_1Const
            {
                Const = stepTimesPosition,
                Vars = new InputDto[] { dto.From.Const }
            };
            return dto2;
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ZeroStep(RangeOverOutlets_Outlet_OperatorDto_ZeroStep dto)
        {
            // Identity
            return dto.From.Var;
        }

        // Reset

        protected override IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            // Requires special visitation
            return Process_Nothing(dto);
        }

        // Reverse

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstSignal(Reverse_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_NoOriginShifting(Reverse_OperatorDto_ConstFactor_NoOriginShifting dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_WithOriginShifting(Reverse_OperatorDto_ConstFactor_WithOriginShifting dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_NoPhaseTracking(Reverse_OperatorDto_VarFactor_NoPhaseTracking dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_WithPhaseTracking(Reverse_OperatorDto_VarFactor_WithPhaseTracking dto)
        {
            return Process_Nothing(dto);
        }

        // Round

        protected override IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto)
        {
            base.Visit_Round_OperatorDto_AllConsts(dto);

            // Pre-calculate
            double result = MathHelper.RoundWithStep(dto.Signal.Const, dto.Step.Const, dto.Offset.Const);
            return new Number_OperatorDto { Number = result };
        }

        protected override IOperatorDto Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
        {
            return Process_Nothing(dto);
        }

        // Sample

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
        {
            return Process_Sample(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_Sample(dto);
        }

        private IOperatorDto Process_Sample(Sample_OperatorDto dto)
        {
            if (dto.SampleID == 0)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }

            return Process_WithFrequency(dto);
        }

        // SawDown

        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        // SawUp

        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        // SetDimension

        protected override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_ConstNumber(SetDimension_OperatorDto_ConstPassThrough_ConstNumber dto)
        {
            return Process_ConstNumber_Identity(dto.PassThrough.Const);
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_VarNumber(SetDimension_OperatorDto_ConstPassThrough_VarNumber dto)
        {
            return Process_ConstNumber_Identity(dto.PassThrough.Const);
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_ConstNumber(SetDimension_OperatorDto_VarPassThrough_ConstNumber dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_VarNumber(SetDimension_OperatorDto_VarPassThrough_VarNumber dto)
        {
            return Process_Nothing(dto);
        }

        // Sine

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        // SortOverDimension

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // SortOverInlets

        protected override IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
        {
            return Process_Nothing(dto);
        }

        // Spectrum

        protected override IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto)
        {
            if (dto.Sound.IsConst)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        // Square

        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        // Squash

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_ConstSignal(Squash_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // Stretch

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal(Stretch_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal_Identity(dto.Signal.Const);
        }

        // Subtract

        protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
        {
            base.Visit_Subtract_OperatorDto(dto);

            if (dto.A.IsConst && dto.B.IsConst)
            {
                // Pre-calculate
                return new Number_OperatorDto { Number = dto.A.Const - dto.B.Const };
            }
            else if (dto.A.IsConst && dto.B.IsVar)
            {
                if (dto.A.IsConstZero)
                {
                    // Identity, switch sign
                    var dto2 = new Negative_OperatorDto();
                    DtoCloner.CloneProperties(dto, dto2);
                    dto2.Number = dto.B;
                    return dto2;
                }
            }
            else if (dto.A.IsVar && dto.B.IsConst)
            {
                if (dto.B.IsConstZero)
                {
                    // Identity
                    return dto.A.Var;
                }

            }

            return Process_Nothing(dto);
        }

        // SumFollower

        protected override IOperatorDto Visit_SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto)
        {
            base.Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(dto);

            // Pre-calculate
            return new Number_OperatorDto { Number = dto.Signal.Const * dto.SampleCount.Const };
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
        {
            return Process_Nothing(dto);
        }

        // SumOverDimension

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto)
        {
            return Process_Nothing(dto);
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto)
        {
            base.Visit_SumOverDimension_OperatorDto_AllConsts(dto);

            // Pre-calculate
            int sampleCount = (int)(dto.Till.Const - dto.From.Const / dto.Step.Const);
            double result = dto.Signal.Const * sampleCount;
            return new Number_OperatorDto { Number = result };
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto)
        {
            return Process_Nothing(dto);
        }

        // ToggleTrigger

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
        {
            return Process_Trigger(dto);
        }

        // Triangle

        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_WithFrequency(dto);
        }

        // Helpers

        private static IOperatorDto Commute(OperatorDtoBase_WithAAndB dto)
        {
            InputDto tempA = dto.A;
            InputDto tempB = dto.B;

            dto.A = tempB;
            dto.B = tempA;

            return dto;
        }

        private IOperatorDto Process_ConstNumber_Identity(double number)
        {
            // Identity
            return new Number_OperatorDto { Number = number };
        }

        private IOperatorDto Process_ConstSignal_Identity(double signal)
        {
            // Identity
            return new Number_OperatorDto { Number = signal };
        }

        private IOperatorDto Process_ConstSound_Identity(double sound)
        {
            // Identity
            return new Number_OperatorDto { Number = sound };
        }

        private IOperatorDto Process_Filter_SoundVarOrConst_OtherInputsVar(IOperatorDto_WithSound dto)
        {
            if (dto.Sound.IsConst)
            {
                return Process_ConstSound_Identity(dto.Sound.Const);
            }
            else
            {
                return Process_Nothing(dto);
            }
        }

        private IOperatorDto Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(
            OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume dto,
            SetFilterParametersWithWidthOrBlobVolumeDelegate setFilterParametersDelegate)
        {
            if (dto.Sound.IsConst)
            {
                return Process_ConstSound_Identity(dto.Sound.Const);
            }

            double limitedFrequency = LimitFrequency(dto.Frequency.Const, dto.NyquistFrequency);

            setFilterParametersDelegate(
                dto.TargetSamplingRate, limitedFrequency, dto.WidthOrBlobVolume.Const,
                out double a0, out double a1, out double a2, out double a3, out double a4);

            dto.A0 = a0;
            dto.A1 = a1;
            dto.A2 = a2;
            dto.A3 = a3;
            dto.A4 = a4;

            return dto;
        }

        private double LimitFrequency(double frequency, double nyquistFrequency)
        {
            double limitedFrequency = frequency;
            if (limitedFrequency > nyquistFrequency)
            {
                limitedFrequency = nyquistFrequency;
            }

            return limitedFrequency;
        }

        /// <summary> 
        /// For overrides that do not add any processing. 
        /// They are overridden for maintainability purposes,
        /// so only new virtual methods show up when typing 'override'.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IOperatorDto Process_Nothing(IOperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        private IOperatorDto Process_NoVars_Consts(
            OperatorDtoBase_Consts dto, 
            Func<IEnumerable<double>, double> aggregationDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            // Pre-calculate
            double result = aggregationDelegate(dto.Consts.Select(x => x.Const));

            return new Number_OperatorDto { Number = result };
        }

        private IOperatorDto Process_NoVars_NoConsts(IOperatorDto dto)
        {
            base.Visit_OperatorDto_Base(dto);

            // 0
            return new Number_OperatorDto_Zero();
        }

        private IOperatorDto Process_ShelfFilter_SoundVarOrConst_OtherInputsConst(
            OperatorDtoBase_ShelfFilter_SoundVarOrConst_OtherInputsConst dto,
            SetShelfFilterParametersDelegate setFilterParametersDelegate)
        {
            if (dto.Sound.IsConst)
            {
                return Process_ConstSound_Identity(dto.Sound.Const);
            }

            double limitedFrequency = LimitFrequency(dto.Frequency.Const, dto.NyquistFrequency);
            setFilterParametersDelegate(
                dto.TargetSamplingRate, limitedFrequency, dto.TransitionSlope.Const, dto.DBGain.Const,
                out double a0, out double a1, out double a2, out double a3, out double a4);

            dto.A0 = a0;
            dto.A1 = a1;
            dto.A2 = a2;
            dto.A3 = a3;
            dto.A4 = a4;

            return dto;
        }

        private IOperatorDto Process_Trigger(OperatorDtoBase_Trigger dto)
        {
            if (dto.PassThroughInput.IsConst && dto.Reset.IsConst)
            {
                return Process_Trigger_ConstPassThrough_ConstReset_Identity(dto);
            }
            else if (dto.PassThroughInput.IsConst && dto.Reset.IsVar)
            {
                return Process_Trigger_ConstPassThrough_VarReset_Identity(dto);
            }
            else if (dto.PassThroughInput.IsVar && dto.Reset.IsConst)
            {
                return Process_Trigger_VarPassThrough_ConstReset_Identity(dto);
            }
            else if (dto.PassThroughInput.IsVar && dto.Reset.IsVar)
            {
                return Process_Nothing(dto);
            }
            else
            {
                throw new VisitationCannotBeHandledException();
            }
        }

        private IOperatorDto Process_Trigger_ConstPassThrough_ConstReset_Identity(OperatorDtoBase_Trigger dto)
        {
            // Identity
            return new Number_OperatorDto { Number = dto.PassThroughInput.Const };
        }

        private IOperatorDto Process_Trigger_ConstPassThrough_VarReset_Identity(OperatorDtoBase_Trigger dto)
        {
            // Identity
            return new Number_OperatorDto { Number = dto.PassThroughInput.Const };
        }

        private IOperatorDto Process_Trigger_VarPassThrough_ConstReset_Identity(OperatorDtoBase_Trigger dto)
        {
            // Identity
            return dto.PassThroughInput.Var;
        }

        private IOperatorDto Process_Vars_NoConsts(OperatorDtoBase_Vars dto)
        {
            base.Visit_OperatorDto_Base(dto);

            switch (dto.Vars.Count)
            {
                case 0:
                    // 0
                    return new Number_OperatorDto_Zero();

                case 1:
                    return dto.Vars[0].Var;

                default:
                    return dto;
            }
        }

        private IOperatorDto Process_WithFrequency(OperatorDtoBase_WithFrequency dto)
        {
            if (dto.Frequency.IsConstZero)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }

            return dto;
        }
    }
}

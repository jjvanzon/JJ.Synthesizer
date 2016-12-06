using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class MathSimplification_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        // Absolute

        protected override OperatorDtoBase Visit_Absolute_OperatorDto_ConstX(Absolute_OperatorDto_ConstX dto)
        {
            base.Visit_Absolute_OperatorDto_ConstX(dto);

            // Pre-calculate
            return new Number_OperatorDto { Number = Math.Abs(dto.X) };
        }

        protected override OperatorDtoBase Visit_Absolute_OperatorDto_VarX(Absolute_OperatorDto_VarX dto)
        {
            // Do nothing
            return base.Visit_Absolute_OperatorDto_VarX(dto);
        }
        
        // Add

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            return Process_OperatorDto_NoVars_Consts(dto, Enumerable.Sum);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            return Process_OperatorDto_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
        {
            base.Visit_Add_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            double constValue = dto.Consts.Sum();

            var dto2 = new Add_OperatorDto_Vars_1Const { Vars = dto.Vars, ConstValue = constValue };

            return Visit_Add_OperatorDto_Vars_1Const(dto2);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            MathPropertiesDto constMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.ConstValue);

            // Identity
            if (constMathProperties.IsConstZero)
            {
                var dto2 = new Add_OperatorDto_Vars_NoConsts { Vars = dto.Vars };
                return Visit_Add_OperatorDto_Vars_NoConsts(dto2);
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_AllVars(AllPassFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_ManyConsts(AllPassFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_ConstSignal(AllPassFilter_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        // And

        /// <summary> Pre-calculate </summary>
        protected override OperatorDtoBase Visit_And_OperatorDto_ConstA_ConstB(And_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_And_OperatorDto_ConstA_ConstB(dto);

            MathPropertiesDto aMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.A);
            MathPropertiesDto bMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            // Pre-calculate
            if (aMathProperties.IsConstNonZero && bMathProperties.IsConstNonZero)
            {
                return new Number_OperatorDto_One();
            }
            else if (aMathProperties.IsConstZero || bMathProperties.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }
            else if (aMathProperties.IsConstSpecialValue || bMathProperties.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_ConstA_VarB(And_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_And_OperatorDto_ConstA_VarB(dto);

            // Commute
            var dto2 = new And_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };

            return Visit_And_OperatorDto_VarA_ConstB(dto2);
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_VarA_ConstB(And_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_And_OperatorDto_VarA_ConstB(dto);

            MathPropertiesDto bMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            if (bMathProperties.IsConstZero)
            {
                // 0
                return new Number_OperatorDto_Zero();
            }
            else if (bMathProperties.IsConstNonZero)
            {
                // Identity
                return dto.AOperatorDto;
            }
            else if (bMathProperties.IsConstSpecialValue)
            {
                // Undefined
                return new Number_OperatorDto_NaN();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstSignal(BandPassFilterConstantPeakGain_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstSignal(BandPassFilterConstantTransitionGain_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Bundle_OperatorDto(Bundle_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_MultiChannel(Cache_OperatorDto_MultiChannel dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_SingleChannel(Cache_OperatorDto_SingleChannel dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_CustomOperator_OperatorDto(CustomOperator_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_DimensionToOutlets_OperatorDto(DimensionToOutlets_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_ConstOrigin(Divide_OperatorDto_ConstA_ConstB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_VarOrigin(Divide_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_ZeroOrigin(Divide_OperatorDto_ConstA_ConstB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ConstOrigin(Divide_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_VarOrigin(Divide_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(Divide_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ConstOrigin(Divide_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_VarOrigin(Divide_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(Divide_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ConstOrigin(Divide_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_VarOrigin(Divide_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(Divide_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        // Equal

        protected override OperatorDtoBase Visit_Equal_OperatorDto_ConstA_ConstB(Equal_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Equal_OperatorDto_ConstA_ConstB(dto);

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

        protected override OperatorDtoBase Visit_Equal_OperatorDto_ConstA_VarB(Equal_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Equal_OperatorDto_ConstA_VarB(dto);

            // Commute
            return new Equal_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_ConstB(Equal_OperatorDto_VarA_ConstB dto)
        {
            // Do nothing
            return base.Visit_Equal_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_VarB(Equal_OperatorDto_VarA_VarB dto)
        {
            // Do nothing
            return base.Visit_Equal_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio(Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio(Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio(Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_VarHigh_VarRatio(Exponent_OperatorDto_ConstLow_VarHigh_VarRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio(Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_ConstHigh_VarRatio(Exponent_OperatorDto_VarLow_ConstHigh_VarRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_VarHigh_ConstRatio(Exponent_OperatorDto_VarLow_VarHigh_ConstRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_VarHigh_VarRatio(Exponent_OperatorDto_VarLow_VarHigh_VarRatio dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        // GreaterThanOrEqual

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_ConstA_ConstB(GreaterThanOrEqual_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_GreaterThanOrEqual_OperatorDto_ConstA_ConstB(dto);

            // Pre-calculate
            if (dto.A >= dto.B)
            {
                return new Number_OperatorDto_One();
            }
            else
            {
                return new Number_OperatorDto_Zero();
            }
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_ConstA_VarB(GreaterThanOrEqual_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_GreaterThanOrEqual_OperatorDto_ConstA_VarB(dto);

            // Commute, switch sign
            return new LessThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(GreaterThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            // Do nothing
            return base.Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(GreaterThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            // Do nothing
            return base.Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_ConstA_ConstB(GreaterThan_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_GreaterThan_OperatorDto_ConstA_ConstB(dto);

            // Pre-calculate
            if (dto.A > dto.B)
            {
                return new Number_OperatorDto_One();
            }
            else
            {
                return new Number_OperatorDto_Zero();
            }
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_ConstA_VarB(GreaterThan_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_GreaterThan_OperatorDto_ConstA_VarB(dto);

            // Commute, switch sign
            return new LessThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_ConstB(GreaterThan_OperatorDto_VarA_ConstB dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_VarB(GreaterThan_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_AllVars(HighPassFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_ManyConsts(HighPassFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_ConstSignal(HighPassFilter_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_AllVars(HighShelfFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_ManyConsts(HighShelfFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_ConstSignal(HighShelfFilter_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Hold_OperatorDto(Hold_OperatorDto_VarSignal dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_ConstThen_ConstElse(If_OperatorDto_ConstCondition_ConstThen_ConstElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_ConstThen_VarElse(If_OperatorDto_ConstCondition_ConstThen_VarElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_VarThen_ConstElse(If_OperatorDto_ConstCondition_VarThen_ConstElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_VarThen_VarElse(If_OperatorDto_ConstCondition_VarThen_VarElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
        {
            throw new NotImplementedException();
        }

        // LessThanOrEqual

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_ConstA_ConstB(LessThanOrEqual_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_LessThanOrEqual_OperatorDto_ConstA_ConstB(dto);

            if (dto.A <= dto.B)
            {
                return new Number_OperatorDto_One();
            }
            else
            {
                return new Number_OperatorDto_Zero();
            }
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_ConstA_VarB(LessThanOrEqual_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_LessThanOrEqual_OperatorDto_ConstA_VarB(dto);

            // Commute, switch sign
            return new GreaterThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(LessThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            // Do nothing
            return base.Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_VarB(LessThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            // Do nothing
            return base.Visit_LessThanOrEqual_OperatorDto_VarA_VarB(dto);
        }

        // LessThan

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_ConstA_ConstB(LessThan_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_LessThan_OperatorDto_ConstA_ConstB(dto);

            // Pre-calculate
            if (dto.A < dto.B)
            {
                return new Number_OperatorDto_One();
            }
            else
            {
                return new Number_OperatorDto_Zero();
            }
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_ConstA_VarB(LessThan_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_LessThan_OperatorDto_ConstA_VarB(dto);

            // Commute, switch sign
            return new GreaterThan_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_ConstB(LessThan_OperatorDto_VarA_ConstB dto)
        {
            // Do nothing
            return base.Visit_LessThan_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_VarB(LessThan_OperatorDto_VarA_VarB dto)
        {
            // Do nothing
            return base.Visit_LessThan_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_AllVars(LowPassFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_ManyConsts(LowPassFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_ConstSignal(LowPassFilter_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_AllVars(LowShelfFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_ManyConsts(LowShelfFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_ConstSignal(LowShelfFilter_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        // MaxOverInlets

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_NoVars_Consts(MaxOverInlets_OperatorDto_NoVars_Consts dto)
        {
            return Process_OperatorDto_NoVars_Consts(dto, Enumerable.Max);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts(MaxOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_Consts(MaxOverInlets_OperatorDto_Vars_Consts dto)
        {
            base.Visit_MaxOverInlets_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            double constValue = dto.Consts.Max();

            return new MaxOverInlets_OperatorDto_Vars_1Const { Vars = dto.Vars, ConstValue = constValue };
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return Process_OperatorDto_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto)
        {
            // Do nothing
            return base.Visit_MaxOverInlets_OperatorDto_1Var_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto)
        {
            // Do nothing
            return base.Visit_MaxOverInlets_OperatorDto_2Vars(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            // Do nothing
            return base.Visit_MaxOverInlets_OperatorDto_Vars_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        // MinOverInlets

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_NoVars_Consts(MinOverInlets_OperatorDto_NoVars_Consts dto)
        {
            return Process_OperatorDto_NoVars_Consts(dto, Enumerable.Min);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_NoVars_NoConsts(MinOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_Consts(MinOverInlets_OperatorDto_Vars_Consts dto)
        {
            base.Visit_MinOverInlets_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            double constValue = dto.Consts.Min();

            return new MinOverInlets_OperatorDto_Vars_1Const { Vars = dto.Vars, ConstValue = constValue };
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return Process_OperatorDto_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_1Var_1Const(MinOverInlets_OperatorDto_1Var_1Const dto)
        {
            // Do nothing
            return base.Visit_MinOverInlets_OperatorDto_1Var_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_2Vars(MinOverInlets_OperatorDto_2Vars dto)
        {
            // Do nothing
            return base.Visit_MinOverInlets_OperatorDto_2Vars(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            // Do nothing
            return base.Visit_MinOverInlets_OperatorDto_Vars_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        // Multiply

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_NoVars_Consts(Multiply_OperatorDto_NoVars_Consts dto)
        {
            return Process_OperatorDto_NoVars_Consts(dto, CollectionExtensions.Product);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_NoVars_NoConsts(Multiply_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            return Process_OperatorDto_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_Consts(Multiply_OperatorDto_Vars_Consts dto)
        {
            base.Visit_Multiply_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            double constValue = dto.Consts.Product();

            var dto2 = new Multiply_OperatorDto_Vars_1Const { Vars = dto.Vars, ConstValue = constValue };

            return Visit_Multiply_OperatorDto_Vars_1Const(dto2);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            MathPropertiesDto constMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.ConstValue);

            // Identity
            if (constMathProperties.IsConstOne)
            {
                var dto2 = new Multiply_OperatorDto_Vars_NoConsts { Vars = dto.Vars };
                return Visit_Multiply_OperatorDto_Vars_NoConsts(dto2);
            }

            return dto;
        }

        // Negative

        protected override OperatorDtoBase Visit_Negative_OperatorDto_ConstX(Negative_OperatorDto_ConstX dto)
        {
            base.Visit_Negative_OperatorDto_ConstX(dto);

            // Pre-calculate
            return new Number_OperatorDto { Number = -dto.X };
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto_VarX(Negative_OperatorDto_VarX dto)
        {
            // Do nothing
            return base.Visit_Negative_OperatorDto_VarX(dto);
        }

        protected override OperatorDtoBase Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_AllVars(NotchFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_ManyConsts(NotchFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_ConstSignal(NotchFilter_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        // NotEqual

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_ConstA_ConstB(NotEqual_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_NotEqual_OperatorDto_ConstA_ConstB(dto);

            // Pre-calculate
            if (dto.A != dto.B)
            {
                return new Number_OperatorDto_One();
            }
            else
            {
                return new Number_OperatorDto_Zero();
            }
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_ConstA_VarB(NotEqual_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_NotEqual_OperatorDto_ConstA_VarB(dto);

            // Commute
            return new NotEqual_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_ConstB(NotEqual_OperatorDto_VarA_ConstB dto)
        {
            // Do nothing
            return base.Visit_NotEqual_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_VarB(NotEqual_OperatorDto_VarA_VarB dto)
        {
            // Do nothing
            return base.Visit_NotEqual_OperatorDto_VarA_VarB(dto);
        }

        // Not

        protected override OperatorDtoBase Visit_Not_OperatorDto_ConstX(Not_OperatorDto_ConstX dto)
        {
            base.Visit_Not_OperatorDto_ConstX(dto);

            // Pre-calculate
            bool isFalse = dto.X == 0.0;
            if (isFalse)
            {
                return new Number_OperatorDto_One();
            }
            else
            {
                return new Number_OperatorDto_Zero();
            }
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto_VarX(Not_OperatorDto_VarX dto)
        {
            // Do nothing
            return base.Visit_Not_OperatorDto_VarX(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            // Do nothing
            return base.Visit_Number_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            // Do nothing
            return base.Visit_Number_OperatorDto_NaN(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            // Do nothing
            return base.Visit_Number_OperatorDto_One(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            // Do nothing
            return base.Visit_Number_OperatorDto_Zero(dto);
        }

        // OneOverX

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_ConstX(OneOverX_OperatorDto_ConstX dto)
        {
            base.Visit_OneOverX_OperatorDto_ConstX(dto);

            // Pre-calculate
            return new Number_OperatorDto { Number = 1.0 / dto.X };
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_VarX(OneOverX_OperatorDto_VarX dto)
        {
            // Do nothing
            return base.Visit_OneOverX_OperatorDto_VarX(dto);
        }

        // Or

        protected override OperatorDtoBase Visit_Or_OperatorDto_ConstA_ConstB(Or_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Or_OperatorDto_ConstA_ConstB(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.A);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            // Pre-calculate
            if (aMathPropertiesDto.IsConstNonZero || bMathPropertiesDto.IsConstNonZero)
            {
                return new Number_OperatorDto_One();
            }
            else if (aMathPropertiesDto.IsConstZero && bMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }
            else if (aMathPropertiesDto.IsConstSpecialValue || bMathPropertiesDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_ConstA_VarB(Or_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Or_OperatorDto_ConstA_VarB(dto);

            // Commute
            var dto2 = new Or_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };

            return Visit_Or_OperatorDto_VarA_ConstB(dto2);
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_VarA_ConstB(Or_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Or_OperatorDto_VarA_ConstB(dto);

            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            if (bMathPropertiesDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }
            else if (bMathPropertiesDto.IsConstNonZero)
            {
                return new Number_OperatorDto_One();
            }
            else if (bMathPropertiesDto.IsConstZero)
            {
                // Identity
                return dto.AOperatorDto;
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
        {
            // Do nothing
            return base.Visit_Or_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_PatchInlet_OperatorDto(PatchInlet_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_PatchOutlet_OperatorDto(PatchOutlet_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_AllVars(PeakingEQFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_ManyConsts(PeakingEQFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_ConstSignal(PeakingEQFilter_OperatorDto_ConstSignal dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        // Power

        protected override OperatorDtoBase Visit_Power_OperatorDto_ConstBase_ConstExponent(Power_OperatorDto_ConstBase_ConstExponent dto)
        {
            base.Visit_Power_OperatorDto_ConstBase_ConstExponent(dto);

            // Pre-calculate
            return new Number_OperatorDto { Number = Math.Pow(dto.Base, dto.Exponent) };
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
        {
            base.Visit_Power_OperatorDto_ConstBase_VarExponent(dto);

            MathPropertiesDto baseMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.Base);

            if (baseMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }
            else if (baseMathPropertiesDto.IsConstOne)
            {
                return new Number_OperatorDto_One();
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            base.Visit_Power_OperatorDto_VarBase_ConstExponent(dto);

            MathPropertiesDto exponentMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.Exponent);

            if (exponentMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_One();
            }
            else if (exponentMathPropertiesDto.IsConstOne)
            {
                return dto.BaseOperatorDto;
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
        {
            // Do nothing
            return base.Visit_Power_OperatorDto_VarBase_VarExponent(dto);
        }

        protected override OperatorDtoBase Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicAbruptSlope(Random_OperatorDto_CubicAbruptSlope dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicEquidistant(Random_OperatorDto_CubicEquidistant dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicSmoothSlope(Random_OperatorDto_CubicSmoothSlope dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Hermite(Random_OperatorDto_Hermite dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Line(Random_OperatorDto_Line dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Stripe(Random_OperatorDto_Stripe dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorCalculator_OnlyConsts(RangeOverDimension_OperatorCalculator_OnlyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorCalculator_OnlyVars(RangeOverDimension_OperatorCalculator_OnlyVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_ConstFrom_ConstStep(RangeOverOutlets_OperatorDto_ConstFrom_ConstStep dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_OperatorDto_ConstFrom_VarStep dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_OperatorDto_VarFrom_ConstStep dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_VarFrom_VarStep(RangeOverOutlets_OperatorDto_VarFrom_VarStep dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDtoBase_ConstSpeed_NoOriginShifting(Reverse_OperatorDtoBase_ConstSpeed_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDtoBase_ConstSpeed_WithOriginShifting(Reverse_OperatorDtoBase_ConstSpeed_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDtoBase_VarSpeed_NoPhaseTracking(Reverse_OperatorDtoBase_VarSpeed_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDtoBase_VarSpeed_WithPhaseTracking(Reverse_OperatorDtoBase_VarSpeed_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto_AllVars(Scaler_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto_ManyConsts(Scaler_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_ConstSignal_ConstPosition(Select_OperatorDto_ConstSignal_ConstPosition dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_ConstSignal_VarPosition(Select_OperatorDto_ConstSignal_VarPosition dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_VarSignal_ConstPosition(Select_OperatorDto_VarSignal_ConstPosition dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_VarSignal_VarPosition(Select_OperatorDto_VarSignal_VarPosition dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_ConstValue(SetDimension_OperatorDto_ConstValue dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_VarValue(SetDimension_OperatorDto_VarValue dto)
        {
            throw new NotImplementedException();
        }

        // Shift

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);

            MathPropertiesDto distanceMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.Distance);
            if (distanceMathProperties.IsConstZero)
            {
                // Identity
                return dto.SignalOperatorDto;
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            // Do nothing
            return base.Visit_Shift_OperatorDto_VarSignal_VarDistance(dto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SortOverInlets_OperatorDto(SortOverInlets_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Spectrum_OperatorDto(Spectrum_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin(Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
        {
            return Process_ConstSignal(dto.Signal);
        }

        // Subtract

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_ConstA_ConstB(Subtract_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Subtract_OperatorDto_ConstA_ConstB(dto);

            // Pre-calculate
            return new Number_OperatorDto { Number = dto.A - dto.B };
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Subtract_OperatorDto_ConstA_VarB(dto);

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.A);

            if (aMathPropertiesDto.IsConstZero)
            {
                // Identity, switch sign
                return new Negative_OperatorDto_VarX { XOperatorDto = dto.BOperatorDto };
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Subtract_OperatorDto_VarA_ConstB(dto);

            MathPropertiesDto bMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            if (bMathProperties.IsConstZero)
            {
                // Identity
                return dto.AOperatorDto;
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
        {
            // Do nothing
            return base.Visit_Subtract_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_SumFollower_OperatorDto(SumFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto_ConstOrigin(TimePower_OperatorDto_ConstOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto_VarOrigin(TimePower_OperatorDto_VarOrigin dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Unbundle_OperatorDto(Unbundle_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        // Helpers

        private OperatorDtoBase Process_OperatorDto_NoVars_Consts(
            OperatorDtoBase_Consts dto, 
            Func<IEnumerable<double>, double> aggregationDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            // Pre-calculate
            double result = aggregationDelegate(dto.Consts);

            return new Number_OperatorDto { Number = result };
        }

        private OperatorDtoBase Process_OperatorDto_NoVars_NoConsts(OperatorDtoBase dto)
        {
            base.Visit_OperatorDto_Base(dto);

            // 0
            return new Number_OperatorDto_Zero();
        }

        private OperatorDtoBase Process_OperatorDto_Vars_NoConsts(OperatorDtoBase_Vars dto)
        {
            base.Visit_OperatorDto_Base(dto);

            switch (dto.Vars.Count)
            {
                case 0:
                    // 0
                    return new Number_OperatorDto_Zero();

                case 1:
                    return dto.Vars[0];

                default:
                    return dto;
            }
        }

        private OperatorDtoBase Process_ConstSignal(double signal)
        {
            // Identity
            return new Number_OperatorDto { Number = signal };
        }
    }
}
using System;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitorBase_AfterMathSimplification : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        protected sealed override IOperatorDto Visit_Absolute_OperatorDto_ConstX(Absolute_OperatorDto_ConstX dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_AllPassFilter_OperatorDto_ConstSound(AllPassFilter_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_And_OperatorDto_ConstA_ConstB(And_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_And_OperatorDto_ConstA_VarB(And_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_And_OperatorDto_VarA_ConstB(And_OperatorDto_VarA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_AverageFollower_OperatorDto_ConstSignal(AverageFollower_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_AverageOverDimension_OperatorDto_ConstSignal(AverageOverDimension_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_AverageOverInlets_OperatorDto_AllConsts(AverageOverInlets_OperatorDto_AllConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstSound(BandPassFilterConstantPeakGain_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstSound(BandPassFilterConstantTransitionGain_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset(ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ChangeTrigger_OperatorDto_ConstPassThrough_VarReset(ChangeTrigger_OperatorDto_ConstPassThrough_VarReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ChangeTrigger_OperatorDto_VarPassThrough_ConstReset(ChangeTrigger_OperatorDto_VarPassThrough_ConstReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(ClosestOverInlets_OperatorDto_ConstInput_ConstItems dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Curve_OperatorDto_NoCurve(Curve_OperatorDto_NoCurve dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Divide_OperatorDto_ConstA_ConstB_ConstOrigin(Divide_OperatorDto_ConstA_ConstB_ConstOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Divide_OperatorDto_ConstA_ConstB_ZeroOrigin(Divide_OperatorDto_ConstA_ConstB_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Equal_OperatorDto_ConstA_ConstB(Equal_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Equal_OperatorDto_ConstA_VarB(Equal_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio(Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_ConstA_ConstB(GreaterThanOrEqual_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_ConstA_VarB(GreaterThanOrEqual_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_GreaterThan_OperatorDto_ConstA_ConstB(GreaterThan_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_GreaterThan_OperatorDto_ConstA_VarB(GreaterThan_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_HighPassFilter_OperatorDto_ConstSound(HighPassFilter_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_HighShelfFilter_OperatorDto_ConstSound(HighShelfFilter_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Hold_OperatorDto_ConstSignal(Hold_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_If_OperatorDto_ConstCondition_ConstThen_ConstElse(If_OperatorDto_ConstCondition_ConstThen_ConstElse dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_If_OperatorDto_ConstCondition_ConstThen_VarElse(If_OperatorDto_ConstCondition_ConstThen_VarElse dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_If_OperatorDto_ConstCondition_VarThen_ConstElse(If_OperatorDto_ConstCondition_VarThen_ConstElse dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_If_OperatorDto_ConstCondition_VarThen_VarElse(If_OperatorDto_ConstCondition_VarThen_VarElse dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_LessThanOrEqual_OperatorDto_ConstA_ConstB(LessThanOrEqual_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_LessThanOrEqual_OperatorDto_ConstA_VarB(LessThanOrEqual_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_LessThan_OperatorDto_ConstA_ConstB(LessThan_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_LessThan_OperatorDto_ConstA_VarB(LessThan_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Loop_OperatorDto_ConstSignal(Loop_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_LowPassFilter_OperatorDto_ConstSound(LowPassFilter_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_LowShelfFilter_OperatorDto_ConstSound(LowShelfFilter_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MaxFollower_OperatorDto_ConstSignal(MaxFollower_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_Consts(MaxOverInlets_OperatorDto_NoVars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts(MaxOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_Consts(MaxOverInlets_OperatorDto_Vars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MinFollower_OperatorDto_ConstSignal(MinFollower_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MinOverInlets_OperatorDto_NoVars_Consts(MinOverInlets_OperatorDto_NoVars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MinOverInlets_OperatorDto_NoVars_NoConsts(MinOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_Consts(MinOverInlets_OperatorDto_Vars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Multiply_OperatorDto_NoVars_Consts(Multiply_OperatorDto_NoVars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Multiply_OperatorDto_NoVars_NoConsts(Multiply_OperatorDto_NoVars_NoConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Multiply_OperatorDto_Vars_Consts(Multiply_OperatorDto_Vars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Negative_OperatorDto_ConstX(Negative_OperatorDto_ConstX dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_NotchFilter_OperatorDto_ConstSound(NotchFilter_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_NotEqual_OperatorDto_ConstA_ConstB(NotEqual_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_NotEqual_OperatorDto_ConstA_VarB(NotEqual_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Not_OperatorDto_ConstX(Not_OperatorDto_ConstX dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_OneOverX_OperatorDto_ConstX(OneOverX_OperatorDto_ConstX dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Or_OperatorDto_ConstA_ConstB(Or_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Or_OperatorDto_ConstA_VarB(Or_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Or_OperatorDto_VarA_ConstB(Or_OperatorDto_VarA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_PeakingEQFilter_OperatorDto_ConstSound(PeakingEQFilter_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Power_OperatorDto_ConstBase_ConstExponent(Power_OperatorDto_ConstBase_ConstExponent dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_PulseTrigger_OperatorDto_ConstPassThrough_ConstReset(PulseTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_PulseTrigger_OperatorDto_ConstPassThrough_VarReset(PulseTrigger_OperatorDto_ConstPassThrough_VarReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_PulseTrigger_OperatorDto_VarPassThrough_ConstReset(PulseTrigger_OperatorDto_VarPassThrough_ConstReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Pulse_OperatorDto_ZeroFrequency(Pulse_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ZeroStep(RangeOverOutlets_Outlet_OperatorDto_ZeroStep dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Reverse_OperatorDto_ConstSignal(Reverse_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Sample_OperatorDto_NoSample(Sample_OperatorDto_NoSample dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Sample_OperatorDto_ZeroFrequency(Sample_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_SawDown_OperatorDto_ZeroFrequency(SawDown_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_SawUp_OperatorDto_ZeroFrequency(SawUp_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Scaler_OperatorDto_AllConsts(Scaler_OperatorDto_AllConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_ConstX(SetDimension_OperatorDto_ConstPassThrough_ConstX dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_VarX(SetDimension_OperatorDto_ConstPassThrough_VarX dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Sine_OperatorDto_ZeroFrequency(Sine_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Spectrum_OperatorDto_ConstSound(Spectrum_OperatorDto_ConstSound dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Square_OperatorDto_ZeroFrequency(Square_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin(Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Subtract_OperatorDto_ConstA_ConstB(Subtract_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_TimePower_OperatorDto_ConstSignal(TimePower_OperatorDto_ConstSignal dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset(ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ToggleTrigger_OperatorDto_ConstPassThrough_VarReset(ToggleTrigger_OperatorDto_ConstPassThrough_VarReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_ToggleTrigger_OperatorDto_VarPassThrough_ConstReset(ToggleTrigger_OperatorDto_VarPassThrough_ConstReset dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Triangle_OperatorDto_ZeroFrequency(Triangle_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }
    }
}

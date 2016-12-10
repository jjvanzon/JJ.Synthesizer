using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitorBase_AfterMathSimplification : OperatorDtoVisitorBase_AfterClassSpecialization
    {
        protected override OperatorDtoBase Visit_Absolute_OperatorDto_ConstX(Absolute_OperatorDto_ConstX dto)
        {
            return base.Visit_Absolute_OperatorDto_ConstX(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            return base.Visit_Add_OperatorDto_NoVars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            return base.Visit_Add_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
        {
            return base.Visit_Add_OperatorDto_Vars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_ConstSignal(AllPassFilter_OperatorDto_ConstSignal dto)
        {
            return base.Visit_AllPassFilter_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_ConstA_ConstB(And_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_And_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_ConstA_VarB(And_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_And_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_VarA_ConstB(And_OperatorDto_VarA_ConstB dto)
        {
            return base.Visit_And_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_AverageFollower_OperatorDto_ConstSignal(AverageFollower_OperatorDto_ConstSignal dto)
        {
            return base.Visit_AverageFollower_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto_ConstSignal(AverageOverDimension_OperatorDto_ConstSignal dto)
        {
            return base.Visit_AverageOverDimension_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_AverageOverInlets_OperatorDto_AllConsts(AverageOverInlets_OperatorDto_AllConsts dto)
        {
            return base.Visit_AverageOverInlets_OperatorDto_AllConsts(dto);
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstSignal(BandPassFilterConstantPeakGain_OperatorDto_ConstSignal dto)
        {
            return base.Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstSignal(BandPassFilterConstantTransitionGain_OperatorDto_ConstSignal dto)
        {
            return base.Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto)
        {
            return base.Visit_Cache_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset(ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
        {
            return base.Visit_ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset(dto);
        }

        protected override OperatorDtoBase Visit_ChangeTrigger_OperatorDto_ConstPassThrough_VarReset(ChangeTrigger_OperatorDto_ConstPassThrough_VarReset dto)
        {
            return base.Visit_ChangeTrigger_OperatorDto_ConstPassThrough_VarReset(dto);
        }

        protected override OperatorDtoBase Visit_ChangeTrigger_OperatorDto_VarPassThrough_ConstReset(ChangeTrigger_OperatorDto_VarPassThrough_ConstReset dto)
        {
            return base.Visit_ChangeTrigger_OperatorDto_VarPassThrough_ConstReset(dto);
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems dto)
        {
            return base.Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(dto);
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(ClosestOverInlets_OperatorDto_ConstInput_ConstItems dto)
        {
            return base.Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(dto);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_ConstOrigin(Divide_OperatorDto_ConstA_ConstB_ConstOrigin dto)
        {
            return base.Visit_Divide_OperatorDto_ConstA_ConstB_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_ZeroOrigin(Divide_OperatorDto_ConstA_ConstB_ZeroOrigin dto)
        {
            return base.Visit_Divide_OperatorDto_ConstA_ConstB_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_ConstA_ConstB(Equal_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_Equal_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_ConstA_VarB(Equal_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_Equal_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio(Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio dto)
        {
            return base.Visit_Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_ConstA_ConstB(GreaterThanOrEqual_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_GreaterThanOrEqual_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_ConstA_VarB(GreaterThanOrEqual_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_GreaterThanOrEqual_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_ConstA_ConstB(GreaterThan_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_GreaterThan_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_ConstA_VarB(GreaterThan_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_GreaterThan_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_ConstSignal(HighPassFilter_OperatorDto_ConstSignal dto)
        {
            return base.Visit_HighPassFilter_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_ConstSignal(HighShelfFilter_OperatorDto_ConstSignal dto)
        {
            return base.Visit_HighShelfFilter_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Hold_OperatorDto_ConstSignal(Hold_OperatorDto_ConstSignal dto)
        {
            return base.Visit_Hold_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_ConstThen_ConstElse(If_OperatorDto_ConstCondition_ConstThen_ConstElse dto)
        {
            return base.Visit_If_OperatorDto_ConstCondition_ConstThen_ConstElse(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_ConstThen_VarElse(If_OperatorDto_ConstCondition_ConstThen_VarElse dto)
        {
            return base.Visit_If_OperatorDto_ConstCondition_ConstThen_VarElse(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_VarThen_ConstElse(If_OperatorDto_ConstCondition_VarThen_ConstElse dto)
        {
            return base.Visit_If_OperatorDto_ConstCondition_VarThen_ConstElse(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_ConstCondition_VarThen_VarElse(If_OperatorDto_ConstCondition_VarThen_VarElse dto)
        {
            return base.Visit_If_OperatorDto_ConstCondition_VarThen_VarElse(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto)
        {
            return base.Visit_Interpolate_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_ConstA_ConstB(LessThanOrEqual_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_LessThanOrEqual_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_ConstA_VarB(LessThanOrEqual_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_LessThanOrEqual_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_ConstA_ConstB(LessThan_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_LessThan_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_ConstA_VarB(LessThan_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_LessThan_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ConstSignal(Loop_OperatorDto_ConstSignal dto)
        {
            return base.Visit_Loop_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_ConstSignal(LowPassFilter_OperatorDto_ConstSignal dto)
        {
            return base.Visit_LowPassFilter_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_ConstSignal(LowShelfFilter_OperatorDto_ConstSignal dto)
        {
            return base.Visit_LowShelfFilter_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_MaxFollower_OperatorDto_ConstSignal(MaxFollower_OperatorDto_ConstSignal dto)
        {
            return base.Visit_MaxFollower_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto)
        {
            return base.Visit_MaxOverDimension_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_NoVars_Consts(MaxOverInlets_OperatorDto_NoVars_Consts dto)
        {
            return base.Visit_MaxOverInlets_OperatorDto_NoVars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts(MaxOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            return base.Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_Consts(MaxOverInlets_OperatorDto_Vars_Consts dto)
        {
            return base.Visit_MaxOverInlets_OperatorDto_Vars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_MinFollower_OperatorDto_ConstSignal(MinFollower_OperatorDto_ConstSignal dto)
        {
            return base.Visit_MinFollower_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto)
        {
            return base.Visit_MinOverDimension_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_NoVars_Consts(MinOverInlets_OperatorDto_NoVars_Consts dto)
        {
            return base.Visit_MinOverInlets_OperatorDto_NoVars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_NoVars_NoConsts(MinOverInlets_OperatorDto_NoVars_NoConsts dto)
        {
            return base.Visit_MinOverInlets_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_Consts(MinOverInlets_OperatorDto_Vars_Consts dto)
        {
            return base.Visit_MinOverInlets_OperatorDto_Vars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin dto)
        {
            return base.Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin dto)
        {
            return base.Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            return base.Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            return base.Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            return base.Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_NoVars_Consts(Multiply_OperatorDto_NoVars_Consts dto)
        {
            return base.Visit_Multiply_OperatorDto_NoVars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_NoVars_NoConsts(Multiply_OperatorDto_NoVars_NoConsts dto)
        {
            return base.Visit_Multiply_OperatorDto_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_Consts(Multiply_OperatorDto_Vars_Consts dto)
        {
            return base.Visit_Multiply_OperatorDto_Vars_Consts(dto);
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto_ConstX(Negative_OperatorDto_ConstX dto)
        {
            return base.Visit_Negative_OperatorDto_ConstX(dto);
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_ConstSignal(NotchFilter_OperatorDto_ConstSignal dto)
        {
            return base.Visit_NotchFilter_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_ConstA_ConstB(NotEqual_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_NotEqual_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_ConstA_VarB(NotEqual_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_NotEqual_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto_ConstX(Not_OperatorDto_ConstX dto)
        {
            return base.Visit_Not_OperatorDto_ConstX(dto);
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_ConstX(OneOverX_OperatorDto_ConstX dto)
        {
            return base.Visit_OneOverX_OperatorDto_ConstX(dto);
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_ConstA_ConstB(Or_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_Or_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_ConstA_VarB(Or_OperatorDto_ConstA_VarB dto)
        {
            return base.Visit_Or_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_VarA_ConstB(Or_OperatorDto_VarA_ConstB dto)
        {
            return base.Visit_Or_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_ConstSignal(PeakingEQFilter_OperatorDto_ConstSignal dto)
        {
            return base.Visit_PeakingEQFilter_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_ConstBase_ConstExponent(Power_OperatorDto_ConstBase_ConstExponent dto)
        {
            return base.Visit_Power_OperatorDto_ConstBase_ConstExponent(dto);
        }

        protected override OperatorDtoBase Visit_PulseTrigger_OperatorDto_ConstPassThrough_ConstReset(PulseTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
        {
            return base.Visit_PulseTrigger_OperatorDto_ConstPassThrough_ConstReset(dto);
        }

        protected override OperatorDtoBase Visit_PulseTrigger_OperatorDto_ConstPassThrough_VarReset(PulseTrigger_OperatorDto_ConstPassThrough_VarReset dto)
        {
            return base.Visit_PulseTrigger_OperatorDto_ConstPassThrough_VarReset(dto);
        }

        protected override OperatorDtoBase Visit_PulseTrigger_OperatorDto_VarPassThrough_ConstReset(PulseTrigger_OperatorDto_VarPassThrough_ConstReset dto)
        {
            return base.Visit_PulseTrigger_OperatorDto_VarPassThrough_ConstReset(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting dto)
        {
            return base.Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting dto)
        {
            return base.Visit_Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking dto)
        {
            return base.Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking dto)
        {
            return base.Visit_Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ZeroFrequency(Pulse_OperatorDto_ZeroFrequency dto)
        {
            return base.Visit_Pulse_OperatorDto_ZeroFrequency(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_ConstSignal(Reverse_OperatorDto_ConstSignal dto)
        {
            return base.Visit_Reverse_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto)
        {
            return base.Visit_Round_OperatorDto_AllConsts(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ZeroFrequency(Sample_OperatorDto_ZeroFrequency dto)
        {
            return base.Visit_Sample_OperatorDto_ZeroFrequency(dto);
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ZeroFrequency(SawDown_OperatorDto_ZeroFrequency dto)
        {
            return base.Visit_SawDown_OperatorDto_ZeroFrequency(dto);
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ZeroFrequency(SawUp_OperatorDto_ZeroFrequency dto)
        {
            return base.Visit_SawUp_OperatorDto_ZeroFrequency(dto);
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto_AllConsts(Scaler_OperatorDto_AllConsts dto)
        {
            return base.Visit_Scaler_OperatorDto_AllConsts(dto);
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_ConstSignal_ConstPosition(Select_OperatorDto_ConstSignal_ConstPosition dto)
        {
            return base.Visit_Select_OperatorDto_ConstSignal_ConstPosition(dto);
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_ConstSignal_VarPosition(Select_OperatorDto_ConstSignal_VarPosition dto)
        {
            return base.Visit_Select_OperatorDto_ConstSignal_VarPosition(dto);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            return base.Visit_Shift_OperatorDto_ConstSignal_ConstDistance(dto);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            return base.Visit_Shift_OperatorDto_ConstSignal_VarDistance(dto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ZeroFrequency(Sine_OperatorDto_ZeroFrequency dto)
        {
            return base.Visit_Sine_OperatorDto_ZeroFrequency(dto);
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto)
        {
            return base.Visit_SortOverDimension_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Spectrum_OperatorDto_ConstSignal(Spectrum_OperatorDto_ConstSignal dto)
        {
            return base.Visit_Spectrum_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ZeroFrequency(Square_OperatorDto_ZeroFrequency dto)
        {
            return base.Visit_Square_OperatorDto_ZeroFrequency(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin(Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
        {
            return base.Visit_Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin dto)
        {
            return base.Visit_Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_ConstA_ConstB(Subtract_OperatorDto_ConstA_ConstB dto)
        {
            return base.Visit_Subtract_OperatorDto_ConstA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto)
        {
            return base.Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(dto);
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto)
        {
            return base.Visit_SumOverDimension_OperatorDto_AllConsts(dto);
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto_ConstSignal(TimePower_OperatorDto_ConstSignal dto)
        {
            return base.Visit_TimePower_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset(ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset dto)
        {
            return base.Visit_ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset(dto);
        }

        protected override OperatorDtoBase Visit_ToggleTrigger_OperatorDto_ConstPassThrough_VarReset(ToggleTrigger_OperatorDto_ConstPassThrough_VarReset dto)
        {
            return base.Visit_ToggleTrigger_OperatorDto_ConstPassThrough_VarReset(dto);
        }

        protected override OperatorDtoBase Visit_ToggleTrigger_OperatorDto_VarPassThrough_ConstReset(ToggleTrigger_OperatorDto_VarPassThrough_ConstReset dto)
        {
            return base.Visit_ToggleTrigger_OperatorDto_VarPassThrough_ConstReset(dto);
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ZeroFrequency(Triangle_OperatorDto_ZeroFrequency dto)
        {
            return base.Visit_Triangle_OperatorDto_ZeroFrequency(dto);
        }
    }
}

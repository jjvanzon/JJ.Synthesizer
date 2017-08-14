using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitorBase
    {
        private readonly Dictionary<Type, Func<IOperatorDto, IOperatorDto>> _delegateDictionary;

        [DebuggerHidden]
        protected virtual IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            Type type = dto.GetType();

            if (!_delegateDictionary.TryGetValue(type, out Func<IOperatorDto, IOperatorDto> func))
            {
                throw new Exception($"No Visit method delegate found in the dictionary for {type.Name}.");
            }

            IList<InputDto> dtoInputs = dto.Inputs.ToArray();

            IOperatorDto dto2 = func(dto);

            // Revisit as long as different instances keep coming.

            while (dto2 != dto || !dto2.Inputs.SequenceEqual(dtoInputs))
            {
                dto = dto2;
                dtoInputs = dto2.Inputs.ToArray();

                dto2 = Visit_OperatorDto_Polymorphic(dto);
            }

            return dto2;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual IOperatorDto Visit_OperatorDto_Base(IOperatorDto dto)
        {
            dto.Inputs = VisitInputs(dto.Inputs);

            return dto;
        }

        [DebuggerHidden]
        protected virtual IReadOnlyList<InputDto> VisitInputs(IReadOnlyList<InputDto> sourceCollection)
        {
            return sourceCollection.Select(x => VisitInput(x)).ToArray();
        }

        [DebuggerHidden]
        protected virtual InputDto VisitInput(InputDto inputDto)
        {
            // ReSharper disable once InvertIf
            if (inputDto.IsVar)
            {
                IOperatorDto var2 = Visit_OperatorDto_Polymorphic(inputDto.Var);
                // ReSharper disable once InvertIf
                if (var2 != inputDto.Var)
                {
                    InputDto dto2 = InputDtoFactory.TryCreateInputDto(var2);
                    return dto2;
                }
            }

            return inputDto;
        }

        public OperatorDtoVisitorBase()
        {
            _delegateDictionary = new Dictionary<Type, Func<IOperatorDto, IOperatorDto>>
            {
                { typeof(Absolute_OperatorDto), x => Visit_Absolute_OperatorDto((Absolute_OperatorDto)x) },
                { typeof(Add_OperatorDto), x => Visit_Add_OperatorDto((Add_OperatorDto)x) },
                { typeof(AllPassFilter_OperatorDto), x => Visit_AllPassFilter_OperatorDto((AllPassFilter_OperatorDto)x) },
                { typeof(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(And_OperatorDto), x => Visit_And_OperatorDto((And_OperatorDto)x) },
                { typeof(AverageFollower_OperatorDto), x => Visit_AverageFollower_OperatorDto((AverageFollower_OperatorDto)x) },
                { typeof(AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar), x => Visit_AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar((AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar)x) },
                { typeof(AverageFollower_OperatorDto_ConstSignal), x => Visit_AverageFollower_OperatorDto_ConstSignal((AverageFollower_OperatorDto_ConstSignal)x) },
                { typeof(AverageOverDimension_OperatorDto), x => Visit_AverageOverDimension_OperatorDto((AverageOverDimension_OperatorDto)x) },
                { typeof(AverageOverDimension_OperatorDto_ConstSignal), x => Visit_AverageOverDimension_OperatorDto_ConstSignal((AverageOverDimension_OperatorDto_ConstSignal)x) },
                { typeof(AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous), x => Visit_AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous((AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous)x) },
                { typeof(AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset), x => Visit_AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset((AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset)x) },
                { typeof(AverageOverInlets_OperatorDto), x => Visit_AverageOverInlets_OperatorDto((AverageOverInlets_OperatorDto)x) },
                { typeof(AverageOverInlets_OperatorDto_AllConsts), x => Visit_AverageOverInlets_OperatorDto_AllConsts((AverageOverInlets_OperatorDto_AllConsts)x) },
                { typeof(AverageOverInlets_OperatorDto_Vars), x => Visit_AverageOverInlets_OperatorDto_Vars((AverageOverInlets_OperatorDto_Vars)x) },
                { typeof(BandPassFilterConstantPeakGain_OperatorDto), x => Visit_BandPassFilterConstantPeakGain_OperatorDto((BandPassFilterConstantPeakGain_OperatorDto)x) },
                { typeof(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar((BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst((BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(BandPassFilterConstantTransitionGain_OperatorDto), x => Visit_BandPassFilterConstantTransitionGain_OperatorDto((BandPassFilterConstantTransitionGain_OperatorDto)x) },
                { typeof(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar((BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst((BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(BooleanToDouble_OperatorDto), x => Visit_BooleanToDouble_OperatorDto((BooleanToDouble_OperatorDto)x) },
                { typeof(Cache_OperatorDto), x => Visit_Cache_OperatorDto((Cache_OperatorDto)x) },
                { typeof(Cache_OperatorDto_ConstSignal), x => Visit_Cache_OperatorDto_ConstSignal((Cache_OperatorDto_ConstSignal)x) },
                { typeof(Cache_OperatorDto_SingleChannel_Block), x => Visit_Cache_OperatorDto_SingleChannel_Block((Cache_OperatorDto_SingleChannel_Block)x) },
                { typeof(Cache_OperatorDto_SingleChannel_Cubic), x => Visit_Cache_OperatorDto_SingleChannel_Cubic((Cache_OperatorDto_SingleChannel_Cubic)x) },
                { typeof(Cache_OperatorDto_SingleChannel_Hermite), x => Visit_Cache_OperatorDto_SingleChannel_Hermite((Cache_OperatorDto_SingleChannel_Hermite)x) },
                { typeof(Cache_OperatorDto_SingleChannel_Line), x => Visit_Cache_OperatorDto_SingleChannel_Line((Cache_OperatorDto_SingleChannel_Line)x) },
                { typeof(Cache_OperatorDto_SingleChannel_Stripe), x => Visit_Cache_OperatorDto_SingleChannel_Stripe((Cache_OperatorDto_SingleChannel_Stripe)x) },
                { typeof(Cache_OperatorDto_MultiChannel_Block), x => Visit_Cache_OperatorDto_MultiChannel_Block((Cache_OperatorDto_MultiChannel_Block)x) },
                { typeof(Cache_OperatorDto_MultiChannel_Cubic), x => Visit_Cache_OperatorDto_MultiChannel_Cubic((Cache_OperatorDto_MultiChannel_Cubic)x) },
                { typeof(Cache_OperatorDto_MultiChannel_Hermite), x => Visit_Cache_OperatorDto_MultiChannel_Hermite((Cache_OperatorDto_MultiChannel_Hermite)x) },
                { typeof(Cache_OperatorDto_MultiChannel_Line), x => Visit_Cache_OperatorDto_MultiChannel_Line((Cache_OperatorDto_MultiChannel_Line)x) },
                { typeof(Cache_OperatorDto_MultiChannel_Stripe), x => Visit_Cache_OperatorDto_MultiChannel_Stripe((Cache_OperatorDto_MultiChannel_Stripe)x) },
                { typeof(ChangeTrigger_OperatorDto), x => Visit_ChangeTrigger_OperatorDto((ChangeTrigger_OperatorDto)x) },
                { typeof(ClosestOverDimension_OperatorDto), x => Visit_ClosestOverDimension_OperatorDto((ClosestOverDimension_OperatorDto)x) },
                { typeof(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous), x => Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous((ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous)x) },
                { typeof(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset), x => Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset((ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset)x) },
                { typeof(ClosestOverDimensionExp_OperatorDto), x => Visit_ClosestOverDimensionExp_OperatorDto((ClosestOverDimensionExp_OperatorDto)x) },
                { typeof(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous), x => Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous((ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous)x) },
                { typeof(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset), x => Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset((ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset)x) },
                { typeof(ClosestOverInlets_OperatorDto), x => Visit_ClosestOverInlets_OperatorDto((ClosestOverInlets_OperatorDto)x) },
                { typeof(ClosestOverInlets_OperatorDto_ConstInput_ConstItems), x => Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems((ClosestOverInlets_OperatorDto_ConstInput_ConstItems)x) },
                { typeof(ClosestOverInlets_OperatorDto_VarInput_VarItems), x => Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems((ClosestOverInlets_OperatorDto_VarInput_VarItems)x) },
                { typeof(ClosestOverInlets_OperatorDto_VarInput_ConstItems), x => Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems((ClosestOverInlets_OperatorDto_VarInput_ConstItems)x) },
                { typeof(ClosestOverInlets_OperatorDto_VarInput_2ConstItems), x => Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems((ClosestOverInlets_OperatorDto_VarInput_2ConstItems)x) },
                { typeof(ClosestOverInletsExp_OperatorDto), x => Visit_ClosestOverInletsExp_OperatorDto((ClosestOverInletsExp_OperatorDto)x) },
                { typeof(ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems), x => Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems((ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems)x) },
                { typeof(ClosestOverInletsExp_OperatorDto_VarInput_VarItems), x => Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems((ClosestOverInletsExp_OperatorDto_VarInput_VarItems)x) },
                { typeof(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems), x => Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems((ClosestOverInletsExp_OperatorDto_VarInput_ConstItems)x) },
                { typeof(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems), x => Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems((ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems)x) },
                { typeof(Curve_OperatorDto), x => Visit_Curve_OperatorDto((Curve_OperatorDto)x) },
                { typeof(Curve_OperatorDto_MinX_NoOriginShifting), x => Visit_Curve_OperatorDto_MinX_NoOriginShifting((Curve_OperatorDto_MinX_NoOriginShifting)x) },
                { typeof(Curve_OperatorDto_MinX_WithOriginShifting), x => Visit_Curve_OperatorDto_MinX_WithOriginShifting((Curve_OperatorDto_MinX_WithOriginShifting)x) },
                { typeof(Curve_OperatorDto_MinXZero_NoOriginShifting), x => Visit_Curve_OperatorDto_MinXZero_NoOriginShifting((Curve_OperatorDto_MinXZero_NoOriginShifting)x) },
                { typeof(Curve_OperatorDto_MinXZero_WithOriginShifting), x => Visit_Curve_OperatorDto_MinXZero_WithOriginShifting((Curve_OperatorDto_MinXZero_WithOriginShifting)x) },
                { typeof(DimensionToOutlets_Outlet_OperatorDto), x => Visit_DimensionToOutlets_Outlet_OperatorDto((DimensionToOutlets_Outlet_OperatorDto)x) },
                { typeof(Divide_OperatorDto), x => Visit_Divide_OperatorDto((Divide_OperatorDto)x) },
                { typeof(DoubleToBoolean_OperatorDto), x => Visit_DoubleToBoolean_OperatorDto((DoubleToBoolean_OperatorDto)x) },
                { typeof(Equal_OperatorDto), x => Visit_Equal_OperatorDto((Equal_OperatorDto)x) },
                { typeof(GetDimension_OperatorDto), x => Visit_GetDimension_OperatorDto((GetDimension_OperatorDto)x) },
                { typeof(GreaterThan_OperatorDto), x => Visit_GreaterThan_OperatorDto((GreaterThan_OperatorDto)x) },
                { typeof(GreaterThanOrEqual_OperatorDto), x => Visit_GreaterThanOrEqual_OperatorDto((GreaterThanOrEqual_OperatorDto)x) },
                { typeof(HighPassFilter_OperatorDto), x => Visit_HighPassFilter_OperatorDto((HighPassFilter_OperatorDto)x) },
                { typeof(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(HighShelfFilter_OperatorDto), x => Visit_HighShelfFilter_OperatorDto((HighShelfFilter_OperatorDto)x) },
                { typeof(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(Hold_OperatorDto), x => Visit_Hold_OperatorDto((Hold_OperatorDto)x) },
                { typeof(Hold_OperatorDto_VarSignal), x => Visit_Hold_OperatorDto_VarSignal((Hold_OperatorDto_VarSignal)x) },
                { typeof(Hold_OperatorDto_ConstSignal), x => Visit_Hold_OperatorDto_ConstSignal((Hold_OperatorDto_ConstSignal)x) },
                { typeof(If_OperatorDto), x => Visit_If_OperatorDto((If_OperatorDto)x) },
                { typeof(InletsToDimension_OperatorDto), x => Visit_InletsToDimension_OperatorDto((InletsToDimension_OperatorDto)x) },
                { typeof(InletsToDimension_OperatorDto_Block), x => Visit_InletsToDimension_OperatorDto_Block((InletsToDimension_OperatorDto_Block)x) },
                { typeof(InletsToDimension_OperatorDto_Stripe_LagBehind), x => Visit_InletsToDimension_OperatorDto_Stripe_LagBehind((InletsToDimension_OperatorDto_Stripe_LagBehind)x) },
                { typeof(InletsToDimension_OperatorDto_Line), x => Visit_InletsToDimension_OperatorDto_Line((InletsToDimension_OperatorDto_Line)x) },
                { typeof(InletsToDimension_OperatorDto_CubicEquidistant), x => Visit_InletsToDimension_OperatorDto_CubicEquidistant((InletsToDimension_OperatorDto_CubicEquidistant)x) },
                { typeof(InletsToDimension_OperatorDto_CubicAbruptSlope), x => Visit_InletsToDimension_OperatorDto_CubicAbruptSlope((InletsToDimension_OperatorDto_CubicAbruptSlope)x) },
                { typeof(InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind), x => Visit_InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind((InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind)x) },
                { typeof(InletsToDimension_OperatorDto_Hermite_LagBehind), x => Visit_InletsToDimension_OperatorDto_Hermite_LagBehind((InletsToDimension_OperatorDto_Hermite_LagBehind)x) },
                { typeof(Interpolate_OperatorDto), x => Visit_Interpolate_OperatorDto((Interpolate_OperatorDto)x) },
                { typeof(Interpolate_OperatorDto_ConstSignal), x => Visit_Interpolate_OperatorDto_ConstSignal((Interpolate_OperatorDto_ConstSignal)x) },
                { typeof(Interpolate_OperatorDto_Block), x => Visit_Interpolate_OperatorDto_Block((Interpolate_OperatorDto_Block)x) },
                { typeof(Interpolate_OperatorDto_CubicAbruptSlope), x => Visit_Interpolate_OperatorDto_CubicAbruptSlope((Interpolate_OperatorDto_CubicAbruptSlope)x) },
                { typeof(Interpolate_OperatorDto_CubicEquidistant), x => Visit_Interpolate_OperatorDto_CubicEquidistant((Interpolate_OperatorDto_CubicEquidistant)x) },
                { typeof(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind), x => Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind((Interpolate_OperatorDto_CubicSmoothSlope_LagBehind)x) },
                { typeof(Interpolate_OperatorDto_Hermite_LagBehind), x => Visit_Interpolate_OperatorDto_Hermite_LagBehind((Interpolate_OperatorDto_Hermite_LagBehind)x) },
                { typeof(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate), x => Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate((Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate)x) },
                { typeof(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate), x => Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate((Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate)x) },
                { typeof(Interpolate_OperatorDto_Stripe_LagBehind), x => Visit_Interpolate_OperatorDto_Stripe_LagBehind((Interpolate_OperatorDto_Stripe_LagBehind)x) },
                { typeof(LessThan_OperatorDto), x => Visit_LessThan_OperatorDto((LessThan_OperatorDto)x) },
                { typeof(LessThanOrEqual_OperatorDto), x => Visit_LessThanOrEqual_OperatorDto((LessThanOrEqual_OperatorDto)x) },
                { typeof(Loop_OperatorDto), x => Visit_Loop_OperatorDto((Loop_OperatorDto)x) },
                { typeof(Loop_OperatorDto_ConstSignal), x => Visit_Loop_OperatorDto_ConstSignal((Loop_OperatorDto_ConstSignal)x) },
                { typeof(Loop_OperatorDto_NoSkipOrRelease_ManyConstants), x => Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants((Loop_OperatorDto_NoSkipOrRelease_ManyConstants)x) },
                { typeof(Loop_OperatorDto_ManyConstants), x => Visit_Loop_OperatorDto_ManyConstants((Loop_OperatorDto_ManyConstants)x) },
                { typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration), x => Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration((Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration)x) },
                { typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration), x => Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration((Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration)x) },
                { typeof(Loop_OperatorDto_NoSkipOrRelease), x => Visit_Loop_OperatorDto_NoSkipOrRelease((Loop_OperatorDto_NoSkipOrRelease)x) },
                { typeof(Loop_OperatorDto_SignalVarOrConst_OtherInputsVar), x => Visit_Loop_OperatorDto_SignalVarOrConst_OtherInputsVar((Loop_OperatorDto_SignalVarOrConst_OtherInputsVar)x) },
                { typeof(LowPassFilter_OperatorDto), x => Visit_LowPassFilter_OperatorDto((LowPassFilter_OperatorDto)x) },
                { typeof(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(LowShelfFilter_OperatorDto), x => Visit_LowShelfFilter_OperatorDto((LowShelfFilter_OperatorDto)x) },
                { typeof(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(MaxFollower_OperatorDto), x => Visit_MaxFollower_OperatorDto((MaxFollower_OperatorDto)x) },
                { typeof(MaxFollower_OperatorDto_ConstSignal), x => Visit_MaxFollower_OperatorDto_ConstSignal((MaxFollower_OperatorDto_ConstSignal)x) },
                { typeof(MaxFollower_OperatorDto_SignalVarOrConst_OtherInputsVar), x => Visit_MaxFollower_OperatorDto_SignalVarOrConst_OtherInputsVar((MaxFollower_OperatorDto_SignalVarOrConst_OtherInputsVar)x) },
                { typeof(MaxOverDimension_OperatorDto), x => Visit_MaxOverDimension_OperatorDto((MaxOverDimension_OperatorDto)x) },
                { typeof(MaxOverDimension_OperatorDto_ConstSignal), x => Visit_MaxOverDimension_OperatorDto_ConstSignal((MaxOverDimension_OperatorDto_ConstSignal)x) },
                { typeof(MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous), x => Visit_MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous((MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous)x) },
                { typeof(MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset), x => Visit_MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset((MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset)x) },
                { typeof(MaxOverInlets_OperatorDto), x => Visit_MaxOverInlets_OperatorDto((MaxOverInlets_OperatorDto)x) },
                { typeof(MaxOverInlets_OperatorDto_Vars_Consts), x => Visit_MaxOverInlets_OperatorDto_Vars_Consts((MaxOverInlets_OperatorDto_Vars_Consts)x) },
                { typeof(MaxOverInlets_OperatorDto_Vars_NoConsts), x => Visit_MaxOverInlets_OperatorDto_Vars_NoConsts((MaxOverInlets_OperatorDto_Vars_NoConsts)x) },
                { typeof(MaxOverInlets_OperatorDto_NoVars_NoConsts), x => Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts((MaxOverInlets_OperatorDto_NoVars_NoConsts)x) },
                { typeof(MaxOverInlets_OperatorDto_NoVars_Consts), x => Visit_MaxOverInlets_OperatorDto_NoVars_Consts((MaxOverInlets_OperatorDto_NoVars_Consts)x) },
                { typeof(MaxOverInlets_OperatorDto_Vars_1Const), x => Visit_MaxOverInlets_OperatorDto_Vars_1Const((MaxOverInlets_OperatorDto_Vars_1Const)x) },
                { typeof(MaxOverInlets_OperatorDto_1Var_1Const), x => Visit_MaxOverInlets_OperatorDto_1Var_1Const((MaxOverInlets_OperatorDto_1Var_1Const)x) },
                { typeof(MaxOverInlets_OperatorDto_2Vars), x => Visit_MaxOverInlets_OperatorDto_2Vars((MaxOverInlets_OperatorDto_2Vars)x) },
                { typeof(MinFollower_OperatorDto), x => Visit_MinFollower_OperatorDto((MinFollower_OperatorDto)x) },
                { typeof(MinFollower_OperatorDto_SignalVarOrConst_OtherInputsVar), x => Visit_MinFollower_OperatorDto_SignalVarOrConst_OtherInputsVar((MinFollower_OperatorDto_SignalVarOrConst_OtherInputsVar)x) },
                { typeof(MinFollower_OperatorDto_ConstSignal), x => Visit_MinFollower_OperatorDto_ConstSignal((MinFollower_OperatorDto_ConstSignal)x) },
                { typeof(MinOverDimension_OperatorDto), x => Visit_MinOverDimension_OperatorDto((MinOverDimension_OperatorDto)x) },
                { typeof(MinOverDimension_OperatorDto_ConstSignal), x => Visit_MinOverDimension_OperatorDto_ConstSignal((MinOverDimension_OperatorDto_ConstSignal)x) },
                { typeof(MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous), x => Visit_MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous((MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous)x) },
                { typeof(MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset), x => Visit_MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset((MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset)x) },
                { typeof(MinOverInlets_OperatorDto), x => Visit_MinOverInlets_OperatorDto((MinOverInlets_OperatorDto)x) },
                { typeof(Multiply_OperatorDto), x => Visit_Multiply_OperatorDto((Multiply_OperatorDto)x) },
                { typeof(Negative_OperatorDto), x => Visit_Negative_OperatorDto((Negative_OperatorDto)x) },
                { typeof(Noise_OperatorDto), x => Visit_Noise_OperatorDto((Noise_OperatorDto)x) },
                { typeof(Not_OperatorDto), x => Visit_Not_OperatorDto((Not_OperatorDto)x) },
                { typeof(NotchFilter_OperatorDto), x => Visit_NotchFilter_OperatorDto((NotchFilter_OperatorDto)x) },
                { typeof(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(NotEqual_OperatorDto), x => Visit_NotEqual_OperatorDto((NotEqual_OperatorDto)x) },
                { typeof(Number_OperatorDto), x => Visit_Number_OperatorDto((Number_OperatorDto)x) },
                { typeof(Number_OperatorDto_NaN), x => Visit_Number_OperatorDto_NaN((Number_OperatorDto_NaN)x) },
                { typeof(Number_OperatorDto_One), x => Visit_Number_OperatorDto_One((Number_OperatorDto_One)x) },
                { typeof(Number_OperatorDto_Zero), x => Visit_Number_OperatorDto_Zero((Number_OperatorDto_Zero)x) },
                { typeof(Or_OperatorDto), x => Visit_Or_OperatorDto((Or_OperatorDto)x) },
                { typeof(PeakingEQFilter_OperatorDto), x => Visit_PeakingEQFilter_OperatorDto((PeakingEQFilter_OperatorDto)x) },
                { typeof(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
                { typeof(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
                { typeof(Power_OperatorDto), x => Visit_Power_OperatorDto((Power_OperatorDto)x) },
                { typeof(PulseTrigger_OperatorDto), x => Visit_PulseTrigger_OperatorDto((PulseTrigger_OperatorDto)x) },
                { typeof(Random_OperatorDto), x => Visit_Random_OperatorDto((Random_OperatorDto)x) },
                { typeof(Random_OperatorDto_Block), x => Visit_Random_OperatorDto_Block((Random_OperatorDto_Block)x) },
                { typeof(Random_OperatorDto_Stripe_LagBehind), x => Visit_Random_OperatorDto_Stripe_LagBehind((Random_OperatorDto_Stripe_LagBehind)x) },
                { typeof(Random_OperatorDto_Line_LagBehind_ConstRate), x => Visit_Random_OperatorDto_Line_LagBehind_ConstRate((Random_OperatorDto_Line_LagBehind_ConstRate)x) },
                { typeof(Random_OperatorDto_Line_LagBehind_VarRate), x => Visit_Random_OperatorDto_Line_LagBehind_VarRate((Random_OperatorDto_Line_LagBehind_VarRate)x) },
                { typeof(Random_OperatorDto_CubicEquidistant), x => Visit_Random_OperatorDto_CubicEquidistant((Random_OperatorDto_CubicEquidistant)x) },
                { typeof(Random_OperatorDto_CubicAbruptSlope), x => Visit_Random_OperatorDto_CubicAbruptSlope((Random_OperatorDto_CubicAbruptSlope)x) },
                { typeof(Random_OperatorDto_CubicSmoothSlope_LagBehind), x => Visit_Random_OperatorDto_CubicSmoothSlope_LagBehind((Random_OperatorDto_CubicSmoothSlope_LagBehind)x) },
                { typeof(Random_OperatorDto_Hermite_LagBehind), x => Visit_Random_OperatorDto_Hermite_LagBehind((Random_OperatorDto_Hermite_LagBehind)x) },
                { typeof(RangeOverDimension_OperatorDto), x => Visit_RangeOverDimension_OperatorDto((RangeOverDimension_OperatorDto)x) },
                { typeof(RangeOverDimension_OperatorDto_OnlyVars), x => Visit_RangeOverDimension_OperatorDto_OnlyVars((RangeOverDimension_OperatorDto_OnlyVars)x) },
                { typeof(RangeOverDimension_OperatorDto_OnlyConsts), x => Visit_RangeOverDimension_OperatorDto_OnlyConsts((RangeOverDimension_OperatorDto_OnlyConsts)x) },
                { typeof(RangeOverDimension_OperatorDto_WithConsts_AndStepOne), x => Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne((RangeOverDimension_OperatorDto_WithConsts_AndStepOne)x) },
                { typeof(RangeOverOutlets_Outlet_OperatorDto), x => Visit_RangeOverOutlets_Outlet_OperatorDto((RangeOverOutlets_Outlet_OperatorDto)x) },
                { typeof(RangeOverOutlets_Outlet_OperatorDto_ZeroStep), x => Visit_RangeOverOutlets_Outlet_OperatorDto_ZeroStep((RangeOverOutlets_Outlet_OperatorDto_ZeroStep)x) },
                { typeof(RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep), x => Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep((RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep)x) },
                { typeof(RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep), x => Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep((RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep)x) },
                { typeof(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep), x => Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep((RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep)x) },
                { typeof(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep), x => Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep((RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep)x) },
                { typeof(Remainder_OperatorDto), x => Visit_Remainder_OperatorDto((Remainder_OperatorDto)x) },
                { typeof(Reset_OperatorDto), x => Visit_Reset_OperatorDto((Reset_OperatorDto)x) },
                { typeof(Round_OperatorDto), x => Visit_Round_OperatorDto((Round_OperatorDto)x) },
                { typeof(Round_OperatorDto_AllConsts), x => Visit_Round_OperatorDto_AllConsts((Round_OperatorDto_AllConsts)x) },
                { typeof(Round_OperatorDto_ConstSignal), x => Visit_Round_OperatorDto_ConstSignal((Round_OperatorDto_ConstSignal)x) },
                { typeof(Round_OperatorDto_VarSignal_StepOne_OffsetZero), x => Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero((Round_OperatorDto_VarSignal_StepOne_OffsetZero)x) },
                { typeof(Round_OperatorDto_VarSignal_VarStep_VarOffset), x => Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset((Round_OperatorDto_VarSignal_VarStep_VarOffset)x) },
                { typeof(Round_OperatorDto_VarSignal_VarStep_ZeroOffset), x => Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset((Round_OperatorDto_VarSignal_VarStep_ZeroOffset)x) },
                { typeof(Round_OperatorDto_VarSignal_VarStep_ConstOffset), x => Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset((Round_OperatorDto_VarSignal_VarStep_ConstOffset)x) },
                { typeof(Round_OperatorDto_VarSignal_ConstStep_VarOffset), x => Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset((Round_OperatorDto_VarSignal_ConstStep_VarOffset)x) },
                { typeof(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset), x => Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset((Round_OperatorDto_VarSignal_ConstStep_ZeroOffset)x) },
                { typeof(Round_OperatorDto_VarSignal_ConstStep_ConstOffset), x => Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset((Round_OperatorDto_VarSignal_ConstStep_ConstOffset)x) },
                { typeof(Sample_OperatorDto), x => Visit_Sample_OperatorDto((Sample_OperatorDto)x) },
                { typeof(Sample_OperatorDto_VarFrequency_WithPhaseTracking), x => Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking((Sample_OperatorDto_VarFrequency_WithPhaseTracking)x) },
                { typeof(Sample_OperatorDto_ConstFrequency_WithOriginShifting), x => Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting((Sample_OperatorDto_ConstFrequency_WithOriginShifting)x) },
                { typeof(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking), x => Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking((Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking)x) },
                { typeof(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting), x => Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting((Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting)x) },
                { typeof(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking), x => Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking((Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking)x) },
                { typeof(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting), x => Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting((Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting)x) },
                { typeof(Sample_OperatorDto_VarFrequency_NoPhaseTracking), x => Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking((Sample_OperatorDto_VarFrequency_NoPhaseTracking)x) },
                { typeof(Sample_OperatorDto_ConstFrequency_NoOriginShifting), x => Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting((Sample_OperatorDto_ConstFrequency_NoOriginShifting)x) },
                { typeof(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking), x => Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking((Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking)x) },
                { typeof(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting), x => Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting((Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting)x) },
                { typeof(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking), x => Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking((Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking)x) },
                { typeof(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting), x => Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting((Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting)x) },
                { typeof(SetDimension_OperatorDto), x => Visit_SetDimension_OperatorDto((SetDimension_OperatorDto)x) },
                { typeof(SetDimension_OperatorDto_VarPassThrough_VarNumber), x => Visit_SetDimension_OperatorDto_VarPassThrough_VarNumber((SetDimension_OperatorDto_VarPassThrough_VarNumber)x) },
                { typeof(SetDimension_OperatorDto_VarPassThrough_ConstNumber), x => Visit_SetDimension_OperatorDto_VarPassThrough_ConstNumber((SetDimension_OperatorDto_VarPassThrough_ConstNumber)x) },
                { typeof(SetDimension_OperatorDto_ConstPassThrough_VarNumber), x => Visit_SetDimension_OperatorDto_ConstPassThrough_VarNumber((SetDimension_OperatorDto_ConstPassThrough_VarNumber)x) },
                { typeof(SetDimension_OperatorDto_ConstPassThrough_ConstNumber), x => Visit_SetDimension_OperatorDto_ConstPassThrough_ConstNumber((SetDimension_OperatorDto_ConstPassThrough_ConstNumber)x) },
                { typeof(Sine_OperatorDto), x => Visit_Sine_OperatorDto((Sine_OperatorDto)x) },
                { typeof(Sine_OperatorDto_ConstFrequency_NoOriginShifting), x => Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting((Sine_OperatorDto_ConstFrequency_NoOriginShifting)x) },
                { typeof(Sine_OperatorDto_ConstFrequency_WithOriginShifting), x => Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting((Sine_OperatorDto_ConstFrequency_WithOriginShifting)x) },
                { typeof(Sine_OperatorDto_VarFrequency_NoPhaseTracking), x => Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking((Sine_OperatorDto_VarFrequency_NoPhaseTracking)x) },
                { typeof(Sine_OperatorDto_VarFrequency_WithPhaseTracking), x => Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking((Sine_OperatorDto_VarFrequency_WithPhaseTracking)x) },
                { typeof(SortOverDimension_OperatorDto), x => Visit_SortOverDimension_OperatorDto((SortOverDimension_OperatorDto)x) },
                { typeof(SortOverDimension_OperatorDto_ConstSignal), x => Visit_SortOverDimension_OperatorDto_ConstSignal((SortOverDimension_OperatorDto_ConstSignal)x) },
                { typeof(SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous), x => Visit_SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous((SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous)x) },
                { typeof(SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset), x => Visit_SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset((SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset)x) },
                { typeof(SortOverInlets_Outlet_OperatorDto), x => Visit_SortOverInlets_Outlet_OperatorDto((SortOverInlets_Outlet_OperatorDto)x) },
                { typeof(Spectrum_OperatorDto), x => Visit_Spectrum_OperatorDto((Spectrum_OperatorDto)x) },
                { typeof(Squash_OperatorDto), x => Visit_Squash_OperatorDto((Squash_OperatorDto)x) },
                { typeof(Squash_OperatorDto_ConstSignal), x => Visit_Squash_OperatorDto_ConstSignal((Squash_OperatorDto_ConstSignal)x) },
                { typeof(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin), x => Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin((Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin)x) },
                { typeof(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin), x => Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin((Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin)x) },
                { typeof(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin), x => Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin((Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin)x) },
                { typeof(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin), x => Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin((Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin)x) },
                { typeof(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin), x => Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin((Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin)x) },
                { typeof(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin), x => Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin((Squash_OperatorDto_VarSignal_VarFactor_VarOrigin)x) },
                { typeof(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting), x => Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting((Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting)x) },
                { typeof(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking), x => Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking((Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking)x) },
                { typeof(Stretch_OperatorDto), x => Visit_Stretch_OperatorDto((Stretch_OperatorDto)x) },
                { typeof(Stretch_OperatorDto_ConstSignal), x => Visit_Stretch_OperatorDto_ConstSignal((Stretch_OperatorDto_ConstSignal)x) },
                { typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin), x => Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin((Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin)x) },
                { typeof(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin), x => Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin((Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin)x) },
                { typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin), x => Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin((Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin)x) },
                { typeof(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin), x => Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin((Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin)x) },
                { typeof(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin), x => Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin((Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin)x) },
                { typeof(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin), x => Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin((Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin)x) },
                { typeof(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting), x => Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting((Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting)x) },
                { typeof(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking), x => Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking((Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking)x) },
                { typeof(Subtract_OperatorDto), x => Visit_Subtract_OperatorDto((Subtract_OperatorDto)x) },
                { typeof(SumFollower_OperatorDto), x => Visit_SumFollower_OperatorDto((SumFollower_OperatorDto)x) },
                { typeof(SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar), x => Visit_SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar((SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar)x) },
                { typeof(SumFollower_OperatorDto_ConstSignal_VarSampleCount), x => Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount((SumFollower_OperatorDto_ConstSignal_VarSampleCount)x) },
                { typeof(SumFollower_OperatorDto_ConstSignal_ConstSampleCount), x => Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount((SumFollower_OperatorDto_ConstSignal_ConstSampleCount)x) },
                { typeof(SumOverDimension_OperatorDto), x => Visit_SumOverDimension_OperatorDto((SumOverDimension_OperatorDto)x) },
                { typeof(SumOverDimension_OperatorDto_AllConsts), x => Visit_SumOverDimension_OperatorDto_AllConsts((SumOverDimension_OperatorDto_AllConsts)x) },
                { typeof(SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous), x => Visit_SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous((SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous)x) },
                { typeof(SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset), x => Visit_SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset((SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset)x) },
                { typeof(ToggleTrigger_OperatorDto), x => Visit_ToggleTrigger_OperatorDto((ToggleTrigger_OperatorDto)x) },
                { typeof(Triangle_OperatorDto), x => Visit_Triangle_OperatorDto((Triangle_OperatorDto)x) },
                { typeof(Triangle_OperatorDto_ConstFrequency_NoOriginShifting), x => Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting((Triangle_OperatorDto_ConstFrequency_NoOriginShifting)x) },
                { typeof(Triangle_OperatorDto_ConstFrequency_WithOriginShifting), x => Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting((Triangle_OperatorDto_ConstFrequency_WithOriginShifting)x) },
                { typeof(Triangle_OperatorDto_VarFrequency_NoPhaseTracking), x => Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking((Triangle_OperatorDto_VarFrequency_NoPhaseTracking)x) },
                { typeof(Triangle_OperatorDto_VarFrequency_WithPhaseTracking), x => Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking((Triangle_OperatorDto_VarFrequency_WithPhaseTracking)x) },
                { typeof(VariableInput_OperatorDto), x => Visit_VariableInput_OperatorDto((VariableInput_OperatorDto)x) },
            };
        }

        [DebuggerHidden] protected virtual IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageFollower_OperatorDto_ConstSignal(AverageFollower_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageOverDimension_OperatorDto_ConstSignal(AverageOverDimension_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageOverInlets_OperatorDto_AllConsts(AverageOverInlets_OperatorDto_AllConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_AverageOverInlets_OperatorDto_Vars(AverageOverInlets_OperatorDto_Vars dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_BooleanToDouble_OperatorDto(BooleanToDouble_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto(Cache_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Block(Cache_OperatorDto_SingleChannel_Block dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Cubic(Cache_OperatorDto_SingleChannel_Cubic dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Hermite(Cache_OperatorDto_SingleChannel_Hermite dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Line(Cache_OperatorDto_SingleChannel_Line dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Stripe(Cache_OperatorDto_SingleChannel_Stripe dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Block(Cache_OperatorDto_MultiChannel_Block dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Cubic(Cache_OperatorDto_MultiChannel_Cubic dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Hermite(Cache_OperatorDto_MultiChannel_Hermite dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Line(Cache_OperatorDto_MultiChannel_Line dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Stripe(Cache_OperatorDto_MultiChannel_Stripe dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInlets_OperatorDto_ConstInput_ConstItems(ClosestOverInlets_OperatorDto_ConstInput_ConstItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems(ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_DoubleToBoolean_OperatorDto(DoubleToBoolean_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Hold_OperatorDto_VarSignal(Hold_OperatorDto_VarSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Hold_OperatorDto_ConstSignal(Hold_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_CubicEquidistant(InletsToDimension_OperatorDto_CubicEquidistant dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_CubicAbruptSlope(InletsToDimension_OperatorDto_CubicAbruptSlope dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind(InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Hermite_LagBehind(InletsToDimension_OperatorDto_Hermite_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto(Loop_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto_ConstSignal(Loop_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Loop_OperatorDto_SignalVarOrConst_OtherInputsVar(Loop_OperatorDto_SignalVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxFollower_OperatorDto_ConstSignal(MaxFollower_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(MaxFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_Consts(MaxOverInlets_OperatorDto_Vars_Consts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_NoConsts(MaxOverInlets_OperatorDto_NoVars_NoConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto_NoVars_Consts(MaxOverInlets_OperatorDto_NoVars_Consts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(MinFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinFollower_OperatorDto_ConstSignal(MinFollower_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto(Random_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_ConstRate(Random_OperatorDto_Line_LagBehind_ConstRate dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_VarRate(Random_OperatorDto_Line_LagBehind_VarRate dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_CubicEquidistant(Random_OperatorDto_CubicEquidistant dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_CubicAbruptSlope(Random_OperatorDto_CubicAbruptSlope dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_CubicSmoothSlope_LagBehind(Random_OperatorDto_CubicSmoothSlope_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Random_OperatorDto_Hermite_LagBehind(Random_OperatorDto_Hermite_LagBehind dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ZeroStep(RangeOverOutlets_Outlet_OperatorDto_ZeroStep dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Remainder_OperatorDto(Remainder_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto(Sample_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_VarNumber(SetDimension_OperatorDto_VarPassThrough_VarNumber dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_ConstNumber(SetDimension_OperatorDto_VarPassThrough_ConstNumber dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_VarNumber(SetDimension_OperatorDto_ConstPassThrough_VarNumber dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SetDimension_OperatorDto_ConstPassThrough_ConstNumber(SetDimension_OperatorDto_ConstPassThrough_ConstNumber dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_ConstSignal(Squash_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto(Stretch_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_ConstSignal(Stretch_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumFollower_OperatorDto(SumFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar(SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Triangle_OperatorDto(Triangle_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto) => Visit_OperatorDto_Base(dto);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
	internal class OperatorDtoVisitorBase
	{
		private readonly Dictionary<IOperatorDto, IOperatorDto> _operatorDto_To_OperatorDto_Dictionary = new Dictionary<IOperatorDto, IOperatorDto>();

		private readonly Dictionary<Type, Func<IOperatorDto, IOperatorDto>> _delegateDictionary;

		/// <summary>
		/// For performance, first checks in a dictionary, whether the operator DTO was already processed
		/// and returns the already processed dto. Otherwise the passed action is executed.
		/// </summary>
		/*[DebuggerHidden]*/
		protected IOperatorDto WithAlreadyProcessedCheck(IOperatorDto dto, Func<IOperatorDto> action)
		{
			if (_operatorDto_To_OperatorDto_Dictionary.TryGetValue(dto, out IOperatorDto dto2))
			{
				//Debug.WriteLine($"{GetType().Name}: Skipping already processed DTO #{_operatorDto_To_OperatorDto_Dictionary.Count - 1} {dto}.");
				return dto2;
			}

			dto2 = action();

			_operatorDto_To_OperatorDto_Dictionary[dto] = dto2;

			return dto2;
		}

		/*[DebuggerHidden]*/
		protected virtual IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
		{
			Type type = dto.GetType();

			if (!_delegateDictionary.TryGetValue(type, out Func<IOperatorDto, IOperatorDto> func))
			{
				throw new Exception($"No Visit method delegate found in the dictionary for {type.Name}.");
			}

			IOperatorDto originalDto = dto;
			IReadOnlyList<InputDto> originalInputDtos = originalDto.Inputs;

			IOperatorDto newDto = func(originalDto);
			IReadOnlyList<InputDto> newInputDtos = newDto.Inputs;

			// Revisit as long as different instances keep coming.

			while (newDto != originalDto || !AreEqual(newInputDtos, originalInputDtos))
			{
				originalDto = newDto;
				originalInputDtos = originalDto.Inputs;

				newDto = Visit_OperatorDto_Polymorphic(newDto);
				newInputDtos = newDto.Inputs;
			}

			return newDto;
		}
	
		private bool AreEqual(IReadOnlyList<InputDto> list1, IReadOnlyList<InputDto> list2)
		{
			if (list1.Count != list2.Count) return false;

			int count = list1.Count;
			for (int i = 0; i < count; i++)
			{
				InputDto item1 = list1[i];
				InputDto item2 = list2[i];

				if (!AreEqual(item1, item2))
				{
					return false;
				}
			}

			return true;
		}

		private bool AreEqual(InputDto item1, InputDto item2)
		{
			if (item1 == item2)
			{
				return true;
			}

			if (item1.IsConst && item2.IsConst)
			{
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				return item1.Const == item2.Const;
			}

			if (item1.IsVar && item2.IsVar)
			{
				return item1.Var == item2.Var;
			}

			return false;
		}

		/*[DebuggerHidden]*/
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// ReSharper disable once VirtualMemberNeverOverridden.Global
		protected virtual IOperatorDto Visit_OperatorDto_Base(IOperatorDto dto)
		{
			dto.Inputs = dto.Inputs.Reverse().Select(VisitInputDto).Reverse().ToArray();

			return dto;
		}

		/*[DebuggerHidden]*/
		// ReSharper disable once VirtualMemberNeverOverridden.Global
		protected virtual InputDto VisitInputDto(InputDto inputDto)
		{
			if (inputDto.IsVar)
			{
				return VisitVarInputDto(inputDto);
			}

			if (inputDto.IsConst)
			{
				return VisitConstInputDto(inputDto);
			}

			throw new Exception(
				$"{nameof(inputDto)}.{nameof(inputDto.IsVar)} and " +
				$"{nameof(inputDto)}.{nameof(inputDto.IsConst)} cannot both be false.");
		}

		/*[DebuggerHidden]*/
		protected virtual InputDto VisitConstInputDto(InputDto inputDto) => inputDto;

		/*[DebuggerHidden]*/
		// ReSharper disable once VirtualMemberNeverOverridden.Global
		protected virtual InputDto VisitVarInputDto(InputDto inputDto)
		{
			IOperatorDto var2 = Visit_OperatorDto_Polymorphic(inputDto.Var);
			// ReSharper disable once InvertIf
			if (var2 != inputDto.Var)
			{
				InputDto inputDto2 = InputDtoFactory.TryCreateInputDto(var2);
				return inputDto2;
			}
			// ReSharper disable once RedundantIfElseBlock
			else
			{
				return inputDto;
			}
		}

		protected OperatorDtoVisitorBase() => _delegateDictionary = new Dictionary<Type, Func<IOperatorDto, IOperatorDto>>
		{
		    { typeof(Add_OperatorDto), x => Visit_Add_OperatorDto((Add_OperatorDto)x) },
		    { typeof(AllPassFilter_OperatorDto), x => Visit_AllPassFilter_OperatorDto((AllPassFilter_OperatorDto)x) },
		    { typeof(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
		    { typeof(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
		    { typeof(And_OperatorDto), x => Visit_And_OperatorDto((And_OperatorDto)x) },
		    { typeof(AverageFollower_OperatorDto), x => Visit_AverageFollower_OperatorDto((AverageFollower_OperatorDto)x) },
		    { typeof(AverageOverDimension_OperatorDto), x => Visit_AverageOverDimension_OperatorDto((AverageOverDimension_OperatorDto)x) },
		    { typeof(AverageOverDimension_OperatorDto_CollectionRecalculationContinuous), x => Visit_AverageOverDimension_OperatorDto_CollectionRecalculationContinuous((AverageOverDimension_OperatorDto_CollectionRecalculationContinuous)x) },
		    { typeof(AverageOverDimension_OperatorDto_CollectionRecalculationUponReset), x => Visit_AverageOverDimension_OperatorDto_CollectionRecalculationUponReset((AverageOverDimension_OperatorDto_CollectionRecalculationUponReset)x) },
		    { typeof(AverageOverInlets_OperatorDto), x => Visit_AverageOverInlets_OperatorDto((AverageOverInlets_OperatorDto)x) },
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
		    { typeof(ClosestOverInletsExp_OperatorDto), x => Visit_ClosestOverInletsExp_OperatorDto((ClosestOverInletsExp_OperatorDto)x) },
		    { typeof(Curve_OperatorDto), x => Visit_Curve_OperatorDto((Curve_OperatorDto)x) },
		    { typeof(Curve_OperatorDto_NoCurve), x => Visit_Curve_OperatorDto_NoCurve((Curve_OperatorDto_NoCurve)x) },
		    { typeof(Curve_OperatorDto_NoOriginShifting), x => Visit_Curve_OperatorDto_NoOriginShifting((Curve_OperatorDto_NoOriginShifting)x) },
		    { typeof(Curve_OperatorDto_WithOriginShifting), x => Visit_Curve_OperatorDto_WithOriginShifting((Curve_OperatorDto_WithOriginShifting)x) },
		    { typeof(DimensionToOutlets_Outlet_OperatorDto), x => Visit_DimensionToOutlets_Outlet_OperatorDto((DimensionToOutlets_Outlet_OperatorDto)x) },
		    { typeof(Divide_OperatorDto), x => Visit_Divide_OperatorDto((Divide_OperatorDto)x) },
		    { typeof(DoubleToBoolean_OperatorDto), x => Visit_DoubleToBoolean_OperatorDto((DoubleToBoolean_OperatorDto)x) },
		    { typeof(Equal_OperatorDto), x => Visit_Equal_OperatorDto((Equal_OperatorDto)x) },
		    { typeof(GetPosition_OperatorDto), x => Visit_GetPosition_OperatorDto((GetPosition_OperatorDto)x) },
		    { typeof(GreaterThan_OperatorDto), x => Visit_GreaterThan_OperatorDto((GreaterThan_OperatorDto)x) },
		    { typeof(GreaterThanOrEqual_OperatorDto), x => Visit_GreaterThanOrEqual_OperatorDto((GreaterThanOrEqual_OperatorDto)x) },
		    { typeof(HighPassFilter_OperatorDto), x => Visit_HighPassFilter_OperatorDto((HighPassFilter_OperatorDto)x) },
		    { typeof(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
		    { typeof(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
		    { typeof(HighShelfFilter_OperatorDto), x => Visit_HighShelfFilter_OperatorDto((HighShelfFilter_OperatorDto)x) },
		    { typeof(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
		    { typeof(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
		    { typeof(Hold_OperatorDto), x => Visit_Hold_OperatorDto((Hold_OperatorDto)x) },
		    { typeof(If_OperatorDto), x => Visit_If_OperatorDto((If_OperatorDto)x) },
		    { typeof(InletsToDimension_OperatorDto), x => Visit_InletsToDimension_OperatorDto((InletsToDimension_OperatorDto)x) },
		    { typeof(InletsToDimension_OperatorDto_Block), x => Visit_InletsToDimension_OperatorDto_Block((InletsToDimension_OperatorDto_Block)x) },
		    { typeof(InletsToDimension_OperatorDto_Stripe_LagBehind), x => Visit_InletsToDimension_OperatorDto_Stripe_LagBehind((InletsToDimension_OperatorDto_Stripe_LagBehind)x) },
		    { typeof(InletsToDimension_OperatorDto_Line), x => Visit_InletsToDimension_OperatorDto_Line((InletsToDimension_OperatorDto_Line)x) },
		    { typeof(InletsToDimension_OperatorDto_Cubic_LagBehind), x => Visit_InletsToDimension_OperatorDto_Cubic_LagBehind((InletsToDimension_OperatorDto_Cubic_LagBehind)x) },
		    { typeof(InletsToDimension_OperatorDto_Hermite_LagBehind), x => Visit_InletsToDimension_OperatorDto_Hermite_LagBehind((InletsToDimension_OperatorDto_Hermite_LagBehind)x) },
		    { typeof(Interpolate_OperatorDto), x => Visit_Interpolate_OperatorDto((Interpolate_OperatorDto)x) },
		    { typeof(Interpolate_OperatorDto_ConstSignal), x => Visit_Interpolate_OperatorDto_ConstSignal((Interpolate_OperatorDto_ConstSignal)x) },
		    { typeof(Interpolate_OperatorDto_Block), x => Visit_Interpolate_OperatorDto_Block((Interpolate_OperatorDto_Block)x) },
		    { typeof(Interpolate_OperatorDto_Cubic_LagBehind), x => Visit_Interpolate_OperatorDto_Cubic_LagBehind((Interpolate_OperatorDto_Cubic_LagBehind)x) },
		    { typeof(Interpolate_OperatorDto_Cubic_LookAhead), x => Visit_Interpolate_OperatorDto_Cubic_LookAhead((Interpolate_OperatorDto_Cubic_LookAhead)x) },
		    { typeof(Interpolate_OperatorDto_Hermite_LagBehind), x => Visit_Interpolate_OperatorDto_Hermite_LagBehind((Interpolate_OperatorDto_Hermite_LagBehind)x) },
		    { typeof(Interpolate_OperatorDto_Hermite_LookAhead), x => Visit_Interpolate_OperatorDto_Hermite_LookAhead((Interpolate_OperatorDto_Hermite_LookAhead)x) },
		    { typeof(Interpolate_OperatorDto_Line_LagBehind), x => Visit_Interpolate_OperatorDto_Line_LagBehind((Interpolate_OperatorDto_Line_LagBehind)x) },
		    { typeof(Interpolate_OperatorDto_Line_LookAhead), x => Visit_Interpolate_OperatorDto_Line_LookAhead((Interpolate_OperatorDto_Line_LookAhead)x) },
		    { typeof(Interpolate_OperatorDto_Stripe_LagBehind), x => Visit_Interpolate_OperatorDto_Stripe_LagBehind((Interpolate_OperatorDto_Stripe_LagBehind)x) },
		    { typeof(Interpolate_OperatorDto_Stripe_LookAhead), x => Visit_Interpolate_OperatorDto_Stripe_LookAhead((Interpolate_OperatorDto_Stripe_LookAhead)x) },
		    { typeof(LessThan_OperatorDto), x => Visit_LessThan_OperatorDto((LessThan_OperatorDto)x) },
		    { typeof(LessThanOrEqual_OperatorDto), x => Visit_LessThanOrEqual_OperatorDto((LessThanOrEqual_OperatorDto)x) },
		    { typeof(Loop_OperatorDto), x => Visit_Loop_OperatorDto((Loop_OperatorDto)x) },
		    { typeof(LowPassFilter_OperatorDto), x => Visit_LowPassFilter_OperatorDto((LowPassFilter_OperatorDto)x) },
		    { typeof(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
		    { typeof(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
		    { typeof(LowShelfFilter_OperatorDto), x => Visit_LowShelfFilter_OperatorDto((LowShelfFilter_OperatorDto)x) },
		    { typeof(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
		    { typeof(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
		    { typeof(MaxFollower_OperatorDto), x => Visit_MaxFollower_OperatorDto((MaxFollower_OperatorDto)x) },
		    { typeof(MaxOverDimension_OperatorDto), x => Visit_MaxOverDimension_OperatorDto((MaxOverDimension_OperatorDto)x) },
		    { typeof(MaxOverDimension_OperatorDto_ConstSignal), x => Visit_MaxOverDimension_OperatorDto_ConstSignal((MaxOverDimension_OperatorDto_ConstSignal)x) },
		    { typeof(MaxOverDimension_OperatorDto_CollectionRecalculationContinuous), x => Visit_MaxOverDimension_OperatorDto_CollectionRecalculationContinuous((MaxOverDimension_OperatorDto_CollectionRecalculationContinuous)x) },
		    { typeof(MaxOverDimension_OperatorDto_CollectionRecalculationUponReset), x => Visit_MaxOverDimension_OperatorDto_CollectionRecalculationUponReset((MaxOverDimension_OperatorDto_CollectionRecalculationUponReset)x) },
		    { typeof(MaxOverInlets_OperatorDto), x => Visit_MaxOverInlets_OperatorDto((MaxOverInlets_OperatorDto)x) },
		    { typeof(MinFollower_OperatorDto), x => Visit_MinFollower_OperatorDto((MinFollower_OperatorDto)x) },
		    { typeof(MinOverDimension_OperatorDto), x => Visit_MinOverDimension_OperatorDto((MinOverDimension_OperatorDto)x) },
		    { typeof(MinOverDimension_OperatorDto_ConstSignal), x => Visit_MinOverDimension_OperatorDto_ConstSignal((MinOverDimension_OperatorDto_ConstSignal)x) },
		    { typeof(MinOverDimension_OperatorDto_CollectionRecalculationContinuous), x => Visit_MinOverDimension_OperatorDto_CollectionRecalculationContinuous((MinOverDimension_OperatorDto_CollectionRecalculationContinuous)x) },
		    { typeof(MinOverDimension_OperatorDto_CollectionRecalculationUponReset), x => Visit_MinOverDimension_OperatorDto_CollectionRecalculationUponReset((MinOverDimension_OperatorDto_CollectionRecalculationUponReset)x) },
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
		    { typeof(Or_OperatorDto), x => Visit_Or_OperatorDto((Or_OperatorDto)x) },
		    { typeof(PeakingEQFilter_OperatorDto), x => Visit_PeakingEQFilter_OperatorDto((PeakingEQFilter_OperatorDto)x) },
		    { typeof(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar), x => Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar((PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar)x) },
		    { typeof(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst), x => Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst((PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst)x) },
		    { typeof(Power_OperatorDto), x => Visit_Power_OperatorDto((Power_OperatorDto)x) },
		    { typeof(PulseTrigger_OperatorDto), x => Visit_PulseTrigger_OperatorDto((PulseTrigger_OperatorDto)x) },
		    { typeof(Random_OperatorDto), x => Visit_Random_OperatorDto((Random_OperatorDto)x) },
		    { typeof(Random_OperatorDto_Block), x => Visit_Random_OperatorDto_Block((Random_OperatorDto_Block)x) },
		    { typeof(Random_OperatorDto_Stripe_LagBehind), x => Visit_Random_OperatorDto_Stripe_LagBehind((Random_OperatorDto_Stripe_LagBehind)x) },
		    { typeof(RangeOverDimension_OperatorDto), x => Visit_RangeOverDimension_OperatorDto((RangeOverDimension_OperatorDto)x) },
		    { typeof(RangeOverDimension_OperatorDto_OnlyVars), x => Visit_RangeOverDimension_OperatorDto_OnlyVars((RangeOverDimension_OperatorDto_OnlyVars)x) },
		    { typeof(RangeOverDimension_OperatorDto_OnlyConsts), x => Visit_RangeOverDimension_OperatorDto_OnlyConsts((RangeOverDimension_OperatorDto_OnlyConsts)x) },
		    { typeof(RangeOverDimension_OperatorDto_WithConsts_AndStepOne), x => Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne((RangeOverDimension_OperatorDto_WithConsts_AndStepOne)x) },
		    { typeof(RangeOverOutlets_Outlet_OperatorDto), x => Visit_RangeOverOutlets_Outlet_OperatorDto((RangeOverOutlets_Outlet_OperatorDto)x) },
		    { typeof(Remainder_OperatorDto), x => Visit_Remainder_OperatorDto((Remainder_OperatorDto)x) },
		    { typeof(Reset_OperatorDto), x => Visit_Reset_OperatorDto((Reset_OperatorDto)x) },
		    { typeof(Round_OperatorDto), x => Visit_Round_OperatorDto((Round_OperatorDto)x) },
		    { typeof(Round_OperatorDto_AllConsts), x => Visit_Round_OperatorDto_AllConsts((Round_OperatorDto_AllConsts)x) },
		    { typeof(Round_OperatorDto_StepOne_ZeroOffset), x => Visit_Round_OperatorDto_StepOne_ZeroOffset((Round_OperatorDto_StepOne_ZeroOffset)x) },
		    { typeof(Round_OperatorDto_WithOffset), x => Visit_Round_OperatorDto_WithOffset((Round_OperatorDto_WithOffset)x) },
		    { typeof(Round_OperatorDto_ZeroOffset), x => Visit_Round_OperatorDto_ZeroOffset((Round_OperatorDto_ZeroOffset)x) },
		    { typeof(SampleWithRate1_OperatorDto), x => Visit_SampleWithRate1_OperatorDto((SampleWithRate1_OperatorDto)x) },
		    { typeof(SampleWithRate1_OperatorDto_MonoToStereo), x => Visit_SampleWithRate1_OperatorDto_MonoToStereo((SampleWithRate1_OperatorDto_MonoToStereo)x) },
		    { typeof(SampleWithRate1_OperatorDto_NoChannelConversion), x => Visit_SampleWithRate1_OperatorDto_NoChannelConversion((SampleWithRate1_OperatorDto_NoChannelConversion)x) },
		    { typeof(SampleWithRate1_OperatorDto_NoSample), x => Visit_SampleWithRate1_OperatorDto_NoSample((SampleWithRate1_OperatorDto_NoSample)x) },
		    { typeof(SampleWithRate1_OperatorDto_StereoToMono), x => Visit_SampleWithRate1_OperatorDto_StereoToMono((SampleWithRate1_OperatorDto_StereoToMono)x) },
		    { typeof(SetPosition_OperatorDto), x => Visit_SetPosition_OperatorDto((SetPosition_OperatorDto)x) },
		    { typeof(SineWithRate1_OperatorDto), x => Visit_SineWithRate1_OperatorDto((SineWithRate1_OperatorDto)x) },
		    { typeof(SortOverDimension_OperatorDto), x => Visit_SortOverDimension_OperatorDto((SortOverDimension_OperatorDto)x) },
		    { typeof(SortOverDimension_OperatorDto_ConstSignal), x => Visit_SortOverDimension_OperatorDto_ConstSignal((SortOverDimension_OperatorDto_ConstSignal)x) },
		    { typeof(SortOverDimension_OperatorDto_CollectionRecalculationContinuous), x => Visit_SortOverDimension_OperatorDto_CollectionRecalculationContinuous((SortOverDimension_OperatorDto_CollectionRecalculationContinuous)x) },
		    { typeof(SortOverDimension_OperatorDto_CollectionRecalculationUponReset), x => Visit_SortOverDimension_OperatorDto_CollectionRecalculationUponReset((SortOverDimension_OperatorDto_CollectionRecalculationUponReset)x) },
		    { typeof(SortOverInlets_Outlet_OperatorDto), x => Visit_SortOverInlets_Outlet_OperatorDto((SortOverInlets_Outlet_OperatorDto)x) },
		    { typeof(Spectrum_OperatorDto), x => Visit_Spectrum_OperatorDto((Spectrum_OperatorDto)x) },
		    { typeof(Squash_OperatorDto), x => Visit_Squash_OperatorDto((Squash_OperatorDto)x) },
		    { typeof(Squash_OperatorDto_FactorZero), x => Visit_Squash_OperatorDto_FactorZero((Squash_OperatorDto_FactorZero)x) },
		    { typeof(Squash_OperatorDto_WithOrigin), x => Visit_Squash_OperatorDto_WithOrigin((Squash_OperatorDto_WithOrigin)x) },
		    { typeof(Squash_OperatorDto_ZeroOrigin), x => Visit_Squash_OperatorDto_ZeroOrigin((Squash_OperatorDto_ZeroOrigin)x) },
		    { typeof(Squash_OperatorDto_ConstFactor_WithOriginShifting), x => Visit_Squash_OperatorDto_ConstFactor_WithOriginShifting((Squash_OperatorDto_ConstFactor_WithOriginShifting)x) },
		    { typeof(Squash_OperatorDto_VarFactor_WithPhaseTracking), x => Visit_Squash_OperatorDto_VarFactor_WithPhaseTracking((Squash_OperatorDto_VarFactor_WithPhaseTracking)x) },
		    { typeof(Subtract_OperatorDto), x => Visit_Subtract_OperatorDto((Subtract_OperatorDto)x) },
		    { typeof(SumFollower_OperatorDto), x => Visit_SumFollower_OperatorDto((SumFollower_OperatorDto)x) },
		    { typeof(SumFollower_OperatorDto_AllVars), x => Visit_SumFollower_OperatorDto_AllVars((SumFollower_OperatorDto_AllVars)x) },
		    { typeof(SumFollower_OperatorDto_ConstSignal_VarSampleCount), x => Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount((SumFollower_OperatorDto_ConstSignal_VarSampleCount)x) },
		    { typeof(SumFollower_OperatorDto_ConstSignal_ConstSampleCount), x => Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount((SumFollower_OperatorDto_ConstSignal_ConstSampleCount)x) },
		    { typeof(SumOverDimension_OperatorDto), x => Visit_SumOverDimension_OperatorDto((SumOverDimension_OperatorDto)x) },
		    { typeof(SumOverDimension_OperatorDto_AllConsts), x => Visit_SumOverDimension_OperatorDto_AllConsts((SumOverDimension_OperatorDto_AllConsts)x) },
		    { typeof(SumOverDimension_OperatorDto_CollectionRecalculationContinuous), x => Visit_SumOverDimension_OperatorDto_CollectionRecalculationContinuous((SumOverDimension_OperatorDto_CollectionRecalculationContinuous)x) },
		    { typeof(SumOverDimension_OperatorDto_CollectionRecalculationUponReset), x => Visit_SumOverDimension_OperatorDto_CollectionRecalculationUponReset((SumOverDimension_OperatorDto_CollectionRecalculationUponReset)x) },
		    { typeof(ToggleTrigger_OperatorDto), x => Visit_ToggleTrigger_OperatorDto((ToggleTrigger_OperatorDto)x) },
		    { typeof(TriangleWithRate1_OperatorDto), x => Visit_TriangleWithRate1_OperatorDto((TriangleWithRate1_OperatorDto)x) },
		    { typeof(VariableInput_OperatorDto), x => Visit_VariableInput_OperatorDto((VariableInput_OperatorDto)x) },
		    { typeof(Sin_OperatorDto), x => Visit_Sin_OperatorDto((Sin_OperatorDto)x) },
            { typeof(Cos_OperatorDto), x => Visit_Cos_OperatorDto((Cos_OperatorDto)x) },
            { typeof(Tan_OperatorDto), x => Visit_Tan_OperatorDto((Tan_OperatorDto)x) },
            { typeof(SinH_OperatorDto), x => Visit_SinH_OperatorDto((SinH_OperatorDto)x) },
            { typeof(CosH_OperatorDto), x => Visit_CosH_OperatorDto((CosH_OperatorDto)x) },
            { typeof(TanH_OperatorDto), x => Visit_TanH_OperatorDto((TanH_OperatorDto)x) },
            { typeof(ArcSin_OperatorDto), x => Visit_ArcSin_OperatorDto((ArcSin_OperatorDto)x) },
            { typeof(ArcCos_OperatorDto), x => Visit_ArcCos_OperatorDto((ArcCos_OperatorDto)x) },
            { typeof(ArcTan_OperatorDto), x => Visit_ArcTan_OperatorDto((ArcTan_OperatorDto)x) },
            { typeof(LogN_OperatorDto), x => Visit_LogN_OperatorDto((LogN_OperatorDto)x) },
            { typeof(Ln_OperatorDto), x => Visit_Ln_OperatorDto((Ln_OperatorDto)x) },
            { typeof(SquareRoot_OperatorDto), x => Visit_SquareRoot_OperatorDto((SquareRoot_OperatorDto)x) },
            { typeof(Sign_OperatorDto), x => Visit_Sign_OperatorDto((Sign_OperatorDto)x) },
            { typeof(Factorial_OperatorDto), x => Visit_Factorial_OperatorDto((Factorial_OperatorDto)x) },
            { typeof(Xor_OperatorDto), x => Visit_Xor_OperatorDto((Xor_OperatorDto)x) },
            { typeof(Ceiling_OperatorDto), x => Visit_Ceiling_OperatorDto((Ceiling_OperatorDto)x) },
            { typeof(Floor_OperatorDto), x => Visit_Floor_OperatorDto((Floor_OperatorDto)x) },
            { typeof(Truncate_OperatorDto), x => Visit_Truncate_OperatorDto((Truncate_OperatorDto)x) }
        };

	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_BooleanToDouble_OperatorDto(BooleanToDouble_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto(Cache_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Block(Cache_OperatorDto_SingleChannel_Block dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Cubic(Cache_OperatorDto_SingleChannel_Cubic dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Hermite(Cache_OperatorDto_SingleChannel_Hermite dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Line(Cache_OperatorDto_SingleChannel_Line dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Stripe(Cache_OperatorDto_SingleChannel_Stripe dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Block(Cache_OperatorDto_MultiChannel_Block dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Cubic(Cache_OperatorDto_MultiChannel_Cubic dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Hermite(Cache_OperatorDto_MultiChannel_Hermite dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Line(Cache_OperatorDto_MultiChannel_Line dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Stripe(Cache_OperatorDto_MultiChannel_Stripe dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Curve_OperatorDto_NoCurve(Curve_OperatorDto_NoCurve dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Curve_OperatorDto_NoOriginShifting(Curve_OperatorDto_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Curve_OperatorDto_WithOriginShifting(Curve_OperatorDto_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_DoubleToBoolean_OperatorDto(DoubleToBoolean_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_GetPosition_OperatorDto(GetPosition_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Cubic_LagBehind(InletsToDimension_OperatorDto_Cubic_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_InletsToDimension_OperatorDto_Hermite_LagBehind(InletsToDimension_OperatorDto_Hermite_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Cubic_LagBehind(Interpolate_OperatorDto_Cubic_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Cubic_LookAhead(Interpolate_OperatorDto_Cubic_LookAhead dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LookAhead(Interpolate_OperatorDto_Hermite_LookAhead dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind(Interpolate_OperatorDto_Line_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Line_LookAhead(Interpolate_OperatorDto_Line_LookAhead dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LookAhead(Interpolate_OperatorDto_Stripe_LookAhead dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Loop_OperatorDto(Loop_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Random_OperatorDto(Random_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Remainder_OperatorDto(Remainder_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Round_OperatorDto_StepOne_ZeroOffset(Round_OperatorDto_StepOne_ZeroOffset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Round_OperatorDto_WithOffset(Round_OperatorDto_WithOffset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Round_OperatorDto_ZeroOffset(Round_OperatorDto_ZeroOffset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SampleWithRate1_OperatorDto(SampleWithRate1_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SampleWithRate1_OperatorDto_MonoToStereo(SampleWithRate1_OperatorDto_MonoToStereo dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SampleWithRate1_OperatorDto_NoChannelConversion(SampleWithRate1_OperatorDto_NoChannelConversion dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SampleWithRate1_OperatorDto_NoSample(SampleWithRate1_OperatorDto_NoSample dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SampleWithRate1_OperatorDto_StereoToMono(SampleWithRate1_OperatorDto_StereoToMono dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SetPosition_OperatorDto(SetPosition_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SineWithRate1_OperatorDto(SineWithRate1_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Squash_OperatorDto_FactorZero(Squash_OperatorDto_FactorZero dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Squash_OperatorDto_WithOrigin(Squash_OperatorDto_WithOrigin dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Squash_OperatorDto_ZeroOrigin(Squash_OperatorDto_ZeroOrigin dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Squash_OperatorDto_ConstFactor_WithOriginShifting(Squash_OperatorDto_ConstFactor_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Squash_OperatorDto_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarFactor_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumFollower_OperatorDto(SumFollower_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_CollectionRecalculationContinuous dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_CollectionRecalculationUponReset dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_TriangleWithRate1_OperatorDto(TriangleWithRate1_OperatorDto dto) => Visit_OperatorDto_Base(dto);
		/*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Sin_OperatorDto(Sin_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Cos_OperatorDto(Cos_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Tan_OperatorDto(Tan_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SinH_OperatorDto(SinH_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_CosH_OperatorDto(CosH_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_TanH_OperatorDto(TanH_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ArcSin_OperatorDto(ArcSin_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ArcCos_OperatorDto(ArcCos_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_ArcTan_OperatorDto(ArcTan_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_LogN_OperatorDto(LogN_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Ln_OperatorDto(Ln_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_SquareRoot_OperatorDto(SquareRoot_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Sign_OperatorDto(Sign_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Factorial_OperatorDto(Factorial_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Xor_OperatorDto(Xor_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Ceiling_OperatorDto(Ceiling_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Floor_OperatorDto(Floor_OperatorDto dto) => Visit_OperatorDto_Base(dto);
	    /*[DebuggerHidden]*/ protected virtual IOperatorDto Visit_Truncate_OperatorDto(Truncate_OperatorDto dto) => Visit_OperatorDto_Base(dto);
    }
}
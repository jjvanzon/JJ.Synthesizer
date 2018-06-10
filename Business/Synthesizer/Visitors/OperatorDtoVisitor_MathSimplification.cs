using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
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

		protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto) => WithAlreadyProcessedCheck(dto, () => base.Visit_OperatorDto_Polymorphic(dto));

	    // Operation-Specific

		protected override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
		{
			base.Visit_Add_OperatorDto(dto);

			AggregateInfo aggregateInfo = dto.GetAggregateInfo();

			if (aggregateInfo.HasVars && aggregateInfo.ConstIsZero)
			{
				// Identity
				dto.Inputs = dto.Inputs.Except(aggregateInfo.Const).ToArray();
				return dto;
			}

			return ProcessAggregateOverInlets(dto, Enumerable.Sum);
		}

		protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetAllPassFilterVariables);

	    protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
		{
			base.Visit_And_OperatorDto(dto);

			if (dto.A.IsConstNonZero && dto.B.IsConstNonZero)
			{
				// Pre-calculate
				return new Number_OperatorDto(1);
			}
			else if (dto.A.IsConstZero || dto.B.IsConstZero)
			{
				// Pre-calculate
				return new Number_OperatorDto(0);
			}
			else if (dto.A.IsConst && dto.B.IsVar)
			{
				// Commute
				return Commute(dto);
			}
			else if (dto.A.IsVar && dto.B.IsConstZero)
			{
				// 0
				return new Number_OperatorDto(0);
			}
			else if (dto.A.IsVar && dto.B.IsConstNonZero)
			{
				// Identity
				return dto.A.Var;
			}
			else
			{
				return dto;
			}
		}

		protected override IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto) => ProcessWithSignal(dto);

	    protected override IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto) => ProcessWithSignal(dto);

	    protected override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
		{
			base.Visit_AverageOverInlets_OperatorDto(dto);

			return ProcessAggregateOverInlets(dto, Enumerable.Average);
		}

		protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetBandPassFilterConstantPeakGainVariables);

	    protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetBandPassFilterConstantTransitionGainVariables);

	    protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_Cache_OperatorDto_ConstSignal(Cache_OperatorDto_ConstSignal dto) => ProcessWithSignal(dto);

	    protected override IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto) => Process_Trigger(dto);

	    protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
		{
			base.Visit_ClosestOverInlets_OperatorDto(dto);

			AggregateInfo aggregateInfo = dto.GetAggregateInfo();

			if (dto.Input.IsConst && aggregateInfo.OnlyConsts)
			{
				// Pre-calculate
				double result = AggregateCalculator.Closest(dto.Input, dto.Items.Select(x => x.Const).ToArray());
				return new Number_OperatorDto { Number = result };
			}
			else if (dto.Input.IsVar && aggregateInfo.OnlyConsts)
			{
				switch (dto.Items.Count)
				{
					case 0:
						// 0
						return new Number_OperatorDto(0);

					case 1:
						// Identity
						return new Number_OperatorDto { Number = dto.Items[0] };

					default:
						return dto;
				}
			}
			else if (aggregateInfo.IsEmpty)
			{
				// 0
				return new Number_OperatorDto(0);
			}
			else
			{
				return dto;
			}
		}

		protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
		{
			base.Visit_ClosestOverInletsExp_OperatorDto(dto);

			AggregateInfo aggregateInfo = dto.GetAggregateInfo();

			if (dto.Input.IsConst && aggregateInfo.OnlyConsts)
			{
				// Pre-calculate
				double result = AggregateCalculator.ClosestExp(dto.Input, dto.Items.Select(x => x.Const).ToArray());
				return new Number_OperatorDto { Number = result };
			}
			else if (dto.Input.IsVar && aggregateInfo.OnlyConsts)
			{
				switch (dto.Items.Count)
				{
					case 0:
						// 0
						return new Number_OperatorDto(0);

					case 1:
						// Identity
						return new Number_OperatorDto { Number = dto.Items[0] };

					default:
						return dto;
				}
			}
			else if (aggregateInfo.IsEmpty)
			{
				// 0
				return new Number_OperatorDto(0);
			}
			else
			{
				return dto;
			}
		}

		protected override IOperatorDto Visit_Curve_OperatorDto_NoCurve(Curve_OperatorDto_NoCurve dto) => ProcessZero(dto);

	    protected override IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto) => ProcessIdentity(dto.OutletPosition);

	    protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
		{
			base.Visit_Divide_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				return new Number_OperatorDto { Number = dto.A / dto.B };
			}
			else if (dto.A.IsConstZero)
			{
				// Identity
				return new Number_OperatorDto { Number = dto.A };
			}
			else if (dto.B.IsConstOne)
			{
				// Identity
				return dto.A.Var;
			}
			else
			{
				return dto;
			}
		}

		protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
		{
			base.Visit_Equal_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				if (dto.A == dto.B)
				{
					return new Number_OperatorDto(1);
				}
				else
				{
					return new Number_OperatorDto(0);
				}
			}
			else if (dto.A.IsConst && dto.B.IsVar)
			{
				// Commute
				return Commute(dto);
			}

			return dto;
		}

		protected override IOperatorDto Visit_GetPosition_OperatorDto(GetPosition_OperatorDto dto)
		{
			base.Visit_GetPosition_OperatorDto(dto);

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

			if (dto.Position.IsVar)
			{
				// Identity
				return dto.Position.Var;
			}

			// This is a little bit tricky:
			// GetPosition with a const is actually a leaf-GetPosition,
			// which means eventually further down the processing lane,
			// they are turned into variable input.

			return dto;
		}

		protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
		{
			base.Visit_GreaterThan_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				if (dto.A > dto.B)
				{
					return new Number_OperatorDto(1);
				}
				else
				{
					return new Number_OperatorDto(0);
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

			return dto;
		}

		protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
		{
			base.Visit_GreaterThanOrEqual_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				if (dto.A >= dto.B)
				{
					return new Number_OperatorDto(1);
				}
				else
				{
					return new Number_OperatorDto(0);
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

			return dto;
		}

		protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetHighPassFilterVariables);

	    protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_ShelfFilter_SoundVarOrConst_OtherInputsConst(dto, BiQuadFilterWithoutFields.SetHighShelfFilterVariables);

	    protected override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto) => ProcessWithSignal(dto);

	    protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
		{
			base.Visit_If_OperatorDto(dto);

			if (dto.Condition.IsConst && dto.Then.IsConst && dto.Else.IsConst)
			{
				// Pre-calculate
				bool isTrue = dto.Condition != 0.0;
				if (isTrue)
				{
					return new Number_OperatorDto { Number = dto.Then };
				}
				else
				{
					return new Number_OperatorDto { Number = dto.Else };
				}
			}
			else if (dto.Condition.IsConst && dto.Then.IsConst && dto.Else.IsVar)
			{
				bool isTrue = dto.Condition != 0.0;
				if (isTrue)
				{
					// Identity
					return new Number_OperatorDto { Number = dto.Then };
				}
				else
				{
					// Identity
					return dto.Else.Var;
				}
			}
			else if (dto.Condition.IsConst && dto.Then.IsVar && dto.Else.IsConst)
			{
				bool isTrue = dto.Condition != 0.0;
				if (isTrue)
				{
					// Identity
					return dto.Then.Var;
				}
				else
				{
					// Identity
					return new Number_OperatorDto { Number = dto.Else };
				}
			}
			else if (dto.Condition.IsConst && dto.Then.IsVar && dto.Else.IsVar)
			{
				bool isTrue = dto.Condition != 0.0;
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
					return new Number_OperatorDto { Number = dto.Then };
				}
			}

			return dto;
		}

		protected override IOperatorDto Visit_Interpolate_OperatorDto_ConstSignal(Interpolate_OperatorDto_ConstSignal dto) => ProcessIdentity(dto);

	    protected override IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
		{
			base.Visit_LessThan_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				if (dto.A < dto.B)
				{
					return new Number_OperatorDto(1);
				}
				else
				{
					return new Number_OperatorDto(0);
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

			return dto;
		}

		protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
		{
			base.Visit_LessThanOrEqual_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				if (dto.A <= dto.B)
				{
					return new Number_OperatorDto(1);
				}
				else
				{
					return new Number_OperatorDto(0);
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

			return dto;
		}

		protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetLowPassFilterVariables);

	    protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_ShelfFilter_SoundVarOrConst_OtherInputsConst(dto, BiQuadFilterWithoutFields.SetHighShelfFilterVariables);

	    protected override IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto) => ProcessWithSignal(dto);

	    protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_ConstSignal(MaxOverDimension_OperatorDto_ConstSignal dto) => ProcessIdentity(dto);

	    protected override IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
		{
			base.Visit_MaxOverInlets_OperatorDto(dto);

			return ProcessAggregateOverInlets(dto, Enumerable.Min);
		}

		protected override IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto) => ProcessWithSignal(dto);

	    protected override IOperatorDto Visit_MinOverDimension_OperatorDto_ConstSignal(MinOverDimension_OperatorDto_ConstSignal dto) => ProcessIdentity(dto);

	    protected override IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
		{
			base.Visit_MinOverInlets_OperatorDto(dto);

			return ProcessAggregateOverInlets(dto, Enumerable.Min);
		}

		protected override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
		{
			base.Visit_Multiply_OperatorDto(dto);

			AggregateInfo aggregateInfo = dto.GetAggregateInfo();

			if (aggregateInfo.HasVars && aggregateInfo.ConstIsOne)
			{
				// Identity
				dto.Inputs = dto.Inputs.Except(aggregateInfo.Const).ToArray();
				return dto;
			}
			else if (aggregateInfo.Consts.Any(x => x.IsConstZero))
			{
				// 0
				return new Number_OperatorDto(0);
			}
			else
			{
				return ProcessAggregateOverInlets(dto, CollectionExtensions.Product);
			}
		}

		protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
		{
			base.Visit_Negative_OperatorDto(dto);

			if (dto.Number.IsConst)
			{
				// Pre-calculate
				return new Number_OperatorDto { Number = -dto.Number };
			}

			return dto;
		}

		protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto) => Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(dto, BiQuadFilterWithoutFields.SetNotchFilterVariables);

	    protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
		{
			base.Visit_NotEqual_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				if (dto.A != dto.B)
				{
					return new Number_OperatorDto(1);
				}
				else
				{
					return new Number_OperatorDto(0);
				}
			}
			else if (dto.A.IsConst && dto.B.IsVar)
			{
				// Commute
				return Commute(dto);
			}

			return dto;
		}

		protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
		{
			base.Visit_Not_OperatorDto(dto);

			if (dto.Number.IsConst)
			{
				// Pre-calculate
				bool isFalse = dto.Number == 0.0;
				if (isFalse)
				{
					return new Number_OperatorDto(1);
				}
				else
				{
					return new Number_OperatorDto(0);
				}
			}

			return dto;
		}

		protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
		{
			base.Visit_Or_OperatorDto(dto);

			if (dto.A.IsConstNonZero || dto.B.IsConstNonZero)
			{
				// Pre-calculate
				return new Number_OperatorDto(1);
			}
			else if (dto.A.IsConstZero && dto.B.IsConstZero)
			{
				// Pre-calculate
				return new Number_OperatorDto(0);
			}
			else if (dto.A.IsConst && dto.B.IsVar)
			{
				// Commute
				return Commute(dto);
			}
			else if (dto.A.IsVar && dto.B.IsConstNonZero)
			{
				// Simplify
				return new Number_OperatorDto(1);
			}
			else if (dto.A.IsVar && dto.B.IsConstZero)
			{
				// Identity
				return dto.A.Var;
			}

			return dto;
		}

		protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto) => Process_Filter_SoundVarOrConst_OtherInputsVar(dto);

	    protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
		{
			base.Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);

			if (dto.Sound.IsConst)
			{
				// Identity
				return new Number_OperatorDto { Number = dto.Sound };
			}

			double limitedFrequency = LimitFrequency(dto.Frequency, dto.NyquistFrequency);

			BiQuadFilterWithoutFields.SetPeakingEQFilterVariables(
				dto.TargetSamplingRate, limitedFrequency, dto.Width, dto.DBGain,
				out double a0, out double a1, out double a2, out double a3, out double a4);

			dto.A0 = a0;
			dto.A1 = a1;
			dto.A2 = a2;
			dto.A3 = a3;
			dto.A4 = a4;

			return dto;
		}

		protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
		{
			base.Visit_Power_OperatorDto(dto);

			if (dto.Base.IsConst && dto.Exponent.IsConst)
			{
				// Pre-calculate
				return new Number_OperatorDto { Number = Math.Pow(dto.Base, dto.Exponent) };
			}
			else if (dto.Base.IsConstZero)
			{
				// 0
				return new Number_OperatorDto(0);
			}
			else if (dto.Base.IsConstOne)
			{
				// 1
				return new Number_OperatorDto(1);
			}
			if (dto.Exponent.IsConstZero)
			{
				// 1
				return new Number_OperatorDto(1);
			}
			else if (dto.Exponent.IsConstOne)
			{
				// Identity
				return dto.Base.Var;
			}
			else
			{
				return dto;
			}
		}

		protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto) => Process_Trigger(dto);

	    protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto(RangeOverOutlets_Outlet_OperatorDto dto)
		{
			base.Visit_RangeOverOutlets_Outlet_OperatorDto(dto);

			var dto2 = new Multiply_OperatorDto { Inputs = new[] { dto.Step, dto.OutletPosition } };
			var dto3 = new Add_OperatorDto { Inputs = new[] { dto.From, dto2 } };
			return dto3;
		}

		protected override IOperatorDto Visit_Remainder_OperatorDto(Remainder_OperatorDto dto)
		{
			base.Visit_Remainder_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				return new Number_OperatorDto { Number = dto.A % dto.B };
			}
			else if (dto.A.IsConstZero)
			{
				// Identity
				return new Number_OperatorDto { Number = dto.A };
			}
			else
			{
				return dto;
			}
		}

		protected override IOperatorDto Visit_Round_OperatorDto_AllConsts(Round_OperatorDto_AllConsts dto)
		{
			base.Visit_Round_OperatorDto_AllConsts(dto);

			// Pre-calculate
			double result = MathHelper.RoundWithStep(dto.Signal, dto.Step, dto.Offset);
			return new Number_OperatorDto { Number = result };
		}

		protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_NoSample(SampleWithRate1_OperatorDto_NoSample dto) => ProcessZero(dto);

	    protected override IOperatorDto Visit_SetPosition_OperatorDto(SetPosition_OperatorDto dto) => dto.Position.VarOrConst;

	    protected override IOperatorDto Visit_SortOverDimension_OperatorDto_ConstSignal(SortOverDimension_OperatorDto_ConstSignal dto) => ProcessIdentity(dto);

	    protected override IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto)
		{
			base.Visit_Spectrum_OperatorDto(dto);

			if (dto.Sound.IsConst)
			{
				// 0
				return new Number_OperatorDto(0);
			}
			else
			{
				return dto;
			}
		}

		protected override IOperatorDto Visit_Squash_OperatorDto_FactorZero(Squash_OperatorDto_FactorZero dto) => ProcessZero(dto);

	    protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
		{
			base.Visit_Subtract_OperatorDto(dto);

			if (dto.A.IsConst && dto.B.IsConst)
			{
				// Pre-calculate
				return new Number_OperatorDto { Number = dto.A - dto.B };
			}
			else if (dto.A.IsConstZero && dto.B.IsVar)
			{
				// Identity, switch sign
				var dto2 = new Negative_OperatorDto();
				DtoCloner.CloneProperties(dto, dto2);
				dto2.Number = dto.B;
				return dto2;
			}
			else if (dto.A.IsVar && dto.B.IsConstZero)
			{
				// Identity
				return dto.A.Var;
			}

			return dto;
		}

		protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(SumFollower_OperatorDto_ConstSignal_ConstSampleCount dto)
		{
			base.Visit_SumFollower_OperatorDto_ConstSignal_ConstSampleCount(dto);

			// Pre-calculate
			return new Number_OperatorDto { Number = dto.Signal * dto.SampleCount };
		}

		protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
		{
			base.Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(dto);

			// Simplify
			var dto2 = new Multiply_OperatorDto { Inputs = new [] { dto.Signal, dto.SampleCount } };

			return dto2;
		}

		protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllConsts(SumOverDimension_OperatorDto_AllConsts dto)
		{
			base.Visit_SumOverDimension_OperatorDto_AllConsts(dto);

			// Pre-calculate
			int sampleCount = (int)(dto.Till - dto.From / dto.Step);
			double result = dto.Signal * sampleCount;
			return new Number_OperatorDto { Number = result };
		}

		protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto) => Process_Trigger(dto);

	    // Helpers

		private static IOperatorDto Commute(OperatorDtoBase_WithAAndB dto)
		{
			InputDto tempA = dto.A;
			InputDto tempB = dto.B;

			dto.A = tempB;
			dto.B = tempA;

			return dto;
		}

		private IOperatorDto ProcessIdentity(double number) => new Number_OperatorDto { Number = number };

	    private IOperatorDto Process_Filter_SoundVarOrConst_OtherInputsVar(IOperatorDto_WithSound dto)
		{
			Visit_OperatorDto_Base(dto);

			if (dto.Sound.IsConst)
			{
				// Identity
				return new Number_OperatorDto { Number = dto.Sound };
			}

			return dto;
		}

		private IOperatorDto Process_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume(
			OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume dto,
			SetFilterParametersWithWidthOrBlobVolumeDelegate setFilterParametersDelegate)
		{
			Visit_OperatorDto_Base(dto);

			if (dto.Sound.IsConst)
			{
				// Identity
				return new Number_OperatorDto { Number = dto.Sound };
			}

			double limitedFrequency = LimitFrequency(dto.Frequency, dto.NyquistFrequency);

			setFilterParametersDelegate(
				dto.TargetSamplingRate, limitedFrequency, dto.WidthOrBlobVolume,
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

		private IOperatorDto ProcessAggregateOverInlets(IOperatorDto_WithAggregateInfo dto, Func<IEnumerable<double>, double> aggregationDelegate)
		{
			AggregateInfo aggregateInfo = dto.GetAggregateInfo();

			if (aggregateInfo.HasVars && aggregateInfo.Consts.Count > 1)
			{
				// Pre-calculate
				InputDto aggregate = aggregationDelegate(aggregateInfo.Consts.Select(x => x.Const));
				dto.Inputs = aggregateInfo.Vars.Concat(aggregate).ToArray();
				return dto;
			}
			else if (aggregateInfo.OnlyVars)
			{
				switch (dto.Inputs.Count)
				{
					case 0:
						// 0
						return new Number_OperatorDto(0);

					case 1:
						// Identity
						return dto.Inputs.Single().Var;

					default:
						return dto;
				}
			}
			else if (aggregateInfo.OnlyConsts)
			{
				// Pre-calculate
				double result = aggregationDelegate(dto.Inputs.Select(x => x.Const));
				return new Number_OperatorDto { Number = result };
			}
			else if (aggregateInfo.IsEmpty)
			{
				// 0
				return new Number_OperatorDto(0);
			}
			else
			{
				return dto;
			}
		}

		private IOperatorDto Process_ShelfFilter_SoundVarOrConst_OtherInputsConst(
			OperatorDtoBase_ShelfFilter_SoundVarOrConst_OtherInputsConst dto,
			SetShelfFilterParametersDelegate setFilterParametersDelegate)
		{
			Visit_OperatorDto_Base(dto);

			if (dto.Sound.IsConst)
			{
				// Identity
				return new Number_OperatorDto { Number = dto.Sound };
			}

			double limitedFrequency = LimitFrequency(dto.Frequency, dto.NyquistFrequency);
			setFilterParametersDelegate(
				dto.TargetSamplingRate, limitedFrequency, dto.TransitionSlope, dto.DBGain,
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
			Visit_OperatorDto_Base(dto);

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

			return dto;
		}

		private IOperatorDto Process_Trigger_ConstPassThrough_ConstReset_Identity(OperatorDtoBase_Trigger dto) => new Number_OperatorDto { Number = dto.PassThroughInput };

	    private IOperatorDto Process_Trigger_ConstPassThrough_VarReset_Identity(OperatorDtoBase_Trigger dto) => new Number_OperatorDto { Number = dto.PassThroughInput };

	    private IOperatorDto Process_Trigger_VarPassThrough_ConstReset_Identity(OperatorDtoBase_Trigger dto) => dto.PassThroughInput.Var;

	    private IOperatorDto ProcessZero(IOperatorDto dto)
		{
			Visit_OperatorDto_Base(dto);

			// 0
			return new Number_OperatorDto(0);
		}

		private IOperatorDto ProcessWithSignal(IOperatorDto_WithSignal dto)
		{
			Visit_OperatorDto_Base(dto);

			if (dto.Signal.IsConst)
			{
				// Identity
				return new Number_OperatorDto { Number = dto.Signal };
			}

			return dto;
		}

		private IOperatorDto ProcessIdentity(IOperatorDto_WithSignal dto)
		{
			Visit_OperatorDto_Base(dto);

			// Identity
			return new Number_OperatorDto { Number = dto.Signal };
		}
	}
}

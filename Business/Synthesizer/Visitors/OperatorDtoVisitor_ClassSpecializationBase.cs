using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions.InvalidValues;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable ConvertIfStatementToSwitchStatement

namespace JJ.Business.Synthesizer.Visitors
{
	internal abstract class OperatorDtoVisitor_ClassSpecializationBase : OperatorDtoVisitorBase
	{
		protected override IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto)
		{
			base.Visit_AllPassFilter_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
			{
				dto2 = new AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto)
		{
			base.Visit_AverageOverDimension_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
			{
				dto2 = new AverageOverDimension_OperatorDto_CollectionRecalculationContinuous();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
			{
				dto2 = new AverageOverDimension_OperatorDto_CollectionRecalculationUponReset();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto)
		{
			base.Visit_BandPassFilterConstantPeakGain_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
			{
				dto2 = new BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto)
		{
			base.Visit_BandPassFilterConstantTransitionGain_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
			{
				dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_Cache_OperatorDto(Cache_OperatorDto dto)
		{
			base.Visit_Cache_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst)
			{
				dto2 = new Cache_OperatorDto_ConstSignal();
			}
			else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Block)
			{
				dto2 = new Cache_OperatorDto_SingleChannel_Block();
			}
			else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
			{
				dto2 = new Cache_OperatorDto_SingleChannel_Cubic();
			}
			else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
			{
				dto2 = new Cache_OperatorDto_SingleChannel_Hermite();
			}
			else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
			{
				dto2 = new Cache_OperatorDto_SingleChannel_Line();
			}
			else if (dto.ChannelCount == 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
			{
				dto2 = new Cache_OperatorDto_SingleChannel_Stripe();
			}
			else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Block)
			{
				dto2 = new Cache_OperatorDto_MultiChannel_Block();
			}
			else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Cubic)
			{
				dto2 = new Cache_OperatorDto_MultiChannel_Cubic();
			}
			else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Hermite)
			{
				dto2 = new Cache_OperatorDto_MultiChannel_Hermite();
			}
			else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Line)
			{
				dto2 = new Cache_OperatorDto_MultiChannel_Line();
			}
			else if (dto.ChannelCount > 1 && dto.InterpolationTypeEnum == InterpolationTypeEnum.Stripe)
			{
				dto2 = new Cache_OperatorDto_MultiChannel_Stripe();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto)
		{
			base.Visit_ClosestOverDimension_OperatorDto(dto);

			IOperatorDto dto2;

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

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto)
		{
			base.Visit_ClosestOverDimensionExp_OperatorDto(dto);

			IOperatorDto dto2;

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

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_Curve_OperatorDto(Curve_OperatorDto dto)
		{
			base.Visit_Curve_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.CurveID == 0)
			{
				dto2 = new Curve_OperatorDto_NoCurve();
			}
			else if (dto.StandardDimensionEnum == DimensionEnum.Time)
			{
				dto2 = new Curve_OperatorDto_WithOriginShifting();
			}
			else if (dto.StandardDimensionEnum != DimensionEnum.Time)
			{
				dto2 = new Curve_OperatorDto_NoOriginShifting();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
		{
			base.Visit_HighPassFilter_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.MinFrequency.IsConst && dto.BlobVolume.IsConst)
			{
				dto2 = new HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
		{
			base.Visit_HighShelfFilter_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.TransitionFrequency.IsConst && dto.TransitionSlope.IsConst & dto.DBGain.IsConst)
			{
				dto2 = new HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto)
		{
			base.Visit_InletsToDimension_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
			{
				dto2 = new InletsToDimension_OperatorDto_Block { ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
			{
				dto2 = new InletsToDimension_OperatorDto_Stripe_LagBehind { ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line)
			{
				dto2 = new InletsToDimension_OperatorDto_Line { ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
			{
				dto2 = new InletsToDimension_OperatorDto_CubicEquidistant { ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
			{
				dto2 = new InletsToDimension_OperatorDto_CubicAbruptSlope { ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
			{
				dto2 = new InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind { ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
			{
				dto2 = new InletsToDimension_OperatorDto_Hermite_LagBehind { ResampleInterpolationTypeEnum = dto.ResampleInterpolationTypeEnum };
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto)
		{
			base.Visit_Interpolate_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst)
			{
				dto2 = new Interpolate_OperatorDto_ConstSignal();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
			{
				dto2 = new Interpolate_OperatorDto_Block();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
			{
				dto2 = new Interpolate_OperatorDto_Stripe_LagBehind();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.SamplingRate.IsConst)
			{
				dto2 = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.SamplingRate.IsVar)
			{
				dto2 = new Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
			{
				dto2 = new Interpolate_OperatorDto_CubicEquidistant();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
			{
				dto2 = new Interpolate_OperatorDto_CubicAbruptSlope();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
			{
				dto2 = new Interpolate_OperatorDto_CubicSmoothSlope_LagBehind();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
			{
				dto2 = new Interpolate_OperatorDto_Hermite_LagBehind();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
		{
			base.Visit_LowPassFilter_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.MaxFrequency.IsConst && dto.BlobVolume.IsConst)
			{
				dto2 = new LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
		{
			base.Visit_LowShelfFilter_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.TransitionSlope.IsConst && dto.TransitionSlope.IsConst & dto.DBGain.IsConst)
			{
				dto2 = new LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto)
		{
			base.Visit_MaxOverDimension_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst)
			{
				dto2 = new MaxOverDimension_OperatorDto_ConstSignal();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
			{
				dto2 = new MaxOverDimension_OperatorDto_CollectionRecalculationContinuous();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
			{
				dto2 = new MaxOverDimension_OperatorDto_CollectionRecalculationUponReset();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto)
		{
			base.Visit_MinOverDimension_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst)
			{
				dto2 = new MinOverDimension_OperatorDto_ConstSignal();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
			{
				dto2 = new MinOverDimension_OperatorDto_CollectionRecalculationContinuous();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
			{
				dto2 = new MinOverDimension_OperatorDto_CollectionRecalculationUponReset();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
		{
			base.Visit_NotchFilter_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.CenterFrequency.IsConst && dto.Width.IsConst)
			{
				dto2 = new NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 = new NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
		{
			base.Visit_PeakingEQFilter_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.CenterFrequency.IsConst && dto.Width.IsConst & dto.DBGain.IsConst)
			{
				dto2 = new PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst();
			}
			else
			{
				dto2 =new PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_Random_OperatorDto(Random_OperatorDto dto)
		{
			base.Visit_Random_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
			{
				dto2 = new Random_OperatorDto_Block();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
			{
				dto2 = new Random_OperatorDto_Stripe_LagBehind();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.Rate.IsConst)
			{
				dto2 = new Random_OperatorDto_Line_LagBehind_ConstRate();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && dto.Rate.IsVar)
			{
				dto2 = new Random_OperatorDto_Line_LagBehind_VarRate();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
			{
				dto2 = new Random_OperatorDto_CubicEquidistant();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
			{
				dto2 = new Random_OperatorDto_CubicAbruptSlope();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
			{
				dto2 = new Random_OperatorDto_CubicSmoothSlope_LagBehind();
			}
			else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
			{
				dto2 = new Random_OperatorDto_Hermite_LagBehind();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto)
		{
			base.Visit_RangeOverDimension_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.From.IsConst && dto.Till.IsConst && dto.Step.IsConst)
			{
				dto2 = new RangeOverDimension_OperatorDto_OnlyConsts();
			}
			else
			{
				dto2 = new RangeOverDimension_OperatorDto_OnlyVars();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_Round_OperatorDto(Round_OperatorDto dto)
		{
			base.Visit_Round_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst && dto.Step.IsConst && dto.Offset.IsConst)
			{
				dto2 = new Round_OperatorDto_AllConsts();
			}
			else if (dto.Step.IsConstOne && dto.Offset.IsConstZero)
			{
				dto2 = new Round_OperatorDto_StepOne_ZeroOffset();
			}
			else if (dto.Offset.IsConstZero)
			{
				dto2 = new Round_OperatorDto_ZeroOffset();
			}
			else
			{
				dto2 = new Round_OperatorDto_WithOffset();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_SampleWithRate1_OperatorDto(SampleWithRate1_OperatorDto dto)
		{
			int sampleChannelCount = dto.SampleChannelCount;
			bool hasTargetChannelCount = sampleChannelCount == dto.TargetChannelCount;
			bool isFromMonoToStereo = sampleChannelCount == 1 && dto.TargetChannelCount == 2;
			bool isFromStereoToMono = sampleChannelCount == 2 && dto.TargetChannelCount == 1;

			IOperatorDto dto2;

			if (dto.SampleID == 0)
			{
				dto2 = new SampleWithRate1_OperatorDto_NoSample();
			}
			else if (hasTargetChannelCount)
			{
				dto2 = new SampleWithRate1_OperatorDto_NoChannelConversion();
			}
			else if (isFromMonoToStereo)
			{
				dto2 = new SampleWithRate1_OperatorDto_MonoToStereo();
			}
			else if (isFromStereoToMono)
			{
				dto2 = new SampleWithRate1_OperatorDto_StereoToMono();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto)
		{
			base.Visit_SortOverDimension_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst)
			{
				dto2 = new SortOverDimension_OperatorDto_ConstSignal();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
			{
				dto2 = new SortOverDimension_OperatorDto_CollectionRecalculationContinuous();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
			{
				dto2 = new SortOverDimension_OperatorDto_CollectionRecalculationUponReset();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto)
		{
			base.Visit_Squash_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Factor.IsConstZero)
			{
				dto2 = new Squash_OperatorDto_FactorZero();
			}
			else if (dto.StandardDimensionEnum == DimensionEnum.Time && dto.Factor.IsVar)
			{
				dto2 = new Squash_OperatorDto_VarFactor_WithPhaseTracking();
			}
			else if (dto.StandardDimensionEnum == DimensionEnum.Time && dto.Factor.IsConst)
			{
				dto2 = new Squash_OperatorDto_ConstFactor_WithOriginShifting();
			}
			else if (dto.Origin.IsConstZero)
			{
				dto2 = new Squash_OperatorDto_ZeroOrigin();
			}
			else
			{
				dto2 = new Squash_OperatorDto_WithOrigin();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto)
		{
			base.Visit_SumOverDimension_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst && dto.From.IsConst && dto.Till.IsConst && dto.Step.IsConst)
			{
				dto2 = new SumOverDimension_OperatorDto_AllConsts();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.Continuous)
			{
				dto2 = new SumOverDimension_OperatorDto_CollectionRecalculationContinuous();
			}
			else if (dto.CollectionRecalculationEnum == CollectionRecalculationEnum.UponReset)
			{
				dto2 = new SumOverDimension_OperatorDto_CollectionRecalculationUponReset();
			}
			else
			{
				throw new VisitationCannotBeHandledException();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}

		protected override IOperatorDto Visit_SumFollower_OperatorDto(SumFollower_OperatorDto dto)
		{
			base.Visit_SumFollower_OperatorDto(dto);

			IOperatorDto dto2;

			if (dto.Signal.IsConst && dto.SampleCount.IsConst)
			{
				dto2 = new SumFollower_OperatorDto_ConstSignal_ConstSampleCount();
			}
			else if (dto.Signal.IsConst && dto.SampleCount.IsVar)
			{
				dto2 = new SumFollower_OperatorDto_ConstSignal_VarSampleCount();
			}
			else
			{
				dto2 = new SumFollower_OperatorDto_AllVars();
			}

			DtoCloner.CloneProperties(dto, dto2);

			return dto2;
		}
	}
}

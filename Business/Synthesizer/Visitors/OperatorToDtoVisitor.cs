using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorToDtoVisitor : OperatorVisitorBase
    {
        private readonly ICurveRepository _curveRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;

        private Stack<OperatorDtoBase> _stack;

        public OperatorToDtoVisitor(
            ICurveRepository curveRepository, 
            IPatchRepository patchRepository, 
            ISampleRepository sampleRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            _curveRepository = curveRepository;
            _patchRepository = patchRepository;
            _sampleRepository = sampleRepository;
            _speakerSetupRepository = speakerSetupRepository;
        }

        public OperatorDtoBase Execute(Operator op)
        {
            _stack = new Stack<OperatorDtoBase>();

            VisitOperatorPolymorphic(op);

            if (_stack.Count != 1)
            {
                throw new Exception(String.Format("{0} should have been 1.", ExpressionHelper.GetText(() => _stack.Count)));
            }

            OperatorDtoBase dto = _stack.Pop();

            return dto;
        }

        protected override void VisitAbsolute(Operator op)
        {
            var dto = new Absolute_OperatorDto();
            Visit_OperatorDtoBase_VarX(op, dto);
        }

        protected override void VisitAdd(Operator op)
        {
            var dto = new Add_OperatorDto();
            Visit_OperatorDtoBase_Vars(op, dto);
        }

        protected override void VisitAllPassFilter(Operator op)
        {
            base.VisitAllPassFilter(op);

            var dto = new AllPassFilter_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                CenterFrequencyOperatorDto = _stack.Pop(),
                BandWidthOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitAnd(Operator op)
        {
            var dto = new And_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitAverageFollower(Operator op)
        {
            var dto = new AverageFollower_OperatorDto();
            Visit_OperatorDtoBase_AggregateFollower(op, dto);
        }

        protected override void VisitAverageOverDimension(Operator op)
        {
            var dto = new AverageOverDimension_OperatorDto();
            Visit_OperatorDtoBase_AggregateOverDimension(op, dto);
        }

        protected override void VisitAverageOverInlets(Operator op)
        {
            var dto = new AverageOverInlets_OperatorDto();
            Visit_OperatorDtoBase_Vars(op, dto);
        }

        protected override void VisitBandPassFilterConstantPeakGain(Operator op)
        {
            var dto = new BandPassFilterConstantPeakGain_OperatorDto();
            Visit_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth(op, dto);
        }

        protected override void VisitBandPassFilterConstantTransitionGain(Operator op)
        {
            var dto = new BandPassFilterConstantTransitionGain_OperatorDto();
            Visit_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth(op, dto);
        }

        protected override void VisitBundle(Operator op)
        {
            base.VisitBundle(op);

            var dto = new Bundle_OperatorDto();
            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitChangeTrigger(Operator op)
        {
            var dto = new ChangeTrigger_OperatorDto();
            Visit_OperatorDtoBase_Trigger(op, dto);
        }

        protected override void VisitClosestOverInlets(Operator op)
        {
            var dto = new ClosestOverInlets_OperatorDto();
            Visit_ClosestOverInlets_OperatorDto(op, dto);
        }

        protected override void VisitClosestOverInletsExp(Operator op)
        {
            var dto = new ClosestOverInletsExp_OperatorDto();
            Visit_ClosestOverInlets_OperatorDto(op, dto);
        }

        protected override void VisitClosestOverDimension(Operator op)
        {
            var dto = new ClosestOverDimension_OperatorDto();
            Visit_ClosestOverDimension_OperatorDto(op, dto);
        }

        protected override void VisitClosestOverDimensionExp(Operator op)
        {
            var dto = new ClosestOverDimensionExp_OperatorDto();
            Visit_ClosestOverDimension_OperatorDto(op, dto);
        }

        protected override void VisitCache(Operator op)
        {
            base.VisitCache(op);

            var wrapper = new Cache_OperatorWrapper(op);
            SpeakerSetupEnum speakerSetupEnum = wrapper.SpeakerSetup;

            var dto = new Cache_OperatorDto
            {
                OperatorID = op.ID,
                SignalOperatorDto = _stack.Pop(),
                StartOperatorDto = _stack.Pop(),
                EndOperatorDto = _stack.Pop(),
                SamplingRateOperatorDto = _stack.Pop(),

                InterpolationTypeEnum = wrapper.InterpolationType,
                SpeakerSetupEnum = speakerSetupEnum
            };

            SpeakerSetup speakerSetup = _speakerSetupRepository.Get((int)speakerSetupEnum);
            dto.ChannelCount = speakerSetup.SpeakerSetupChannels.Count;

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitCurveOperator(Operator op)
        {
            base.VisitCurveOperator(op);

            var wrapper = new Curve_OperatorWrapper(op, _curveRepository);

            var dto = new Curve_OperatorDto();

            Curve curve = wrapper.Curve;

            if (curve != null)
            {
                dto.CurveID = curve.ID;
                dto.MinX = curve.Nodes
                                .OrderBy(x => x.X)
                                .First()
                                .X;
            }

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitCustomOperator(Operator op)
        {
            base.VisitCustomOperator(op);

            var wrapper = new CustomOperator_OperatorWrapper(op, _patchRepository);

            var dto = new CustomOperator_OperatorDto
            {
                UnderlyingPatchID = wrapper.UnderlyingPatchID
            };  

            int count = op.Inlets.Count;
            var inputOperatorDtos = new OperatorDtoBase[count];
            for (int i = 0; i < count; i++)
            {
                inputOperatorDtos[i] = _stack.Pop();
            }
            dto.InputOperatorDtos = inputOperatorDtos;


            _stack.Push(dto);
        }

        protected override void VisitDimensionToOutlets(Operator op)
        {
            base.VisitDimensionToOutlets(op);

            var dto = new DimensionToOutlets_OperatorDto
            {
                OperandOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);        
        }

        protected override void VisitDivide(Operator op)
        {
            base.VisitDivide(op);

            var dto = new Divide_OperatorDto
            {
                AOperatorDto = _stack.Pop(),
                BOperatorDto = _stack.Pop(),
                OriginOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitEqual(Operator op)
        {
            var dto = new Equal_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitExponent(Operator op)
        {
            base.VisitExponent(op);

            var dto = new Exponent_OperatorDto
            {
                LowOperatorDto = _stack.Pop(),
                HighOperatorDto = _stack.Pop(),
                RatioOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitGetDimension(Operator op)
        {
            base.VisitGetDimension(op);

            var dto = new GetDimension_OperatorDto();
            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitGreaterThan(Operator op)
        {
            var dto = new GreaterThan_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitGreaterThanOrEqual(Operator op)
        {
            var dto = new GreaterThanOrEqual_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitHold(Operator op)
        {
            base.VisitHold(op);

            var dto = new Hold_OperatorDto_VarSignal();

            _stack.Push(dto);
        }

        protected override void VisitHighPassFilter(Operator op)
        {
            base.VisitHighPassFilter(op);

            var dto = new HighPassFilter_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                MinFrequencyOperatorDto = _stack.Pop(),
                BandWidthOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitHighShelfFilter(Operator op)
        {
            var dto = new HighShelfFilter_OperatorDto();
            Visit_OperatorDtoBase_ShelfFilter_AllVars(op, dto);
        }

        protected override void VisitIf(Operator op)
        {
            base.VisitIf(op);

            var dto = new If_OperatorDto
            {
                ConditionOperatorDto = _stack.Pop(),
                ThenOperatorDto = _stack.Pop(),
                ElseOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitInletsToDimension(Operator op)
        {
            base.VisitInletsToDimension(op);

            var wrapper = new InletsToDimension_OperatorWrapper(op);

            var dto = new InletsToDimension_OperatorDto
            {
                ResampleInterpolationTypeEnum = wrapper.InterpolationType
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitLessThan(Operator op)
        {
            var dto = new LessThan_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }
        
        protected override void VisitLessThanOrEqual(Operator op)
        {
            var dto = new LessThanOrEqual_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitLoop(Operator op)
        {
            base.VisitLoop(op);

            var dto = new Loop_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                SkipOperatorDto = _stack.Pop(),
                LoopStartMarkerOperatorDto = _stack.Pop(),
                LoopEndMarkerOperatorDto = _stack.Pop(),
                ReleaseEndMarkerOperatorDto = _stack.Pop(),
                NoteDurationOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }
        
        protected override void VisitLowPassFilter(Operator op)
        {
            base.VisitLowPassFilter(op);

            var dto = new LowPassFilter_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                MaxFrequencyOperatorDto = _stack.Pop(),
                BandWidthOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }
        
        protected override void VisitLowShelfFilter(Operator op)
        {
            var dto = new LowShelfFilter_OperatorDto();
            Visit_OperatorDtoBase_ShelfFilter_AllVars(op, dto);
        }
        
        protected override void VisitMaxOverDimension(Operator op)
        {
            var dto = new MaxOverDimension_OperatorDto();
            Visit_OperatorDtoBase_AggregateOverDimension(op, dto);
        }
        
        protected override void VisitMaxOverInlets(Operator op)
        {
            var dto = new MaxOverInlets_OperatorDto();
            Visit_OperatorDtoBase_Vars(op, dto);
        }
        
        protected override void VisitMaxFollower(Operator op)
        {
            var dto = new MaxFollower_OperatorDto();
            Visit_OperatorDtoBase_AggregateFollower(op, dto);
        }

        protected override void VisitMinOverDimension(Operator op)
        {
            var dto = new MinOverDimension_OperatorDto();
            Visit_OperatorDtoBase_AggregateOverDimension(op, dto);
        }

        protected override void VisitMinOverInlets(Operator op)
        {
            var dto = new MinOverInlets_OperatorDto();
            Visit_OperatorDtoBase_Vars(op, dto);
        }
        
        protected override void VisitMinFollower(Operator op)
        {
            var dto = new MinFollower_OperatorDto();
            Visit_OperatorDtoBase_AggregateFollower(op, dto);
        }
        
        protected override void VisitMultiply(Operator op)
        {
            var dto = new Multiply_OperatorDto();
            Visit_OperatorDtoBase_Vars(op, dto);
        }
        
        protected override void VisitMultiplyWithOrigin(Operator op)
        {
            base.VisitMultiplyWithOrigin(op);

            var dto = new MultiplyWithOrigin_OperatorDto
            {
                AOperatorDto = _stack.Pop(),
                BOperatorDto = _stack.Pop(),
                OriginOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitNegative(Operator op)
        {
            var dto = new Negative_OperatorDto();
            Visit_OperatorDtoBase_VarX(op, dto);
        }

        protected override void VisitNoise(Operator op)
        {
            base.VisitNoise(op);

            var dto = new Noise_OperatorDto();
            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }
        
        protected override void VisitNot(Operator op)
        {
            var dto = new Not_OperatorDto();
            Visit_OperatorDtoBase_VarX(op, dto);
        }

        protected override void VisitNotchFilter(Operator op)
        {
            base.VisitNotchFilter(op);

            var dto = new NotchFilter_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                CenterFrequencyOperatorDto = _stack.Pop(),
                BandWidthOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitNotEqual(Operator op)
        {
            var dto = new NotEqual_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitNumber(Operator op)
        {
            base.VisitNumber(op);

            var wrapper = new Number_OperatorWrapper(op);

            var dto = new Number_OperatorDto
            {
                Number = wrapper.Number
            };

            _stack.Push(dto);
        }
        
        protected override void VisitOneOverX(Operator op)
        {
            var dto = new OneOverX_OperatorDto();
            Visit_OperatorDtoBase_VarX(op, dto);
        }

        protected override void VisitOr(Operator op)
        {
            var dto = new Or_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitPatchInlet(Operator op)
        {
            base.VisitPatchInlet(op);

            var wrapper = new PatchInlet_OperatorWrapper(op);

            var dto = new PatchInlet_OperatorDto
            {
                DefaultValue = wrapper.Inlet.DefaultValue ?? 0.0,
                ListIndex = wrapper.ListIndex,
                Name = op.Name,
                DimensionEnum = wrapper.Inlet.GetDimensionEnum()
            };

            _stack.Push(dto);
        }

        protected override void VisitPatchOutlet(Operator op)
        {
            base.VisitPatchOutlet(op);

            var wrapper = new PatchOutlet_OperatorWrapper(op);

            var dto = new PatchOutlet_OperatorDto
            {
                ListIndex = wrapper.ListIndex,
                Name = op.Name,
                DimensionEnum = wrapper.Inlet.GetDimensionEnum()
            };

            _stack.Push(dto);
        }

        protected override void VisitPeakingEQFilter(Operator op)
        {
            base.VisitPeakingEQFilter(op);

            var dto = new PeakingEQFilter_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                CenterFrequencyOperatorDto = _stack.Pop(),
                BandWidthOperatorDto = _stack.Pop(),
                DBGainOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }
        
        protected override void VisitPower(Operator op)
        {
            base.VisitPower(op);

            var dto = new Power_OperatorDto
            {
                BaseOperatorDto = _stack.Pop(),
                ExponentOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }
        
        protected override void VisitPulse(Operator op)
        {
            base.VisitPulse(op);

            var dto = new Pulse_OperatorDto
            {
                FrequencyOperatorDto = _stack.Pop(),
                WidthOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }
        
        protected override void VisitPulseTrigger(Operator op)
        {
            var dto = new PulseTrigger_OperatorDto();
            Visit_OperatorDtoBase_Trigger(op, dto);
        }

        protected override void VisitRandom(Operator op)
        {
            base.VisitRandom(op);

            var wrapper = new Random_OperatorWrapper(op);

            var dto = new Random_OperatorDto
            {
                RateOperatorDto = _stack.Pop(),
                ResampleInterpolationTypeEnum = wrapper.InterpolationType
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }
        
        protected override void VisitRangeOverDimension(Operator op)
        {
            base.VisitRangeOverDimension(op);

            var dto = new RangeOverDimension_OperatorDto
            {
                FromOperatorDto = _stack.Pop(),
                TillOperatorDto = _stack.Pop(),
                StepOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitRangeOverOutlets(Operator op)
        {
            base.VisitRangeOverOutlets(op);

            var wrapper = new RangeOverOutlets_OperatorWrapper(op);

            var dto = new RangeOverOutlets_OperatorDto
            {
                FromOperatorDto = _stack.Pop(),
                StepOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitInterpolate(Operator op)
        {
            base.VisitInterpolate(op);

            var wrapper = new Interpolate_OperatorWrapper(op);

            var dto = new Interpolate_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                SamplingRateOperatorDto = _stack.Pop(),
                ResampleInterpolationTypeEnum = wrapper.InterpolationType
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitReset(Operator op)
        {
            base.VisitReset(op);

            var dto = new Reset_OperatorDto
            {
                PassThroughInputOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }
        
        protected override void VisitReverse(Operator op)
        {
            base.VisitReverse(op);

            var dto = new Reverse_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                SpeedOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }
        
        protected override void VisitRound(Operator op)
        {
            base.VisitRound(op);

            var dto = new Round_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                StepOperatorDto = _stack.Pop(),
                OffsetOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            var dto = new Sample_OperatorDto();
            Visit_OperatorDtoBase_VarFrequency(op, dto);

            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);
            Sample sample = wrapper.Sample;

            if (sample != null)
            {
                dto.SampleID = sample.ID;
                dto.InterpolationTypeEnum = sample.GetInterpolationTypeEnum();
                dto.ChannelCount = sample.GetChannelCount();
            }
        }

        protected override void VisitSawDown(Operator op)
        {
            var dto = new SawDown_OperatorDto();
            Visit_OperatorDtoBase_VarFrequency(op, dto);
        }

        protected override void VisitSawUp(Operator op)
        {
            var dto = new SawUp_OperatorDto();
            Visit_OperatorDtoBase_VarFrequency(op, dto);
        }
        
        protected override void VisitScaler(Operator op)
        {
            base.VisitScaler(op);

            var dto = new Scaler_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                SourceValueAOperatorDto = _stack.Pop(),
                SourceValueBOperatorDto = _stack.Pop(),
                TargetValueAOperatorDto = _stack.Pop(),
                TargetValueBOperatorDto = _stack.Pop()
            };

            _stack.Push(dto);
        }
        
        protected override void VisitSelect(Operator op)
        {
            base.VisitSelect(op);

            var dto = new Select_OperatorDto
            {
                SignalOperatorDto  = _stack.Pop(),
                PositionOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitSetDimension(Operator op)
        {
            base.VisitSetDimension(op);

            var dto = new SetDimension_OperatorDto
            {
                PassThroughInputOperatorDto = _stack.Pop(),
                ValueOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitShift(Operator op)
        {
            base.VisitShift(op);

            var dto = new Shift_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                DistanceOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitSine(Operator op)
        {
            var dto = new Sine_OperatorDto();
            Visit_OperatorDtoBase_VarFrequency(op, dto);
        }
        
        protected override void VisitSortOverInlets(Operator op)
        {
            var dto = new SortOverInlets_OperatorDto();
            Visit_OperatorDtoBase_Vars(op, dto);
        }
        
        protected override void VisitSortOverDimension(Operator op)
        {
            var dto = new SortOverDimension_OperatorDto();
            Visit_OperatorDtoBase_AggregateOverDimension(op, dto);
        }
        
        protected override void VisitSpectrum(Operator op)
        {
            base.VisitSpectrum(op);

            var dto = new Spectrum_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                StartOperatorDto = _stack.Pop(),
                EndOperatorDto = _stack.Pop(),
                FrequencyCountOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }
        
        protected override void VisitSquare(Operator op)
        {
            var dto = new Square_OperatorDto();
            Visit_OperatorDtoBase_VarFrequency(op, dto);
        }

        protected override void VisitSquash(Operator op)
        {
            var dto = new Squash_OperatorDto();
            Visit_StretchOrSquash_OperatorDto(op, dto);
        }

        protected override void VisitStretch(Operator op)
        {
            var dto = new Stretch_OperatorDto();
            Visit_StretchOrSquash_OperatorDto(op, dto);
        }

        protected override void VisitSubtract(Operator op)
        {
            var dto = new Subtract_OperatorDto();
            Visit_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitSumOverDimension(Operator op)
        {
            var dto = new SumOverDimension_OperatorDto();
            Visit_OperatorDtoBase_AggregateOverDimension(op, dto);
        }
        
        protected override void VisitSumFollower(Operator op)
        {
            var dto = new SumFollower_OperatorDto();
            Visit_OperatorDtoBase_AggregateFollower(op, dto);
        }

        protected override void VisitTimePower(Operator op)
        {
            base.VisitTimePower(op);

            var dto = new TimePower_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                ExponentOperatorDto = _stack.Pop(),
                OriginOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitTriangle(Operator op)
        {
            var dto = new Triangle_OperatorDto();
            Visit_OperatorDtoBase_VarFrequency(op, dto);
        }

        protected override void VisitToggleTrigger(Operator op)
        {
            var dto = new ToggleTrigger_OperatorDto();
            Visit_OperatorDtoBase_Trigger(op, dto);
        }

        protected override void VisitUnbundle(Operator op)
        {
            base.VisitUnbundle(op);

            var dto = new Unbundle_OperatorDto
            {
                InputOperatorDto = _stack.Pop(),
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        // Private Methods

        private void Visit_ClosestOverDimension_OperatorDto(Operator op, ClosestOverDimension_OperatorDto dto)
        {
            VisitOperatorBase(op);

            var wrapper = new ClosestOverDimension_OperatorWrapper(op);

            dto.InputOperatorDto = _stack.Pop();
            dto.CollectionOperatorDto = _stack.Pop();
            dto.FromOperatorDto = _stack.Pop();
            dto.TillOperatorDto = _stack.Pop();
            dto.StepOperatorDto = _stack.Pop();
            dto.CollectionRecalculationEnum = wrapper.CollectionRecalculation;

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Visit_ClosestOverInlets_OperatorDto(Operator op, ClosestOverInlets_OperatorDto dto)
        {
            VisitOperatorBase(op);

            dto.InputOperatorDto = _stack.Pop();

            int count = op.Inlets.Count;
            var itemOperatorDtos = new OperatorDtoBase[count];
            for (int i = 0; i < count; i++)
            {
                itemOperatorDtos[i] = _stack.Pop();
            }
            dto.ItemOperatorDtos = itemOperatorDtos;

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_AggregateFollower(Operator op, OperatorDtoBase_AggregateFollower_AllVars dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.SliceLengthOperatorDto = _stack.Pop();
            dto.SampleCountOperatorDto = _stack.Pop();

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_AggregateOverDimension(Operator op, OperatorDtoBase_AggregateOverDimension_AllVars dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.FromOperatorDto = _stack.Pop();
            dto.TillOperatorDto = _stack.Pop();
            dto.StepOperatorDto = _stack.Pop();

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth(Operator op, OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.CenterFrequencyOperatorDto = _stack.Pop();
            dto.BandWidthOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Visit_StretchOrSquash_OperatorDto(Operator op, StretchOrSquash_OperatorDto dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.FactorOperatorDto = _stack.Pop();
            dto.OriginOperatorDto = _stack.Pop();

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_ShelfFilter_AllVars(Operator op, OperatorDtoBase_ShelfFilter_AllVars dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.TransitionFrequencyOperatorDto = _stack.Pop();
            dto.TransitionSlopeOperatorDto = _stack.Pop();
            dto.DBGainOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_Trigger(Operator op, OperatorDtoBase_Trigger_VarPassThrough_VarReset dto)
        {
            VisitOperatorBase(op);

            dto.PassThroughInputOperatorDto = _stack.Pop();
            dto.ResetOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_VarA_VarB(Operator op, OperatorDtoBase_VarA_VarB dto)
        {
            VisitOperatorBase(op);

            dto.AOperatorDto = _stack.Pop();
            dto.BOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_VarFrequency(Operator op, OperatorDtoBase_VarFrequency dto)
        {
            VisitOperatorBase(op);

            dto.FrequencyOperatorDto = _stack.Pop();

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_Vars(Operator op, OperatorDtoBase_Vars dto)
        {
            VisitOperatorBase(op);

            int count = op.Inlets.Count;
            var vars = new OperatorDtoBase[count];
            for (int i = 0; i < count; i++)
            {
                vars[i] = _stack.Pop();
            }
            dto.Vars = vars;

            _stack.Push(dto);
        }

        private void Visit_OperatorDtoBase_VarX(Operator op, OperatorDtoBase_VarX dto)
        {
            VisitOperatorBase(op);

            dto.XOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void SetDimensionProperties(Operator op, IOperatorDto_WithDimension dto)
        {
            dto.StandardDimensionEnum = op.GetStandardDimensionEnum();
            dto.CustomDimensionName = op.CustomDimensionName;
        }
    }
}

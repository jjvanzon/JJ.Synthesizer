using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorEntityToDtoVisitor : OperatorEntityVisitorBase_WithInletCoalescing
    {
        private readonly CalculatorCache _calculatorCache;
        private readonly ICurveRepository _curveRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;

        private Stack<IOperatorDto> _stack;
        private Outlet _topLevelOutlet;

        /// <summary> Needed to maintain instance integrity. </summary>
        private Dictionary<Operator, VariableInput_OperatorDto> _patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary;

        public OperatorEntityToDtoVisitor(
            [NotNull] CalculatorCache calculatorCache,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] IPatchRepository patchRepository,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ISpeakerSetupRepository speakerSetupRepository)
        {
            if (calculatorCache == null) throw new ArgumentNullException(nameof(calculatorCache));
            if (curveRepository == null) throw new ArgumentNullException(nameof(curveRepository));
            if (patchRepository == null) throw new ArgumentNullException(nameof(patchRepository));
            if (sampleRepository == null) throw new ArgumentNullException(nameof(sampleRepository));
            if (speakerSetupRepository == null) throw new ArgumentNullException(nameof(speakerSetupRepository));

            _calculatorCache = calculatorCache;
            _curveRepository = curveRepository;
            _patchRepository = patchRepository;
            _sampleRepository = sampleRepository;
            _speakerSetupRepository = speakerSetupRepository;
        }

        public IOperatorDto Execute(Operator op)
        {
            if (op.Outlets.Count != 1) throw new NotEqualException(() => op.Outlets.Count, 1);

            Outlet topLevelOutlet = op.Outlets[0];

            return Execute(topLevelOutlet);
        }

        public IOperatorDto Execute(Outlet topLevelOutlet)
        {
            _topLevelOutlet = topLevelOutlet;

            _stack = new Stack<IOperatorDto>();
            _patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary = new Dictionary<Operator, VariableInput_OperatorDto>();

            VisitOutletPolymorphic(topLevelOutlet);

            IOperatorDto dto = _stack.Pop();

            return dto;
        }

        protected override void VisitOperatorPolymorphic(Operator op)
        {
            bool hasOutletVisitation = HasOutletVisitation(op);
            if (hasOutletVisitation)
            {
                base.VisitOperatorPolymorphic(op);
            }
            else
            {
                VisitorHelper.WithStackCheck(_stack, () => base.VisitOperatorPolymorphic(op));

                IOperatorDto dto = _stack.Peek();
                dto.OperatorID = op.ID;
            }
        }

        protected override void VisitOutletPolymorphic(Outlet outlet)
        {
            bool hasOutletVisitation = HasOutletVisitation(outlet);
            if (hasOutletVisitation)
            {
                VisitorHelper.WithStackCheck(_stack, () => base.VisitOutletPolymorphic(outlet));

                IOperatorDto dto = _stack.Peek();
                dto.OperatorID = outlet.Operator.ID;
            }
            else
            {
                base.VisitOutletPolymorphic(outlet);
            }
        }

        protected override void VisitAbsolute(Operator op)
        {
            var dto = new Absolute_OperatorDto();
            Process_OperatorDtoBase_VarX(op, dto);
        }

        protected override void VisitAdd(Operator op)
        {
            var dto = new Add_OperatorDto();
            Process_OperatorDtoBase_Vars(op, dto);
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
            Process_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitAverageFollower(Operator op)
        {
            var dto = new AverageFollower_OperatorDto();
            Process_OperatorDtoBase_AggregateFollower(op, dto);
        }

        protected override void VisitAverageOverDimension(Operator op)
        {
            var dto = new AverageOverDimension_OperatorDto();
            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
        }

        protected override void VisitAverageOverInlets(Operator op)
        {
            var dto = new AverageOverInlets_OperatorDto();
            Process_OperatorDtoBase_Vars(op, dto);
        }

        protected override void VisitBandPassFilterConstantPeakGain(Operator op)
        {
            var dto = new BandPassFilterConstantPeakGain_OperatorDto();
            Process_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth(op, dto);
        }

        protected override void VisitBandPassFilterConstantTransitionGain(Operator op)
        {
            var dto = new BandPassFilterConstantTransitionGain_OperatorDto();
            Process_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth(op, dto);
        }

        protected override void VisitChangeTrigger(Operator op)
        {
            var dto = new ChangeTrigger_OperatorDto();
            Process_OperatorDtoBase_Trigger(op, dto);
        }

        protected override void VisitClosestOverInlets(Operator op)
        {
            var dto = new ClosestOverInlets_OperatorDto();
            Process_ClosestOverInlets_OperatorDto(op, dto);
        }

        protected override void VisitClosestOverInletsExp(Operator op)
        {
            var dto = new ClosestOverInletsExp_OperatorDto();
            Process_ClosestOverInlets_OperatorDto(op, dto);
        }

        protected override void VisitClosestOverDimension(Operator op)
        {
            var dto = new ClosestOverDimension_OperatorDto();
            Process_ClosestOverDimension_OperatorDto(op, dto);
        }

        protected override void VisitClosestOverDimensionExp(Operator op)
        {
            var dto = new ClosestOverDimensionExp_OperatorDto();
            Process_ClosestOverDimension_OperatorDto(op, dto);
        }

        protected override void VisitCache(Operator op)
        {
            base.VisitCache(op);

            var wrapper = new Cache_OperatorWrapper(op);

            var dto = new Cache_OperatorDto
            {
                OperatorID = op.ID,
                SignalOperatorDto = _stack.Pop(),
                StartOperatorDto = _stack.Pop(),
                EndOperatorDto = _stack.Pop(),
                SamplingRateOperatorDto = _stack.Pop(),
                InterpolationTypeEnum = wrapper.InterpolationType,
                SpeakerSetupEnum = wrapper.SpeakerSetup,
                ChannelCount = wrapper.GetChannelCount(_speakerSetupRepository)
            };

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
                dto.ArrayDto = _calculatorCache.GetCurveArrayDto(curve.ID, _curveRepository);
                dto.MinX = curve.Nodes
                                .OrderBy(x => x.X)
                                .First()
                                .X;
            }

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitDimensionToOutletsOutlet(Outlet outlet)
        {
            base.VisitDimensionToOutletsOutlet(outlet);

            var dto = new DimensionToOutlets_Outlet_OperatorDto
            {
                OperandOperatorDto = _stack.Pop(),
                OutletListIndex = outlet.ListIndex
            };

            SetDimensionProperties(outlet.Operator, dto);

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
            Process_OperatorDtoBase_VarA_VarB(op, dto);
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
            Process_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitGreaterThanOrEqual(Operator op)
        {
            var dto = new GreaterThanOrEqual_OperatorDto();
            Process_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitHold(Operator op)
        {
            base.VisitHold(op);

            var dto = new Hold_OperatorDto_VarSignal { SignalOperatorDto = _stack.Pop() };

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
            Process_OperatorDtoBase_ShelfFilter_AllVars(op, dto);
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
            var wrapper = new InletsToDimension_OperatorWrapper(op);

            var dto = new InletsToDimension_OperatorDto
            {
                ResampleInterpolationTypeEnum = wrapper.InterpolationType
            };

            Process_OperatorDtoBase_Vars(op, dto);

            SetDimensionProperties(op, dto);
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

        protected override void VisitLessThan(Operator op)
        {
            var dto = new LessThan_OperatorDto();
            Process_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitLessThanOrEqual(Operator op)
        {
            var dto = new LessThanOrEqual_OperatorDto();
            Process_OperatorDtoBase_VarA_VarB(op, dto);
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
            Process_OperatorDtoBase_ShelfFilter_AllVars(op, dto);
        }

        protected override void VisitMaxOverDimension(Operator op)
        {
            var dto = new MaxOverDimension_OperatorDto();
            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
        }

        protected override void VisitMaxOverInlets(Operator op)
        {
            var dto = new MaxOverInlets_OperatorDto();
            Process_OperatorDtoBase_Vars(op, dto);
        }

        protected override void VisitMaxFollower(Operator op)
        {
            var dto = new MaxFollower_OperatorDto();
            Process_OperatorDtoBase_AggregateFollower(op, dto);
        }

        protected override void VisitMinOverDimension(Operator op)
        {
            var dto = new MinOverDimension_OperatorDto();
            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
        }

        protected override void VisitMinOverInlets(Operator op)
        {
            var dto = new MinOverInlets_OperatorDto();
            Process_OperatorDtoBase_Vars(op, dto);
        }

        protected override void VisitMinFollower(Operator op)
        {
            var dto = new MinFollower_OperatorDto();
            Process_OperatorDtoBase_AggregateFollower(op, dto);
        }

        protected override void VisitMultiply(Operator op)
        {
            var dto = new Multiply_OperatorDto();
            Process_OperatorDtoBase_Vars(op, dto);
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
            Process_OperatorDtoBase_VarX(op, dto);
        }

        protected override void VisitNoise(Operator op)
        {
            base.VisitNoise(op);

            var dto = new Noise_OperatorDto
            {
                OperatorID = op.ID,
                ArrayDto = _calculatorCache.GetNoiseArrayDto()
            };

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitNot(Operator op)
        {
            var dto = new Not_OperatorDto();
            Process_OperatorDtoBase_VarX(op, dto);
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
            Process_OperatorDtoBase_VarA_VarB(op, dto);
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
            Process_OperatorDtoBase_VarX(op, dto);
        }

        protected override void VisitOr(Operator op)
        {
            var dto = new Or_OperatorDto();
            Process_OperatorDtoBase_VarA_VarB(op, dto);
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

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        protected override void VisitPulseTrigger(Operator op)
        {
            var dto = new PulseTrigger_OperatorDto();
            Process_OperatorDtoBase_Trigger(op, dto);
        }

        protected override void VisitRandom(Operator op)
        {
            base.VisitRandom(op);

            var wrapper = new Random_OperatorWrapper(op);

            var dto = new Random_OperatorDto
            {
                OperatorID = op.ID,
                RateOperatorDto = _stack.Pop(),
                ResampleInterpolationTypeEnum = wrapper.InterpolationType,
                ArrayDto = _calculatorCache.GetRandomArrayDto(wrapper.InterpolationType)
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

        protected override void VisitRangeOverOutletsOutlet(Outlet outlet)
        {
            base.VisitRangeOverOutletsOutlet(outlet);

            var dto = new RangeOverOutlets_Outlet_OperatorDto
            {
                FromOperatorDto = _stack.Pop(),
                StepOperatorDto = _stack.Pop(),
                OutletListIndex = outlet.ListIndex
            };

            _stack.Push(dto);
        }

        protected override void VisitReset(Operator op)
        {
            base.VisitReset(op);

            var wrapper = new Reset_OperatorWrapper(op);

            var dto = new Reset_OperatorDto
            {
                PassThroughInputOperatorDto = _stack.Pop(),
                Name = op.Name,
                ListIndex = wrapper.ListIndex
            };

            _stack.Push(dto);
        }

        protected override void VisitReverse(Operator op)
        {
            base.VisitReverse(op);

            var dto = new Reverse_OperatorDto
            {
                SignalOperatorDto = _stack.Pop(),
                FactorOperatorDto = _stack.Pop(),
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
            Process_OperatorDtoBase_VarFrequency(op, dto);

            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);
            Sample sample = wrapper.Sample;

            // ReSharper disable once InvertIf
            if (sample != null)
            {
                dto.SampleID = sample.ID;
                dto.InterpolationTypeEnum = sample.GetInterpolationTypeEnum();
                dto.SampleChannelCount = sample.GetChannelCount();
                dto.ArrayDtos = _calculatorCache.GetSampleArrayDtos(sample.ID, _sampleRepository);
            }
        }

        protected override void VisitSawDown(Operator op)
        {
            var dto = new SawDown_OperatorDto();
            Process_OperatorDtoBase_VarFrequency(op, dto);
        }

        protected override void VisitSawUp(Operator op)
        {
            var dto = new SawUp_OperatorDto();
            Process_OperatorDtoBase_VarFrequency(op, dto);
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
            Process_OperatorDtoBase_VarFrequency(op, dto);
        }

        protected override void VisitSortOverInletsOutlet(Outlet outlet)
        {
            var dto = new SortOverInlets_Outlet_OperatorDto
            {
                OutletListIndex = outlet.ListIndex,
            };

            Process_OperatorDtoBase_Vars(outlet.Operator, dto);

            SetDimensionProperties(outlet.Operator, dto);
        }

        protected override void VisitSortOverDimension(Operator op)
        {
            var dto = new SortOverDimension_OperatorDto();
            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
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
            Process_OperatorDtoBase_VarFrequency(op, dto);
        }

        protected override void VisitSquash(Operator op)
        {
            var dto = new Squash_OperatorDto();
            Process_StretchOrSquash_OperatorDto(op, dto);
        }

        protected override void VisitStretch(Operator op)
        {
            var dto = new Stretch_OperatorDto();
            Process_StretchOrSquash_OperatorDto(op, dto);
        }

        protected override void VisitSubtract(Operator op)
        {
            var dto = new Subtract_OperatorDto();
            Process_OperatorDtoBase_VarA_VarB(op, dto);
        }

        protected override void VisitSumOverDimension(Operator op)
        {
            var dto = new SumOverDimension_OperatorDto();
            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
        }

        protected override void VisitSumFollower(Operator op)
        {
            var dto = new SumFollower_OperatorDto();
            Process_OperatorDtoBase_AggregateFollower(op, dto);
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
            Process_OperatorDtoBase_VarFrequency(op, dto);
        }

        protected override void VisitToggleTrigger(Operator op)
        {
            var dto = new ToggleTrigger_OperatorDto();
            Process_OperatorDtoBase_Trigger(op, dto);
        }

        // Private Methods

        private void Process_ClosestOverDimension_OperatorDto(Operator op, ClosestOverDimension_OperatorDto dto)
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

        private void Process_ClosestOverInlets_OperatorDto(Operator op, ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
        {
            VisitOperatorBase(op);

            dto.InputOperatorDto = _stack.Pop();
            dto.ItemOperatorDtos = op.Inlets.Skip(1).Select(x => _stack.Pop()).ToArray();

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_AggregateFollower(Operator op, OperatorDtoBase_AggregateFollower_AllVars dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.SliceLengthOperatorDto = _stack.Pop();
            dto.SampleCountOperatorDto = _stack.Pop();

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_AggregateOverDimension(Operator op, OperatorDtoBase_AggregateOverDimension_AllVars dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.FromOperatorDto = _stack.Pop();
            dto.TillOperatorDto = _stack.Pop();
            dto.StepOperatorDto = _stack.Pop();

            var wrapper = new OperatorWrapperBase_AggregateOverDimension(op);
            dto.CollectionRecalculationEnum = wrapper.CollectionRecalculation;

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth(
            Operator op,
            OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.CenterFrequencyOperatorDto = _stack.Pop();
            dto.BandWidthOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Process_StretchOrSquash_OperatorDto(Operator op, StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.FactorOperatorDto = _stack.Pop();
            dto.OriginOperatorDto = _stack.Pop();

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_ShelfFilter_AllVars(Operator op, OperatorDtoBase_ShelfFilter_AllVars dto)
        {
            VisitOperatorBase(op);

            dto.SignalOperatorDto = _stack.Pop();
            dto.TransitionFrequencyOperatorDto = _stack.Pop();
            dto.TransitionSlopeOperatorDto = _stack.Pop();
            dto.DBGainOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_Trigger(Operator op, OperatorDtoBase_Trigger_VarPassThrough_VarReset dto)
        {
            VisitOperatorBase(op);

            dto.PassThroughInputOperatorDto = _stack.Pop();
            dto.ResetOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_VarA_VarB(Operator op, OperatorDtoBase_VarA_VarB dto)
        {
            VisitOperatorBase(op);

            dto.AOperatorDto = _stack.Pop();
            dto.BOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_VarFrequency(Operator op, OperatorDtoBase_VarFrequency dto)
        {
            VisitOperatorBase(op);

            dto.FrequencyOperatorDto = _stack.Pop();

            SetDimensionProperties(op, dto);

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_Vars(Operator op, OperatorDtoBase_Vars dto)
        {
            VisitOperatorBase(op);

            dto.Vars = op.Inlets.Where(x => x.InputOutlet != null).Select(x => _stack.Pop()).ToArray();

            _stack.Push(dto);
        }

        private void Process_OperatorDtoBase_VarX(Operator op, OperatorDtoBase_VarX dto)
        {
            VisitOperatorBase(op);

            dto.XOperatorDto = _stack.Pop();

            _stack.Push(dto);
        }

        private void SetDimensionProperties(Operator op, IOperatorDto_WithDimension dto)
        {
            dto.StandardDimensionEnum = op.GetStandardDimensionEnum();
            dto.CanonicalCustomDimensionName = NameHelper.ToCanonical(op.CustomDimensionName);
        }

        // Special Visitation

        protected override void InsertNumber(double number)
        {
            _stack.Push(new Number_OperatorDto { Number = number });
        }

        /// <summary> As soon as you encounter a CustomOperator's Outlet, the evaluation has to take a completely different course. </summary>
        protected override void VisitCustomOperatorOutlet(Outlet customOperatorOutlet)
        {
            // NOTE: Do not try to separate this concept into a different class.
            // It has been tried and resulted in something much more complicated than these two lines of code.
            // Most magic is in the InletOutletMatcher, which is a difficult class, fully worked out and tested,
            // that needs to tap into the entity model, not DTO model.

            // Resolve the underlying patch's outlet
            Outlet patchOutlet_Outlet = InletOutletMatcher.ApplyCustomOperatorToUnderlyingPatch(customOperatorOutlet, _patchRepository);

            // Visit the underlying patch's outlet.
            VisitOperatorPolymorphic(patchOutlet_Outlet.Operator);
        }

        /// <summary>
        /// Top-level PatchInlets need to be converted to VariableInput_OperatorDto's
        /// and instance integrity needs to maintained over them.
        /// </summary>
        protected override void VisitPatchInlet(Operator op)
        {
            base.VisitPatchInlet(op);

            IOperatorDto inputDto = _stack.Pop();

            bool isTopLevelPatchInlet = IsTopLevelPatchInlet(op);
            if (isTopLevelPatchInlet)
            {
                VariableInput_OperatorDto dto;
                if (!_patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary.TryGetValue(op, out dto))
                {
                    var wrapper = new PatchInlet_OperatorWrapper(op);

                    dto = new VariableInput_OperatorDto
                    {
                        DimensionEnum = wrapper.DimensionEnum,
                        CanonicalName = NameHelper.ToCanonical(wrapper.Name),
                        ListIndex = wrapper.ListIndex ?? 0,
                        DefaultValue = wrapper.DefaultValue ?? 0.0
                    };

                    _patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary[op] = dto;
                }

                _stack.Push(dto);
            }
            else
            {
                _stack.Push(inputDto);
            }
        }

        private bool IsTopLevelPatchInlet(Operator op)
        {
            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.PatchInlet)
            {
                return false;
            }

            return op.Patch.ID == _topLevelOutlet.Operator.Patch.ID;
        }
    }
}
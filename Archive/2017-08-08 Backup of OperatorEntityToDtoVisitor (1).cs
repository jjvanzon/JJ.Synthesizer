//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation;
//using JJ.Business.Synthesizer.CopiedCode.FromFramework;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Framework.Exceptions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.RepositoryInterfaces;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorEntityToDtoVisitor : OperatorEntityVisitorBase_WithInletCoalescing
//    {
//        private readonly CalculatorCache _calculatorCache;
//        private readonly ICurveRepository _curveRepository;
//        private readonly ISampleRepository _sampleRepository;
//        private readonly ISpeakerSetupRepository _speakerSetupRepository;

//        private Stack<IOperatorDto> _stack;
//        private Outlet _topLevelOutlet;

//        /// <summary> Needed to maintain instance integrity. </summary>
//        private Dictionary<Operator, VariableInput_OperatorDto> _patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary;

//        public OperatorEntityToDtoVisitor(
//            CalculatorCache calculatorCache,
//            ICurveRepository curveRepository,
//            ISampleRepository sampleRepository,
//            ISpeakerSetupRepository speakerSetupRepository)
//        {
//            _calculatorCache = calculatorCache ?? throw new ArgumentNullException(nameof(calculatorCache));
//            _curveRepository = curveRepository ?? throw new ArgumentNullException(nameof(curveRepository));
//            _sampleRepository = sampleRepository ?? throw new ArgumentNullException(nameof(sampleRepository));
//            _speakerSetupRepository = speakerSetupRepository ?? throw new ArgumentNullException(nameof(speakerSetupRepository));
//        }

//        public IOperatorDto Execute(Operator op)
//        {
//            if (op.Outlets.Count != 1) throw new NotEqualException(() => op.Outlets.Count, 1);

//            Outlet topLevelOutlet = op.Outlets[0];

//            return Execute(topLevelOutlet);
//        }

//        public IOperatorDto Execute(Outlet topLevelOutlet)
//        {
//            _topLevelOutlet = topLevelOutlet;

//            _stack = new Stack<IOperatorDto>();
//            _patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary = new Dictionary<Operator, VariableInput_OperatorDto>();

//            VisitOutletPolymorphic(topLevelOutlet);

//            IOperatorDto dto = _stack.Pop();

//            return dto;
//        }

//        protected override void VisitOperatorPolymorphic(Operator op)
//        {
//            bool hasOutletVisitation = HasOutletVisitation(op);
//            if (hasOutletVisitation)
//            {
//                base.VisitOperatorPolymorphic(op);
//            }
//            else
//            {
//                VisitorHelper.WithStackCheck(_stack, () => base.VisitOperatorPolymorphic(op));

//                IOperatorDto dto = _stack.Peek();
//                dto.OperatorID = op.ID;
//            }
//        }

//        protected override void VisitOutletPolymorphic(Outlet outlet)
//        {
//            bool hasOutletVisitation = HasOutletVisitation(outlet);
//            if (hasOutletVisitation)
//            {
//                VisitorHelper.WithStackCheck(_stack, () => base.VisitOutletPolymorphic(outlet));

//                IOperatorDto dto = _stack.Peek();
//                dto.OperatorID = outlet.Operator.ID;
//            }
//            else
//            {
//                base.VisitOutletPolymorphic(outlet);
//            }
//        }

//        protected override void VisitAbsolute(Operator op)
//        {
//            var dto = new Absolute_OperatorDto();
//            Process_OperatorDtoBase_WithNumber(op, dto);
//        }

//        protected override void VisitAdd(Operator op)
//        {
//            var dto = new Add_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitAllPassFilter(Operator op)
//        {
//            base.VisitAllPassFilter(op);

//            var dto = new AllPassFilter_OperatorDto
//            {
//                Sound = PopInput(),
//                CenterFrequency = PopInput(),
//                Width = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        private InputDto PopInput() => InputDtoFactory.CreateInputDto(_stack.Pop());

//        private void PopInputs(IOperatorDto dto)
//        {
//            int count = dto.Inputs.Count();

//            dto.Inputs = CollectionHelper.Repeat(count, () => PopInput()).ToArray(); ;
//        }

//        protected override void VisitAnd(Operator op)
//        {
//            var dto = new And_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitAverageFollower(Operator op)
//        {
//            var dto = new AverageFollower_OperatorDto();
//            Process_OperatorDtoBase_AggregateFollower(op, dto);
//        }

//        protected override void VisitAverageOverDimension(Operator op)
//        {
//            var dto = new AverageOverDimension_OperatorDto();
//            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
//        }

//        protected override void VisitAverageOverInlets(Operator op)
//        {
//            var dto = new AverageOverInlets_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitBandPassFilterConstantPeakGain(Operator op)
//        {
//            var dto = new BandPassFilterConstantPeakGain_OperatorDto();
//            Process_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth(op, dto);
//        }

//        protected override void VisitBandPassFilterConstantTransitionGain(Operator op)
//        {
//            var dto = new BandPassFilterConstantTransitionGain_OperatorDto();
//            Process_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth(op, dto);
//        }

//        protected override void VisitChangeTrigger(Operator op)
//        {
//            var dto = new ChangeTrigger_OperatorDto();
//            Process_OperatorDtoBase_Trigger(op, dto);
//        }

//        protected override void VisitClosestOverInlets(Operator op)
//        {
//            var dto = new ClosestOverInlets_OperatorDto();
//            Process_ClosestOverInlets_OperatorDto(op, dto);
//        }

//        protected override void VisitClosestOverInletsExp(Operator op)
//        {
//            var dto = new ClosestOverInletsExp_OperatorDto();
//            Process_ClosestOverInlets_OperatorDto(op, dto);
//        }

//        protected override void VisitClosestOverDimension(Operator op)
//        {
//            var dto = new ClosestOverDimension_OperatorDto();
//            Process_ClosestOverDimension_OperatorDto(op, dto);
//        }

//        protected override void VisitClosestOverDimensionExp(Operator op)
//        {
//            var dto = new ClosestOverDimensionExp_OperatorDto();
//            Process_ClosestOverDimension_OperatorDto(op, dto);
//        }

//        protected override void VisitCache(Operator op)
//        {
//            base.VisitCache(op);

//            var wrapper = new Cache_OperatorWrapper(op);

//            var dto = new Cache_OperatorDto
//            {
//                OperatorID = op.ID,
//                Signal = PopInput(),
//                Start = PopInput(),
//                End = PopInput(),
//                SamplingRate = PopInput(),
//                InterpolationTypeEnum = wrapper.InterpolationType,
//                SpeakerSetupEnum = wrapper.SpeakerSetup,
//                ChannelCount = wrapper.GetChannelCount(_speakerSetupRepository)
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitCurveOperator(Operator op)
//        {
//            base.VisitCurveOperator(op);

//            var wrapper = new Curve_OperatorWrapper(op, _curveRepository);

//            var dto = new Curve_OperatorDto();

//            Curve curve = wrapper.Curve;

//            if (curve != null)
//            {
//                dto.CurveID = curve.ID;
//                dto.ArrayDto = _calculatorCache.GetCurveArrayDto(curve.ID, _curveRepository);
//                dto.MinX = curve.Nodes
//                                .OrderBy(x => x.X)
//                                .First()
//                                .X;
//            }

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitDimensionToOutletsOutlet(Outlet outlet)
//        {
//            base.VisitDimensionToOutletsOutlet(outlet);

//            if (!outlet.RepetitionPosition.HasValue) throw new NullException(() => outlet.RepetitionPosition);
//            int position = outlet.RepetitionPosition.Value;

//            var dto = new DimensionToOutlets_Outlet_OperatorDto
//            {
//                Signal = PopInput(),
//                OutletPosition = position
//            };

//            SetDimensionProperties(outlet.Operator, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitDivide(Operator op)
//        {
//            base.VisitDivide(op);

//            var dto = new Divide_OperatorDto
//            {
//                A = PopInput(),
//                B = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitEqual(Operator op)
//        {
//            var dto = new Equal_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitGetDimension(Operator op)
//        {
//            base.VisitGetDimension(op);

//            var dto = new GetDimension_OperatorDto();
//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitGreaterThan(Operator op)
//        {
//            var dto = new GreaterThan_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitGreaterThanOrEqual(Operator op)
//        {
//            var dto = new GreaterThanOrEqual_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitHold(Operator op)
//        {
//            base.VisitHold(op);

//            var dto = new Hold_OperatorDto_VarSignal
//            {
//                Signal = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitHighPassFilter(Operator op)
//        {
//            base.VisitHighPassFilter(op);

//            var dto = new HighPassFilter_OperatorDto
//            {
//                Sound = PopInput(),
//                MinFrequency = PopInput(),
//                BlobVolume = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitHighShelfFilter(Operator op)
//        {
//            var dto = new HighShelfFilter_OperatorDto();
//            Process_OperatorDtoBase_ShelfFilter_AllVars(op, dto);
//        }

//        protected override void VisitIf(Operator op)
//        {
//            base.VisitIf(op);

//            var dto = new If_OperatorDto
//            {
//                Condition = PopInput(),
//                Then = PopInput(),
//                Else = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitInletsToDimension(Operator op)
//        {
//            var wrapper = new InletsToDimension_OperatorWrapper(op);

//            var dto = new InletsToDimension_OperatorDto
//            {
//                ResampleInterpolationTypeEnum = wrapper.InterpolationType
//            };

//            Process_OperatorDtoBase_Vars(op, dto);

//            SetDimensionProperties(op, dto);
//        }

//        protected override void VisitInterpolate(Operator op)
//        {
//            base.VisitInterpolate(op);

//            var wrapper = new Interpolate_OperatorWrapper(op);

//            var dto = new Interpolate_OperatorDto
//            {
//                Signal = PopInput(),
//                SamplingRate = PopInput(),
//                ResampleInterpolationTypeEnum = wrapper.InterpolationType
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitLessThan(Operator op)
//        {
//            var dto = new LessThan_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitLessThanOrEqual(Operator op)
//        {
//            var dto = new LessThanOrEqual_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitLoop(Operator op)
//        {
//            base.VisitLoop(op);

//            var dto = new Loop_OperatorDto
//            {
//                Signal = PopInput(),
//                Skip = PopInput(),
//                LoopStartMarker = PopInput(),
//                LoopEndMarker = PopInput(),
//                ReleaseEndMarker = PopInput(),
//                NoteDuration = PopInput(),
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitLowPassFilter(Operator op)
//        {
//            base.VisitLowPassFilter(op);

//            var dto = new LowPassFilter_OperatorDto
//            {
//                Sound = PopInput(),
//                MaxFrequency = PopInput(),
//                BlobVolume = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitLowShelfFilter(Operator op)
//        {
//            var dto = new LowShelfFilter_OperatorDto();
//            Process_OperatorDtoBase_ShelfFilter_AllVars(op, dto);
//        }
        
//        protected override void VisitMaxOverDimension(Operator op)
//        {
//            var dto = new MaxOverDimension_OperatorDto();
//            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
//        }

//        protected override void VisitMaxOverInlets(Operator op)
//        {
//            var dto = new MaxOverInlets_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitMaxFollower(Operator op)
//        {
//            var dto = new MaxFollower_OperatorDto();
//            Process_OperatorDtoBase_AggregateFollower(op, dto);
//        }

//        protected override void VisitMinOverDimension(Operator op)
//        {
//            var dto = new MinOverDimension_OperatorDto();
//            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
//        }

//        protected override void VisitMinOverInlets(Operator op)
//        {
//            var dto = new MinOverInlets_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitMinFollower(Operator op)
//        {
//            var dto = new MinFollower_OperatorDto();
//            Process_OperatorDtoBase_AggregateFollower(op, dto);
//        }

//        protected override void VisitMultiply(Operator op)
//        {
//            var dto = new Multiply_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitNegative(Operator op)
//        {
//            var dto = new Negative_OperatorDto();
//            Process_OperatorDtoBase_VarNumber(op, dto);
//        }

//        protected override void VisitNoise(Operator op)
//        {
//            base.VisitNoise(op);

//            var dto = new Noise_OperatorDto
//            {
//                OperatorID = op.ID,
//                ArrayDto = _calculatorCache.GetNoiseArrayDto()
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitNot(Operator op)
//        {
//            var dto = new Not_OperatorDto();
//            Process_OperatorDtoBase_VarNumber(op, dto);
//        }

//        protected override void VisitNotchFilter(Operator op)
//        {
//            base.VisitNotchFilter(op);

//            var dto = new NotchFilter_OperatorDto
//            {
//                Sound = PopInput(),
//                CenterFrequency = PopInput(),
//                Width = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitNotEqual(Operator op)
//        {
//            var dto = new NotEqual_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitNumber(Operator op)
//        {
//            base.VisitNumber(op);

//            var wrapper = new Number_OperatorWrapper(op);

//            var dto = new Number_OperatorDto
//            {
//                Number = wrapper.Number
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitOr(Operator op)
//        {
//            var dto = new Or_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitPeakingEQFilter(Operator op)
//        {
//            base.VisitPeakingEQFilter(op);

//            var dto = new PeakingEQFilter_OperatorDto
//            {
//                Sound = PopInput(),
//                CenterFrequency = PopInput(),
//                Width = PopInput(),
//                DBGain = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitPower(Operator op)
//        {
//            base.VisitPower(op);

//            var dto = new Power_OperatorDto
//            {
//                Base = PopInput(),
//                Exponent = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitPulse(Operator op)
//        {
//            base.VisitPulse(op);

//            var dto = new Pulse_OperatorDto
//            {
//                Frequency = PopInput(),
//                Width = PopInput()
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitPulseTrigger(Operator op)
//        {
//            var dto = new PulseTrigger_OperatorDto();
//            Process_OperatorDtoBase_Trigger(op, dto);
//        }

//        protected override void VisitRandom(Operator op)
//        {
//            base.VisitRandom(op);

//            var wrapper = new Random_OperatorWrapper(op);

//            var dto = new Random_OperatorDto
//            {
//                OperatorID = op.ID,
//                Rate = PopInput(),
//                ResampleInterpolationTypeEnum = wrapper.InterpolationType,
//                ArrayDto = _calculatorCache.GetRandomArrayDto(wrapper.InterpolationType)
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitRangeOverDimension(Operator op)
//        {
//            base.VisitRangeOverDimension(op);

//            var dto = new RangeOverDimension_OperatorDto
//            {
//                From = PopInput(),
//                Till = PopInput(),
//                Step = PopInput()
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitRangeOverOutletsOutlet(Outlet outlet)
//        {
//            base.VisitRangeOverOutletsOutlet(outlet);

//            var dto = new RangeOverOutlets_Outlet_OperatorDto
//            {
//                From = PopInput(),
//                Step = PopInput(),
//                OutletPosition = outlet.Position
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitReset(Operator op)
//        {
//            base.VisitReset(op);

//            var wrapper = new Reset_OperatorWrapper(op);

//            var dto = new Reset_OperatorDto
//            {
//                PassThroughInput = PopInput(),
//                Name = op.Name,
//                Position = wrapper.Position
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitReverse(Operator op)
//        {
//            base.VisitReverse(op);

//            var dto = new Reverse_OperatorDto
//            {
//                Signal = PopInput(),
//                Factor = PopInput(),
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitRound(Operator op)
//        {
//            base.VisitRound(op);

//            var dto = new Round_OperatorDto
//            {
//                Signal = PopInput(),
//                Step = PopInput(),
//                Offset = PopInput()
//            };

//            _stack.Push(dto);
//        }

//        protected override void VisitSampleOperator(Operator op)
//        {
//            var dto = new Sample_OperatorDto();
//            Process_OperatorDtoBase_VarFrequency(op, dto);

//            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);
//            Sample sample = wrapper.Sample;

//            // ReSharper disable once InvertIf
//            if (sample != null)
//            {
//                dto.SampleID = sample.ID;
//                dto.InterpolationTypeEnum = sample.GetInterpolationTypeEnum();
//                dto.SampleChannelCount = sample.GetChannelCount();
//                dto.ArrayDtos = _calculatorCache.GetSampleArrayDtos(sample.ID, _sampleRepository);
//            }
//        }

//        protected override void VisitSawDown(Operator op)
//        {
//            var dto = new SawDown_OperatorDto();
//            Process_OperatorDtoBase_VarFrequency(op, dto);
//        }

//        protected override void VisitSawUp(Operator op)
//        {
//            var dto = new SawUp_OperatorDto();
//            Process_OperatorDtoBase_VarFrequency(op, dto);
//        }

//        protected override void VisitSetDimension(Operator op)
//        {
//            base.VisitSetDimension(op);

//            var dto = new SetDimension_OperatorDto
//            {
//                PassThrough = PopInput(),
//                Number = PopInput()
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitSine(Operator op)
//        {
//            var dto = new Sine_OperatorDto();
//            Process_OperatorDtoBase_VarFrequency(op, dto);
//        }

//        protected override void VisitSortOverInletsOutlet(Outlet outlet)
//        {
//            var dto = new SortOverInlets_Outlet_OperatorDto
//            {
//                OutletPosition = outlet.Position,
//            };

//            Process_OperatorDtoBase_Vars(outlet.Operator, dto);

//            SetDimensionProperties(outlet.Operator, dto);
//        }

//        protected override void VisitSortOverDimension(Operator op)
//        {
//            var dto = new SortOverDimension_OperatorDto();
//            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
//        }

//        protected override void VisitSpectrum(Operator op)
//        {
//            base.VisitSpectrum(op);

//            var dto = new Spectrum_OperatorDto
//            {
//                Sound = PopInput(),
//                Start = PopInput(),
//                End = PopInput(),
//                FrequencyCount = PopInput()
//            };

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        protected override void VisitSquare(Operator op)
//        {
//            var dto = new Square_OperatorDto();
//            Process_OperatorDtoBase_VarFrequency(op, dto);
//        }

//        protected override void VisitSquash(Operator op)
//        {
//            var dto = new Squash_OperatorDto();
//            Process_StretchOrSquash_OperatorDto(op, dto);
//        }

//        protected override void VisitStretch(Operator op)
//        {
//            var dto = new Stretch_OperatorDto();
//            Process_StretchOrSquash_OperatorDto(op, dto);
//        }

//        protected override void VisitSubtract(Operator op)
//        {
//            var dto = new Subtract_OperatorDto();
//            Process_OperatorDtoBase_VarA_VarB(op, dto);
//        }

//        protected override void VisitSumOverDimension(Operator op)
//        {
//            var dto = new SumOverDimension_OperatorDto();
//            Process_OperatorDtoBase_AggregateOverDimension(op, dto);
//        }

//        protected override void VisitSumFollower(Operator op)
//        {
//            var dto = new SumFollower_OperatorDto();
//            Process_OperatorDtoBase_AggregateFollower(op, dto);
//        }

//        protected override void VisitTriangle(Operator op)
//        {
//            var dto = new Triangle_OperatorDto();
//            Process_OperatorDtoBase_VarFrequency(op, dto);
//        }

//        protected override void VisitToggleTrigger(Operator op)
//        {
//            var dto = new ToggleTrigger_OperatorDto();
//            Process_OperatorDtoBase_Trigger(op, dto);
//        }

//        // Private Methods

//        private void Process_ClosestOverDimension_OperatorDto(Operator op, ClosestOverDimension_OperatorDto dto)
//        {
//            VisitOperatorBase(op);

//            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);

//            dto.Input = PopInput();
//            dto.Collection = PopInput();
//            dto.From = PopInput();
//            dto.Till = PopInput();
//            dto.Step = PopInput();
//            dto.CollectionRecalculationEnum = wrapper.CollectionRecalculation;

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        private void Process_ClosestOverInlets_OperatorDto(Operator op, ClosestOverInlets_OperatorDto dto)
//        {
//            VisitOperatorBase(op);

//            dto.Input = PopInput();
//            dto.Items = op.Inlets
//                          .Skip(1)
//                          .Select(x => _stack.Pop())
//                          .Select(x => InputDtoFactory.CreateInputDto(x))
//                          .ToArray();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_AggregateFollower(Operator op, OperatorDtoBase_AggregateFollower_AllVars dto)
//        {
//            VisitOperatorBase(op);

//            dto.Signal = PopInput();
//            dto.SliceLength = PopInput();
//            dto.SampleCount = PopInput();

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_AggregateOverDimension(Operator op, OperatorDtoBase_AggregateOverDimension dto)
//        {
//            VisitOperatorBase(op);

//            dto.Signal = PopInput();
//            dto.From = PopInput();
//            dto.Till = PopInput();
//            dto.Step = PopInput();

//            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
//            dto.CollectionRecalculationEnum = wrapper.CollectionRecalculation;

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth(
//            Operator op,
//            OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth dto)
//        {
//            VisitOperatorBase(op);

//            dto.Sound = PopInput();
//            dto.CenterFrequency = PopInput();
//            dto.Width = PopInput();

//            _stack.Push(dto);
//        }

//        private void Process_StretchOrSquash_OperatorDto(Operator op, StretchOrSquash_OperatorDtoBase_WithOrigin dto)
//        {
//            VisitOperatorBase(op);

//            dto.Signal = PopInput();
//            dto.Factor = PopInput();
//            dto.Origin = PopInput();

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_ShelfFilter_AllVars(Operator op, OperatorDtoBase_ShelfFilter_AllVars dto)
//        {
//            VisitOperatorBase(op);

//            dto.Sound = PopInput();
//            dto.TransitionFrequency = PopInput();
//            dto.TransitionSlope = PopInput();
//            dto.DBGain = PopInput();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_Trigger(Operator op, OperatorDtoBase_Trigger dto)
//        {
//            VisitOperatorBase(op);

//            dto.PassThroughInput = PopInput();
//            dto.Reset = PopInput();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_VarA_VarB(Operator op, OperatorDtoBase_WithAAndB dto)
//        {
//            VisitOperatorBase(op);

//            dto.A = PopInput();
//            dto.B = PopInput();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_VarFrequency(Operator op, OperatorDtoBase_WithFrequency dto)
//        {
//            VisitOperatorBase(op);

//            dto.Frequency = PopInput();

//            SetDimensionProperties(op, dto);

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_Vars(Operator op, OperatorDtoBase_Vars dto)
//        {
//            VisitOperatorBase(op);

//            dto.Vars = op.Inlets
//                         .Where(x => x.InputOutlet != null)
//                         .Select(x => _stack.Pop())
//                         .Select(x => InputDtoFactory.CreateInputDto(x))
//                         .ToArray();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_WithNumber(Operator op, OperatorDtoBase_WithNumber dto)
//        {
//            VisitOperatorBase(op);

//            dto.Number = PopInput();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_VarNumber(Operator op, OperatorDtoBase_WithNumber dto)
//        {
//            VisitOperatorBase(op);

//            dto.Number = PopInput();

//            _stack.Push(dto);
//        }

//        private void SetDimensionProperties(Operator op, IOperatorDto_WithDimension dto)
//        {
//            dto.StandardDimensionEnum = op.GetStandardDimensionEnumWithFallback();
//            dto.CanonicalCustomDimensionName = NameHelper.ToCanonical(op.GetCustomDimensionNameWithFallback());
//        }

//        // Special Visitation

//        protected override void InsertNumber(double number)
//        {
//            _stack.Push(new Number_OperatorDto { Number = number });
//        }

//        /// <summary> As soon as you encounter a CustomOperator's Outlet, the evaluation has to take a completely different course. </summary>
//        protected override void VisitCustomOperatorOutlet(Outlet customOperatorOutlet)
//        {
//            // NOTE: Do not try to separate this concept into a different visitor class.
//            // It has been tried and resulted in something much more complicated than these two lines of code.
//            // Most magic is in the InletOutletMatcher, which is a difficult class, fully worked out and tested,
//            // that needs to tap into the entity model, not DTO model.

//            // Resolve the underlying patch's outlet
//            Outlet patchOutlet_Outlet = InletOutletMatcher.ApplyCustomOperatorToUnderlyingPatch(customOperatorOutlet);

//            // Visit the underlying patch's outlet.
//            VisitOperatorPolymorphic(patchOutlet_Outlet.Operator);
//        }

//        /// <summary>
//        /// Top-level PatchInlets need to be converted to VariableInput_OperatorDto's
//        /// and instance integrity needs to maintained over them.
//        /// </summary>
//        protected override void VisitPatchInlet(Operator op)
//        {
//            base.VisitPatchInlet(op);

//            IOperatorDto inputDto = _stack.Pop();

//            bool isTopLevelPatchInlet = IsTopLevelPatchInlet(op);
//            if (isTopLevelPatchInlet)
//            {
//                if (!_patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary.TryGetValue(op, out VariableInput_OperatorDto dto))
//                {
//                    var wrapper = new PatchInletOrOutlet_OperatorWrapper(op);

//                    dto = new VariableInput_OperatorDto
//                    {
//                        DimensionEnum = wrapper.Inlet.GetDimensionEnumWithFallback(),
//                        CanonicalName = NameHelper.ToCanonical(wrapper.Inlet.GetNameWithFallback()),
//                        Position = wrapper.Inlet.Position,
//                        DefaultValue = wrapper.Inlet.DefaultValue ?? 0.0
//                    };

//                    _patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary[op] = dto;
//                }

//                _stack.Push(dto);
//            }
//            else
//            {
//                _stack.Push(inputDto);
//            }
//        }

//        private bool IsTopLevelPatchInlet(Operator op)
//        {
//            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.PatchInlet)
//            {
//                return false;
//            }

//            return op.Patch.ID == _topLevelOutlet.Operator.Patch.ID;
//        }
//    }
//}
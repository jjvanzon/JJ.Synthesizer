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

//        // General Visitation

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

//        // Operator Visitation

//        protected override void VisitAbsolute(Operator op)
//        {
//            var dto = new Absolute_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitAdd(Operator op)
//        {
//            var dto = new Add_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitAllPassFilter(Operator op)
//        {
//            var dto = new AllPassFilter_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitAnd(Operator op)
//        {
//            var dto = new And_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitAverageFollower(Operator op)
//        {
//            var dto = new AverageFollower_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitAverageOverDimension(Operator op)
//        {
//            var dto = new AverageOverDimension_OperatorDto();
//            Process_OperatorDtoBase_WithCollectionRecalculation(op, dto);
//        }

//        protected override void VisitAverageOverInlets(Operator op)
//        {
//            var dto = new AverageOverInlets_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitBandPassFilterConstantPeakGain(Operator op)
//        {
//            var dto = new BandPassFilterConstantPeakGain_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitBandPassFilterConstantTransitionGain(Operator op)
//        {
//            var dto = new BandPassFilterConstantTransitionGain_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitChangeTrigger(Operator op)
//        {
//            var dto = new ChangeTrigger_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
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
//            Process_OperatorDtoBase_WithCollectionRecalculation(op, dto);
//        }

//        protected override void VisitClosestOverDimensionExp(Operator op)
//        {
//            var dto = new ClosestOverDimensionExp_OperatorDto();
//            Process_OperatorDtoBase_WithCollectionRecalculation(op, dto);
//        }

//        protected override void VisitCache(Operator op)
//        {
//            var dto = new Cache_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);

//            var wrapper = new Cache_OperatorWrapper(op);
//            dto.InterpolationTypeEnum = wrapper.InterpolationType;
//            dto.SpeakerSetupEnum = wrapper.SpeakerSetup;
//            dto.ChannelCount = wrapper.GetChannelCount(_speakerSetupRepository);
//        }

//        protected override void VisitCurveOperator(Operator op)
//        {
//            var dto = new Curve_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);

//            var wrapper = new Curve_OperatorWrapper(op, _curveRepository);

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
//        }

//        protected override void VisitDimensionToOutletsOutlet(Outlet outlet)
//        {
//            var dto = new DimensionToOutlets_Outlet_OperatorDto();

//            base.VisitDimensionToOutletsOutlet(outlet);

//            PopAndSetInputs(dto);

//            SetDimensionProperties(outlet.Operator, dto);

//            _stack.Push(dto);

//            if (!outlet.RepetitionPosition.HasValue)
//            {
//                throw new NullException(() => outlet.RepetitionPosition);
//            }

//            dto.OutletPosition = outlet.RepetitionPosition.Value;
//        }

//        protected override void VisitDivide(Operator op)
//        {
//            var dto = new Divide_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitEqual(Operator op)
//        {
//            var dto = new Equal_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitGetDimension(Operator op)
//        {
//            var dto = new GetDimension_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitGreaterThan(Operator op)
//        {
//            var dto = new GreaterThan_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitGreaterThanOrEqual(Operator op)
//        {
//            var dto = new GreaterThanOrEqual_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitHold(Operator op)
//        {
//            var dto = new Hold_OperatorDto_VarSignal();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitHighPassFilter(Operator op)
//        {
//            var dto = new HighPassFilter_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitHighShelfFilter(Operator op)
//        {
//            var dto = new HighShelfFilter_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitIf(Operator op)
//        {
//            var dto = new If_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitInletsToDimension(Operator op)
//        {
//            var dto = new InletsToDimension_OperatorDto();

//            Process_OperatorDtoBase_Vars(op, dto);

//            SetDimensionProperties(op, dto);

//            var wrapper = new InletsToDimension_OperatorWrapper(op);
//            dto.ResampleInterpolationTypeEnum = wrapper.InterpolationType;
//        }

//        protected override void VisitInterpolate(Operator op)
//        {
//            var dto = new Interpolate_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);

//            var wrapper = new Interpolate_OperatorWrapper(op);
//            dto.ResampleInterpolationTypeEnum = wrapper.InterpolationType;
//        }

//        protected override void VisitLessThan(Operator op)
//        {
//            var dto = new LessThan_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitLessThanOrEqual(Operator op)
//        {
//            var dto = new LessThanOrEqual_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitLoop(Operator op)
//        {
//            var dto = new Loop_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitLowPassFilter(Operator op)
//        {
//            var dto = new LowPassFilter_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitLowShelfFilter(Operator op)
//        {
//            var dto = new LowShelfFilter_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }
        
//        protected override void VisitMaxOverDimension(Operator op)
//        {
//            var dto = new MaxOverDimension_OperatorDto();
//            Process_OperatorDtoBase_WithCollectionRecalculation(op, dto);
//        }

//        protected override void VisitMaxOverInlets(Operator op)
//        {
//            var dto = new MaxOverInlets_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitMaxFollower(Operator op)
//        {
//            var dto = new MaxFollower_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitMinOverDimension(Operator op)
//        {
//            var dto = new MinOverDimension_OperatorDto();
//            Process_OperatorDtoBase_WithCollectionRecalculation(op, dto);
//        }

//        protected override void VisitMinOverInlets(Operator op)
//        {
//            var dto = new MinOverInlets_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitMinFollower(Operator op)
//        {
//            var dto = new MinFollower_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitMultiply(Operator op)
//        {
//            var dto = new Multiply_OperatorDto();
//            Process_OperatorDtoBase_Vars(op, dto);
//        }

//        protected override void VisitNegative(Operator op)
//        {
//            var dto = new Negative_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitNoise(Operator op)
//        {
//            var dto = new Noise_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);

//            dto.ArrayDto = _calculatorCache.GetNoiseArrayDto();
//        }

//        protected override void VisitNot(Operator op)
//        {
//            var dto = new Not_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitNotchFilter(Operator op)
//        {
//            var dto = new NotchFilter_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitNotEqual(Operator op)
//        {
//            var dto = new NotEqual_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitNumber(Operator op)
//        {
//            var dto = new Number_OperatorDto();
//            Process_OperatorDtoBase(op, dto);

//            var wrapper = new Number_OperatorWrapper(op);
//            dto.Number = wrapper.Number;
//        }

//        protected override void VisitOr(Operator op)
//        {
//            var dto = new Or_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitPeakingEQFilter(Operator op)
//        {
//            var dto = new PeakingEQFilter_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitPower(Operator op)
//        {
//            var dto = new Power_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitPulse(Operator op)
//        {
//            var dto = new Pulse_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitPulseTrigger(Operator op)
//        {
//            var dto = new PulseTrigger_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitRandom(Operator op)
//        {
//            var dto = new Random_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);

//            var wrapper = new Random_OperatorWrapper(op);
//            dto.ResampleInterpolationTypeEnum = wrapper.InterpolationType;
//            dto.ArrayDto = _calculatorCache.GetRandomArrayDto(wrapper.InterpolationType);
//        }

//        protected override void VisitRangeOverDimension(Operator op)
//        {
//            var dto = new RangeOverDimension_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitRangeOverOutletsOutlet(Outlet outlet)
//        {
//            var dto = new RangeOverOutlets_Outlet_OperatorDto();

//            base.VisitRangeOverOutletsOutlet(outlet);

//            PopAndSetInputs(dto);

//            _stack.Push(dto);

//            dto.OutletPosition = outlet.Position;
//        }

//        protected override void VisitReset(Operator op)
//        {
//            var dto = new Reset_OperatorDto();
//            Process_OperatorDtoBase(op, dto);

//            dto.Name = op.Name;

//            var wrapper = new Reset_OperatorWrapper(op);
//            dto.Position = wrapper.Position;
//        }

//        protected override void VisitReverse(Operator op)
//        {
//            var dto = new Reverse_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitRound(Operator op)
//        {
//            var dto = new Round_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitSampleOperator(Operator op)
//        {
//            var dto = new Sample_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);

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
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitSawUp(Operator op)
//        {
//            var dto = new SawUp_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitSetDimension(Operator op)
//        {
//            var dto = new SetDimension_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitSine(Operator op)
//        {
//            var dto = new Sine_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitSortOverInletsOutlet(Outlet outlet)
//        {
//            var dto = new SortOverInlets_Outlet_OperatorDto();

//            Process_OperatorDtoBase_Vars(outlet.Operator, dto);

//            SetDimensionProperties(outlet.Operator, dto);

//            dto.OutletPosition = outlet.Position;
//        }

//        protected override void VisitSortOverDimension(Operator op)
//        {
//            var dto = new SortOverDimension_OperatorDto();
//            Process_OperatorDtoBase_WithCollectionRecalculation(op, dto);
//        }

//        protected override void VisitSpectrum(Operator op)
//        {
//            var dto = new Spectrum_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitSquare(Operator op)
//        {
//            var dto = new Square_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitSquash(Operator op)
//        {
//            var dto = new Squash_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitStretch(Operator op)
//        {
//            var dto = new Stretch_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitSubtract(Operator op)
//        {
//            var dto = new Subtract_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        protected override void VisitSumOverDimension(Operator op)
//        {
//            var dto = new SumOverDimension_OperatorDto();
//            Process_OperatorDtoBase_WithCollectionRecalculation(op, dto);
//        }

//        protected override void VisitSumFollower(Operator op)
//        {
//            var dto = new SumFollower_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitTriangle(Operator op)
//        {
//            var dto = new Triangle_OperatorDto();
//            Process_OperatorDtoBase_WithDimension(op, dto);
//        }

//        protected override void VisitToggleTrigger(Operator op)
//        {
//            var dto = new ToggleTrigger_OperatorDto();
//            Process_OperatorDtoBase(op, dto);
//        }

//        // Helpers

//        private void Process_ClosestOverInlets_OperatorDto(Operator op, ClosestOverInlets_OperatorDto dto)
//        {
//            VisitOperatorBase(op);

//            dto.OperatorID = op.ID;

//            dto.Input = PopInput();
//            dto.Items = CollectionHelper.Repeat(op.Inlets.Count - 1, () => PopInput()).ToArray();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_Vars(Operator op, OperatorDtoBase_Vars dto)
//        {
//            VisitOperatorBase(op);

//            dto.OperatorID = op.ID;

//            dto.Vars = CollectionHelper.Repeat(op.Inlets.Count, () => PopInput()).ToArray();

//            _stack.Push(dto);
//        }

//        private void Process_OperatorDtoBase_WithCollectionRecalculation(Operator op, OperatorDtoBase_WithCollectionRecalculation dto)
//        {
//            Process_OperatorDtoBase_WithDimension(op, dto);

//            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
//            dto.CollectionRecalculationEnum = wrapper.CollectionRecalculation;
//        }

//        private void Process_OperatorDtoBase_WithDimension(Operator op, IOperatorDto_WithDimension dto)
//        {
//            Process_OperatorDtoBase(op, dto);

//            SetDimensionProperties(op, dto);
//        }

//        private void Process_OperatorDtoBase(Operator op, IOperatorDto dto)
//        {
//            VisitOperatorBase(op);

//            dto.OperatorID = op.ID;

//            PopAndSetInputs(dto);

//            _stack.Push(dto);
//        }

//        private void PopAndSetInputs(IOperatorDto dto)
//        {
//            int count = dto.Inputs.Count();

//            dto.Inputs = CollectionHelper.Repeat(count, () => PopInput()).ToArray();
//        }

//        private InputDto PopInput()
//        {
//            return InputDtoFactory.CreateInputDto(_stack.Pop());
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
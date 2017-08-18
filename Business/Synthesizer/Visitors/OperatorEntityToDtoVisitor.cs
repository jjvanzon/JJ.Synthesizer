using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorEntityToDtoVisitor : OperatorEntityVisitorBase_WithInletCoalescing
    {
        private readonly CalculatorCache _calculatorCache;
        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;

        private Stack<IOperatorDto> _stack;
        private Outlet _topLevelOutlet;

        /// <summary> Needed to maintain instance integrity. </summary>
        private Dictionary<Operator, VariableInput_OperatorDto> _patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary;

        public OperatorEntityToDtoVisitor(
            CalculatorCache calculatorCache,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            _calculatorCache = calculatorCache ?? throw new ArgumentNullException(nameof(calculatorCache));
            _curveRepository = curveRepository ?? throw new ArgumentNullException(nameof(curveRepository));
            _sampleRepository = sampleRepository ?? throw new ArgumentNullException(nameof(sampleRepository));
            _speakerSetupRepository = speakerSetupRepository ?? throw new ArgumentNullException(nameof(speakerSetupRepository));
        }

        // General Visitation

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

        // Operator Visitation

        protected override void VisitAbsolute(Operator op) => ProcessOperator(op, new Absolute_OperatorDto());
        protected override void VisitAdd(Operator op) => ProcessOperator(op, new Add_OperatorDto());
        protected override void VisitAllPassFilter(Operator op) => ProcessOperator(op, new AllPassFilter_OperatorDto());
        protected override void VisitAnd(Operator op) => ProcessOperator(op, new And_OperatorDto());
        protected override void VisitAverageFollower(Operator op) => ProcessOperator(op, new AverageFollower_OperatorDto());
        protected override void VisitAverageOverDimension(Operator op) => ProcessOperator(op, new AverageOverDimension_OperatorDto());
        protected override void VisitAverageOverInlets(Operator op) => ProcessOperator(op, new AverageOverInlets_OperatorDto());
        protected override void VisitBandPassFilterConstantPeakGain(Operator op) => ProcessOperator(op, new BandPassFilterConstantPeakGain_OperatorDto());
        protected override void VisitBandPassFilterConstantTransitionGain(Operator op) => ProcessOperator(op, new BandPassFilterConstantTransitionGain_OperatorDto());
        protected override void VisitChangeTrigger(Operator op) => ProcessOperator(op, new ChangeTrigger_OperatorDto());
        protected override void VisitClosestOverInlets(Operator op) => ProcessOperator(op, new ClosestOverInlets_OperatorDto());
        protected override void VisitClosestOverInletsExp(Operator op) => ProcessOperator(op, new ClosestOverInletsExp_OperatorDto());
        protected override void VisitClosestOverDimension(Operator op) => ProcessOperator(op, new ClosestOverDimension_OperatorDto());
        protected override void VisitClosestOverDimensionExp(Operator op) => ProcessOperator(op, new ClosestOverDimensionExp_OperatorDto());

        protected override void VisitCache(Operator op)
        {
            var dto = new Cache_OperatorDto();
            ProcessOperator(op, dto);

            var wrapper = new Cache_OperatorWrapper(op);
            dto.InterpolationTypeEnum = wrapper.InterpolationType;
            dto.SpeakerSetupEnum = wrapper.SpeakerSetup;
            dto.ChannelCount = wrapper.GetChannelCount(_speakerSetupRepository);
        }

        protected override void VisitCurveOperator(Operator op)
        {
            var dto = new Curve_OperatorDto();
            ProcessOperator(op, dto);

            var wrapper = new Curve_OperatorWrapper(op, _curveRepository);

            Curve curve = wrapper.Curve;
            if (curve == null)
            {
                return;
            }

            dto.CurveID = curve.ID;
            dto.ArrayDto = _calculatorCache.GetCurveArrayDto(curve.ID, _curveRepository);
            dto.MinX = curve.Nodes
                            .OrderBy(x => x.X)
                            .First()
                            .X;
        }

        protected override void VisitDimensionToOutletsOutlet(Outlet outlet)
        {
            base.VisitDimensionToOutletsOutlet(outlet);

            Operator op = outlet.Operator;

            var dto = new DimensionToOutlets_Outlet_OperatorDto
            {
                OperatorID = op.ID,
                Signal = PopInputDto(),
                StandardDimensionEnum = op.GetStandardDimensionEnumWithFallback(),
                CanonicalCustomDimensionName = NameHelper.ToCanonical(op.GetCustomDimensionNameWithFallback())
            };

            if (!outlet.RepetitionPosition.HasValue)
            {
                throw new NullException(() => outlet.RepetitionPosition);
            }
            dto.OutletPosition = outlet.RepetitionPosition.Value;

            _stack.Push(dto);
        }

        protected override void VisitDivide(Operator op) => ProcessOperator(op, new Divide_OperatorDto());
        protected override void VisitEqual(Operator op) => ProcessOperator(op, new Equal_OperatorDto());
        protected override void VisitGetDimension(Operator op) => ProcessOperator(op, new GetDimension_OperatorDto());
        protected override void VisitGreaterThan(Operator op) => ProcessOperator(op, new GreaterThan_OperatorDto());
        protected override void VisitGreaterThanOrEqual(Operator op) => ProcessOperator(op, new GreaterThanOrEqual_OperatorDto());
        protected override void VisitHold(Operator op) => ProcessOperator(op, new Hold_OperatorDto());
        protected override void VisitHighPassFilter(Operator op) => ProcessOperator(op, new HighPassFilter_OperatorDto());
        protected override void VisitHighShelfFilter(Operator op) => ProcessOperator(op, new HighShelfFilter_OperatorDto());
        protected override void VisitIf(Operator op) => ProcessOperator(op, new If_OperatorDto());

        protected override void VisitInletsToDimension(Operator op)
        {
            var dto = new InletsToDimension_OperatorDto();
            ProcessOperator(op, dto);

            var wrapper = new InletsToDimension_OperatorWrapper(op);
            dto.ResampleInterpolationTypeEnum = wrapper.InterpolationType;
        }

        protected override void VisitInterpolate(Operator op)
        {
            var dto = new Interpolate_OperatorDto();
            ProcessOperator(op, dto);

            var wrapper = new Interpolate_OperatorWrapper(op);
            dto.ResampleInterpolationTypeEnum = wrapper.InterpolationType;
        }

        protected override void VisitLessThan(Operator op) => ProcessOperator(op, new LessThan_OperatorDto());
        protected override void VisitLessThanOrEqual(Operator op) => ProcessOperator(op, new LessThanOrEqual_OperatorDto());
        protected override void VisitLoop(Operator op) => ProcessOperator(op, new Loop_OperatorDto());
        protected override void VisitLowPassFilter(Operator op) => ProcessOperator(op, new LowPassFilter_OperatorDto());
        protected override void VisitLowShelfFilter(Operator op) => ProcessOperator(op, new LowShelfFilter_OperatorDto());
        protected override void VisitMaxOverDimension(Operator op) => ProcessOperator(op, new MaxOverDimension_OperatorDto());
        protected override void VisitMaxOverInlets(Operator op) => ProcessOperator(op, new MaxOverInlets_OperatorDto());
        protected override void VisitMaxFollower(Operator op) => ProcessOperator(op, new MaxFollower_OperatorDto());
        protected override void VisitMinOverDimension(Operator op) => ProcessOperator(op, new MinOverDimension_OperatorDto());
        protected override void VisitMinOverInlets(Operator op) => ProcessOperator(op, new MinOverInlets_OperatorDto());
        protected override void VisitMinFollower(Operator op) => ProcessOperator(op, new MinFollower_OperatorDto());
        protected override void VisitMultiply(Operator op) => ProcessOperator(op, new Multiply_OperatorDto());
        protected override void VisitNegative(Operator op) => ProcessOperator(op, new Negative_OperatorDto());

        protected override void VisitNoise(Operator op)
        {
            var dto = new Noise_OperatorDto();
            ProcessOperator(op, dto);

            dto.ArrayDto = _calculatorCache.GetNoiseArrayDto();
        }

        protected override void VisitNot(Operator op) => ProcessOperator(op, new Not_OperatorDto());
        protected override void VisitNotchFilter(Operator op) => ProcessOperator(op, new NotchFilter_OperatorDto());
        protected override void VisitNotEqual(Operator op) => ProcessOperator(op, new NotEqual_OperatorDto());

        protected override void VisitNumber(Operator op)
        {
            var dto = new Number_OperatorDto();
            ProcessOperator(op, dto);

            var wrapper = new Number_OperatorWrapper(op);
            dto.Number = wrapper.Number;
        }

        protected override void VisitOr(Operator op) => ProcessOperator(op, new Or_OperatorDto());
        protected override void VisitPeakingEQFilter(Operator op) => ProcessOperator(op, new PeakingEQFilter_OperatorDto());
        protected override void VisitPower(Operator op) => ProcessOperator(op, new Power_OperatorDto());
        protected override void VisitPulseTrigger(Operator op) => ProcessOperator(op, new PulseTrigger_OperatorDto());

        protected override void VisitRandom(Operator op)
        {
            var dto = new Random_OperatorDto();
            ProcessOperator(op, dto);

            var wrapper = new Random_OperatorWrapper(op);
            dto.ResampleInterpolationTypeEnum = wrapper.InterpolationType;
            dto.ArrayDto = _calculatorCache.GetRandomArrayDto(wrapper.InterpolationType);
        }

        protected override void VisitRangeOverDimension(Operator op) => ProcessOperator(op, new RangeOverDimension_OperatorDto());

        protected override void VisitRangeOverOutletsOutlet(Outlet outlet)
        {
            base.VisitRangeOverOutletsOutlet(outlet);

            Operator op = outlet.Operator;

            var dto = new RangeOverOutlets_Outlet_OperatorDto
            {
                OperatorID = op.ID,
                From = PopInputDto(),
                Step = PopInputDto()
            };

            if (!outlet.RepetitionPosition.HasValue)
            {
                throw new NullException(() => outlet.RepetitionPosition);
            }
            dto.OutletPosition = outlet.RepetitionPosition.Value;

            _stack.Push(dto);
        }

        protected override void VisitRemainder(Operator op) => ProcessOperator(op, new Remainder_OperatorDto());

        protected override void VisitReset(Operator op)
        {
            var dto = new Reset_OperatorDto();
            ProcessOperator(op, dto);

            dto.Name = op.Name;

            var wrapper = new Reset_OperatorWrapper(op);
            dto.Position = wrapper.Position;
        }

        protected override void VisitRound(Operator op) => ProcessOperator(op, new Round_OperatorDto());

        /// <see cref="OperatorEntityVisitorBase.VisitSampleWithRate1"/>
        private Operator _currentSampleOperator;

        /// <see cref="OperatorEntityVisitorBase.VisitSampleWithRate1"/>
        protected override void VisitSampleOutlet(Outlet outlet)
        {
            _currentSampleOperator = outlet.Operator;
            base.VisitSampleOutlet(outlet);
        }

        /// <see cref="OperatorEntityVisitorBase.VisitSampleWithRate1"/>
        protected override void VisitSampleWithRate1(Operator op)
        {
            var dto = new SampleWithRate1_OperatorDto();
            ProcessOperator(op, dto);

            SetDimensionProperties(_currentSampleOperator, dto);

            var wrapper = new Sample_OperatorWrapper(_currentSampleOperator, _sampleRepository);
            Sample sample = wrapper.Sample;

            if (sample != null)
            {
                dto.SampleID = sample.ID;
                dto.InterpolationTypeEnum = sample.GetInterpolationTypeEnum();
                dto.SampleChannelCount = sample.GetChannelCount();
                dto.ArrayDtos = _calculatorCache.GetSampleArrayDtos(sample.ID, _sampleRepository);
            }
        }

        protected override void VisitSetDimension(Operator op) => ProcessOperator(op, new SetDimension_OperatorDto());
        protected override void VisitSineWithRate1(Operator op) => ProcessOperator(op, new SineWithRate1_OperatorDto());

        protected override void VisitSortOverInletsOutlet(Outlet outlet)
        {
            base.VisitSortOverInletsOutlet(outlet);

            Operator op = outlet.Operator;

            // ReSharper disable once UseObjectOrCollectionInitializer
            var dto = new SortOverInlets_Outlet_OperatorDto
            {
                OperatorID = op.ID,
                StandardDimensionEnum = op.GetStandardDimensionEnumWithFallback(),
                CanonicalCustomDimensionName = NameHelper.ToCanonical(op.GetCustomDimensionNameWithFallback())
            };

            dto.Inputs = CollectionHelper.Repeat(op.Inlets.Count, () => PopInputDto())
                                         .Where(x => x != null)
                                         .ToArray();

            if (!outlet.RepetitionPosition.HasValue)
            {
                throw new NullException(() => outlet.RepetitionPosition);
            }
            dto.OutletPosition = outlet.RepetitionPosition.Value;

            _stack.Push(dto);
        }

        protected override void VisitSortOverDimension(Operator op) => ProcessOperator(op, new SortOverDimension_OperatorDto());
        protected override void VisitSpectrum(Operator op) => ProcessOperator(op, new Spectrum_OperatorDto());
        protected override void VisitSquash(Operator op) => ProcessOperator(op, new Squash_OperatorDto());
        protected override void VisitSubtract(Operator op) => ProcessOperator(op, new Subtract_OperatorDto());
        protected override void VisitSumOverDimension(Operator op) => ProcessOperator(op, new SumOverDimension_OperatorDto());
        protected override void VisitSumFollower(Operator op) => ProcessOperator(op, new SumFollower_OperatorDto());
        protected override void VisitTriangleWithRate1(Operator op) => ProcessOperator(op, new TriangleWithRate1_OperatorDto());
        protected override void VisitToggleTrigger(Operator op) => ProcessOperator(op, new ToggleTrigger_OperatorDto());

        // Helpers

        /// <summary>
        /// Contains all the code shared by all the operator types' processing +
        /// setting the CollectionRecalculation.
        /// </summary>
        private void ProcessOperator(Operator op, OperatorDtoBase_WithCollectionRecalculation dto)
        {
            ProcessOperator(op, (IOperatorDto)dto);

            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
            dto.CollectionRecalculationEnum = wrapper.CollectionRecalculation;
        }

        /// <summary>
        /// Contains all the code shared by all the operator types' processing.
        /// </summary>
        private void ProcessOperator(Operator op, IOperatorDto dto)
        {
            VisitOperatorBase(op);

            dto.OperatorID = op.ID;

            dto.Inputs = CollectionHelper.Repeat(op.Inlets.Count, () => PopInputDto())
                                         .Where(x => x != null)
                                         .ToArray();

            {
                if (dto is IOperatorDto_WithDimension castedDto)
                {
                    SetDimensionProperties(op, castedDto);
                }
            }

            {
                if (dto is IOperatorDto_WithAggregateInfo castedDto)
                {
                    castedDto.AggregateInfo = AggregateInfoFactory.CreateAggregateInfo(dto.Inputs);
                }
            }

            _stack.Push(dto);
        }

        private static void SetDimensionProperties(Operator op, IOperatorDto_WithDimension dto)
        {
            dto.StandardDimensionEnum = op.GetStandardDimensionEnumWithFallback();
            dto.CanonicalCustomDimensionName = NameHelper.ToCanonical(op.GetCustomDimensionNameWithFallback());
        }

        private InputDto PopInputDto()
        {
            return InputDtoFactory.TryCreateInputDto(_stack.Pop());
        }

        // Special Visitation

        protected override void InsertNumber(double number)
        {
            _stack.Push(new Number_OperatorDto { Number = number });
        }

        protected override void InsertEmptyInput()
        {
            _stack.Push(null);
        }

        /// <summary> As soon as you encounter a Derived Operator's Outlet, the evaluation has to take a completely different course. </summary>
        protected override void VisitDerivedOperatorOutlet(Outlet customOperatorOutlet)
        {
            // NOTE: Do not try to separate this concept into a different visitor class.
            // It has been tried and resulted in something much more complicated than these two lines of code.
            // Most magic is in the InletOutletMatcher, which is a difficult class, fully worked out and tested,
            // that needs to tap into the entity model, not DTO model.

            // Resolve the underlying patch's outlet
            Outlet patchOutlet_Outlet = InletOutletMatcher.ApplyDerivedOperatorToUnderlyingPatch(customOperatorOutlet);

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
                if (!_patchInlet_Operator_To_VariableInput_OperatorDto_Dictionary.TryGetValue(op, out VariableInput_OperatorDto dto))
                {
                    var wrapper = new PatchInletOrOutlet_OperatorWrapper(op);

                    dto = new VariableInput_OperatorDto
                    {
                        DimensionEnum = wrapper.Inlet.GetDimensionEnumWithFallback(),
                        CanonicalName = NameHelper.ToCanonical(wrapper.Inlet.GetNameWithFallback()),
                        Position = wrapper.Inlet.Position,
                        DefaultValue = wrapper.Inlet.DefaultValue ?? 0.0
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
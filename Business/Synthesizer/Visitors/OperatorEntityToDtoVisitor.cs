using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;

// ReSharper disable ObjectCreationAsStatement

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
            }
        }

        protected override void VisitOutletPolymorphic(Outlet outlet)
        {
            bool hasOutletVisitation = HasOutletVisitation(outlet);

            if (hasOutletVisitation)
            {
                VisitorHelper.WithStackCheck(_stack, () => base.VisitOutletPolymorphic(outlet));
            }
            else
            {
                base.VisitOutletPolymorphic(outlet);
            }
        }

        // Operator Visitation

        protected override void VisitAdd(Operator op) => ProcessPolymorphicRecursive(op, new Add_OperatorDto());
        protected override void VisitAllPassFilter(Operator op) => ProcessPolymorphicRecursive(op, new AllPassFilter_OperatorDto());
        protected override void VisitAnd(Operator op) => ProcessPolymorphicRecursive(op, new And_OperatorDto());
        protected override void VisitArcCos(Operator op) => ProcessPolymorphicRecursive(op, new ArcCos_OperatorDto());
        protected override void VisitArcSin(Operator op) => ProcessPolymorphicRecursive(op, new ArcSin_OperatorDto());
        protected override void VisitArcTan(Operator op) => ProcessPolymorphicRecursive(op, new ArcTan_OperatorDto());
        protected override void VisitAverageFollower(Operator op) => ProcessPolymorphicRecursive(op, new AverageFollower_OperatorDto());
        protected override void VisitAverageOverDimension(Operator op) => ProcessPolymorphicRecursive(op, new AverageOverDimension_OperatorDto());
        protected override void VisitAverageOverInlets(Operator op) => ProcessPolymorphicRecursive(op, new AverageOverInlets_OperatorDto());

        protected override void VisitBandPassFilterConstantPeakGain(Operator op)
            => ProcessPolymorphicRecursive(op, new BandPassFilterConstantPeakGain_OperatorDto());

        protected override void VisitBandPassFilterConstantTransitionGain(Operator op)
            => ProcessPolymorphicRecursive(op, new BandPassFilterConstantTransitionGain_OperatorDto());

        protected override void VisitCache(Operator op)
        {
            var dto = new Cache_OperatorDto();
            ProcessPolymorphicRecursive(op, dto);

            var wrapper = new Cache_OperatorWrapper(op);
            dto.InterpolationTypeEnum = wrapper.InterpolationType;
            dto.SpeakerSetupEnum = wrapper.SpeakerSetup;
            dto.ChannelCount = wrapper.GetChannelCount(_speakerSetupRepository);
        }

        protected override void VisitCeiling(Operator op) => ProcessPolymorphicRecursive(op, new Ceiling_OperatorDto());
        protected override void VisitChangeTrigger(Operator op) => ProcessPolymorphicRecursive(op, new ChangeTrigger_OperatorDto());
        protected override void VisitClosestOverDimension(Operator op) => ProcessPolymorphicRecursive(op, new ClosestOverDimension_OperatorDto());

        protected override void VisitClosestOverDimensionExp(Operator op)
            => ProcessPolymorphicRecursive(op, new ClosestOverDimensionExp_OperatorDto());

        protected override void VisitClosestOverInlets(Operator op) => ProcessPolymorphicRecursive(op, new ClosestOverInlets_OperatorDto());
        protected override void VisitClosestOverInletsExp(Operator op) => ProcessPolymorphicRecursive(op, new ClosestOverInletsExp_OperatorDto());
        protected override void VisitCos(Operator op) => ProcessPolymorphicRecursive(op, new Cos_OperatorDto());
        protected override void VisitCosH(Operator op) => ProcessPolymorphicRecursive(op, new CosH_OperatorDto());

        protected override void VisitCurveOperator(Operator op)
        {
            var dto = new Curve_OperatorDto();
            ProcessPolymorphicRecursive(op, dto);

            Curve curve = op.Curve;

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

            var dto = new DimensionToOutlets_Outlet_OperatorDto();

            ProcessPolymorphic(op, dto);

            if (!outlet.RepetitionPosition.HasValue)
            {
                throw new NullException(() => outlet.RepetitionPosition);
            }

            dto.OutletPosition = outlet.RepetitionPosition.Value;
        }

        protected override void VisitDivide(Operator op) => ProcessPolymorphicRecursive(op, new Divide_OperatorDto());
        protected override void VisitEqual(Operator op) => ProcessPolymorphicRecursive(op, new Equal_OperatorDto());
        protected override void VisitFloor(Operator op) => ProcessPolymorphicRecursive(op, new Floor_OperatorDto());
        protected override void VisitGetPosition(Operator op) => ProcessPolymorphicRecursive(op, new GetPosition_OperatorDto());
        protected override void VisitGreaterThan(Operator op) => ProcessPolymorphicRecursive(op, new GreaterThan_OperatorDto());
        protected override void VisitGreaterThanOrEqual(Operator op) => ProcessPolymorphicRecursive(op, new GreaterThanOrEqual_OperatorDto());
        protected override void VisitHighPassFilter(Operator op) => ProcessPolymorphicRecursive(op, new HighPassFilter_OperatorDto());
        protected override void VisitHighShelfFilter(Operator op) => ProcessPolymorphicRecursive(op, new HighShelfFilter_OperatorDto());
        protected override void VisitHold(Operator op) => ProcessPolymorphicRecursive(op, new Hold_OperatorDto());
        protected override void VisitIf(Operator op) => ProcessPolymorphicRecursive(op, new If_OperatorDto());

        protected override void VisitInletsToDimension(Operator op)
        {
            base.VisitInletsToDimension(op);

            var dto = new InletsToDimension_OperatorDto();

            ProcessPolymorphic(op, dto);

            InputDto positionInput = new Number_OperatorDto(double.NaN);
            dto.Inputs = dto.Inputs.Concat(positionInput).ToArray();
        }

        protected override void VisitInterpolate(Operator op) => ProcessPolymorphicRecursive(op, new Interpolate_OperatorDto());
        protected override void VisitLessThan(Operator op) => ProcessPolymorphicRecursive(op, new LessThan_OperatorDto());
        protected override void VisitLessThanOrEqual(Operator op) => ProcessPolymorphicRecursive(op, new LessThanOrEqual_OperatorDto());
        protected override void VisitLn(Operator op) => ProcessPolymorphicRecursive(op, new Ln_OperatorDto());
        protected override void VisitLogN(Operator op) => ProcessPolymorphicRecursive(op, new LogN_OperatorDto());
        protected override void VisitLoop(Operator op) => ProcessPolymorphicRecursive(op, new Loop_OperatorDto());
        protected override void VisitLowPassFilter(Operator op) => ProcessPolymorphicRecursive(op, new LowPassFilter_OperatorDto());
        protected override void VisitLowShelfFilter(Operator op) => ProcessPolymorphicRecursive(op, new LowShelfFilter_OperatorDto());
        protected override void VisitMaxFollower(Operator op) => ProcessPolymorphicRecursive(op, new MaxFollower_OperatorDto());
        protected override void VisitMaxOverDimension(Operator op) => ProcessPolymorphicRecursive(op, new MaxOverDimension_OperatorDto());
        protected override void VisitMaxOverInlets(Operator op) => ProcessPolymorphicRecursive(op, new MaxOverInlets_OperatorDto());
        protected override void VisitMinFollower(Operator op) => ProcessPolymorphicRecursive(op, new MinFollower_OperatorDto());
        protected override void VisitMinOverDimension(Operator op) => ProcessPolymorphicRecursive(op, new MinOverDimension_OperatorDto());
        protected override void VisitMinOverInlets(Operator op) => ProcessPolymorphicRecursive(op, new MinOverInlets_OperatorDto());
        protected override void VisitMultiply(Operator op) => ProcessPolymorphicRecursive(op, new Multiply_OperatorDto());
        protected override void VisitNegative(Operator op) => ProcessPolymorphicRecursive(op, new Negative_OperatorDto());
        protected override void VisitNoise(Operator op) => ProcessPolymorphicRecursive(op, new Noise_OperatorDto());
        protected override void VisitNot(Operator op) => ProcessPolymorphicRecursive(op, new Not_OperatorDto());
        protected override void VisitNotchFilter(Operator op) => ProcessPolymorphicRecursive(op, new NotchFilter_OperatorDto());
        protected override void VisitNotEqual(Operator op) => ProcessPolymorphicRecursive(op, new NotEqual_OperatorDto());
        protected override void VisitNumber(Operator op) => ProcessPolymorphicRecursive(op, new Number_OperatorDto());
        protected override void VisitOr(Operator op) => ProcessPolymorphicRecursive(op, new Or_OperatorDto());
        protected override void VisitPeakingEQFilter(Operator op) => ProcessPolymorphicRecursive(op, new PeakingEQFilter_OperatorDto());
        protected override void VisitPower(Operator op) => ProcessPolymorphicRecursive(op, new Power_OperatorDto());
        protected override void VisitPulseTrigger(Operator op) => ProcessPolymorphicRecursive(op, new PulseTrigger_OperatorDto());

        protected override void VisitRandomOutlet(Outlet outlet)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_WithInterpolation(op);
            InterpolationTypeEnum interpolationTypeEnum = wrapper.InterpolationType;

            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Block:
                case InterpolationTypeEnum.Stripe:
                    base.VisitRandomOutlet(outlet);
                    ProcessPolymorphic(op, new Random_OperatorDto());
                    break;

                default:
                    Operator underlyingInterpolateOperator = op.UnderlyingPatch.EnumerateOperatorsOfType(OperatorTypeEnum.Interpolate).Single();

                    new OperatorWrapper_WithInterpolation(underlyingInterpolateOperator)
                    {
                        InterpolationType = interpolationTypeEnum
                    };

                    VisitDerivedOperatorOutlet(outlet);
                    break;
            }
        }

        protected override void VisitRandomStripe(Operator op) => ProcessPolymorphicRecursive(op, new Random_OperatorDto());
        protected override void VisitRangeOverDimension(Operator op) => ProcessPolymorphicRecursive(op, new RangeOverDimension_OperatorDto());

        protected override void VisitRangeOverOutletsOutlet(Outlet outlet)
        {
            base.VisitRangeOverOutletsOutlet(outlet);

            var dto = new RangeOverOutlets_Outlet_OperatorDto();

            ProcessPolymorphic(outlet.Operator, dto);

            if (!outlet.RepetitionPosition.HasValue)
            {
                throw new NullException(() => outlet.RepetitionPosition);
            }

            dto.OutletPosition = outlet.RepetitionPosition.Value;
        }

        protected override void VisitRemainder(Operator op) => ProcessPolymorphicRecursive(op, new Remainder_OperatorDto());
        protected override void VisitReset(Operator op) => ProcessPolymorphicRecursive(op, new Reset_OperatorDto());
        protected override void VisitRound(Operator op) => ProcessPolymorphicRecursive(op, new Round_OperatorDto());

        /// <see cref="OperatorEntityVisitorBase.VisitSampleWithRate1" />
        private Operator _currentSampleOperator;

        /// <see cref="OperatorEntityVisitorBase.VisitSampleWithRate1" />
        protected override void VisitSampleOutlet(Outlet outlet)
        {
            _currentSampleOperator = outlet.Operator;
            base.VisitSampleOutlet(outlet);
        }

        /// <inheritdoc />
        protected override void VisitSampleWithRate1(Operator op)
        {
            var dto = new SampleWithRate1_OperatorDto();

            ProcessPolymorphicRecursive(op, dto);

            Sample sample = _currentSampleOperator.Sample;
            dto.SampleID = sample.ID;
            dto.InterpolationTypeEnum = sample.GetInterpolationTypeEnum();
            dto.SampleChannelCount = sample.GetChannelCount();
            dto.ArrayDtos = _calculatorCache.GetSampleArrayDtos(sample.ID, _sampleRepository);
        }

        protected override void VisitSetPosition(Operator op) => ProcessPolymorphicRecursive(op, new SetPosition_OperatorDto());
        protected override void VisitSign(Operator op) => ProcessPolymorphicRecursive(op, new Sign_OperatorDto());
        protected override void VisitSin(Operator op) => ProcessPolymorphicRecursive(op, new Sin_OperatorDto());
        protected override void VisitSineWithRate1(Operator op) => ProcessPolymorphicRecursive(op, new SineWithRate1_OperatorDto());
        protected override void VisitSinH(Operator op) => ProcessPolymorphicRecursive(op, new SinH_OperatorDto());
        protected override void VisitSortOverDimension(Operator op) => ProcessPolymorphicRecursive(op, new SortOverDimension_OperatorDto());

        protected override void VisitSortOverInletsOutlet(Outlet outlet)
            => ProcessPolymorphicRecursive(outlet, new SortOverInlets_Outlet_OperatorDto());

        protected override void VisitSpectrum(Operator op) => ProcessPolymorphicRecursive(op, new Spectrum_OperatorDto());
        protected override void VisitSquareRoot(Operator op) => ProcessPolymorphicRecursive(op, new SquareRoot_OperatorDto());
        protected override void VisitSquash(Operator op) => ProcessPolymorphicRecursive(op, new Squash_OperatorDto());
        protected override void VisitSubtract(Operator op) => ProcessPolymorphicRecursive(op, new Subtract_OperatorDto());
        protected override void VisitSumFollower(Operator op) => ProcessPolymorphicRecursive(op, new SumFollower_OperatorDto());
        protected override void VisitSumOverDimension(Operator op) => ProcessPolymorphicRecursive(op, new SumOverDimension_OperatorDto());
        protected override void VisitTan(Operator op) => ProcessPolymorphicRecursive(op, new Tan_OperatorDto());
        protected override void VisitTanH(Operator op) => ProcessPolymorphicRecursive(op, new TanH_OperatorDto());
        protected override void VisitToggleTrigger(Operator op) => ProcessPolymorphicRecursive(op, new ToggleTrigger_OperatorDto());
        protected override void VisitTriangleWithRate1(Operator op) => ProcessPolymorphicRecursive(op, new TriangleWithRate1_OperatorDto());
        protected override void VisitTruncate(Operator op) => ProcessPolymorphicRecursive(op, new Truncate_OperatorDto());
        protected override void VisitXor(Operator op) => ProcessPolymorphicRecursive(op, new Xor_OperatorDto());

        // Helpers

        /// <see cref="ProcessPolymorphic" />
        private void ProcessPolymorphicRecursive(Operator op, IOperatorDto dto)
        {
            VisitOperatorBase(op);
            ProcessPolymorphic(op, dto);
        }

        private void ProcessPolymorphicRecursive(Outlet outlet, IOperatorDto dto)
        {
            VisitOutletBase(outlet);
            ProcessPolymorphic(outlet.Operator, dto);
        }

        /// <summary>
        /// In theory some of the code here only applies to a single type of operator,
        /// but it prevents errors to make this method handle more than it needs to.
        /// It was too easy to forget common properties like dimensions.
        /// </summary>
        private void ProcessPolymorphic(Operator op, IOperatorDto dto)
        {
            dto.Inputs = CollectionHelper.Repeat(op.Inlets.Count, PopInputDto)
                                         .Where(x => x != null)
                                         .ToArray();

            {
                if (dto is IOperatorDto_WithDimension castedDto)
                {
                    SetDimensionProperties(op, castedDto);
                }
            }

            {
                if (dto is OperatorDtoBase_WithCollectionRecalculation castedDto)
                {
                    var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
                    castedDto.CollectionRecalculationEnum = wrapper.CollectionRecalculation;
                }
            }

            {
                if (dto is IOperatorDto_WithInterpolation castedDto)
                {
                    var wrapper = new OperatorWrapper_WithInterpolation(op);
                    castedDto.InterpolationTypeEnum = wrapper.InterpolationType;
                }
            }

            {
                if (dto is IOperatorDto_WithInterpolation_AndFollowingMode castedDto)
                {
                    var wrapper = new OperatorWrapper_WithInterpolation_AndFollowingMode(op);
                    castedDto.FollowingModeEnum = wrapper.FollowingMode;
                }
            }

            {
                if (dto is Noise_OperatorDto castedDto)
                {
                    castedDto.ArrayDto = _calculatorCache.GetNoiseArrayDto();
                }
            }

            {
                if (dto is Random_OperatorDto castedDto)
                {
                    var wrapper = new OperatorWrapper_WithInterpolation(op);
                    castedDto.ArrayDto = _calculatorCache.GetRandomArrayDto(wrapper.InterpolationType);
                }
            }

            {
                if (dto is Number_OperatorDto castedDto)
                {
                    var wrapper = new Number_OperatorWrapper(op);
                    castedDto.Number = wrapper.Number;
                }
            }

            {
                if (dto is Reset_OperatorDto castedDto)
                {
                    var wrapper = new Reset_OperatorWrapper(op);
                    castedDto.Position = wrapper.Position;
                    castedDto.Name = op.Name;
                }
            }

            {
                if (dto is IOperatorDto_PositionReader castedDto)
                {
                    castedDto.Position = castedDto.Position ?? new Number_OperatorDto(double.NaN);
                }
            }

            {
                if (dto is IOperatorDto_WithAdditionalChannelDimension castedDto)
                {
                    castedDto.Channel = castedDto.Channel ?? new Number_OperatorDto(double.NaN);
                }
            }

            _stack.Push(dto);
        }

        private static void SetDimensionProperties(Operator op, IOperatorDto_WithDimension dto)
        {
            dto.StandardDimensionEnum = op.GetStandardDimensionEnumWithFallback();
            dto.CanonicalCustomDimensionName = NameHelper.ToCanonical(op.GetCustomDimensionNameWithFallback());
        }

        private InputDto PopInputDto() => InputDtoFactory.TryCreateInputDto(_stack.Pop());

        // Special Visitation

        protected override void InsertNumber(double number) => _stack.Push(new Number_OperatorDto { Number = number });

        protected override void InsertEmptyInput() => _stack.Push(null);

        /// <summary>
        /// As soon as you encounter a Derived Operator's Outlet, the evaluation has to take a completely different
        /// course.
        /// </summary>
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
                        StandardDimensionEnum = wrapper.Inlet.GetDimensionEnumWithFallback(),
                        CanonicalCustomDimensionName = NameHelper.ToCanonical(wrapper.Inlet.GetNameWithFallback()),
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
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

// ReSharper disable UnusedVariable

// ReSharper disable ImplicitlyCapturedClosure

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoToCalculatorVisitorBase : OperatorDtoVisitorBase_AfterDtoPreprocessing
    {
        private Dictionary<IOperatorDto, OperatorCalculatorBase> _dto_To_Calculator_Dictionary;
        private Dictionary<(DimensionEnum, string), VariableInput_OperatorCalculator> _dimensionToVariableInputCalculatorDictionary;
        private IList<ResettableOperatorTuple> _resettableOperatorTuples;

        protected readonly CalculatorCache _calculatorCache;
        protected Stack<OperatorCalculatorBase> _stack;

        public OperatorDtoToCalculatorVisitorBase(
            CalculatorCache calculatorCache,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _calculatorCache = calculatorCache ?? throw new NullException(() => calculatorCache);
        }

        public ToCalculatorResult Execute(IOperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            _stack = new Stack<OperatorCalculatorBase>();
            _dto_To_Calculator_Dictionary = new Dictionary<IOperatorDto, OperatorCalculatorBase>();
            _dimensionToVariableInputCalculatorDictionary = new Dictionary<(DimensionEnum, string), VariableInput_OperatorCalculator>();
            _resettableOperatorTuples = new List<ResettableOperatorTuple>();

            Visit_OperatorDto_Polymorphic(dto);

            OperatorCalculatorBase rootCalculator = _stack.Pop();

            var result = new ToCalculatorResult(
                rootCalculator,
                _dto_To_Calculator_Dictionary.Values.OfType<VariableInput_OperatorCalculator>().ToArray(),
                _resettableOperatorTuples);

            return result;
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            if (_dto_To_Calculator_Dictionary.TryGetValue(dto, out OperatorCalculatorBase calculator))
            {
                _stack.Push(calculator);
            }
            else
            {
                VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

                _dto_To_Calculator_Dictionary[dto] = _stack.Peek();
            }

            return dto;
        }

        /// <summary> Returned dto is the same as input dto, but that makes the caller code shorter. </summary>
        private IOperatorDto ProcessOperatorDto(IOperatorDto dto, Func<OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            int stackCountBefore = _stack.Count;

            Visit_OperatorDto_Base(dto);

            OperatorCalculatorBase calculator = createOperatorCalculatorDelegate();

            // Pop non-popped (unused) inputs.
            while (_stack.Count > stackCountBefore)
            {
                _stack.Pop();
            }

            _stack.Push(calculator);

            return dto;
        }

        private VariableInput_OperatorCalculator GetPositionOutputCalculator(IOperatorDto_WithDimension dto)
            => _dimensionToVariableInputCalculatorDictionary[(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName)];

        protected override InputDto VisitConstInputDto(InputDto inputDto)
        {
            _stack.Push(new Number_OperatorCalculator(inputDto.Const));

            return inputDto;
        }

        protected override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Add_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(
            AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new AllPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(
            AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new AllPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new And_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new AverageFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationContinuous(
            AverageOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
            => ProcessOperatorDto(
                dto,
                () => new AverageOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationUponReset(
            AverageOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
            => ProcessOperatorDto(
                dto,
                () => new AverageOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new AverageOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst(
            BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new BandPassFilterConstantPeakGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar(
            BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new BandPassFilterConstantPeakGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst(
            BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new BandPassFilterConstantTransitionGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar(
            BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new BandPassFilterConstantTransitionGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new ChangeTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(
            ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
            => ProcessOperatorDto(
                dto,
                () => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationContinuous(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(
            ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
            => ProcessOperatorDto(
                dto,
                () => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationUponReset(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(
            ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
            => ProcessOperatorDto(
                dto,
                () => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(
            ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
            => ProcessOperatorDto(
                dto,
                () => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () => new ClosestOverInlets_OperatorCalculator(_stack.Pop(), dto.Items.Select(x => _stack.Pop()).ToArray()));

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () => new ClosestOverInletsExp_OperatorCalculator(_stack.Pop(), dto.Items.Select(x => _stack.Pop()).ToArray()));

        protected override IOperatorDto Visit_Curve_OperatorDto_NoOriginShifting(Curve_OperatorDto_NoOriginShifting dto) => ProcessCurveOperator(dto);

        protected override IOperatorDto Visit_Curve_OperatorDto_WithOriginShifting(Curve_OperatorDto_WithOriginShifting dto)
            => ProcessCurveOperator(dto);

        private IOperatorDto ProcessCurveOperator(Curve_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () => OperatorCalculatorFactory.Create_Curve_OperatorCalculator(_stack.Pop(), dto.ArrayDto, dto.StandardDimensionEnum));

        protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Divide_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Equal_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new GreaterThanOrEqual_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new GreaterThan_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(
            HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new HighPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(
            HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new HighPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(
            HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new HighShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(
            HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new HighShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Hold_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new If_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto)
            => ProcessOperatorDto(
                dto,
                () => new InletsToDimension_OperatorCalculator_Block(dto.InputsExceptPosition.Select(x => _stack.Pop()).ToArray(), _stack.Pop()));

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto)
            => ProcessOperatorDto(
                dto,
                () => new InletsToDimension_OperatorCalculator_Stripe(dto.InputsExceptPosition.Select(x => _stack.Pop()).ToArray(), _stack.Pop()));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Block(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Cubic_LagBehind(Interpolate_OperatorDto_Cubic_LagBehind dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Cubic_LagBehind(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Cubic_LookAhead(Interpolate_OperatorDto_Cubic_LookAhead dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Cubic_LookAhead(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Hermite_LagBehind(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LookAhead(Interpolate_OperatorDto_Hermite_LookAhead dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Hermite_LookAhead(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind(Interpolate_OperatorDto_Line_LagBehind dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Line_LagBehind(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LookAhead(Interpolate_OperatorDto_Line_LookAhead dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Line_LookAhead(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Stripe_LagBehind(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LookAhead(Interpolate_OperatorDto_Stripe_LookAhead dto)
            => ProcessOperatorDto(
                dto,
                () => new Interpolate_OperatorCalculator_Stripe_LookAhead(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new LessThanOrEqual_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new LessThan_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Loop_OperatorDto(Loop_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    OperatorCalculatorBase skipCalculator = _stack.Pop();
                    OperatorCalculatorBase loopStartMarkerCalculator = _stack.Pop();
                    OperatorCalculatorBase loopEndMarkerCalculator = _stack.Pop();
                    OperatorCalculatorBase releaseEndMarkerCalculator = _stack.Pop();
                    OperatorCalculatorBase noteDurationCalculator = _stack.Pop();
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Loop_OperatorCalculator_WithPositionOutput(
                        positionCalculator,
                        skipCalculator,
                        loopStartMarkerCalculator,
                        loopEndMarkerCalculator,
                        releaseEndMarkerCalculator,
                        noteDurationCalculator);
                });

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(
            LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new LowPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(
            LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new LowPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(
            LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new LowShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(
            LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new LowShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () => new MaxFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationContinuous(
            MaxOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
            => ProcessOperatorDto(
                dto,
                () => new MaxOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationUponReset(
            MaxOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
            => ProcessOperatorDto(
                dto,
                () => new MaxOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));

        protected override IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () => new MinFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationContinuous(
            MinOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
            => ProcessOperatorDto(
                dto,
                () => new MinOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationUponReset(
            MinOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
            => ProcessOperatorDto(
                dto,
                () => new MinOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));

        protected override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Multiply_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));

        protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Negative_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    NoiseCalculator noiseCalculator = _calculatorCache.GetNoiseCalculator(dto.OperationIdentity);
                    return new Noise_OperatorCalculator(_stack.Pop(), noiseCalculator);
                });

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(
            NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(
            NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new NotEqual_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Not_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Number_OperatorCalculator(dto.Number));

        protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Or_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(
            PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
            => ProcessOperatorDto(
                dto,
                () => new PeakingEQFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(
            PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
            => ProcessOperatorDto(
                dto,
                () => new PeakingEQFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));

        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Power_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new PulseTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    RandomCalculator_Block randomCalculator = _calculatorCache.GetRandomCalculator_Block(dto.OperationIdentity);
                    return new Random_OperatorCalculator_Block(randomCalculator, _stack.Pop(), _stack.Pop());
                });

        protected override IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    RandomCalculator_Stripe randomCalculator = _calculatorCache.GetRandomCalculator_Stripe(dto.OperationIdentity);
                    return new Random_OperatorCalculator_Stripe(randomCalculator, _stack.Pop(), _stack.Pop());
                });

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
            => ProcessOperatorDto(dto, () => new RangeOverDimension_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto)
            => ProcessOperatorDto(dto, () => new RangeOverDimension_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(
            RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto)
            => ProcessOperatorDto(dto, () => new RangeOverDimension_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Remainder_OperatorDto(Remainder_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Remainder_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    OperatorCalculatorBase calculator = _stack.Peek();
                    _resettableOperatorTuples.Add(new ResettableOperatorTuple(calculator, dto.Name, dto.Position));
                    return calculator;
                });

        protected override IOperatorDto Visit_Round_OperatorDto_StepOne_ZeroOffset(Round_OperatorDto_StepOne_ZeroOffset dto)
            => ProcessOperatorDto(dto, () => new Round_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Round_OperatorDto_WithOffset(Round_OperatorDto_WithOffset dto)
            => ProcessOperatorDto(dto, () => new Round_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Round_OperatorDto_ZeroOffset(Round_OperatorDto_ZeroOffset dto)
            => ProcessOperatorDto(dto, () => new Round_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_MonoToStereo(SampleWithRate1_OperatorDto_MonoToStereo dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    ArrayDto arrayDto = dto.ArrayDtos.Single();
                    ICalculatorWithPosition underlyingCalculator = ArrayCalculatorFactory.CreateArrayCalculator(arrayDto);
                    return new SampleWithRate1_OperatorCalculator_MonoToStereo(_stack.Pop(), underlyingCalculator);
                });

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_NoChannelConversion(SampleWithRate1_OperatorDto_NoChannelConversion dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    IList<ICalculatorWithPosition> underlyingCalculators =
                        dto.ArrayDtos.Select(ArrayCalculatorFactory.CreateArrayCalculator).ToArray();

                    return new SampleWithRate1_OperatorCalculator_NoChannelConversion(_stack.Pop(), _stack.Pop(), underlyingCalculators);
                });

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_StereoToMono(SampleWithRate1_OperatorDto_StereoToMono dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    IList<ICalculatorWithPosition> underlyingCalculators =
                        dto.ArrayDtos.Select(ArrayCalculatorFactory.CreateArrayCalculator).ToArray();

                    return new SampleWithRate1_OperatorCalculator_StereoToMono(_stack.Pop(), underlyingCalculators);
                });

        protected override IOperatorDto Visit_SineWithRate1_OperatorDto(SineWithRate1_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new SineWithRate1_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationContinuous(
            SortOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
            => ProcessOperatorDto(
                dto,
                () => new SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationUponReset(
            SortOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
            => ProcessOperatorDto(
                dto,
                () => new SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new SortOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray(), _stack.Pop()));

        protected override IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () => new Spectrum_OperatorCalculator(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_Squash_OperatorDto_WithOrigin(Squash_OperatorDto_WithOrigin dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_WithOrigin_WithPositionOutput(positionCalculator, factorCalculator, originCalculator);
                });

        protected override IOperatorDto Visit_Squash_OperatorDto_ZeroOrigin(Squash_OperatorDto_ZeroOrigin dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_ZeroOrigin_WithPositionOutput(positionCalculator, factorCalculator);
                });

        protected override IOperatorDto Visit_Squash_OperatorDto_ConstFactor_WithOriginShifting(
            Squash_OperatorDto_ConstFactor_WithOriginShifting dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_ConstFactor_WithOriginShifting_WithPositionOutput(positionCalculator, factorCalculator);
                });

        protected override IOperatorDto Visit_Squash_OperatorDto_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarFactor_WithPhaseTracking dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_VarFactor_WithPhaseTracking_WithPositionOutput(positionCalculator, factorCalculator);
                });

        protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
            => ProcessOperatorDto(dto, () => new SumFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationContinuous(
            SumOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
            => ProcessOperatorDto(
                dto,
                () => new SumOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationUponReset(
            SumOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
            => ProcessOperatorDto(
                dto,
                () => new SumOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    _stack.Pop(),
                    GetPositionOutputCalculator(dto)));

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new ToggleTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_TriangleWithRate1_OperatorDto(TriangleWithRate1_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new TriangleWithRate1_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
            => ProcessOperatorDto(
                dto,
                () =>
                {
                    var calculator = new VariableInput_OperatorCalculator(
                        dto.StandardDimensionEnum,
                        dto.CanonicalCustomDimensionName,
                        dto.Position,
                        dto.DefaultValue);

                    _dimensionToVariableInputCalculatorDictionary[(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName)] = calculator;
                    return calculator;
                });

        protected override IOperatorDto Visit_Sin_OperatorDto(Sin_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Sin_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Cos_OperatorDto(Cos_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Cos_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Tan_OperatorDto(Tan_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Tan_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_SinH_OperatorDto(SinH_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new SinH_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_CosH_OperatorDto(CosH_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Cos_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_TanH_OperatorDto(TanH_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Tan_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_ArcSin_OperatorDto(ArcSin_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new ArcSin_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_ArcCos_OperatorDto(ArcCos_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new ArcCos_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_ArcTan_OperatorDto(ArcTan_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new ArcTan_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_LogN_OperatorDto(LogN_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new LogN_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Ln_OperatorDto(Ln_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Ln_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_SquareRoot_OperatorDto(SquareRoot_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new SquareRoot_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Sign_OperatorDto(Sign_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Sign_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Xor_OperatorDto(Xor_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Xor_OperatorCalculator(_stack.Pop(), _stack.Pop()));

        protected override IOperatorDto Visit_Ceiling_OperatorDto(Ceiling_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Ceiling_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Floor_OperatorDto(Floor_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Floor_OperatorCalculator(_stack.Pop()));

        protected override IOperatorDto Visit_Truncate_OperatorDto(Truncate_OperatorDto dto)
            => ProcessOperatorDto(dto, () => new Truncate_OperatorCalculator(_stack.Pop()));
    }
}
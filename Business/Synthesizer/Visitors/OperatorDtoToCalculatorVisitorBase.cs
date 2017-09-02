using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
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
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        protected override InputDto VisitConstInputDto(InputDto inputDto)
        {
            _stack.Push(new Number_OperatorCalculator(inputDto.Const));

            return inputDto;
        }

        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Absolute_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Add_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new AllPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new AllPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new And_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new AverageFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessOperatorDto(dto, () => new AverageOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessOperatorDto(dto, () => new AverageOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new AverageOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantPeakGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantPeakGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantTransitionGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar(BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantTransitionGain_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new ChangeTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInlets_OperatorCalculator(_stack.Pop(), dto.Items.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInletsExp_OperatorCalculator(_stack.Pop(), dto.Items.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_NoOriginShifting(Curve_OperatorDto_NoOriginShifting dto)
        {
            return ProcessCurveOperator(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_WithOriginShifting(Curve_OperatorDto_WithOriginShifting dto)
        {
            return ProcessCurveOperator(dto);
        }

        private IOperatorDto ProcessCurveOperator(Curve_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.Create_Curve_OperatorCalculator(_stack.Pop(), dto.ArrayDto, dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Equal_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThanOrEqual_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThan_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new HighPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new HighPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new HighShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new HighShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Hold_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto)
        {
            return ProcessOperatorDto(dto, () => new InletsToDimension_OperatorCalculator_Block(dto.Inputs.Select(x => _stack.Pop()).ToArray(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto)
        {
            return ProcessOperatorDto(dto, () => new InletsToDimension_OperatorCalculator_Stripe(dto.Inputs.Select(x => _stack.Pop()).ToArray(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_Block(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_CubicAbruptSlope(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_CubicEquidistant(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_CubicSmoothSlope_LagBehind(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_Hermite_LagBehind(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate(_stack.Pop(), dto.SamplingRate.Const, _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_Line_LagBehind_VarSamplingRate(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
        {
            return ProcessOperatorDto(dto, () => new Interpolate_OperatorCalculator_Stripe_LagBehind(_stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new LessThanOrEqual_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new LessThan_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new Loop_OperatorCalculator_WithPositionOutput(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new LowPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new LowPassFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new LowShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new LowShelfFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new MaxFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new MinFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Multiply_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Negative_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () =>
            { 
                NoiseCalculator noiseCalculator = _calculatorCache.GetNoiseCalculator(dto.OperationIdentity);
                return new Noise_OperatorCalculator(_stack.Pop(), noiseCalculator);
            });
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new NotEqual_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Not_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator(dto.Number));
        }

        protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Or_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return ProcessOperatorDto(dto, () => new PeakingEQFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return ProcessOperatorDto(dto, () => new PeakingEQFilter_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dto.TargetSamplingRate));
        }

        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new PulseTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
        {
            return ProcessOperatorDto(dto, () =>
            {
                RandomCalculator_Block randomCalculator = _calculatorCache.GetRandomCalculator_Block(dto.OperationIdentity);
                return new Random_OperatorCalculator_Block(randomCalculator, _stack.Pop(), _stack.Pop());
            });
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto)
        {
            return ProcessOperatorDto(dto, () =>
            {
                RandomCalculator_Stripe randomCalculator = _calculatorCache.GetRandomCalculator_Stripe(dto.OperationIdentity);
                return new Random_OperatorCalculator_Stripe(randomCalculator, _stack.Pop(), _stack.Pop());
            });
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverDimension_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverDimension_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverDimension_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Remainder_OperatorDto(Remainder_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Remainder_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () =>
            {
                OperatorCalculatorBase calculator = _stack.Peek();
                _resettableOperatorTuples.Add(new ResettableOperatorTuple(calculator, dto.Name, dto.Position));
                return calculator;
            });
        }

        protected override IOperatorDto Visit_Round_OperatorDto_StepOne_ZeroOffset(Round_OperatorDto_StepOne_ZeroOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_WithOffset(Round_OperatorDto_WithOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_ZeroOffset(Round_OperatorDto_ZeroOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_MonoToStereo(SampleWithRate1_OperatorDto_MonoToStereo dto)
        {
            return ProcessOperatorDto(dto, () =>
            {
                ArrayDto arrayDto = dto.ArrayDtos.Single();
                ICalculatorWithPosition underlyingCalculator = ArrayCalculatorFactory.CreateArrayCalculator(arrayDto);
                return new SampleWithRate1_OperatorCalculator_MonoToStereo(_stack.Pop(), underlyingCalculator);
            });
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_NoChannelConversion(SampleWithRate1_OperatorDto_NoChannelConversion dto)
        {
            return ProcessOperatorDto(dto, () =>
            {
                IList<ICalculatorWithPosition> underlyingCalculators = dto.ArrayDtos.Select(x1 => ArrayCalculatorFactory.CreateArrayCalculator(x1)).ToArray();
                return new SampleWithRate1_OperatorCalculator_NoChannelConversion(_stack.Pop(), _stack.Pop(), underlyingCalculators);
            });
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_StereoToMono(SampleWithRate1_OperatorDto_StereoToMono dto)
        {
            return ProcessOperatorDto(dto, () =>
            {
                IList<ICalculatorWithPosition> underlyingCalculators = dto.ArrayDtos.Select(x1 => ArrayCalculatorFactory.CreateArrayCalculator(x1)).ToArray();
                return new SampleWithRate1_OperatorCalculator_StereoToMono(_stack.Pop(), underlyingCalculators);
            });
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new SetDimension_OperatorCalculator(_stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_SineWithRate1_OperatorDto(SineWithRate1_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new SineWithRate1_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessOperatorDto(dto, () => new SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessOperatorDto(dto, () => new SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new SortOverInlets_OperatorCalculator(dto.Inputs.Select(x => _stack.Pop()).ToArray(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Spectrum_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_WithOrigin(Squash_OperatorDto_WithOrigin dto)
        {
            return ProcessOperatorDto(
                dto,
                () =>
                {
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_WithOrigin_WithPositionOutput(positionCalculator, factorCalculator, originCalculator);
                });
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_ZeroOrigin(Squash_OperatorDto_ZeroOrigin dto)
        {
            return ProcessOperatorDto(
                dto,
                () =>
                {
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_ZeroOrigin_WithPositionOutput(positionCalculator, factorCalculator);
                });
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_ConstFactor_WithOriginShifting(Squash_OperatorDto_ConstFactor_WithOriginShifting dto)
        {
            return ProcessOperatorDto(
                dto,
                () =>
                {
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_ConstFactor_WithOriginShifting_WithPositionOutput(positionCalculator, factorCalculator);
                });
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarFactor_WithPhaseTracking dto)
        {
            return ProcessOperatorDto(
                dto,
                () =>
                {
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase signalCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase factorCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase originCalculator = _stack.Pop();
                    // ReSharper disable once UnusedVariable
                    OperatorCalculatorBase positionCalculator = _stack.Pop();

                    return new Squash_OperatorCalculator_VarFactor_WithPhaseTracking_WithPositionOutput(positionCalculator, factorCalculator);
                });
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new SumFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessOperatorDto(dto, () => new SumOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessOperatorDto(dto, () => new SumOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), GetPositionOutputCalculator(dto)));
        }

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new ToggleTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_TriangleWithRate1_OperatorDto(TriangleWithRate1_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new TriangleWithRate1_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () =>
            { 
                var calculator = new VariableInput_OperatorCalculator(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName, dto.Position, dto.DefaultValue);
                _dimensionToVariableInputCalculatorDictionary[(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName)] = calculator;
                return calculator;
            });
        }

        // Helpers

        /// <summary>
        /// Returned dto is the same as input dto,
        /// but that makes the caller code shorter.
        /// </summary>
        private IOperatorDto ProcessOperatorDto(IOperatorDto dto, Func<OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            if (!_dto_To_Calculator_Dictionary.TryGetValue(dto, out OperatorCalculatorBase calculator))
            {
                int stackCountBefore = _stack.Count;

                base.Visit_OperatorDto_Base(dto);

                calculator = createOperatorCalculatorDelegate();

                // Pop non-popped (unused) inputs.
                while (_stack.Count > stackCountBefore)
                {
                    _stack.Pop();
                }

                _dto_To_Calculator_Dictionary[dto] = calculator;
            }

            _stack.Push(calculator);

            return dto;
        }

        private VariableInput_OperatorCalculator GetPositionOutputCalculator(IOperatorDto_WithDimension dto)
        {
            return _dimensionToVariableInputCalculatorDictionary[(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName)];
        }
    }
}
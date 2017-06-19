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

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoToCalculatorVisitorBase : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private readonly double _targetSamplingRate;
        private readonly int _samplesBetweenApplyFilterVariables;
        private Dictionary<VariableInput_OperatorDto, VariableInput_OperatorCalculator> _variableInput_OperatorDto_To_Calculator_Dictionary;
        private IList<ResettableOperatorTuple> _resettableOperatorTuples;

        protected readonly CalculatorCache _calculatorCache;
        protected DimensionStackCollection _dimensionStackCollection;
        protected Stack<OperatorCalculatorBase> _stack;

        public OperatorDtoToCalculatorVisitorBase(
            int targetSamplingRate, 
            double secondsBetweenApplyFilterVariables,
            CalculatorCache calculatorCache,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _targetSamplingRate = targetSamplingRate;
            _calculatorCache = calculatorCache ?? throw new NullException(() => calculatorCache);

            _samplesBetweenApplyFilterVariables = VisitorHelper.GetSamplesBetweenApplyFilterVariables(secondsBetweenApplyFilterVariables, targetSamplingRate);
        }

        public ToCalculatorResult Execute(IOperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            _stack = new Stack<OperatorCalculatorBase>();
            _dimensionStackCollection = new DimensionStackCollection();
            _variableInput_OperatorDto_To_Calculator_Dictionary = new Dictionary<VariableInput_OperatorDto, VariableInput_OperatorCalculator>();
            _resettableOperatorTuples = new List<ResettableOperatorTuple>();

            Visit_OperatorDto_Polymorphic(dto);

            OperatorCalculatorBase rootCalculator = _stack.Pop();

            VisitorHelper.AssertDimensionStacksCountsAre1(_dimensionStackCollection);

            var result = new ToCalculatorResult(
                rootCalculator,
                _dimensionStackCollection,
                _variableInput_OperatorDto_To_Calculator_Dictionary.Values.ToArray(),
                _resettableOperatorTuples);

            return result;
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        protected override IOperatorDto Visit_Absolute_OperatorDto_VarNumber(Absolute_OperatorDto_VarNumber dto)
        {
            return ProcessOperatorDto(dto, () => new Absolute_OperatorCalculator_VarNumber(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateAddCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateAddCalculator_Vars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_AllVars(AllPassFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new AllPassFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_ManyConsts(AllPassFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new AllPassFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.CenterFrequency, dto.Width, _targetSamplingRate));
        }
        
        protected override IOperatorDto Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new And_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_AverageFollower_OperatorDto_AllVars(AverageFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new AverageFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new AverageOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new AverageOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto_Vars(AverageOverInlets_OperatorDto_Vars dto)
        {
            return ProcessOperatorDto(dto, () => new AverageOverInlets_OperatorCalculator(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth(BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantPeakGain_OperatorCalculator_ConstCenterFrequency_ConstWidth(_stack.Pop(), dto.CenterFrequency, dto.Width, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth(BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantPeakGain_OperatorCalculator_VarCenterFrequency_VarWidth(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth(BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstWidth(_stack.Pop(), dto.CenterFrequency, dto.Width, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth(BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarWidth(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto_VarPassThrough_VarReset(ChangeTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            return ProcessOperatorDto(dto, () => new ChangeTrigger_OperatorCalculator_VarPassThrough_VarReset(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInletsExp_OperatorCalculator_VarInput_2ConstItems(_stack.Pop(), dto.Item1, dto.Item2));
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInletsExp_OperatorCalculator_VarInput_ConstItems(_stack.Pop(), dto.Items));
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInletsExp_OperatorCalculator_VarInput_VarItems(_stack.Pop(), dto.ItemOperatorDtos.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInlets_OperatorCalculator_VarInput_2ConstItems(_stack.Pop(), dto.Item1, dto.Item2));
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInlets_OperatorCalculator_VarInput_ConstItems(_stack.Pop(), dto.Items));
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInlets_OperatorCalculator_VarInput_VarItems(_stack.Pop(), dto.ItemOperatorDtos.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
        {
            return ProcessCurveOperator(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
        {
            return ProcessCurveOperator(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
        {
            return ProcessCurveOperator(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
        {
            return ProcessCurveOperator(dto);
        }

        private IOperatorDto ProcessCurveOperator(Curve_OperatorDtoBase_WithoutMinX dto)
        {
            base.Visit_OperatorDto_Base(dto);

            OperatorCalculatorBase calculator = OperatorCalculatorFactory.Create_Curve_OperatorCalculator(
                dto.ArrayDto,
                dto.StandardDimensionEnum,
                _dimensionStackCollection);

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new DimensionToOutlets_OperatorCalculator(_stack.Pop(), dto.OutletPosition, dimensionStack));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_ConstA_ConstB_VarOrigin(Divide_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_ConstB_VarOrigin(dto.A, dto.B, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_ConstA_VarB_ConstOrigin(Divide_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_VarB_ConstOrigin(dto.A, _stack.Pop(), dto.Origin));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_ConstA_VarB_VarOrigin(Divide_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_VarB_VarOrigin(dto.A, _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(Divide_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_VarB_ZeroOrigin(dto.A, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_ConstB_ConstOrigin(Divide_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_ConstB_ConstOrigin(_stack.Pop(), dto.B, dto.Origin));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_ConstB_VarOrigin(Divide_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_ConstB_VarOrigin(_stack.Pop(), dto.B, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(Divide_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_ConstB_ZeroOrigin(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_VarB_ConstOrigin(Divide_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_VarB_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_VarB_VarOrigin(Divide_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_VarB_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(Divide_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_VarB_ZeroOrigin(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Equal_OperatorDto_VarA_ConstB(Equal_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new Equal_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_Equal_OperatorDto_VarA_VarB(Equal_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Equal_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio(Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_ConstLow_ConstHigh_VarRatio(dto.Low, dto.High, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio(Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_ConstLow_VarHigh_ConstRatio(dto.Low, _stack.Pop(), dto.Ratio));
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto_ConstLow_VarHigh_VarRatio(Exponent_OperatorDto_ConstLow_VarHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_ConstLow_VarHigh_VarRatio(dto.Low, _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio(Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_ConstHigh_ConstRatio(_stack.Pop(), dto.High, dto.Ratio));
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto_VarLow_ConstHigh_VarRatio(Exponent_OperatorDto_VarLow_ConstHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_ConstHigh_VarRatio(_stack.Pop(), dto.High, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto_VarLow_VarHigh_ConstRatio(Exponent_OperatorDto_VarLow_VarHigh_ConstRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_VarHigh_ConstRatio(_stack.Pop(), _stack.Pop(), dto.Ratio));
        }

        protected override IOperatorDto Visit_Exponent_OperatorDto_VarLow_VarHigh_VarRatio(Exponent_OperatorDto_VarLow_VarHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_VarHigh_VarRatio(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new GetDimension_OperatorCalculator(dimensionStack));
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(GreaterThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThanOrEqual_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(GreaterThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThanOrEqual_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto_VarA_ConstB(GreaterThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThan_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto_VarA_VarB(GreaterThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThan_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_AllVars(HighPassFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new HighPassFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_ManyConsts(HighPassFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new HighPassFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.MinFrequency, dto.BlobVolume, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_AllVars(HighShelfFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new HighShelfFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_ManyConsts(HighShelfFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new HighShelfFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.TransitionFrequency, dto.TransitionSlope, dto.DBGain, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_Hold_OperatorDto_VarSignal(Hold_OperatorDto_VarSignal dto)
        {
            return ProcessOperatorDto(dto, () => new Hold_OperatorCalculator_VarSignal(_stack.Pop()));
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_ConstThen_ConstElse(_stack.Pop(), dto.Then, dto.Else));
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_ConstThen_VarElse(_stack.Pop(), dto.Then, _stack.Pop()));
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_VarThen_ConstElse(_stack.Pop(), _stack.Pop(), dto.Else));
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_VarThen_VarElse(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new InletsToDimension_OperatorCalculator_Block(dto.Vars.Select(x => _stack.Pop()).ToArray(), dimensionStack));
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new InletsToDimension_OperatorCalculator_Stripe(dto.Vars.Select(x => _stack.Pop()).ToArray(), dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Block(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_CubicAbruptSlope(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_CubicEquidistant(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_CubicSmoothSlope_LagBehind(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Hermite_LagBehind(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate(_stack.Pop(), dto.SamplingRate, dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Line_LagBehind_VarSamplingRate(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Stripe_LagBehind(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(LessThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThanOrEqual_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_VarA_VarB(LessThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThanOrEqual_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_LessThan_OperatorDto_VarA_ConstB(LessThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThan_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_LessThan_OperatorDto_VarA_VarB(LessThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThan_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(_stack.Pop(), dto.SkipAndLoopStartMarker, dto.LoopEndMarker, dimensionStack));
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(_stack.Pop(), dto.SkipAndLoopStartMarker, _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_ManyConstants(_stack.Pop(), dto.Skip, dto.LoopStartMarker, dto.LoopEndMarker, dto.ReleaseEndMarker, _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_NoSkipOrRelease(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants(_stack.Pop(), dto.LoopStartMarker, dto.LoopEndMarker, _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_AllVars(LowPassFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new LowPassFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_ManyConsts(LowPassFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new LowPassFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.MaxFrequency, dto.BlobVolume, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_AllVars(LowShelfFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new LowShelfFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_ManyConsts(LowShelfFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new LowShelfFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.TransitionFrequency, dto.TransitionSlope, dto.DBGain, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_MaxFollower_OperatorDto_AllVars(MaxFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MaxFollower_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MaxOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MaxOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_1Var_1Const(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_2Vars(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_AllVars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_MinFollower_OperatorDto_AllVars(MinFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MinFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MinOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MinOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_1Var_1Const(MinOverInlets_OperatorDto_1Var_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_1Var_1Const(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_2Vars(MinOverInlets_OperatorDto_2Vars dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_2Vars(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_AllVars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_ConstA_ConstB_VarOrigin(dto.A, dto.B, _stack.Pop()));
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_ConstOrigin(dto.A, _stack.Pop(), dto.Origin));
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_VarOrigin(dto.A, _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_ConstOrigin(_stack.Pop(), dto.B, dto.Origin));
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_VarOrigin(_stack.Pop(), dto.B, _stack.Pop()));
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_VarB_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin));
        }

        protected override IOperatorDto Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_VarB_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateMultiplyCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateMultiplyCalculator_Vars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override IOperatorDto Visit_Negative_OperatorDto_VarNumber(Negative_OperatorDto_VarNumber dto)
        {
            return ProcessOperatorDto(dto, () => new Negative_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            base.Visit_Noise_OperatorDto(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            // Cast to concrete calculator type for performance.
            NoiseCalculator noiseCalculator = _calculatorCache.GetNoiseCalculator(dto.OperatorID);

            var calculator = new Noise_OperatorCalculator(noiseCalculator, dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_AllVars(NotchFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_ManyConsts(NotchFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.CenterFrequency, dto.Width, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto_VarA_ConstB(NotEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new NotEqual_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto_VarA_VarB(NotEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new NotEqual_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Not_OperatorDto_VarNumber(Not_OperatorDto_VarNumber dto)
        {
            return ProcessOperatorDto(dto, () => new Not_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator(dto.Number));
        }

        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator(double.NaN));
        }

        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator_One());
        }

        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator_Zero());
        }

        protected override IOperatorDto Visit_OneOverX_OperatorDto_VarNumber(OneOverX_OperatorDto_VarNumber dto)
        {
            return ProcessOperatorDto(dto, () => new OneOverX_OperatorCalculator(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Or_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_AllVars(PeakingEQFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new PeakingEQFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_ManyConsts(PeakingEQFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new PeakingEQFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.CenterFrequency, dto.Width, dto.DBGain, _targetSamplingRate));
        }

        protected override IOperatorDto Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_ConstBase_VarExponent(dto.Base, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_VarBase_ConstExponent(_stack.Pop(), dto.Exponent));
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_VarBase_VarExponent(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_Exponent2(Power_OperatorDto_VarBase_Exponent2 dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_VarBase_Exponent2(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_Exponent3(Power_OperatorDto_VarBase_Exponent3 dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_VarBase_Exponent3(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_Exponent4(Power_OperatorDto_VarBase_Exponent4 dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_VarBase_Exponent4(_stack.Pop()));
        }

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto_VarPassThrough_VarReset(PulseTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            return ProcessOperatorDto(dto, () => new PulseTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_NoOriginShifting(dto.Frequency, dto.Width, dimensionStack));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_WithOriginShifting(dto.Frequency, dto.Width, dimensionStack));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_VarWidth_NoOriginShifting(dto.Frequency, _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_VarWidth_WithOriginShifting(dto.Frequency, _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_ConstWidth_NoPhaseTracking(_stack.Pop(), dto.Width, dimensionStack));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_ConstWidth_WithPhaseTracking(_stack.Pop(), dto.Width, dimensionStack));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_VarWidth_NoPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_VarWidth_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
        {
            base.Visit_OperatorDto_Base(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            RandomCalculator_Block randomCalculator = _calculatorCache.GetRandomCalculator_Block(dto.OperatorID);

            var calculator = new Random_OperatorCalculator_Block_VarFrequency(randomCalculator, _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto)
        {
            base.Visit_OperatorDto_Base(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            RandomCalculator_Stripe randomCalculator = _calculatorCache.GetRandomCalculator_Stripe(dto.OperatorID);

            var calculator = new Random_OperatorCalculator_Stripe_VarFrequency(randomCalculator, _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new RangeOverDimension_OperatorCalculator_OnlyConsts(dto.From, dto.Till, dto.Step, dimensionStack));
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new RangeOverDimension_OperatorCalculator_OnlyVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new RangeOverDimension_OperatorCalculator_WithConsts_AndStepOne(dto.From, dto.Till, dimensionStack));
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverOutlets_OperatorCalculator_ConstFrom_VarStep(dto.From, _stack.Pop(), dto.OutletPosition));
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverOutlets_OperatorCalculator_VarFrom_VarStep(_stack.Pop(), _stack.Pop(), dto.OutletPosition));
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_NoOriginShifting(Reverse_OperatorDto_ConstFactor_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_ConstFactor_NoOriginShifting(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_WithOriginShifting(Reverse_OperatorDto_ConstFactor_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_ConstFactor_WithOriginShifting(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_NoPhaseTracking(Reverse_OperatorDto_VarFactor_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_VarFactor_NoPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_WithPhaseTracking(Reverse_OperatorDto_VarFactor_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_VarFactor_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_ConstSignal(dto.Signal, _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_ConstStep_ConstOffset(_stack.Pop(), dto.Step, dto.Offset));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_ConstStep_VarOffset(_stack.Pop(), dto.Step, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_ConstStep_ZeroOffset(_stack.Pop(), dto.Step));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_StepOne_OffsetZero(_stack.Pop()));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_VarStep_ConstOffset(_stack.Pop(), _stack.Pop(), dto.Offset));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_VarStep_VarOffset(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_VarStep_ZeroOffset(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
        {
            return Process_Sample_OperatorDto_SingleInputChannel_SingleOutputChannel(dto, (dimensionStack, underlyingCalculator) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_ConstFrequency_MonoToStereo_NoOriginShifting(dto.Frequency, underlyingCalculator, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
        {
            return Process_Sample_OperatorDto_SingleInputChannel_SingleOutputChannel(dto, (dimensionStack, underlyingCalculator) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_ConstFrequency_MonoToStereo_WithOriginShifting(dto.Frequency, underlyingCalculator, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannel_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, underlyingCalculators, dimensionStack, channelDimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannels_SingleOutputChannel(dto, (dimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_ConstFrequency_StereoToMono_NoOriginShifting(dto.Frequency, underlyingCalculators, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannels_SingleOutputChannel(dto, (dimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_ConstFrequency_StereoToMono_WithOriginShifting(dto.Frequency, underlyingCalculators, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannel_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, underlyingCalculators, dimensionStack, channelDimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_SingleInputChannel_SingleOutputChannel(dto, (dimensionStack, underlyingCalculator) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_VarFrequency_MonoToStereo_NoPhaseTracking(_stack.Pop(), underlyingCalculator, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_SingleInputChannel_SingleOutputChannel(dto, (dimensionStack, underlyingCalculator) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_VarFrequency_MonoToStereo_WithPhaseTracking(_stack.Pop(), underlyingCalculator, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannel_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), underlyingCalculators, dimensionStack, channelDimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannels_SingleOutputChannel(dto, (dimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_VarFrequency_StereoToMono_NoPhaseTracking(_stack.Pop(), underlyingCalculators, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannels_SingleOutputChannel(dto, (dimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_VarFrequency_StereoToMono_WithPhaseTracking(_stack.Pop(), underlyingCalculators, dimensionStack);
            });
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_MultipleInputChannel_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, underlyingCalculators) =>
            {
                // ReSharper disable once ConvertToLambdaExpression
                return new Sample_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), underlyingCalculators, dimensionStack, channelDimensionStack);
            });
        }

        private IOperatorDto Process_Sample_OperatorDto_SingleInputChannel_SingleOutputChannel(
            ISample_OperatorDto_WithSampleID dto,
            Func<DimensionStack, ICalculatorWithPosition, OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            ArrayDto arrayDto = dto.ArrayDtos.Single();
            ICalculatorWithPosition underlyingCalculator = ArrayCalculatorFactory.CreateArrayCalculator(arrayDto);
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = createOperatorCalculatorDelegate(dimensionStack, underlyingCalculator);
            _stack.Push(calculator);

            return dto;
        }

        /// <param name="createOperatorCalculatorDelegate"> Second DimensionStack parameter is the channelDimensionStack </param>
        private IOperatorDto Process_Sample_OperatorDto_MultipleInputChannel_MultipleOutputChannels(
            ISample_OperatorDto_WithSampleID dto,
            Func<DimensionStack, DimensionStack, IList<ICalculatorWithPosition>, OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            IList<ICalculatorWithPosition> underlyingCalculators = dto.ArrayDtos.Select(x => ArrayCalculatorFactory.CreateArrayCalculator(x)).ToArray();
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            DimensionStack channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);

            var calculator = createOperatorCalculatorDelegate(dimensionStack, channelDimensionStack, underlyingCalculators);
            _stack.Push(calculator);

            return dto;
        }

        private IOperatorDto Process_Sample_OperatorDto_MultipleInputChannels_SingleOutputChannel(
            ISample_OperatorDto_WithSampleID dto,
            Func<DimensionStack, IList<ICalculatorWithPosition>, OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            IList<ICalculatorWithPosition> underlyingCalculators = dto.ArrayDtos.Select(x => ArrayCalculatorFactory.CreateArrayCalculator(x)).ToArray();
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = createOperatorCalculatorDelegate(dimensionStack, underlyingCalculators);
            _stack.Push(calculator);

            return dto;
        }
        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Scaler_OperatorDto_AllVars(Scaler_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new Scaler_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Scaler_OperatorDto_ManyConsts(Scaler_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new Scaler_OperatorCalculator_ManyConsts(_stack.Pop(), dto.SourceValueA, dto.SourceValueB, dto.TargetValueA, dto.TargetValueB));
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_ConstNumber(SetDimension_OperatorDto_VarPassThrough_ConstNumber dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SetDimension_OperatorCalculator_VarPassThrough_ConstNumber(_stack.Pop(), dto.Number, dimensionStack));
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_VarNumber(SetDimension_OperatorDto_VarPassThrough_VarNumber dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SetDimension_OperatorCalculator_VarPassThrough_VarNumber(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Shift_OperatorCalculator_VarSignal_ConstDistance(_stack.Pop(), dto.Distance, dimensionStack));
        }

        protected override IOperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Shift_OperatorCalculator_VarSignal_VarDistance(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SortOverInlets_OperatorCalculator(dto.Vars.Select(x => _stack.Pop()).ToArray(), dimensionStack));
        }

        protected override IOperatorDto Visit_Spectrum_OperatorDto_AllVars(Spectrum_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Spectrum_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(_stack.Pop(), dto.Factor, dto.Origin, dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(_stack.Pop(), dto.Factor, _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_WithOriginShifting(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin, dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(_stack.Pop(), dto.Factor, dto.Origin, dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(_stack.Pop(), dto.Factor, _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_WithOriginShifting(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin, dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator_ConstA_VarB(dto.A, _stack.Pop()));
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SumFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SumOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SumOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin(TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new TimePower_OperatorCalculator_VarSignal_VarExponent_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin(TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new TimePower_OperatorCalculator_VarSignal_VarExponent_ZeroOrigin(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto_VarPassThrough_VarReset(ToggleTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            return ProcessOperatorDto(dto, () => new ToggleTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        // Special Visitation

        /// <summary> Instance integrity needs to be maintained over the (top-level) PatchInlets. </summary>
        protected override IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            VariableInput_OperatorCalculator calculator;
            if (!_variableInput_OperatorDto_To_Calculator_Dictionary.TryGetValue(dto, out calculator))
            {
                calculator = new VariableInput_OperatorCalculator(dto.DimensionEnum, dto.CanonicalName, dto.Position, dto.DefaultValue);

                _variableInput_OperatorDto_To_Calculator_Dictionary[dto] = calculator;
            }

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            base.Visit_Reset_OperatorDto(dto);

            OperatorCalculatorBase calculator = _stack.Peek();

            _resettableOperatorTuples.Add(new ResettableOperatorTuple(calculator, dto.Name, dto.Position));

            return dto;
        }

        // Helpers

        private IOperatorDto ProcessOperatorDto(IOperatorDto dto, Func<OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            OperatorCalculatorBase calculator = createOperatorCalculatorDelegate();
            _stack.Push(calculator);

            return dto;
        }

        /// <summary> Will get a DimensionStack and pass it to the OperatorCalculator, which is then pushed onto the _stack. </summary>
        private IOperatorDto ProcessWithDimension(IOperatorDto_WithDimension dto, Func<DimensionStack, OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            OperatorCalculatorBase calculator = createOperatorCalculatorDelegate(dimensionStack);
            _stack.Push(calculator);

            return dto;
        }
    }
}
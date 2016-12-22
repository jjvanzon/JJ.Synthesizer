using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoToCalculatorVisitor : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private readonly int _targetChannelCount;
        private readonly double _targetSamplingRate;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly CalculatorCache _calculatorCache;
        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly Stack<OperatorCalculatorBase> _stack = new Stack<OperatorCalculatorBase>();

        private DimensionStackCollection _dimensionStackCollection;

        public OperatorDtoToCalculatorVisitor(
            int targetSamplingRate, 
            int targetChannelCount, 
            double secondsBetweenApplyFilterVariables,
            CalculatorCache calculatorCache,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (calculatorCache == null) throw new NullException(() => calculatorCache);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _targetSamplingRate = targetSamplingRate;
            _targetChannelCount = targetChannelCount;
            _calculatorCache = calculatorCache;
            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;

            _samplesBetweenApplyFilterVariables = GetSamplesBetweenApplyFilterVariables(secondsBetweenApplyFilterVariables, targetSamplingRate);
        }

        private int GetSamplesBetweenApplyFilterVariables(double secondsBetweenApplyFilterVariables, int samplingRate)
        {
            double samplesBetweenApplyFilterVariablesDouble = secondsBetweenApplyFilterVariables * samplingRate;
            if (!ConversionHelper.CanCastToPositiveInt32(samplesBetweenApplyFilterVariablesDouble))
            {
                throw new Exception(String.Format("samplesBetweenApplyFilterVariablesDouble {0} cannot be cast to positive Int32.", samplesBetweenApplyFilterVariablesDouble));
            }
            int samplesBetweenApplyFilterVariables = (int)(secondsBetweenApplyFilterVariables * samplingRate);
            return samplesBetweenApplyFilterVariables;
        }

        public OperatorCalculatorBase Execute(OperatorDtoBase dto)
        {
            var preProcessing = new OperatorDtoVisitor_PreProcessing(_targetChannelCount);
            dto = preProcessing.Execute(dto);

            _dimensionStackCollection = new DimensionStackCollection();
            
            Visit_OperatorDto_Polymorphic(dto);

            if (_stack.Count != 1)
            {
                throw new NotEqualException(() => _stack.Count, 1);
            }

            foreach (DimensionStack dimensionStack in _dimensionStackCollection.GetDimensionStacks())
            {
                if (dimensionStack.Count != 1) // 1, because a single item is added by default as when the DimensionStackCollection is initialized.
                {
                    throw new Exception(String.Format(
                        "DimensionStack.Count for DimensionStack {0} should be 1 but it is {1}.",
                        new { dimensionStack.CustomDimensionName, dimensionStack.StandardDimensionEnum },
                        dimensionStack.Count));
                }
            }

            return _stack.Pop();
        }

        protected override OperatorDtoBase Visit_Absolute_OperatorDto_VarX(Absolute_OperatorDto_VarX dto)
        {
            return ProcessOperatorDto(dto, () => new Absolute_OperatorCalculator_VarX(_stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateAddCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateAddCalculator_Vars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_AllVars(AllPassFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new AllPassFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_ManyConsts(AllPassFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new AllPassFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new And_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_AverageFollower_OperatorDto_AllVars(AverageFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new AverageFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new AverageOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new AverageOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_AverageOverInlets_OperatorDto_Vars(AverageOverInlets_OperatorDto_Vars dto)
        {
            return ProcessOperatorDto(dto, () => new AverageOverInlets_OperatorCalculator(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantPeakGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantPeakGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            return ProcessOperatorDto(dto, () => new BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new DimensionToOutlets_OperatorCalculator(_stack.Pop(), dto.OutletListIndex, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_MultiChannel_BlockInterpolation(Cache_OperatorDto_MultiChannel_BlockInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_MultiChannel_CubicInterpolation(Cache_OperatorDto_MultiChannel_CubicInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_MultiChannel_HermiteInterpolation(Cache_OperatorDto_MultiChannel_HermiteInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_MultiChannel_LineInterpolation(Cache_OperatorDto_MultiChannel_LineInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_MultiChannel_StripeInterpolation(Cache_OperatorDto_MultiChannel_StripeInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_SingleChannel_BlockInterpolation(Cache_OperatorDto_SingleChannel_BlockInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_SingleChannel_CubicInterpolation(Cache_OperatorDto_SingleChannel_CubicInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_SingleChannel_HermiteInterpolation(Cache_OperatorDto_SingleChannel_HermiteInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_SingleChannel_LineInterpolation(Cache_OperatorDto_SingleChannel_LineInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_SingleChannel_StripeInterpolation(Cache_OperatorDto_SingleChannel_StripeInterpolation dto)
        {
            return Process_Cache_OperatorDtoBase_NotConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_ChangeTrigger_OperatorDto_VarPassThrough_VarReset(ChangeTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            return ProcessOperatorDto(dto, () => new ChangeTrigger_OperatorCalculator_VarPassThrough_VarReset(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new ClosestOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInletsExp_OperatorCalculator_VarInput_2ConstItems(_stack.Pop(), dto.Item1, dto.Item2));
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInletsExp_OperatorCalculator_VarInput_ConstItems(_stack.Pop(), dto.Items));
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInletsExp_OperatorCalculator_VarInput_VarItems(_stack.Pop(), dto.ItemOperatorDtos.Select(x => _stack.Pop()).ToArray()));
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInlets_OperatorCalculator_VarInput_2ConstItems(_stack.Pop(), dto.Item1, dto.Item2));
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInlets_OperatorCalculator_VarInput_ConstItems(_stack.Pop(), dto.Items));
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
        {
            return ProcessOperatorDto(dto, () => new ClosestOverInlets_OperatorCalculator_VarInput_VarItems(_stack.Pop(), dto.ItemOperatorDtos.Select(x => _stack.Pop()).ToArray()));
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
        {
            base.Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var curveCalculator = (CurveCalculator_MinXZero)_calculatorCache.GetCurveCalculator(dto.CurveID, _curveRepository);

            var calculator = new Curve_OperatorCalculator_MinXZero_NoOriginShifting(curveCalculator, dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
        {
            base.Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var curveCalculator = (CurveCalculator_MinXZero)_calculatorCache.GetCurveCalculator(dto.CurveID, _curveRepository);

            var calculator = new Curve_OperatorCalculator_MinXZero_WithOriginShifting(curveCalculator, dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
        {
            base.Visit_Curve_OperatorDto_MinX_NoOriginShifting(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var curveCalculator = (CurveCalculator_MinX)_calculatorCache.GetCurveCalculator(dto.CurveID, _curveRepository);

            var calculator = new Curve_OperatorCalculator_MinX_NoOriginShifting(curveCalculator, dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
        {
            base.Visit_Curve_OperatorDto_MinX_WithOriginShifting(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var curveCalculator = (CurveCalculator_MinX)_calculatorCache.GetCurveCalculator(dto.CurveID, _curveRepository);

            var calculator = new Curve_OperatorCalculator_MinX_WithOriginShifting(curveCalculator, dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_VarOrigin(Divide_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_ConstB_VarOrigin(dto.A, dto.B, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ConstOrigin(Divide_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_VarB_ConstOrigin(dto.A, _stack.Pop(), dto.Origin));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_VarOrigin(Divide_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_VarB_VarOrigin(dto.A, _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(Divide_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_ConstA_VarB_ZeroOrigin(dto.A, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ConstOrigin(Divide_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_ConstB_ConstOrigin(_stack.Pop(), dto.B, dto.Origin));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_VarOrigin(Divide_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_ConstB_VarOrigin(_stack.Pop(), dto.B, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(Divide_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_ConstB_ZeroOrigin(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ConstOrigin(Divide_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_VarB_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_VarOrigin(Divide_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_VarB_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(Divide_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new Divide_OperatorCalculator_VarA_VarB_ZeroOrigin(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_ConstB(Equal_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new Equal_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_VarB(Equal_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Equal_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio(Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_ConstLow_ConstHigh_VarRatio(dto.Low, dto.High, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio(Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_ConstLow_VarHigh_ConstRatio(dto.Low, _stack.Pop(), dto.Ratio));
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_VarHigh_VarRatio(Exponent_OperatorDto_ConstLow_VarHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_ConstLow_VarHigh_VarRatio(dto.Low, _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio(Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_ConstHigh_ConstRatio(_stack.Pop(), dto.High, dto.Ratio));
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_ConstHigh_VarRatio(Exponent_OperatorDto_VarLow_ConstHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_ConstHigh_VarRatio(_stack.Pop(), dto.High, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_VarHigh_ConstRatio(Exponent_OperatorDto_VarLow_VarHigh_ConstRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_VarHigh_ConstRatio(_stack.Pop(), _stack.Pop(), dto.Ratio));
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_VarHigh_VarRatio(Exponent_OperatorDto_VarLow_VarHigh_VarRatio dto)
        {
            return ProcessOperatorDto(dto, () => new Exponent_OperatorCalculator_VarLow_VarHigh_VarRatio(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new GetDimension_OperatorCalculator(dimensionStack));
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(GreaterThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThanOrEqual_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(GreaterThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThanOrEqual_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_ConstB(GreaterThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThan_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_VarB(GreaterThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new GreaterThan_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_AllVars(HighPassFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new HighPassFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_ManyConsts(HighPassFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new HighPassFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.MinFrequency, dto.BandWidth, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_AllVars(HighShelfFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new HighShelfFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_ManyConsts(HighShelfFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new HighShelfFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.TransitionFrequency, dto.TransitionSlope, dto.DBGain, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_Hold_OperatorDto_VarSignal(Hold_OperatorDto_VarSignal dto)
        {
            return ProcessOperatorDto(dto, () => new Hold_OperatorCalculator_VarSignal(_stack.Pop()));
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_ConstThen_ConstElse(_stack.Pop(), dto.Then, dto.Else));
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_ConstThen_VarElse(_stack.Pop(), dto.Then, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_VarThen_ConstElse(_stack.Pop(), _stack.Pop(), dto.Else));
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto)
        {
            return ProcessOperatorDto(dto, () => new If_OperatorCalculator_VarCondition_VarThen_VarElse(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new InletsToDimension_OperatorCalculator_BlockInterpolation(dto.Vars.Select(x => _stack.Pop()).ToArray(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto_Stripe(InletsToDimension_OperatorDto_Stripe dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new InletsToDimension_OperatorCalculator_StripeInterpolation(dto.Vars.Select(x => _stack.Pop()).ToArray(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line dto)
        {
            return Process_InletsToDimension_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto_CubicEquidistant(InletsToDimension_OperatorDto_CubicEquidistant dto)
        {
            return Process_InletsToDimension_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto_CubicAbruptSlope(InletsToDimension_OperatorDto_CubicAbruptSlope dto)
        {
            return Process_InletsToDimension_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto_CubicSmoothSlope(InletsToDimension_OperatorDto_CubicSmoothSlope dto)
        {
            return Process_InletsToDimension_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto_Hermite(InletsToDimension_OperatorDto_Hermite dto)
        {
            return Process_InletsToDimension_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Block(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_CubicAbruptSlope(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_CubicEquidistant(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_CubicSmoothSlope_LagBehind(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Hermite_LagBehind(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate(_stack.Pop(), dto.SamplingRate, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Line_LagBehind_VarSamplingRate(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Interpolate_OperatorCalculator_Stripe_LagBehind(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(LessThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThanOrEqual_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_VarB(LessThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThanOrEqual_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_ConstB(LessThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThan_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_VarB(LessThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new LessThan_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(_stack.Pop(), dto.SkipAndLoopStartMarker, dto.LoopEndMarker, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(_stack.Pop(), dto.SkipAndLoopStartMarker, _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_ManyConstants(_stack.Pop(), dto.Skip, dto.LoopStartMarker, dto.LoopEndMarker, dto.ReleaseEndMarker, _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_NoSkipOrRelease(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants(_stack.Pop(), dto.LoopStartMarker, dto.LoopEndMarker, _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_AllVars(LowPassFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new LowPassFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_ManyConsts(LowPassFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new LowPassFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.MaxFrequency, dto.BandWidth, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_AllVars(LowShelfFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new LowShelfFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_ManyConsts(LowShelfFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new LowShelfFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.TransitionFrequency, dto.TransitionSlope, dto.DBGain, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_MaxFollower_OperatorDto_AllVars(MaxFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MaxFollower_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MaxOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MaxOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_1Var_1Const(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_2Vars(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => new MaxOverInlets_OperatorCalculator_AllVars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override OperatorDtoBase Visit_MinFollower_OperatorDto_AllVars(MinFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MinFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MinOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new MinOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_1Var_1Const(MinOverInlets_OperatorDto_1Var_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_1Var_1Const(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_2Vars(MinOverInlets_OperatorDto_2Vars dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_2Vars(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => new MinOverInlets_OperatorCalculator_AllVars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_ConstA_ConstB_VarOrigin(dto.A, dto.B, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_ConstOrigin(dto.A, _stack.Pop(), dto.Origin));
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_ConstA_VarB_VarOrigin(dto.A, _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_ConstOrigin(_stack.Pop(), dto.B, dto.Origin));
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_ConstB_VarOrigin(_stack.Pop(), dto.B, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_VarB_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin));
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return ProcessOperatorDto(dto, () => new MultiplyWithOrigin_OperatorCalculator_VarA_VarB_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateMultiplyCalculator_Vars_1Const(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ConstValue));
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessOperatorDto(dto, () => OperatorCalculatorFactory.CreateMultiplyCalculator_Vars(dto.Vars.Select(x => _stack.Pop()).ToArray()));
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto_VarX(Negative_OperatorDto_VarX dto)
        {
            return ProcessOperatorDto(dto, () => new Negative_OperatorCalculator(_stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            base.Visit_Noise_OperatorDto(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            NoiseCalculator noiseCalculator = _calculatorCache.GetNoiseCalculator(dto.OperatorID);

            var calculator = new Noise_OperatorCalculator(noiseCalculator, dimensionStack);

            return dto;
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_AllVars(NotchFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_ManyConsts(NotchFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new NotchFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_ConstB(NotEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new NotEqual_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_VarB(NotEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new NotEqual_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto_VarX(Not_OperatorDto_VarX dto)
        {
            return ProcessOperatorDto(dto, () => new Not_OperatorCalculator(_stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator(dto.Number));
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator(Double.NaN));
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator_One());
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            return ProcessOperatorDto(dto, () => new Number_OperatorCalculator_Zero());
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_VarX(OneOverX_OperatorDto_VarX dto)
        {
            return ProcessOperatorDto(dto, () => new OneOverX_OperatorCalculator(_stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Or_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_AllVars(PeakingEQFilter_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new PeakingEQFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables));
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_ManyConsts(PeakingEQFilter_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new PeakingEQFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, dto.DBGain, _targetSamplingRate));
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_ConstBase_VarExponent(dto.Base, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_VarBase_ConstExponent(_stack.Pop(), dto.Exponent));
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
        {
            return ProcessOperatorDto(dto, () => new Power_OperatorCalculator_VarBase_VarExponent(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_PulseTrigger_OperatorDto_VarPassThrough_VarReset(PulseTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            return ProcessOperatorDto(dto, () => new PulseTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_NoOriginShifting(dto.Frequency, dto.Width, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_WithOriginShifting(dto.Frequency, dto.Width, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_VarWidth_NoOriginShifting(dto.Frequency, _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_ConstFrequency_VarWidth_WithOriginShifting(dto.Frequency, _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_ConstWidth_NoPhaseTracking(_stack.Pop(), dto.Width, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_ConstWidth_WithPhaseTracking(_stack.Pop(), dto.Width, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_VarWidth_NoPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Pulse_OperatorCalculator_VarFrequency_VarWidth_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
        {
            return Process_Random_OperatorDto_BlockAndStripe(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicAbruptSlope(Random_OperatorDto_CubicAbruptSlope dto)
        {
            return Process_Random_OperatorDto_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicEquidistant(Random_OperatorDto_CubicEquidistant dto)
        {
            return Process_Random_OperatorDto_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicSmoothSlope(Random_OperatorDto_CubicSmoothSlope dto)
        {
            return Process_Random_OperatorDto_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Hermite(Random_OperatorDto_Hermite dto)
        {
            return Process_Random_OperatorDto_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Line(Random_OperatorDto_Line dto)
        {
            return Process_Random_OperatorDto_OtherInterpolationTypes(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Stripe(Random_OperatorDto_Stripe dto)
        {
            return Process_Random_OperatorDto_BlockAndStripe(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new RangeOverDimension_OperatorCalculator_OnlyConsts(dto.From, dto.Till, dto.Step, dimensionStack));
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new RangeOverDimension_OperatorCalculator_OnlyVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new RangeOverDimension_OperatorCalculator_WithConsts_AndStepOne(dto.From, dto.Till, dimensionStack));
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverOutlets_OperatorCalculator_ConstFrom_VarStep(dto.From, _stack.Pop(), dto.OutletListIndex));
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverOutlets_OperatorCalculator_VarFrom_ConstStep(_stack.Pop(), dto.Step, dto.OutletListIndex));
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep dto)
        {
            return ProcessOperatorDto(dto, () => new RangeOverOutlets_OperatorCalculator_VarFrom_VarStep(_stack.Pop(), _stack.Pop(), dto.OutletListIndex));
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_ConstSpeed_NoOriginShifting(Reverse_OperatorDto_ConstSpeed_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_ConstSpeed_NoOriginShifting(_stack.Pop(), dto.Speed, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_ConstSpeed_WithOriginShifting(Reverse_OperatorDto_ConstSpeed_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_ConstSpeed_WithOriginShifting(_stack.Pop(), dto.Speed, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_VarSpeed_NoPhaseTracking(Reverse_OperatorDto_VarSpeed_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_VarSpeed_NoPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_VarSpeed_WithPhaseTracking(Reverse_OperatorDto_VarSpeed_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Reverse_OperatorCalculator_VarSpeed_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_ConstSignal(dto.Signal, _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_ConstStep_ConstOffset(_stack.Pop(), dto.StepOperatorDto, dto.Offset));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_ConstStep_VarOffset(_stack.Pop(), dto.StepOperatorDto, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_ConstStep_ZeroOffset(_stack.Pop(), dto.Step));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_StepOne_OffsetZero(_stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_VarStep_ConstOffset(_stack.Pop(), _stack.Pop(), dto.Offset));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_VarStep_VarOffset(_stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
        {
            return ProcessOperatorDto(dto, () => new Round_OperatorCalculator_VarSignal_VarStep_ZeroOffset(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_ConstFrequency_MonoToStereo_NoOriginShifting(dto.Frequency, sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_ConstFrequency_MonoToStereo_WithOriginShifting(dto.Frequency, sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_Sample_OperatorDto_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, sampleCalculator, dimensionStack, channelDimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_ConstFrequency_StereoToMono_NoOriginShifting(dto.Frequency, sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_ConstFrequency_StereoToMono_WithOriginShifting(dto.Frequency, sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_Sample_OperatorDto_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, sampleCalculator, dimensionStack, channelDimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_VarFrequency_MonoToStereo_NoPhaseTracking(_stack.Pop(), sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_VarFrequency_MonoToStereo_WithPhaseTracking(_stack.Pop(), sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), sampleCalculator, dimensionStack, channelDimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_VarFrequency_StereoToMono_NoPhaseTracking(_stack.Pop(), sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_SingleOutputChannel(dto, (dimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_VarFrequency_StereoToMono_WithPhaseTracking(_stack.Pop(), sampleCalculator, dimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_Sample_OperatorDto_MultipleOutputChannels(dto, (dimensionStack, channelDimensionStack, sampleCalculator) =>
            {
                return new Sample_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), sampleCalculator, dimensionStack, channelDimensionStack);
            });
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawDown_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SawUp_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto_AllVars(Scaler_OperatorDto_AllVars dto)
        {
            return ProcessOperatorDto(dto, () => new Scaler_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto_ManyConsts(Scaler_OperatorDto_ManyConsts dto)
        {
            return ProcessOperatorDto(dto, () => new Scaler_OperatorCalculator_ManyConsts(_stack.Pop(), dto.SourceValueA, dto.SourceValueB, dto.TargetValueA, dto.TargetValueB));
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_VarSignal_ConstPosition(Select_OperatorDto_VarSignal_ConstPosition dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Select_OperatorCalculator_VarSignal_ConstPosition(_stack.Pop(), dto.Position, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_VarSignal_VarPosition(Select_OperatorDto_VarSignal_VarPosition dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Select_OperatorCalculator_VarSignal_VarPosition(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_ConstValue(SetDimension_OperatorDto_ConstValue dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SetDimension_OperatorCalculator_ConstValue(_stack.Pop(), dto.Value, dimensionStack));
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_VarValue(SetDimension_OperatorDto_VarValue dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SetDimension_OperatorCalculator_VarValue(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Shift_OperatorCalculator_VarSignal_ConstDistance(_stack.Pop(), dto.Distance, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Shift_OperatorCalculator_VarSignal_VarDistance(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SortOverInlets_OperatorCalculator(dto.Vars.Select(x => _stack.Pop()).ToArray(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Spectrum_OperatorDto_AllVars(Spectrum_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Spectrum_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Square_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(_stack.Pop(), dto.Factor, dto.Origin, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(_stack.Pop(), dto.Factor, _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_WithOriginShifting(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Squash_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(_stack.Pop(), dto.Factor, dto.Origin, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(_stack.Pop(), dto.Factor, _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_WithOriginShifting(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(_stack.Pop(), dto.Factor, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_WithPhaseTracking(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Stretch_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator_ConstA_VarB(dto.A, _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
        {
            return ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator_VarA_ConstB(_stack.Pop(), dto.B));
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
        {
            return ProcessOperatorDto(dto, () => new Subtract_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SumFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SumOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new SumOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin(TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new TimePower_OperatorCalculator_VarSignal_VarExponent_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin(TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new TimePower_OperatorCalculator_VarSignal_VarExponent_ZeroOrigin(_stack.Pop(), _stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_ToggleTrigger_OperatorDto_VarPassThrough_VarReset(ToggleTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            return ProcessOperatorDto(dto, () => new ToggleTrigger_OperatorCalculator(_stack.Pop(), _stack.Pop()));
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_ConstFrequency_WithOriginShifting(dto.Frequency, dimensionStack));
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), dimensionStack));
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new Triangle_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), dimensionStack));
        }

        // Helpers

        /// <summary>
        /// The Cache operator requires more lengthy code, while most methods are very short,
        /// because it is the only operator type for which you need to 
        /// calculate during optimization time, so calculate while the executable calculation is still being built up.
        /// </summary>
        private OperatorDtoBase Process_Cache_OperatorDtoBase_NotConstSignal(Cache_OperatorDtoBase_NotConstSignal dto)
        {
            base.Visit_OperatorDto_Base(dto);

            OperatorCalculatorBase calculator;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            DimensionStack channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase startCalculator = _stack.Pop();
            OperatorCalculatorBase endCalculator = _stack.Pop();
            OperatorCalculatorBase samplingRateCalculator = _stack.Pop();

            double start = startCalculator.Calculate();
            double end = endCalculator.Calculate();
            double samplingRate = samplingRateCalculator.Calculate();

            bool parametersAreValid = CalculationHelper.CacheParametersAreValid(start, end, samplingRate);
            if (!parametersAreValid)
            {
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else
            {
                IList<ArrayCalculatorBase> arrayCalculators = _calculatorCache.GetCacheArrayCalculators(
                    dto.OperatorID,
                    signalCalculator,
                    start,
                    end,
                    samplingRate,
                    dto.ChannelCount,
                    dto.InterpolationTypeEnum,
                    dimensionStack,
                    channelDimensionStack);

                calculator = OperatorCalculatorFactory.Create_Cache_OperatorCalculator(arrayCalculators, dimensionStack, channelDimensionStack);
            }

            _stack.Push(calculator);

            return dto;
        }

        private OperatorDtoBase ProcessOperatorDto(OperatorDtoBase dto, Func<OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            OperatorCalculatorBase calculator = createOperatorCalculatorDelegate();
            _stack.Push(calculator);

            return dto;
        }

        /// <summary> Will get a DimensionStack and pass it to the OperatorCalculator, which is then pushed onto the _stack. </summary>
        private OperatorDtoBase ProcessWithDimension(OperatorDtoBase dto, Func<DimensionStack, OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            var dtoWithDimension = (IOperatorDtoWithDimension)dto;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dtoWithDimension);

            OperatorCalculatorBase calculator = createOperatorCalculatorDelegate(dimensionStack);

            _stack.Push(calculator);

            return dto;
        }

        private OperatorDtoBase Process_InletsToDimension_OtherInterpolationTypes(InletsToDimension_OperatorDto dto)
        {
            return ProcessWithDimension(dto, dimensionStack => new InletsToDimension_OperatorCalculator_OtherInterpolationTypes(dto.Vars.Select(x => _stack.Pop()).ToArray(), dto.ResampleInterpolationTypeEnum, dimensionStack));
        }

        private OperatorDtoBase Process_Random_OperatorDto_OtherInterpolationTypes(Random_OperatorDto dto)
        {
            base.Visit_OperatorDto_Base(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            var randomCalculator = _calculatorCache.GetRandomCalculator(dto.OperatorID, dto.ResampleInterpolationTypeEnum);

            var calculator = new Random_OperatorCalculator_OtherInterpolationTypes(randomCalculator, _stack.Pop(), dto.ResampleInterpolationTypeEnum, dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        private OperatorDtoBase Process_Random_OperatorDto_BlockAndStripe(Random_OperatorDto dto)
        {
            base.Visit_OperatorDto_Base(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            var randomCalculator = _calculatorCache.GetRandomCalculator(dto.OperatorID, dto.ResampleInterpolationTypeEnum);

            var calculator = new Random_OperatorCalculator_BlockAndStripe_VarFrequency(randomCalculator, _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        private OperatorDtoBase Process_Sample_OperatorDto_SingleOutputChannel(
            OperatorDtoBase dto,
            Func<DimensionStack, ISampleCalculator, OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            var dtoWithSampleID = (ISample_OperatorDto_WithSampleID)dto;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dtoWithSampleID);
            ISampleCalculator sampleCalculator = _calculatorCache.GetSampleCalculator(dtoWithSampleID.SampleID, _sampleRepository);

            var calculator = createOperatorCalculatorDelegate(dimensionStack, sampleCalculator);

            _stack.Push(calculator);

            return dto;
        }

        /// <param name="createOperatorCalculatorDelegate">
        /// Second DimensionStack parameter is the channelDimensionStack
        /// </param>
        private OperatorDtoBase Process_Sample_OperatorDto_MultipleOutputChannels(
            OperatorDtoBase dto,
            Func<DimensionStack, DimensionStack, ISampleCalculator, OperatorCalculatorBase> createOperatorCalculatorDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            var dtoWithSampleID = (ISample_OperatorDto_WithSampleID)dto;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dtoWithSampleID);
            DimensionStack channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);
            ISampleCalculator sampleCalculator = _calculatorCache.GetSampleCalculator(dtoWithSampleID.SampleID, _sampleRepository);

            var calculator = createOperatorCalculatorDelegate(dimensionStack, channelDimensionStack, sampleCalculator);

            _stack.Push(calculator);

            return dto;
        }
    }
}
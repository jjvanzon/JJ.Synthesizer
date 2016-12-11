using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoToCalculatorVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        private readonly int _targetChannelCount;
        private readonly double _targetSamplingRate;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly CalculatorCache _calculatorCache;
        private readonly ICurveRepository _curveRepository;
        private readonly Stack<OperatorCalculatorBase> _stack = new Stack<OperatorCalculatorBase>();

        private DimensionStackCollection _dimensionStackCollection;

        public OperatorDtoToCalculatorVisitor(
            int targetSamplingRate, 
            int targetChannelCount, 
            double secondsBetweenApplyFilterVariables,
            CalculatorCache calculatorCache,
            ICurveRepository curveRepository)
        {
            if (calculatorCache == null) throw new NullException(() => calculatorCache);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            _targetSamplingRate = targetSamplingRate;
            _targetChannelCount = targetChannelCount;
            _calculatorCache = calculatorCache;
            _curveRepository = curveRepository;

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
            var preProcessing = new PreProcessing_OperatorDtoVisitor(_targetChannelCount);
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
            base.Visit_Absolute_OperatorDto_VarX(dto);

            var calculator = new Absolute_OperatorCalculator_VarX(_stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            OperatorCalculatorBase calculator = OperatorCalculatorFactory.CreateAddCalculatorWithConst(dto.ConstValue, dto.Vars.Select(x => _stack.Pop()).ToArray());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_Vars_NoConsts(dto);

            OperatorCalculatorBase calculator = OperatorCalculatorFactory.CreateAddCalculatorOnlyVars(dto.InputOperatorDtos.Select(x => _stack.Pop()).ToArray());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_AllVars(AllPassFilter_OperatorDto_AllVars dto)
        {
            base.Visit_AllPassFilter_OperatorDto_AllVars(dto);

            var calculator = new AllPassFilter_OperatorCalculator_AllVars(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_ManyConsts(AllPassFilter_OperatorDto_ManyConsts dto)
        {
            base.Visit_AllPassFilter_OperatorDto_ManyConsts(dto);

            var calculator = new AllPassFilter_OperatorCalculator_ManyConsts(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, _targetSamplingRate);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
        {
            base.Visit_And_OperatorDto_VarA_VarB(dto);

            var calculator = new And_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_AverageFollower_OperatorDto_AllVars(AverageFollower_OperatorDto_AllVars dto)
        {
            base.Visit_AverageFollower_OperatorDto_AllVars(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new AverageFollower_OperatorCalculator(_stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            base.Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new AverageOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            base.Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new AverageOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_AverageOverInlets_OperatorDto_Vars(AverageOverInlets_OperatorDto_Vars dto)
        {
            base.Visit_AverageOverInlets_OperatorDto_Vars(dto);

            var calculator = new AverageOverInlets_OperatorCalculator(dto.Vars.Select(x => _stack.Pop()).ToArray());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            base.Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(dto);

            var calculator = new BandPassFilterConstantPeakGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, _targetSamplingRate);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            base.Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth(dto);

            var calculator = new BandPassFilterConstantPeakGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            base.Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(dto);

            var calculator = new BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(_stack.Pop(), dto.CenterFrequency, dto.BandWidth, _targetSamplingRate);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            base.Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth(dto);

            var calculator = new BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(_stack.Pop(), _stack.Pop(), _stack.Pop(), _targetSamplingRate, _samplesBetweenApplyFilterVariables);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Bundle_OperatorDto(Bundle_OperatorDto dto)
        {
            base.Visit_Bundle_OperatorDto(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new Bundle_OperatorCalculator(dto.InputOperatorDtos.Select(x => _stack.Pop()).ToArray(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_MultiChannel(Cache_OperatorDto_MultiChannel dto)
        {
            base.Visit_Cache_OperatorDto_MultiChannel(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);
            DimensionStack channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);

            // TODO: Use and adapt the CalculatorCache. See PatchCalculatorVisitor.
            throw new NotImplementedException();

            //var calculator = new Cache_OperatorCalculator_MultiChannel(null, dimensionStack, channelDimensionStack);
            
            return base.Visit_Cache_OperatorDto_MultiChannel(dto);
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto_SingleChannel(Cache_OperatorDto_SingleChannel dto)
        {
            // TODO: Use and adapt the CalculatorCache. See PatchCalculatorVisitor.
            throw new NotImplementedException();
            return base.Visit_Cache_OperatorDto_SingleChannel(dto);
        }

        protected override OperatorDtoBase Visit_ChangeTrigger_OperatorDto_VarPassThrough_VarReset(ChangeTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            base.Visit_ChangeTrigger_OperatorDto_VarPassThrough_VarReset(dto);

            var calculator = new ChangeTrigger_OperatorCalculator_VarPassThrough_VarReset(_stack.Pop(), _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
        {
            base.Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
        {
            base.Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            base.Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new ClosestOverDimension_OperatorCalculator_CollectionRecalculationContinuous(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            base.Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            var calculator = new ClosestOverDimension_OperatorCalculator_CollectionRecalculationUponReset(_stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), _stack.Pop(), dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(dto);

            var calculator = new ClosestOverInletsExp_OperatorCalculator_VarInput_2ConstItems(_stack.Pop(), dto.Item1, dto.Item2);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(dto);

            var calculator = new ClosestOverInletsExp_OperatorCalculator_VarInput_ConstItems(_stack.Pop(), dto.Items);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto)
        {
            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(dto);

            var calculator = new ClosestOverInletsExp_OperatorCalculator_VarInput_VarItems(_stack.Pop(), dto.ItemOperatorDtos.Select(x => _stack.Pop()).ToArray());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(dto);

            var calculator = new ClosestOverInlets_OperatorCalculator_VarInput_2ConstItems(_stack.Pop(), dto.Item1, dto.Item2);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(dto);

            var calculator = new ClosestOverInlets_OperatorCalculator_VarInput_ConstItems(_stack.Pop(), dto.Items);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(dto);

            var calculator = new ClosestOverInlets_OperatorCalculator_VarInput_VarItems(_stack.Pop(), dto.ItemOperatorDtos.Select(x => _stack.Pop()).ToArray());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
        {
            base.Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(dto);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

            throw new NotImplementedException();

            //// TODO: In a specialized Curve_OperatorDto, the CurveID should be not-nullable. 
            //// In case Curve was null, this should already have been specialized to a Number_OperatorDto_Zero in the previous visitors.
            //ICurveCalculator curveCalculator = _calculatorCache.GetCurveCalculator(dto.CurveID, _curveRepository);

            //// TODO: The Calculator used to (indirectly) determine what the type of calculator was, 
            //// but now we have DTO's which also already determine it. You could add more methods to CalculatorCache to get a specific type of calculator,
            //// but it seems responsibilities are spread around now... and the CalculatorCache starts to do much more than just offer instance intergrity
            //// over several types of calculators.
            //var calculator = new Curve_OperatorCalculator_MinXZero_NoOriginShifting(curveCalculator, dimensionStack);
            //_stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Curve_OperatorDto_MinX_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Curve_OperatorDto_MinX_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_CustomOperator_OperatorDto(CustomOperator_OperatorDto dto)
        {
            // TODO: This overload should already have been sealed by a base class,
            // after the visitor is programmed, which 'inlines' custom operators.
            throw new NotImplementedException();
            return base.Visit_CustomOperator_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_DimensionToOutlets_OperatorDto(DimensionToOutlets_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_DimensionToOutlets_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_VarOrigin(Divide_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            base.Visit_Divide_OperatorDto_ConstA_ConstB_VarOrigin(dto);

            var calculator = new Divide_OperatorCalculator_ConstA_ConstB_VarOrigin(dto.A, dto.B, _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ConstOrigin(Divide_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            base.Visit_Divide_OperatorDto_ConstA_VarB_ConstOrigin(dto);

            var calculator = new Divide_OperatorCalculator_ConstA_VarB_ConstOrigin(dto.A, _stack.Pop(), dto.Origin);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_VarOrigin(Divide_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            base.Visit_Divide_OperatorDto_ConstA_VarB_VarOrigin(dto);

            var calculator = new Divide_OperatorCalculator_ConstA_VarB_VarOrigin(dto.A, _stack.Pop(), _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(Divide_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            base.Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(dto);

            var calculator = new Divide_OperatorCalculator_ConstA_VarB_ZeroOrigin(dto.A, _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ConstOrigin(Divide_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            base.Visit_Divide_OperatorDto_VarA_ConstB_ConstOrigin(dto);

            var calculator = new Divide_OperatorCalculator_VarA_ConstB_ConstOrigin(_stack.Pop(), dto.B, dto.Origin);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_VarOrigin(Divide_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            base.Visit_Divide_OperatorDto_VarA_ConstB_VarOrigin(dto);

            var calculator = new Divide_OperatorCalculator_VarA_ConstB_VarOrigin(_stack.Pop(), dto.B, _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(Divide_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            base.Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(dto);

            var calculator = new Divide_OperatorCalculator_VarA_ConstB_ZeroOrigin(_stack.Pop(), dto.B);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ConstOrigin(Divide_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            base.Visit_Divide_OperatorDto_VarA_VarB_ConstOrigin(dto);

            var calculator = new Divide_OperatorCalculator_VarA_VarB_ConstOrigin(_stack.Pop(), _stack.Pop(), dto.Origin);
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_VarOrigin(Divide_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            base.Visit_Divide_OperatorDto_VarA_VarB_VarOrigin(dto);

            var calculator = new Divide_OperatorCalculator_VarA_VarB_VarOrigin(_stack.Pop(), _stack.Pop(), _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(Divide_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            base.Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(dto);

            var calculator = new Divide_OperatorCalculator_VarA_VarB_ZeroOrigin(_stack.Pop(), _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_ConstB(Equal_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Equal_OperatorDto_VarA_ConstB(dto);

            // TODO: Equals and NotEquals operators do not have have _VarA_ConstB variation, but the other way around: ConstA_VarB. Refactor that for consistency.
            //var calculator = new Equal_OperatorCalculator_ConstA_VarB
            throw new NotImplementedException();

            return dto;
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_VarB(Equal_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_Equal_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio(Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio dto)
        {
            throw new NotImplementedException();
            return base.Visit_Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio(Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio dto)
        {
            throw new NotImplementedException();
            return base.Visit_Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_ConstLow_VarHigh_VarRatio(Exponent_OperatorDto_ConstLow_VarHigh_VarRatio dto)
        {
            throw new NotImplementedException();
            return base.Visit_Exponent_OperatorDto_ConstLow_VarHigh_VarRatio(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio(Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio dto)
        {
            throw new NotImplementedException();
            return base.Visit_Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_ConstHigh_VarRatio(Exponent_OperatorDto_VarLow_ConstHigh_VarRatio dto)
        {
            throw new NotImplementedException();
            return base.Visit_Exponent_OperatorDto_VarLow_ConstHigh_VarRatio(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_VarHigh_ConstRatio(Exponent_OperatorDto_VarLow_VarHigh_ConstRatio dto)
        {
            throw new NotImplementedException();
            return base.Visit_Exponent_OperatorDto_VarLow_VarHigh_ConstRatio(dto);
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto_VarLow_VarHigh_VarRatio(Exponent_OperatorDto_VarLow_VarHigh_VarRatio dto)
        {
            throw new NotImplementedException();
            return base.Visit_Exponent_OperatorDto_VarLow_VarHigh_VarRatio(dto);
        }

        protected override OperatorDtoBase Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_GetDimension_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(GreaterThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            throw new NotImplementedException();
            return base.Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(GreaterThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_ConstB(GreaterThan_OperatorDto_VarA_ConstB dto)
        {
            throw new NotImplementedException();
            return base.Visit_GreaterThan_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_VarB(GreaterThan_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_GreaterThan_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_AllVars(HighPassFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_HighPassFilter_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_ManyConsts(HighPassFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_HighPassFilter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_AllVars(HighShelfFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_HighShelfFilter_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_ManyConsts(HighShelfFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_HighShelfFilter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_Hold_OperatorDto_VarSignal(Hold_OperatorDto_VarSignal dto)
        {
            throw new NotImplementedException();
            return base.Visit_Hold_OperatorDto_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
        {
            throw new NotImplementedException();
            return base.Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto)
        {
            throw new NotImplementedException();
            return base.Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto)
        {
            throw new NotImplementedException();
            return base.Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(dto);
        }

        protected override OperatorDtoBase Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto)
        {
            throw new NotImplementedException();
            return base.Visit_If_OperatorDto_VarCondition_VarThen_VarElse(dto);
        }

        protected override OperatorDtoBase Visit_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_InletsToDimension_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_Block(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_CubicAbruptSlope(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_CubicEquidistant(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_Hermite_LagBehind(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(dto);
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
        {
            throw new NotImplementedException();
            return base.Visit_Interpolate_OperatorDto_Stripe_LagBehind(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto)
        {
            throw new NotImplementedException();
            return base.Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(dto);
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(LessThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            throw new NotImplementedException();
            return base.Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_VarB(LessThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_LessThanOrEqual_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_ConstB(LessThan_OperatorDto_VarA_ConstB dto)
        {
            throw new NotImplementedException();
            return base.Visit_LessThan_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_VarB(LessThan_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_LessThan_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_Loop_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto)
        {
            throw new NotImplementedException();
            return base.Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto)
        {
            throw new NotImplementedException();
            return base.Visit_Loop_OperatorDto_ManyConstants(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto)
        {
            throw new NotImplementedException();
            return base.Visit_Loop_OperatorDto_NoSkipOrRelease(dto);
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto)
        {
            throw new NotImplementedException();
            return base.Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(dto);
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_AllVars(LowPassFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_LowPassFilter_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_ManyConsts(LowPassFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_LowPassFilter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_AllVars(LowShelfFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_LowShelfFilter_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_ManyConsts(LowShelfFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_LowShelfFilter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_MaxFollower_OperatorDto_AllVars(MaxFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_MaxFollower_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
            return base.Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
            return base.Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto)
        {
            throw new NotImplementedException();
            return base.Visit_MaxOverInlets_OperatorDto_1Var_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto)
        {
            throw new NotImplementedException();
            return base.Visit_MaxOverInlets_OperatorDto_2Vars(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            throw new NotImplementedException();
            return base.Visit_MaxOverInlets_OperatorDto_Vars_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MinFollower_OperatorDto_AllVars(MinFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_MinFollower_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
            return base.Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(dto);
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
            return base.Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_1Var_1Const(MinOverInlets_OperatorDto_1Var_1Const dto)
        {
            throw new NotImplementedException();
            return base.Visit_MinOverInlets_OperatorDto_1Var_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_2Vars(MinOverInlets_OperatorDto_2Vars dto)
        {
            throw new NotImplementedException();
            return base.Visit_MinOverInlets_OperatorDto_2Vars(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            throw new NotImplementedException();
            return base.Visit_MinOverInlets_OperatorDto_Vars_1Const(dto);
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_MinOverInlets_OperatorDto_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            throw new NotImplementedException();
            return base.Visit_Multiply_OperatorDto_Vars_1Const(dto);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_Multiply_OperatorDto_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto_VarX(Negative_OperatorDto_VarX dto)
        {
            throw new NotImplementedException();
            return base.Visit_Negative_OperatorDto_VarX(dto);
        }

        protected override OperatorDtoBase Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_Noise_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_AllVars(NotchFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_NotchFilter_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_ManyConsts(NotchFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_NotchFilter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_ConstB(NotEqual_OperatorDto_VarA_ConstB dto)
        {
            throw new NotImplementedException();
            return base.Visit_NotEqual_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_VarB(NotEqual_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_NotEqual_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto_VarX(Not_OperatorDto_VarX dto)
        {
            throw new NotImplementedException();
            return base.Visit_Not_OperatorDto_VarX(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_Number_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            throw new NotImplementedException();
            return base.Visit_Number_OperatorDto_NaN(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            throw new NotImplementedException();
            return base.Visit_Number_OperatorDto_One(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            throw new NotImplementedException();
            return base.Visit_Number_OperatorDto_Zero(dto);
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_VarX(OneOverX_OperatorDto_VarX dto)
        {
            throw new NotImplementedException();
            return base.Visit_OneOverX_OperatorDto_VarX(dto);
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_Or_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_PatchInlet_OperatorDto(PatchInlet_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_PatchInlet_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_PatchOutlet_OperatorDto(PatchOutlet_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_PatchOutlet_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_AllVars(PeakingEQFilter_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_PeakingEQFilter_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_ManyConsts(PeakingEQFilter_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_PeakingEQFilter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
        {
            throw new NotImplementedException();
            return base.Visit_Power_OperatorDto_ConstBase_VarExponent(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            throw new NotImplementedException();
            return base.Visit_Power_OperatorDto_VarBase_ConstExponent(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
        {
            throw new NotImplementedException();
            return base.Visit_Power_OperatorDto_VarBase_VarExponent(dto);
        }

        protected override OperatorDtoBase Visit_PulseTrigger_OperatorDto_VarPassThrough_VarReset(PulseTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            throw new NotImplementedException();
            return base.Visit_PulseTrigger_OperatorDto_VarPassThrough_VarReset(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
        {
            throw new NotImplementedException();
            return base.Visit_Random_OperatorDto_Block(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicAbruptSlope(Random_OperatorDto_CubicAbruptSlope dto)
        {
            throw new NotImplementedException();
            return base.Visit_Random_OperatorDto_CubicAbruptSlope(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicEquidistant(Random_OperatorDto_CubicEquidistant dto)
        {
            throw new NotImplementedException();
            return base.Visit_Random_OperatorDto_CubicEquidistant(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_CubicSmoothSlope(Random_OperatorDto_CubicSmoothSlope dto)
        {
            throw new NotImplementedException();
            return base.Visit_Random_OperatorDto_CubicSmoothSlope(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Hermite(Random_OperatorDto_Hermite dto)
        {
            throw new NotImplementedException();
            return base.Visit_Random_OperatorDto_Hermite(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Line(Random_OperatorDto_Line dto)
        {
            throw new NotImplementedException();
            return base.Visit_Random_OperatorDto_Line(dto);
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto_Stripe(Random_OperatorDto_Stripe dto)
        {
            throw new NotImplementedException();
            return base.Visit_Random_OperatorDto_Stripe(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorCalculator_OnlyConsts(Dto.RangeOverDimension_OperatorCalculator_OnlyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_RangeOverDimension_OperatorCalculator_OnlyConsts(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorCalculator_OnlyVars(Dto.RangeOverDimension_OperatorCalculator_OnlyVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_RangeOverDimension_OperatorCalculator_OnlyVars(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorCalculator_WithConsts_AndStepOne(Dto.RangeOverDimension_OperatorCalculator_WithConsts_AndStepOne dto)
        {
            throw new NotImplementedException();
            return base.Visit_RangeOverDimension_OperatorCalculator_WithConsts_AndStepOne(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_ConstFrom_ConstStep(RangeOverOutlets_OperatorDto_ConstFrom_ConstStep dto)
        {
            throw new NotImplementedException();
            return base.Visit_RangeOverOutlets_OperatorDto_ConstFrom_ConstStep(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_OperatorDto_ConstFrom_VarStep dto)
        {
            throw new NotImplementedException();
            return base.Visit_RangeOverOutlets_OperatorDto_ConstFrom_VarStep(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_VarFrom_ConstStep(RangeOverOutlets_OperatorDto_VarFrom_ConstStep dto)
        {
            throw new NotImplementedException();
            return base.Visit_RangeOverOutlets_OperatorDto_VarFrom_ConstStep(dto);
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto_VarFrom_VarStep(RangeOverOutlets_OperatorDto_VarFrom_VarStep dto)
        {
            throw new NotImplementedException();
            return base.Visit_RangeOverOutlets_OperatorDto_VarFrom_VarStep(dto);
        }

        protected override OperatorDtoBase Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_Reset_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_ConstSpeed_NoOriginShifting(Reverse_OperatorDto_ConstSpeed_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Reverse_OperatorDto_ConstSpeed_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_ConstSpeed_WithOriginShifting(Reverse_OperatorDto_ConstSpeed_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Reverse_OperatorDto_ConstSpeed_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_VarSpeed_NoPhaseTracking(Reverse_OperatorDto_VarSpeed_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Reverse_OperatorDto_VarSpeed_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_VarSpeed_WithPhaseTracking(Reverse_OperatorDto_VarSpeed_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Reverse_OperatorDto_VarSpeed_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_ConstSignal(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(dto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
        {
            throw new NotImplementedException();
            return base.Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto_AllVars(Scaler_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_Scaler_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto_ManyConsts(Scaler_OperatorDto_ManyConsts dto)
        {
            throw new NotImplementedException();
            return base.Visit_Scaler_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_VarSignal_ConstPosition(Select_OperatorDto_VarSignal_ConstPosition dto)
        {
            throw new NotImplementedException();
            return base.Visit_Select_OperatorDto_VarSignal_ConstPosition(dto);
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto_VarSignal_VarPosition(Select_OperatorDto_VarSignal_VarPosition dto)
        {
            throw new NotImplementedException();
            return base.Visit_Select_OperatorDto_VarSignal_VarPosition(dto);
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_ConstValue(SetDimension_OperatorDto_ConstValue dto)
        {
            throw new NotImplementedException();
            return base.Visit_SetDimension_OperatorDto_ConstValue(dto);
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_VarValue(SetDimension_OperatorDto_VarValue dto)
        {
            throw new NotImplementedException();
            return base.Visit_SetDimension_OperatorDto_VarValue(dto);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            throw new NotImplementedException();
            return base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            throw new NotImplementedException();
            return base.Visit_Shift_OperatorDto_VarSignal_VarDistance(dto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
            return base.Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(dto);
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
            return base.Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(dto);
        }

        protected override OperatorDtoBase Visit_SortOverInlets_OperatorDto(SortOverInlets_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_SortOverInlets_OperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Spectrum_OperatorDto_AllVars(Spectrum_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_Spectrum_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_Subtract_OperatorDto_ConstA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
        {
            throw new NotImplementedException();
            return base.Visit_Subtract_OperatorDto_VarA_ConstB(dto);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
        {
            throw new NotImplementedException();
            return base.Visit_Subtract_OperatorDto_VarA_VarB(dto);
        }

        protected override OperatorDtoBase Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
            return base.Visit_SumFollower_OperatorDto_AllVars(dto);
        }

        protected override OperatorDtoBase Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
        {
            throw new NotImplementedException();
            return base.Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(dto);
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
            return base.Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(dto);
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
            return base.Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(dto);
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin(TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin(dto);
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin(TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin dto)
        {
            throw new NotImplementedException();
            return base.Visit_TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_ToggleTrigger_OperatorDto_VarPassThrough_VarReset(ToggleTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            throw new NotImplementedException();
            return base.Visit_ToggleTrigger_OperatorDto_VarPassThrough_VarReset(dto);
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            throw new NotImplementedException();
            return base.Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            throw new NotImplementedException();
            return base.Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Unbundle_OperatorDto(Unbundle_OperatorDto dto)
        {
            throw new NotImplementedException();
            return base.Visit_Unbundle_OperatorDto(dto);
        }
    }
}
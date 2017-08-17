using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary> Under construction. </summary>
    internal class OperatorDtoToCalculatorVisitor_WithCacheOperators : OperatorDtoToCalculatorVisitorBase
    {
        public OperatorDtoToCalculatorVisitor_WithCacheOperators(
            int targetSamplingRate, 
            double secondsBetweenApplyFilterVariables, 
            CalculatorCache calculatorCache, 
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository) 
            : base(
                  targetSamplingRate, 
                  secondsBetweenApplyFilterVariables, 
                  calculatorCache, 
                  curveRepository, 
                  sampleRepository)
        { }

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        //protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(dto);

        //    return ProcessWithDimensionTranformation(dto);
        //}

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Block(Cache_OperatorDto_MultiChannel_Block dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Cubic(Cache_OperatorDto_MultiChannel_Cubic dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Hermite(Cache_OperatorDto_MultiChannel_Hermite dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Line(Cache_OperatorDto_MultiChannel_Line dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Stripe(Cache_OperatorDto_MultiChannel_Stripe dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Block(Cache_OperatorDto_SingleChannel_Block dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Cubic(Cache_OperatorDto_SingleChannel_Cubic dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Hermite(Cache_OperatorDto_SingleChannel_Hermite dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Line(Cache_OperatorDto_SingleChannel_Line dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Stripe(Cache_OperatorDto_SingleChannel_Stripe dto)
        {
            return Process_Cache_OperatorDto(dto);
        }

        // Helpers

        /// <summary>
        /// The Cache operator requires more lengthy code, while most methods are very short,
        /// because it is the only operator type for which you need to 
        /// calculate during optimization time, so calculate while the executable calculation is still being built up.
        /// </summary>
        private IOperatorDto Process_Cache_OperatorDto(Cache_OperatorDto dto)
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
                calculator = new Number_OperatorCalculator(double.NaN);
            }
            else
            {
                IList<ArrayDto> arrayDtos = _calculatorCache.GetCacheArrayDtos(
                    dto.OperatorID,
                    signalCalculator,
                    start,
                    end,
                    samplingRate,
                    dto.ChannelCount,
                    dto.InterpolationTypeEnum,
                    dimensionStack,
                    channelDimensionStack);

                IList<ICalculatorWithPosition> arrayCalculators = arrayDtos.Select(x => ArrayCalculatorFactory.CreateArrayCalculator(x)).ToArray();

                calculator = OperatorCalculatorFactory.Create_Cache_OperatorCalculator(arrayCalculators, dimensionStack, channelDimensionStack);
            }

            _stack.Push(calculator);

            return dto;
        }

        //private IOperatorDto ProcessWithDimensionTranformation<TDto>(TDto dto)
        //    where TDto : OperatorDtoBase, IOperatorDtoWithDimension
        //{
        //    var calculator = _stack.Peek() as IPositionTransformer;
        //    if (calculator == null)
        //    {
        //        throw new InvalidTypeException<IPositionTransformer>(() => calculator);
        //    }

        //    double transformedPosition = calculator.GetTransformedPosition();

        //    DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dto);

        //    dimensionStack.Push(transformedPosition);

        //    return dto;
        //}
    }
}

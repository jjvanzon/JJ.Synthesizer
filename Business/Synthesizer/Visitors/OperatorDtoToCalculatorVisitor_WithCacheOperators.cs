using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoToCalculatorVisitor_WithCacheOperators : OperatorDtoToCalculatorVisitorBase
    {
        private readonly CalculatorCache _calculatorCache;

        public OperatorDtoToCalculatorVisitor_WithCacheOperators(
            int targetSamplingRate, 
            int targetChannelCount, 
            double secondsBetweenApplyFilterVariables, 
            CalculatorCache calculatorCache, 
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository) 
            : base(
                  targetSamplingRate, 
                  targetChannelCount, 
                  secondsBetweenApplyFilterVariables, 
                  calculatorCache, 
                  curveRepository, 
                  sampleRepository)
        { }

        //protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        //{
        //    base.Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(dto);

        //    OperatorCalculatorBase calculator = _stack.Peek();

        //    calculator.Calculate();

        //    return dto;
        //}

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
    }
}

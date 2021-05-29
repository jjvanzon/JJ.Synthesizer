using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.TypeChecking;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary> Under construction. </summary>
    internal class OperatorDtoToCalculatorVisitor_WithCacheOperators : OperatorDtoToCalculatorVisitorBase
    {
        public OperatorDtoToCalculatorVisitor_WithCacheOperators(
            CalculatorCache calculatorCache, 
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository) 
            : base(
                  calculatorCache, 
                  curveRepository, 
                  sampleRepository)
        { }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Block(Cache_OperatorDto_MultiChannel_Block dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Cubic(Cache_OperatorDto_MultiChannel_Cubic dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Hermite(Cache_OperatorDto_MultiChannel_Hermite dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Line(Cache_OperatorDto_MultiChannel_Line dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Stripe(Cache_OperatorDto_MultiChannel_Stripe dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Block(Cache_OperatorDto_SingleChannel_Block dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Cubic(Cache_OperatorDto_SingleChannel_Cubic dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Hermite(Cache_OperatorDto_SingleChannel_Hermite dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Line(Cache_OperatorDto_SingleChannel_Line dto) => Process_Cache_OperatorDto(dto);

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Stripe(Cache_OperatorDto_SingleChannel_Stripe dto) => Process_Cache_OperatorDto(dto);

        // Helpers

        /// <summary>
        /// The Cache operator requires more lengthy code, while most methods are very short,
        /// because it is the only operator type for which you need to 
        /// calculate during optimization time, so calculate while the executable calculation is still being built up.
        /// </summary>
        private IOperatorDto Process_Cache_OperatorDto(Cache_OperatorDto dto)
        {
            Visit_OperatorDto_Base(dto);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase startCalculator = _stack.Pop();
            OperatorCalculatorBase endCalculator = _stack.Pop();
            OperatorCalculatorBase samplingRateCalculator = _stack.Pop();

            OperatorCalculatorBase positionCalculator = _stack.Pop();
            if (!(positionCalculator is VariableInput_OperatorCalculator castedPositionCalculator))
            {
                throw new IsNotTypeException<VariableInput_OperatorCalculator>(() => positionCalculator);
            }

            OperatorCalculatorBase channelCalculator = _stack.Pop();
            if (!(channelCalculator is VariableInput_OperatorCalculator castedChannelCalculator))
            {
                throw new IsNotTypeException<VariableInput_OperatorCalculator>(() => channelCalculator);
            }

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
                    dto.OperationIdentity,
                    signalCalculator,
                    start,
                    end,
                    samplingRate,
                    dto.ChannelCount,
                    dto.InterpolationTypeEnum,
                    castedPositionCalculator,
                    castedChannelCalculator);

                IList<ICalculatorWithPosition> arrayCalculators = arrayDtos.Select(ArrayCalculatorFactory.CreateArrayCalculator).ToArray();

                calculator = OperatorCalculatorFactory.Create_Cache_OperatorCalculator(arrayCalculators, castedPositionCalculator, castedChannelCalculator);
            }

            _stack.Push(calculator);

            return dto;
        }
    }
}

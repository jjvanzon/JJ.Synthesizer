using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class GetDimension_OperatorCalculator : OperatorCalculatorBase
    {
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly DimensionEnum _dimensionEnum;

        public GetDimension_OperatorCalculator(DimensionEnum dimensionEnum)
        {
            _dimensionEnum = dimensionEnum;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Temporary implementation (2016-03-10), before we have more dimension values on the call stack.
            switch (_dimensionEnum)
            {
                case DimensionEnum.Time:
                    return time;

                case DimensionEnum.Channel:
                    return channelIndex;

                default:
                    return DEFAULT_DIMENSION_VALUE;
            }
        }
    }
}

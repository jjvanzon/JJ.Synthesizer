using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class GetDimension_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly int _dimensionEnumInt;

        public GetDimension_OperatorCalculator(DimensionEnum dimensionEnum)
        {
            _dimensionEnumInt = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            return dimensionStack.Get(_dimensionEnumInt);
        }
    }
}

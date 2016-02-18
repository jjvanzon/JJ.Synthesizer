using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Cache_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ArrayCalculatorBase[] _arrayCalculator;

        public Cache_OperatorCalculator(ArrayCalculatorBase[] arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculator = arrayCalculators;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator[channelIndex].CalculateValue(time);
        }
    }
}

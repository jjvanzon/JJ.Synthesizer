using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Cache_OperatorCalculator_SingleChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel(TArrayCalculator arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel<TArrayCalculator> : OperatorCalculatorBase
        where TArrayCalculator : ArrayCalculatorBase
    {
        private readonly TArrayCalculator[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel(IList<TArrayCalculator> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }
}
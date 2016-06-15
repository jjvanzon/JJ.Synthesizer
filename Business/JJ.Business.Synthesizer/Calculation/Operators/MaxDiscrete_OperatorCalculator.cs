using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MaxDiscrete_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _firstOperandCalculator;
        private readonly OperatorCalculatorBase[] _remainingOperandCalculators;
        private readonly double _remainingOperandCalculatorsMaxIndex;
        
        public MaxDiscrete_OperatorCalculator(IList<OperatorCalculatorBase> operandCalculators)
            : base(operandCalculators)
        {
            _firstOperandCalculator = operandCalculators.First();
            _remainingOperandCalculators = operandCalculators.Skip(1).ToArray();
            _remainingOperandCalculatorsMaxIndex = _remainingOperandCalculators.Length - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double result = _firstOperandCalculator.Calculate();

            for (int i = 0; i < _remainingOperandCalculatorsMaxIndex; i++)
            {
                double result2 = _remainingOperandCalculators[i].Calculate();

                if (result2 > result)
                {
                    result = result2;
                }
            }

            return result;
        }
    }
}

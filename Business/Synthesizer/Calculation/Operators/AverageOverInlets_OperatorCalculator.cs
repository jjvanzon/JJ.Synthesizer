using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // You could imagine many more optimized calculations, such as first operand is const and several,
    // that omit the loop, but future optimizations will just make that work obsolete again.

    internal class AverageOverInlets_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _firstOperandCalculator;
        private readonly OperatorCalculatorBase[] _remainingOperandCalculators;
        private readonly double _remainingOperandCalculatorsCount;
        private readonly double _count;

        public AverageOverInlets_OperatorCalculator(IList<OperatorCalculatorBase> operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);
            if (operandCalculators.Count == 0) throw new CollectionEmptyException(() => operandCalculators);

            _firstOperandCalculator = operandCalculators.First();
            _remainingOperandCalculators = operandCalculators.Skip(1).ToArray();
            _remainingOperandCalculatorsCount = _remainingOperandCalculators.Length;
            _count = operandCalculators.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double sum = _firstOperandCalculator.Calculate();

            for (int i = 0; i < _remainingOperandCalculatorsCount; i++)
            {
                double value = _remainingOperandCalculators[i].Calculate();
                sum += value;
            }

            return sum / _count;
        }
    }
}

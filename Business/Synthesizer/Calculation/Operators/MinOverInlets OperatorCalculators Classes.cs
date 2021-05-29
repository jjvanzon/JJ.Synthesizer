using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MinOverInlets_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _firstOperandCalculator;
        private readonly OperatorCalculatorBase[] _remainingOperandCalculators;
        private readonly double _remainingOperandCalculatorsCount;
        
        public MinOverInlets_OperatorCalculator(IList<OperatorCalculatorBase> operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);
            if (operandCalculators.Count <= 2) throw new LessThanOrEqualException(() => operandCalculators.Count, 2);

            _firstOperandCalculator = operandCalculators.First();
            _remainingOperandCalculators = operandCalculators.Skip(1).ToArray();
            _remainingOperandCalculatorsCount = _remainingOperandCalculators.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double result = _firstOperandCalculator.Calculate();

            for (int i = 0; i < _remainingOperandCalculatorsCount; i++)
            {
                double result2 = _remainingOperandCalculators[i].Calculate();

                if (result2 < result)
                {
                    result = result2;
                }
            }

            return result;
        }
    }
}

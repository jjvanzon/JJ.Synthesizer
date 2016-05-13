using System;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Adder_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _operandCalculators;

        public Adder_OperatorCalculator(OperatorCalculatorBase[] operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);

            _operandCalculators = operandCalculators;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double result = 0;

            for (int i = 0; i < _operandCalculators.Length; i++)
            {
                double result2 = _operandCalculators[i].Calculate();

                result += result2;
            }

            return result;
        }
    }
}

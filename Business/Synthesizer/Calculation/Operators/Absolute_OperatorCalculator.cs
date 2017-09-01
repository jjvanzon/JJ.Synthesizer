using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Absolute_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numberCalculator;

        public Absolute_OperatorCalculator(OperatorCalculatorBase numberCalculator)
            : base(new[] { numberCalculator })
        {
            _numberCalculator = numberCalculator ?? throw new NullException(() => numberCalculator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double number = _numberCalculator.Calculate();

            if (number >= 0.0)
            {
                return number;
            }
            else
            {
                return -number;
            }
        }
    }
}
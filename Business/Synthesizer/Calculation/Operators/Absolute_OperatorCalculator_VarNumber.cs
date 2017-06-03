using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Absolute_OperatorCalculator_VarNumber : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numberCalculator;

        public Absolute_OperatorCalculator_VarNumber(OperatorCalculatorBase numberCalculator)
            : base(new[] { numberCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(numberCalculator, () => numberCalculator);

            _numberCalculator = numberCalculator;
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
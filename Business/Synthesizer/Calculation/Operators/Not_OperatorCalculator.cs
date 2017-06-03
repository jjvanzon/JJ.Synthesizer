using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Not_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numberCalculator;

        public Not_OperatorCalculator(OperatorCalculatorBase numberCalculator)
            : base(new[] { numberCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(numberCalculator, () => numberCalculator);

            _numberCalculator = numberCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double number = _numberCalculator.Calculate();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool isFalse = number == 0.0;

            if (isFalse) return 1.0;
            else return 0.0;
        }
    }
}
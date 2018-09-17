using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Not_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numberCalculator;

        public Not_OperatorCalculator(OperatorCalculatorBase numberCalculator)
            : base(new[] { numberCalculator })
            => _numberCalculator = numberCalculator;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double number = _numberCalculator.Calculate();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool isFalse = number == 0.0;

            return isFalse ? 1.0 : 0.0;
        }
    }
}
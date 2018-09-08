using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LogN_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _numberCalculator;
        private readonly OperatorCalculatorBase _baseCalculator;

        public LogN_OperatorCalculator(OperatorCalculatorBase numberCalculator, OperatorCalculatorBase baseCalculator)
            : base(new[] { numberCalculator, baseCalculator })
        {
            _numberCalculator = numberCalculator;
            _baseCalculator = baseCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double number = _numberCalculator.Calculate();
            double @base = _baseCalculator.Calculate();

            return Math.Log(number, @base);
        }
    }
}
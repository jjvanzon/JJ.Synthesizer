using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Power_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _baseCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;

        public Power_OperatorCalculator(OperatorCalculatorBase baseCalculator, OperatorCalculatorBase exponentCalculator)
            : base(new[] { baseCalculator, exponentCalculator })
        {
            _baseCalculator = baseCalculator;
            _exponentCalculator = exponentCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double @base = _baseCalculator.Calculate();
            double exponent = _exponentCalculator.Calculate();
            return Math.Pow(@base, exponent);
        }
    }
}
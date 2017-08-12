using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Remainder_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Remainder_OperatorCalculator(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
            : base(new[] { aCalculator, bCalculator })
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (aCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);
            if (bCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            return a % b;
        }
    }

}

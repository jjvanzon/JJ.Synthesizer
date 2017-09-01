using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Round_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly OperatorCalculatorBase _offsetCalculator;

        public Round_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase offsetCalculator)
            : base(new[] { signalCalculator, stepCalculator, offsetCalculator })
        {
            _signalCalculator = signalCalculator;
            _stepCalculator = stepCalculator;
            _offsetCalculator = offsetCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double step = _stepCalculator.Calculate();
            double offset = _offsetCalculator.Calculate();

            double result = MathHelper.RoundWithStep(signal, step, offset);
            return result;
        }
    }
}
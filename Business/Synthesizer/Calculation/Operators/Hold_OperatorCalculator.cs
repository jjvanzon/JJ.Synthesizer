using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Hold_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private double _value;

        public Hold_OperatorCalculator(OperatorCalculatorBase signalCalculator)
            : base(new[] { signalCalculator })
        {
            _signalCalculator = signalCalculator;

            ResetPrivate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate() => _value;

        public override void Reset() => ResetPrivate();

        private void ResetPrivate() => _value = _signalCalculator.Calculate();
    }
}
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SetDimension_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _passThroughCalculator;
        private readonly OperatorCalculatorBase _numberCalculator;
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

        public SetDimension_OperatorCalculator(
            OperatorCalculatorBase passThroughCalculator,
            OperatorCalculatorBase numberCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(new[] { passThroughCalculator, numberCalculator, positionOutputCalculator })
        {
            _passThroughCalculator = passThroughCalculator;
            _numberCalculator = numberCalculator;
            _positionOutputCalculator = positionOutputCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _numberCalculator.Calculate();

            _positionOutputCalculator._value = position;

            double outputValue = _passThroughCalculator.Calculate();

            return outputValue;
        }

        public override void Reset()
        {
            double position = _numberCalculator.Calculate();

            _positionOutputCalculator._value = position;

            base.Reset();
        }
    }
}

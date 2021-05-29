namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal sealed class Interpolate_OperatorCalculator_Block_LookAhead
        : Interpolate_OperatorCalculator_Base_2X1Y_LookAhead
    {
        public Interpolate_OperatorCalculator_Block_LookAhead(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator)
            => ResetNonRecursive();

        protected override void ResetNonRecursive() => Interpolate_OperatorCalculator_Block_Helper.ResetNonRecursive(this);
    }
}
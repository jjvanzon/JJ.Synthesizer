namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator : Resample_OperatorCalculatorCubicRamses
    {
        public Resample_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
            : base(signalCalculator, samplingRateCalculator)
        { }
    }
}

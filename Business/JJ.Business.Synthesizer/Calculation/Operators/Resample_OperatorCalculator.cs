namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator : Resample_OperatorCalculator_CubicRamses
    {
        public Resample_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
            : base(signalCalculator, samplingRateCalculator)
        { }
    }
}

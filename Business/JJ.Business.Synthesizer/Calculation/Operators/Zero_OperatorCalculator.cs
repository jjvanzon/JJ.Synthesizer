namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Zero_OperatorCalculator : OperatorCalculatorBase
    {
        public override double Calculate(double time, int channelIndex)
        {
            return 0;
        }
    }
}

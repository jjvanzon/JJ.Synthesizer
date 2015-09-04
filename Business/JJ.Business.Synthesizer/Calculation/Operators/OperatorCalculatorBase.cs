namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase // Dispatch through a base class is faster than using an interface.
    {
        public abstract double Calculate(double time, int channelIndex);
    }
}

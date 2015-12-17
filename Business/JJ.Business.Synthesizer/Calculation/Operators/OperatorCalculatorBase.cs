namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Dispatch through a base class is faster than using an interface.
    internal abstract class OperatorCalculatorBase 
    {
        public abstract double Calculate(double time, int channelIndex);
    }
}

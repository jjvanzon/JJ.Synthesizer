namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Dispatch through a base class is faster than using an interface.

    /// <summary>
    /// If you have child OperatorCalculators use OperatorCalculatorBase_WithChildCalculators instead.
    /// </summary>
    internal abstract class OperatorCalculatorBase
    {
        public abstract double Calculate(DimensionStack dimensionStack);

        /// <summary> Does nothing </summary>
        public virtual void Reset(DimensionStack dimensionStack) { }
    }
}

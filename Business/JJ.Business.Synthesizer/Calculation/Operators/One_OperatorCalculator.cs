using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// Slight performace gain compared to the Number_OperatorCalculator.
    /// It has Number_OperatorCalculator as a base class to make it participate in the 
    /// optimization mechanisms in OptimizedPatchCalculatorVisitor.
    /// </summary>
    internal class One_OperatorCalculator : Number_OperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate(DimensionStack dimensionStack)
        {
            return 1;
        }
    }
}

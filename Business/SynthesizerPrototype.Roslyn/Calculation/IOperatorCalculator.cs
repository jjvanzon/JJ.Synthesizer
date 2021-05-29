using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Calculation
{
    /// <summary> Public to be accessible to run-time generated assemblies. </summary>
    public interface IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Calculate();
    }
}

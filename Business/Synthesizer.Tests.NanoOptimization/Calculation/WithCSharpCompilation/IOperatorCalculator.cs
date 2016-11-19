using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithCSharpCompilation
{
    /// <summary> Public to be accessible to run-time generated assemblies. </summary>
    public interface IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Calculate();
    }
}

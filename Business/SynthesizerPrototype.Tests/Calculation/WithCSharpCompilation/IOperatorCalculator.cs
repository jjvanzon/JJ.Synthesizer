using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithCSharpCompilation
{
    /// <summary> Public to be accessible to run-time generated assemblies. </summary>
    public interface IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Calculate();
    }
}

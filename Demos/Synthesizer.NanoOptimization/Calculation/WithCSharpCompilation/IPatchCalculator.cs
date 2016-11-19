using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithCSharpCompilation
{
    /// <summary> Public to be accessible to run-time generated assemblies. </summary>
    public interface IPatchCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double[] Calculate(double startTime, double frameDuration);
        void Reset();
        void SetInput(int listIndex, double input);
    }
}
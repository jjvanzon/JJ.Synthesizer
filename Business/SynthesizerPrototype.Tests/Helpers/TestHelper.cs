using System;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers
{
    internal static class TestHelper
    {
        public static string GetPerformanceInfoMessage(int iterationCount, TimeSpan elapsed)
            => $"{iterationCount} iterations, time elapsed: {elapsed.TotalMilliseconds} ms ";
    }
}
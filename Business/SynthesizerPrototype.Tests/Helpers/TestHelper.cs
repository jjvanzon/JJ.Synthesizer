using System;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers
{
    internal class TestHelper
    {
        public static string GetPerformanceInfoMessage(int iterationCount, TimeSpan elapsed)
        {
            return $"{iterationCount} iterations, time elapsed: {elapsed.TotalMilliseconds} ms ";
        }
    }
}

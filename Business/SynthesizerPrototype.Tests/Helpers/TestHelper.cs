using System;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers
{
    internal class TestHelper
    {
        public static string GetPerformanceInfoMessage(int iterationCount, TimeSpan elapsed)
        {
            return String.Format("{0} iterations, time elapsed: {1} ms ", iterationCount, elapsed.TotalMilliseconds);
        }
    }
}

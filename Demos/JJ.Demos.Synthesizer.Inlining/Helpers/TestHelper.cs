using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal class TestHelper
    {
        public const double SAMPLING_RATE = 44100.0;
        public const double SECONDS = 10.0;
        public const int ITERATION_COUNT = (int)(SECONDS * SAMPLING_RATE);

        public static string GetPerformanceInfoMessage(int iterationCount, TimeSpan elapsed)
        {
            return String.Format("{0} iterations, time elapsed: {1} ms ", iterationCount, elapsed.TotalMilliseconds);
        }
    }
}

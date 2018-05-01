using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	internal static class TestHelper
	{
		public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
		{
			const int frameCount = 1;
			var buffer = new float[1];

			patchCalculator.Calculate(buffer, frameCount, time);

			return buffer[0];
		}
	}
}

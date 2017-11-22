using System.IO;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	internal static class TestHelper
	{
		private const string VIOLIN_16BIT_MONO_RAW_FILE_NAME = "violin_16bit_mono.raw";
		private const string VIOLIN_16BIT_MONO_44100_WAV_FILE_NAME = "violin_16bit_mono_44100.wav";

		public static Stream GetViolin16BitMonoRawStream()
		{
			Stream stream = EmbeddedResourceHelper.GetEmbeddedResourceStream(typeof(TestHelper).Assembly, "TestResources", VIOLIN_16BIT_MONO_RAW_FILE_NAME);
			return stream;
		}

		public static Stream GetViolin16BitMono44100WavStream()
		{
			Stream stream = EmbeddedResourceHelper.GetEmbeddedResourceStream(typeof(TestHelper).Assembly, "TestResources", VIOLIN_16BIT_MONO_44100_WAV_FILE_NAME);
			return stream;
		}

		public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
		{
			const int frameCount = 1;
			var buffer = new float[1];

			patchCalculator.Calculate(buffer, frameCount, time);

			return buffer[0];
		}
	}
}

using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class AudioFileOutputExtensions
	{
		public static double GetEndTime(this AudioFileOutput audioFileOutput)
		{
			if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

			return audioFileOutput.StartTime + audioFileOutput.Duration;
		}

		public static int GetChannelCount(this AudioFileOutput audioFileOutput)
		{
			if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
			return audioFileOutput.SpeakerSetup.SpeakerSetupChannels.Count;
		}

		public static double GetFrameDuration(this AudioFileOutput audioFileOutput)
		{
			if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (audioFileOutput.SamplingRate == 0.0) throw new ZeroException(() => audioFileOutput.SamplingRate);

			// ReSharper disable once RedundantCast
			double frameDuration = 1.0 / (double)audioFileOutput.SamplingRate;
			return frameDuration;
		}
	}
}

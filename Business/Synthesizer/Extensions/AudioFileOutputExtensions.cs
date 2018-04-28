using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
// ReSharper disable RedundantCast

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
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (audioFileOutput.TimeMultiplier == 0.0) throw new ZeroException(() => audioFileOutput.TimeMultiplier);

			double frameDuration = 1.0 / (double)audioFileOutput.SamplingRate / (double)audioFileOutput.TimeMultiplier;
			return frameDuration;
		}

		public static int GetFrameCount(this AudioFileOutput audioFileOutput)
		{
			double duration = audioFileOutput.Duration;
			double frameDuration = audioFileOutput.GetFrameDuration();
			int frameCount = (int)(duration / frameDuration);
			return frameCount;
		}

		public static int GetValueCount(this AudioFileOutput audioFileOutput)
		{
			int frameCount = audioFileOutput.GetFrameCount();
			int channelCount = audioFileOutput.GetChannelCount();
			int valueCount = frameCount * channelCount;
			return valueCount;
		}
	}
}

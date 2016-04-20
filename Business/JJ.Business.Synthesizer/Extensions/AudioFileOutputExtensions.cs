using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;

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

        public static double GetSampleDuration(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            if (audioFileOutput.SamplingRate == 0.0) throw new ZeroException(() => audioFileOutput.SamplingRate);

            double sampleDuration = 1 / audioFileOutput.SamplingRate;
            return sampleDuration;
        }
    }
}

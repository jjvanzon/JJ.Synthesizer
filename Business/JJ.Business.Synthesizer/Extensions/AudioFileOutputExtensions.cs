using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

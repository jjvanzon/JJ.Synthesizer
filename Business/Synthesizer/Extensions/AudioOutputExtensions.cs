using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class AudioOutputExtensions
    {
        public static int GetChannelCount(this AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.SpeakerSetup.SpeakerSetupChannels.Count;
        }

        public static double GetFrameDuration(this AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (entity.SamplingRate == 0.0) throw new ZeroException(() => entity.SamplingRate);

            // ReSharper disable once RedundantCast
            double frameDuration = 1.0 / (double)entity.SamplingRate;
            return frameDuration;
        }

        /// <summary> 
        /// The desired buffer frame count is only an indication, 
        /// that could be adapted to the audio device's capabilities.
        /// </summary>
        public static double GetDesiredBufferFrameCount(this AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            double bufferFrameCount = entity.SamplingRate * entity.DesiredBufferDuration;

            return bufferFrameCount;
        }
    }
}

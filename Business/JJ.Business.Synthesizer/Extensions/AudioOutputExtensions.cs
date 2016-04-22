using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class AudioOutputExtensions
    {
        public static int GetChannelCount(this AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.SpeakerSetup.SpeakerSetupChannels.Count;
        }

        public static double GetSampleDuration(this AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.SamplingRate == 0.0) throw new ZeroException(() => entity.SamplingRate);

            double sampleDuration = 1.0 / (double)entity.SamplingRate;
            return sampleDuration;
        }

        public static int GetBufferFrameCount(this AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            double bufferFrameCount = entity.SamplingRate * entity.BufferDuration;

            if (!ConversionHelper.CanCastToInt32(bufferFrameCount))
            {
                throw new Exception(String.Format("bufferFrameCount '{0}' cannot be cast to Int32.", bufferFrameCount));
            }

            return (int)bufferFrameCount;
        }
    }
}

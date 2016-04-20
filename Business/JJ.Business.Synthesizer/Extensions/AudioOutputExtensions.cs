using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class AudioOutputExtensions
    {
        public static double GetSampleDuration(this AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);
            if (audioOutput.SamplingRate == 0.0) throw new ZeroException(() => audioOutput.SamplingRate);

            double sampleDuration = 1.0 / (double)audioOutput.SamplingRate;
            return sampleDuration;
        }
    }
}

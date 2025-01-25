using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class AudioLengthExtensionWishes
    {
        // A Duration Attribute
        
        // Synth-Bound
        
        public static double AudioLength(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength.Value;
        }
        
        public static SynthWishes AudioLength(this SynthWishes obj, double? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value);
            return obj;
        }
        
        public static double AudioLength(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength.Value;
        }
        
        public static FlowNode AudioLength(this FlowNode obj, double? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value);
            return obj;
        }
        
        internal static double AudioLength(this ConfigResolver obj, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength(synthWishes).Value;
        }
        
        internal static ConfigResolver AudioLength(this ConfigResolver obj, double? value, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value, synthWishes);
            return obj;
        }
        
        // Global-Bound
        
        internal static double? AudioLength(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioLength;
        }

        // Tape-Bound
        
        public static double AudioLength(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }
        
        public static Tape AudioLength(this Tape obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }
        
        public static double AudioLength(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeConfig AudioLength(this TapeConfig obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        public static double AudioLength(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeAction AudioLength(this TapeAction obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        public static double AudioLength(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeActions AudioLength(this TapeActions obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static double AudioLength(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            // TODO: From bytes[] / filePath?
            return AudioLength(obj.UnderlyingAudioFileOutput);
        }
        
        public static Buff AudioLength(this Buff obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.AudioLength(AssertAudioLength(value));
            return obj;
        }
        
        public static double AudioLength(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }
        
        public static AudioFileOutput AudioLength(this AudioFileOutput obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }
        
        // Independent after Taping
        
        public static double AudioLength(this Sample obj) => obj.GetDuration();
        
        /// <summary>
        /// Adjusts sampling rate to match the new audio length.
        /// </summary>
        public static Sample AudioLength(this Sample obj, double newLength)
        {
            if (obj == null) throw new NullException(() => obj);
            
            double oldLength       = obj.AudioLength();
            int    oldSamplingRate = obj.SamplingRate;
            double lengthRatio     = newLength / oldLength;
            int    newSamplingRate = (int)(oldSamplingRate / lengthRatio);
            
            AssertAudioLength(oldLength);
            AssertSamplingRate(oldSamplingRate);
            AssertAudioLength(newLength);
            AssertSamplingRate(newSamplingRate);
            
            obj.SamplingRate = newSamplingRate;
            return obj;
        }
                
        public static double AudioLength(this AudioInfoWish infoWish, int courtesyFrames)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return AudioLengthFromFrameCount(infoWish.FrameCount, infoWish.SamplingRate, courtesyFrames);
        }
        
        public static AudioInfoWish AudioLength(this AudioInfoWish infoWish, double value, int courtesyFrames)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = FrameCount(value, infoWish.SamplingRate, courtesyFrames);
            return infoWish;
        }
        
        public static double AudioLength(this AudioFileInfo info, int courtesyFrames) => info.ToWish().AudioLength(courtesyFrames);
        
        public static AudioFileInfo AudioLength(this AudioFileInfo info, double value, int courtesyFrames)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = FrameCount(value, info.SamplingRate, courtesyFrames);
            return info;
        }

        // Immutable
        
        public static double AudioLength(this WavHeaderStruct obj, int courtesyFrames)
            => obj.ToWish().AudioLength(courtesyFrames);
        
        public static WavHeaderStruct AudioLength(this WavHeaderStruct obj, double value, int courtesyFrames) 
            => obj.ToWish().AudioLength(value, courtesyFrames).ToWavHeader();

        // Conversion Formula

        // From FrameCount

        public static double? AudioLengthFromFrameCount(this int? frameCount, int samplingRate, int courtesyFrames)
            => ConfigWishes.AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        public static double AudioLengthFromFrameCount(this int frameCount, int samplingRate, int courtesyFrames)
            => ConfigWishes.AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        public static double? AudioLength(this int? frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.AudioLength(frameCount, samplingRate, courtesyFrames);

        public static double AudioLength(this int frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.AudioLength(frameCount, samplingRate, courtesyFrames);

        // From ByteCount

        public static double? AudioLengthFromByteCount(this int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double AudioLengthFromByteCount(this int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double? AudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double AudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
    }

    // Conversion Formula
    
    public partial class ConfigWishes
    {
        // From FrameCount
        
        public static double? AudioLengthFromFrameCount(int? frameCount, int samplingRate, int courtesyFrames) 
            => (double?)AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames) / AssertSamplingRate(samplingRate);

        public static double AudioLengthFromFrameCount(int frameCount, int samplingRate, int courtesyFrames) 
            => (double)AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames) / AssertSamplingRate(samplingRate);

        public static double? AudioLength(int? frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);
        
        public static double AudioLength(int frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        // From ByteCount
        
        public static double? AudioLengthFromByteCount(int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
        {
            if (!Has(byteCount)) return byteCount;
            return AudioLengthFromByteCount(byteCount.Value, frameSize, samplingRate, headerLength, courtesyFrames);
        }

        public static double AudioLengthFromByteCount(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
        {
            AssertByteCount(byteCount);
            AssertHeaderLength(headerLength);
            AssertFrameSize(frameSize);
            AssertSamplingRate(samplingRate);
            AssertCourtesyFrames(courtesyFrames);
            AssertFrameSize(frameSize);

            double frameCount = (double)(byteCount - headerLength) / frameSize;
            return (frameCount - courtesyFrames) / samplingRate;
        }
        
        public static double? AudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
        
        public static double AudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
    }
}
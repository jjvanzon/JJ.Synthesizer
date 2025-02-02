using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class AudioLengthExtensionWishes
    {
        // A Duration Attribute
        
        // Synth-Bound
        
        public static double AudioLength(this SynthWishes obj) => GetAudioLength(obj);
        public static double GetAudioLength(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength.Value;
        }
        
        public static SynthWishes AudioLength(this SynthWishes obj, double? value) => SetAudioLength(obj, value);
        public static SynthWishes WithAudioLength(this SynthWishes obj, double? value) => SetAudioLength(obj, value);
        public static SynthWishes SetAudioLength(this SynthWishes obj, double? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value);
            return obj;
        }
        
        public static double AudioLength(this FlowNode obj) => GetAudioLength(obj);
        public static double GetAudioLength(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength.Value;
        }
        
        public static FlowNode AudioLength(this FlowNode obj, double? value) => SetAudioLength(obj, value);
        public static FlowNode WithAudioLength(this FlowNode obj, double? value) => SetAudioLength(obj, value);
        public static FlowNode SetAudioLength(this FlowNode obj, double? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value);
            return obj;
        }
        
        internal static double AudioLength(this ConfigResolver obj, SynthWishes synthWishes) => GetAudioLength(obj, synthWishes);
        internal static double GetAudioLength(this ConfigResolver obj, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength(synthWishes).Value;
        }
        
        internal static ConfigResolver AudioLength(this ConfigResolver obj, double? value, SynthWishes synthWishes) => SetAudioLength(obj, value, synthWishes);
        internal static ConfigResolver WithAudioLength(this ConfigResolver obj, double? value, SynthWishes synthWishes) => SetAudioLength(obj, value, synthWishes);
        internal static ConfigResolver SetAudioLength(this ConfigResolver obj, double? value, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value, synthWishes);
            return obj;
        }
        
        // Global-Bound
        
        internal static double? AudioLength(this ConfigSection obj) => GetAudioLength(obj);
        internal static double? GetAudioLength(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioLength;
        }

        // Tape-Bound
        
        public static double AudioLength(this Tape obj) => GetAudioLength(obj);
        public static double GetAudioLength(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }
        
        public static Tape AudioLength(this Tape obj, double value) => SetAudioLength(obj, value);
        public static Tape WithAudioLength(this Tape obj, double value) => SetAudioLength(obj, value);
        public static Tape SetAudioLength(this Tape obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }
        
        public static double AudioLength(this TapeConfig obj) => GetAudioLength(obj);
        public static double GetAudioLength(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeConfig AudioLength(this TapeConfig obj, double value) => SetAudioLength(obj, value);
        public static TapeConfig WithAudioLength(this TapeConfig obj, double value) => SetAudioLength(obj, value);
        public static TapeConfig SetAudioLength(this TapeConfig obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        public static double AudioLength(this TapeAction obj) => GetAudioLength(obj);
        public static double GetAudioLength(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeAction AudioLength(this TapeAction obj, double value) => SetAudioLength(obj, value);
        public static TapeAction WithAudioLength(this TapeAction obj, double value) => SetAudioLength(obj, value);
        public static TapeAction SetAudioLength(this TapeAction obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        public static double AudioLength(this TapeActions obj) => GetAudioLength(obj);
        public static double GetAudioLength(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeActions AudioLength(this TapeActions obj, double value) => SetAudioLength(obj, value);
        public static TapeActions WithAudioLength(this TapeActions obj, double value) => SetAudioLength(obj, value);
        public static TapeActions SetAudioLength(this TapeActions obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static double AudioLength(this Buff obj) => GetAudioLength(obj);
        public static double GetAudioLength(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            // TODO: From bytes[] / filePath?
            return AudioLength(obj.UnderlyingAudioFileOutput);
        }
        
        public static Buff AudioLength(this Buff obj, double value) => SetAudioLength(obj, value);
        public static Buff WithAudioLength(this Buff obj, double value) => SetAudioLength(obj, value);
        public static Buff SetAudioLength(this Buff obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.AudioLength(AssertAudioLength(value));
            return obj;
        }
        
        public static double AudioLength(this AudioFileOutput obj) => GetAudioLength(obj);
        public static double GetAudioLength(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }
        
        public static AudioFileOutput AudioLength(this AudioFileOutput obj, double value) => SetAudioLength(obj, value);
        public static AudioFileOutput WithAudioLength(this AudioFileOutput obj, double value) => SetAudioLength(obj, value);
        public static AudioFileOutput SetAudioLength(this AudioFileOutput obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }
        
        // Independent after Taping
        
        public static double AudioLength(this Sample obj) => GetAudioLength(obj);
        public static double GetAudioLength(this Sample obj)
        {
            return obj.GetDuration();
        }
        
        /// <inheritdoc cref="docs._sampleaudiolength" />
        public static Sample AudioLength(this Sample obj, double newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="docs._sampleaudiolength" />
        public static Sample WithAudioLength(this Sample obj, double newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="docs._sampleaudiolength" />
        public static Sample SetAudioLength(this Sample obj, double newLength)
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
                
        public static double AudioLength(this AudioInfoWish infoWish, int courtesyFrames) => GetAudioLength(infoWish, courtesyFrames);
        public static double GetAudioLength(this AudioInfoWish infoWish, int courtesyFrames)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return AudioLengthFromFrameCount(infoWish.FrameCount, infoWish.SamplingRate, courtesyFrames);
        }
        
        public static AudioInfoWish AudioLength(this AudioInfoWish infoWish, double value, int courtesyFrames) => SetAudioLength(infoWish, value, courtesyFrames);
        public static AudioInfoWish WithAudioLength(this AudioInfoWish infoWish, double value, int courtesyFrames) => SetAudioLength(infoWish, value, courtesyFrames);
        public static AudioInfoWish SetAudioLength(this AudioInfoWish infoWish, double value, int courtesyFrames)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = FrameCountFromAudioLength(value, infoWish.SamplingRate, courtesyFrames);
            return infoWish;
        }
        
        public static double AudioLength(this AudioFileInfo info, int courtesyFrames) => GetAudioLength(info, courtesyFrames);
        public static double GetAudioLength(this AudioFileInfo info, int courtesyFrames)
        {
            return info.ToWish().AudioLength(courtesyFrames);
        }
        
        public static AudioFileInfo AudioLength(this AudioFileInfo info, double value, int courtesyFrames) => SetAudioLength(info, value, courtesyFrames);
        public static AudioFileInfo WithAudioLength(this AudioFileInfo info, double value, int courtesyFrames) => SetAudioLength(info, value, courtesyFrames);
        public static AudioFileInfo SetAudioLength(this AudioFileInfo info, double value, int courtesyFrames)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = FrameCountFromAudioLength(value, info.SamplingRate, courtesyFrames);
            return info;
        }

        // Immutable
        
        public static double AudioLength(this WavHeaderStruct obj, int courtesyFrames) => GetAudioLength(obj, courtesyFrames);
        public static double GetAudioLength(this WavHeaderStruct obj, int courtesyFrames)
        {
            return obj.ToWish().AudioLength(courtesyFrames);
        }
        
        public static WavHeaderStruct AudioLength(this WavHeaderStruct obj, double value, int courtesyFrames) => SetAudioLength(obj, value, courtesyFrames);
        public static WavHeaderStruct WithAudioLength(this WavHeaderStruct obj, double value, int courtesyFrames) => SetAudioLength(obj, value, courtesyFrames);
        public static WavHeaderStruct SetAudioLength(this WavHeaderStruct obj, double value, int courtesyFrames)
        {
            return obj.ToWish().AudioLength(value, courtesyFrames).ToWavHeader();
        }
        
        // Conversion Formula

        // From FrameCount

        public static double? AudioLength(this int? frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.AudioLength(frameCount, samplingRate, courtesyFrames);

        public static double AudioLength(this int frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.AudioLength(frameCount, samplingRate, courtesyFrames);

        public static double? GetAudioLength(this int? frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.GetAudioLength(frameCount, samplingRate, courtesyFrames);

        public static double GetAudioLength(this int frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.GetAudioLength(frameCount, samplingRate, courtesyFrames);

        public static double? ToAudioLength(this int? frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.ToAudioLength(frameCount, samplingRate, courtesyFrames);

        public static double ToAudioLength(this int frameCount, int samplingRate, int courtesyFrames) 
            => ConfigWishes.ToAudioLength(frameCount, samplingRate, courtesyFrames);

        public static double? FrameCountToAudioLength(this int? frameCount, int samplingRate, int courtesyFrames)
            => ConfigWishes.FrameCountToAudioLength(frameCount, samplingRate, courtesyFrames);

        public static double FrameCountToAudioLength(this int frameCount, int samplingRate, int courtesyFrames)
            => ConfigWishes.FrameCountToAudioLength(frameCount, samplingRate, courtesyFrames);

        public static double? AudioLengthFromFrameCount(this int? frameCount, int samplingRate, int courtesyFrames)
            => ConfigWishes.AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        public static double AudioLengthFromFrameCount(this int frameCount, int samplingRate, int courtesyFrames)
            => ConfigWishes.AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        // From ByteCount

        public static double? AudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double AudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double? GetAudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.GetAudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double GetAudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.GetAudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double? ToAudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.ToAudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double ToAudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.ToAudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double? ByteCountToAudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountToAudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double ByteCountToAudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountToAudioLength(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double? AudioLengthFromByteCount(this int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);

        public static double AudioLengthFromByteCount(this int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => ConfigWishes.AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
    }

    // Conversion Formula
    
    public partial class ConfigWishes
    {
        // From FrameCount

        public static double? AudioLength(int? frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);
        
        public static double AudioLength(int frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        public static double? GetAudioLength(int? frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);
        
        public static double GetAudioLength(int frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        public static double? ToAudioLength(int? frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);
        
        public static double ToAudioLength(int frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);

        public static double? FrameCountToAudioLength(int? frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);
        
        public static double FrameCountToAudioLength(int frameCount, int samplingRate, int courtesyFrames) 
            => AudioLengthFromFrameCount(frameCount, samplingRate, courtesyFrames);
        
        public static double? AudioLengthFromFrameCount(int? frameCount, int samplingRate, int courtesyFrames) 
            => (double?)AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames) / AssertSamplingRate(samplingRate);

        public static double AudioLengthFromFrameCount(int frameCount, int samplingRate, int courtesyFrames) 
            => (double)AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames) / AssertSamplingRate(samplingRate);

        // From ByteCount
 
        public static double? AudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
        
        public static double AudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
 
        public static double? GetAudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
        
        public static double GetAudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
 
        public static double? ToAudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
        
        public static double ToAudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
 
        public static double? ByteCountToAudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
        
        public static double ByteCountToAudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength, courtesyFrames);
        
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
    }
}
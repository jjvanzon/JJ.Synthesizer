using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Duration Attribute
        
        public static double AudioLength(int frameCount, int samplingRate)
            => (double)frameCount / samplingRate;
        
        public static double AudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames = 0)
            => (double)(byteCount - headerLength) / frameSize / samplingRate - courtesyFrames * frameSize;
        
        public static double AudioLength(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength.Value;
        }
        
        public static SynthWishes AudioLength(this SynthWishes obj, double value)
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
        
        public static FlowNode AudioLength(this FlowNode obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value);
            return obj;
        }
        
        public static double AudioLength(this ConfigWishes obj, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength(synthWishes).Value;
        }
        
        public static ConfigWishes AudioLength(this ConfigWishes obj, double value, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value, synthWishes);
            return obj;
        }
        
        internal static double AudioLength(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioLength ?? ConfigWishes.DefaultAudioLength;
        }
        
        public static double AudioLength(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            // TODO: From bytes[] / filePath?
            return AudioLength(obj.UnderlyingAudioFileOutput);
        }
        
        public static Buff AudioLength(this Buff obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj.UnderlyingAudioFileOutput);
            obj.UnderlyingAudioFileOutput.AudioLength(value);
            return obj;
        }
        
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
        
        public static double AudioLength(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetDuration();
        }
        
        public static Sample AudioLength(this Sample obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            double originalAudioLength = obj.AudioLength();
            obj.SamplingRate = (int)(obj.SamplingRate * value / originalAudioLength);
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
        
        public static double AudioLength(this WavHeaderStruct obj)
            => obj.ToWish().AudioLength();
        
        public static WavHeaderStruct AudioLength(this WavHeaderStruct obj, double value)
        {
            return obj.ToWish().AudioLength(value).ToWavHeader();
        }
        
        public static double AudioLength(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            if (infoWish.FrameCount == 0) return 0;
            if (infoWish.Channels == 0) throw new Exception("info.Channels == 0");
            if (infoWish.SamplingRate == 0) throw new Exception("info.SamplingRate == 0");
            return (double)infoWish.FrameCount / infoWish.Channels / infoWish.SamplingRate;
        }
        
        public static AudioInfoWish AudioLength(this AudioInfoWish infoWish, double value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = (int)(value * infoWish.SamplingRate);
            return infoWish;
        }
        
        public static double AudioLength(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.ToWish().AudioLength();
        }
        
        public static AudioFileInfo AudioLength(this AudioFileInfo info, double value)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = (int)(value * info.SamplingRate);
            return info;
        }
    }
}
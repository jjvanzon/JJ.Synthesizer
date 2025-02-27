using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="_configextensionwishes"/>
    public static class AudioLengthExtensionWishes
    {
        // A Duration Attribute
        
        // Synth-Bound
        
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(this SynthWishes obj) => ConfigWishes.AudioLength(obj);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode GetAudioLength(this SynthWishes obj) => ConfigWishes.GetAudioLength(obj);

        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes AudioLength(this SynthWishes obj, double? newLength) => ConfigWishes.AudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes AudioLength(this SynthWishes obj, FlowNode newLength) => ConfigWishes.AudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes WithAudioLength(this SynthWishes obj, double? newLength) => ConfigWishes.WithAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes WithAudioLength(this SynthWishes obj, FlowNode newLength) => ConfigWishes.WithAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes SetAudioLength(this SynthWishes obj, double? newLength) => ConfigWishes.SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes SetAudioLength(this SynthWishes obj, FlowNode newLength) => ConfigWishes.SetAudioLength(obj, newLength);

        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(this FlowNode obj) => ConfigWishes.AudioLength(obj);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode GetAudioLength(this FlowNode obj) => ConfigWishes.GetAudioLength(obj);

        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(this FlowNode obj, double? newLength) => ConfigWishes.AudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(this FlowNode obj, FlowNode newLength) => ConfigWishes.AudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode WithAudioLength(this FlowNode obj, double? newLength) => ConfigWishes.WithAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode WithAudioLength(this FlowNode obj, FlowNode newLength) => ConfigWishes.WithAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode SetAudioLength(this FlowNode obj, double? newLength) => ConfigWishes.SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode SetAudioLength(this FlowNode obj, FlowNode newLength) => ConfigWishes.SetAudioLength(obj, newLength);

        /// <inheritdoc cref="_audiolength" />
        internal static FlowNode AudioLength(this ConfigResolver obj, SynthWishes synthWishes) => ConfigWishes.AudioLength(obj, synthWishes);
        /// <inheritdoc cref="_audiolength" />
        internal static FlowNode GetAudioLength(this ConfigResolver obj, SynthWishes synthWishes) => ConfigWishes.GetAudioLength(obj, synthWishes);

        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver AudioLength(this ConfigResolver obj, double? newLength, SynthWishes synthWishes) => ConfigWishes.AudioLength(obj, newLength, synthWishes);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver AudioLength(this ConfigResolver obj, FlowNode newLength) => ConfigWishes.AudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver WithAudioLength(this ConfigResolver obj, double? newLength, SynthWishes synthWishes) => ConfigWishes.WithAudioLength(obj, newLength, synthWishes);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver WithAudioLength(this ConfigResolver obj, FlowNode newLength) => ConfigWishes.WithAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver SetAudioLength(this ConfigResolver obj, double? newLength, SynthWishes synthWishes) => ConfigWishes.SetAudioLength(obj, newLength, synthWishes);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver SetAudioLength(this ConfigResolver obj, FlowNode newLength) => ConfigWishes.SetAudioLength(obj, newLength);

        // Global-Bound

        internal static double? AudioLength(this ConfigSection obj) => ConfigWishes.AudioLength(obj);
        internal static double? GetAudioLength(this ConfigSection obj) => ConfigWishes.GetAudioLength(obj);

        // Tape-Bound

        public static double AudioLength(this Tape obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this Tape obj) => ConfigWishes.GetAudioLength(obj);

        public static Tape AudioLength(this Tape obj, double value) => ConfigWishes.AudioLength(obj, value);
        public static Tape WithAudioLength(this Tape obj, double value) => ConfigWishes.WithAudioLength(obj, value);
        public static Tape SetAudioLength(this Tape obj, double value) => ConfigWishes.SetAudioLength(obj, value);

        public static double AudioLength(this TapeConfig obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this TapeConfig obj) => ConfigWishes.GetAudioLength(obj);

        public static TapeConfig AudioLength(this TapeConfig obj, double value) => ConfigWishes.AudioLength(obj, value);
        public static TapeConfig WithAudioLength(this TapeConfig obj, double value) => ConfigWishes.WithAudioLength(obj, value);
        public static TapeConfig SetAudioLength(this TapeConfig obj, double value) => ConfigWishes.SetAudioLength(obj, value);

        public static double AudioLength(this TapeAction obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this TapeAction obj) => ConfigWishes.GetAudioLength(obj);

        public static TapeAction AudioLength(this TapeAction obj, double value) => ConfigWishes.AudioLength(obj, value);
        public static TapeAction WithAudioLength(this TapeAction obj, double value) => ConfigWishes.WithAudioLength(obj, value);
        public static TapeAction SetAudioLength(this TapeAction obj, double value) => ConfigWishes.SetAudioLength(obj, value);

        public static double AudioLength(this TapeActions obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this TapeActions obj) => ConfigWishes.GetAudioLength(obj);

        public static TapeActions AudioLength(this TapeActions obj, double value) => ConfigWishes.AudioLength(obj, value);
        public static TapeActions WithAudioLength(this TapeActions obj, double value) => ConfigWishes.WithAudioLength(obj, value);
        public static TapeActions SetAudioLength(this TapeActions obj, double value) => ConfigWishes.SetAudioLength(obj, value);

        // Buff-Bound

        public static double AudioLength(this Buff obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this Buff obj) => ConfigWishes.GetAudioLength(obj);

        public static Buff AudioLength(this Buff obj, double value) => ConfigWishes.AudioLength(obj, value);
        public static Buff WithAudioLength(this Buff obj, double value) => ConfigWishes.WithAudioLength(obj, value);
        public static Buff SetAudioLength(this Buff obj, double value) => ConfigWishes.SetAudioLength(obj, value);

        public static double AudioLength(this AudioFileOutput obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this AudioFileOutput obj) => ConfigWishes.GetAudioLength(obj);

        public static AudioFileOutput AudioLength(this AudioFileOutput obj, double value) => ConfigWishes.AudioLength(obj, value);
        public static AudioFileOutput WithAudioLength(this AudioFileOutput obj, double value) => ConfigWishes.WithAudioLength(obj, value);
        public static AudioFileOutput SetAudioLength(this AudioFileOutput obj, double value) => ConfigWishes.SetAudioLength(obj, value);

        // Independent after Taping

        public static double AudioLength(this Sample obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this Sample obj) => ConfigWishes.GetAudioLength(obj);

        /// <inheritdoc cref="_sampleaudiolength" />
        public static Sample AudioLength(this Sample obj, double newLength) => ConfigWishes.AudioLength(obj, newLength);
        /// <inheritdoc cref="_sampleaudiolength" />
        public static Sample WithAudioLength(this Sample obj, double newLength) => ConfigWishes.WithAudioLength(obj, newLength);
        /// <inheritdoc cref="_sampleaudiolength" />
        public static Sample SetAudioLength(this Sample obj, double newLength) => ConfigWishes.SetAudioLength(obj, newLength);

        public static double AudioLength(this AudioInfoWish infoWish) => ConfigWishes.AudioLength(infoWish);
        public static double GetAudioLength(this AudioInfoWish infoWish) => ConfigWishes.GetAudioLength(infoWish);

        public static AudioInfoWish AudioLength(this AudioInfoWish infoWish, double value) => ConfigWishes.AudioLength(infoWish, value);
        public static AudioInfoWish WithAudioLength(this AudioInfoWish infoWish, double value) => ConfigWishes.WithAudioLength(infoWish, value);
        public static AudioInfoWish SetAudioLength(this AudioInfoWish infoWish, double value) => ConfigWishes.SetAudioLength(infoWish, value);

        public static double AudioLength(this AudioFileInfo info) => ConfigWishes.AudioLength(info);
        public static double GetAudioLength(this AudioFileInfo info) => ConfigWishes.GetAudioLength(info);

        public static AudioFileInfo AudioLength(this AudioFileInfo info, double value) => ConfigWishes.AudioLength(info, value);
        public static AudioFileInfo WithAudioLength(this AudioFileInfo info, double value) => ConfigWishes.WithAudioLength(info, value);
        public static AudioFileInfo SetAudioLength(this AudioFileInfo info, double value) => ConfigWishes.SetAudioLength(info, value);

        // Immutable

        public static double AudioLength(this WavHeaderStruct obj) => ConfigWishes.AudioLength(obj);
        public static double GetAudioLength(this WavHeaderStruct obj) => ConfigWishes.GetAudioLength(obj);

        public static WavHeaderStruct AudioLength(this WavHeaderStruct obj, double value) => ConfigWishes.AudioLength(obj, value);
        public static WavHeaderStruct WithAudioLength(this WavHeaderStruct obj, double value) => ConfigWishes.WithAudioLength(obj, value);
        public static WavHeaderStruct SetAudioLength(this WavHeaderStruct obj, double value) => ConfigWishes.SetAudioLength(obj, value);

        // Conversion Formula

        // From FrameCount

        public static double? AudioLength(this int? frameCount, int samplingRate) 
            => ConfigWishes.AudioLength(frameCount, samplingRate);

        public static double AudioLength(this int frameCount, int samplingRate) 
            => ConfigWishes.AudioLength(frameCount, samplingRate);

        public static double? GetAudioLength(this int? frameCount, int samplingRate) 
            => ConfigWishes.GetAudioLength(frameCount, samplingRate);

        public static double GetAudioLength(this int frameCount, int samplingRate) 
            => ConfigWishes.GetAudioLength(frameCount, samplingRate);

        public static double? ToAudioLength(this int? frameCount, int samplingRate) 
            => ConfigWishes.ToAudioLength(frameCount, samplingRate);

        public static double ToAudioLength(this int frameCount, int samplingRate) 
            => ConfigWishes.ToAudioLength(frameCount, samplingRate);

        public static double? FrameCountToAudioLength(this int? frameCount, int samplingRate)
            => ConfigWishes.FrameCountToAudioLength(frameCount, samplingRate);

        public static double FrameCountToAudioLength(this int frameCount, int samplingRate)
            => ConfigWishes.FrameCountToAudioLength(frameCount, samplingRate);

        public static double? AudioLengthFromFrameCount(this int? frameCount, int samplingRate)
            => ConfigWishes.AudioLengthFromFrameCount(frameCount, samplingRate);

        public static double AudioLengthFromFrameCount(this int frameCount, int samplingRate)
            => ConfigWishes.AudioLengthFromFrameCount(frameCount, samplingRate);

        // From ByteCount

        public static double? AudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.AudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double AudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.AudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double? GetAudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.GetAudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double GetAudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.GetAudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double? ToAudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.ToAudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double ToAudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.ToAudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double? ByteCountToAudioLength(this int? byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.ByteCountToAudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double ByteCountToAudioLength(this int byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.ByteCountToAudioLength(byteCount, frameSize, samplingRate, headerLength);

        public static double? AudioLengthFromByteCount(this int? byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);

        public static double AudioLengthFromByteCount(this int byteCount, int frameSize, int samplingRate, int headerLength)
            => ConfigWishes.AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
    }

    
    public partial class ConfigWishes
    {
        // A Duration Attribute
        
        // Synth-Bound
        
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(SynthWishes obj) => GetAudioLength(obj);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode GetAudioLength(SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength;
        }
        
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes AudioLength(SynthWishes obj, double? newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes AudioLength(SynthWishes obj, FlowNode newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes WithAudioLength(SynthWishes obj, double? newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes WithAudioLength(SynthWishes obj, FlowNode newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes SetAudioLength(SynthWishes obj, double? newLength)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(newLength);
            return obj;
        }
        /// <inheritdoc cref="_audiolength" />
        public static SynthWishes SetAudioLength(SynthWishes obj, FlowNode newLength)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(newLength);
            return obj;
        }
        
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(FlowNode obj) => GetAudioLength(obj);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode GetAudioLength(FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength;
        }
        
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(FlowNode obj, double? newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode AudioLength(FlowNode obj, FlowNode newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode WithAudioLength(FlowNode obj, double? newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode WithAudioLength(FlowNode obj, FlowNode newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode SetAudioLength(FlowNode obj, double? newLength)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(newLength);
            return obj;
        }
        /// <inheritdoc cref="_audiolength" />
        public static FlowNode SetAudioLength(FlowNode obj, FlowNode newLength)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(newLength);
            return obj;
        }
        
        /// <inheritdoc cref="_audiolength" />
        internal static FlowNode AudioLength(ConfigResolver obj, SynthWishes synthWishes) => GetAudioLength(obj, synthWishes);
        /// <inheritdoc cref="_audiolength" />
        internal static FlowNode GetAudioLength(ConfigResolver obj, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength(synthWishes);
        }
        
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver AudioLength(ConfigResolver obj, double? newLength, SynthWishes synthWishes) => SetAudioLength(obj, newLength, synthWishes);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver AudioLength(ConfigResolver obj, FlowNode newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver WithAudioLength(ConfigResolver obj, double? newLength, SynthWishes synthWishes) => SetAudioLength(obj, newLength, synthWishes);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver WithAudioLength(ConfigResolver obj, FlowNode newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver SetAudioLength(ConfigResolver obj, double? newLength, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(newLength, synthWishes);
            return obj;
        }
        /// <inheritdoc cref="_audiolength" />
        internal static ConfigResolver SetAudioLength(ConfigResolver obj, FlowNode newLength)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(newLength);
            return obj;
        }
        
        // Global-Bound
        
        internal static double? AudioLength(ConfigSection obj) => GetAudioLength(obj);
        internal static double? GetAudioLength(ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioLength;
        }

        // Tape-Bound
        
        public static double AudioLength(Tape obj) => GetAudioLength(obj);
        public static double GetAudioLength(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }
        
        public static Tape AudioLength(Tape obj, double value) => SetAudioLength(obj, value);
        public static Tape WithAudioLength(Tape obj, double value) => SetAudioLength(obj, value);
        public static Tape SetAudioLength(Tape obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }
        
        public static double AudioLength(TapeConfig obj) => GetAudioLength(obj);
        public static double GetAudioLength(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeConfig AudioLength(TapeConfig obj, double value) => SetAudioLength(obj, value);
        public static TapeConfig WithAudioLength(TapeConfig obj, double value) => SetAudioLength(obj, value);
        public static TapeConfig SetAudioLength(TapeConfig obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        public static double AudioLength(TapeAction obj) => GetAudioLength(obj);
        public static double GetAudioLength(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeAction AudioLength(TapeAction obj, double value) => SetAudioLength(obj, value);
        public static TapeAction WithAudioLength(TapeAction obj, double value) => SetAudioLength(obj, value);
        public static TapeAction SetAudioLength(TapeAction obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        public static double AudioLength(TapeActions obj) => GetAudioLength(obj);
        public static double GetAudioLength(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }
        
        public static TapeActions AudioLength(TapeActions obj, double value) => SetAudioLength(obj, value);
        public static TapeActions WithAudioLength(TapeActions obj, double value) => SetAudioLength(obj, value);
        public static TapeActions SetAudioLength(TapeActions obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static double AudioLength(Buff obj) => GetAudioLength(obj);
        public static double GetAudioLength(Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            // TODO: From bytes[] / filePath?
            return AudioLength(obj.UnderlyingAudioFileOutput);
        }
        
        public static Buff AudioLength(Buff obj, double value) => SetAudioLength(obj, value);
        public static Buff WithAudioLength(Buff obj, double value) => SetAudioLength(obj, value);
        public static Buff SetAudioLength(Buff obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.AudioLength(AssertAudioLength(value));
            return obj;
        }
        
        public static double AudioLength(AudioFileOutput obj) => GetAudioLength(obj);
        public static double GetAudioLength(AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }
        
        public static AudioFileOutput AudioLength(AudioFileOutput obj, double value) => SetAudioLength(obj, value);
        public static AudioFileOutput WithAudioLength(AudioFileOutput obj, double value) => SetAudioLength(obj, value);
        public static AudioFileOutput SetAudioLength(AudioFileOutput obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }
        
        // Independent after Taping
        
        public static double AudioLength(Sample obj) => GetAudioLength(obj);
        public static double GetAudioLength(Sample obj)
        {
            return obj.GetDuration();
        }
        
        /// <inheritdoc cref="_sampleaudiolength" />
        public static Sample AudioLength(Sample obj, double newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_sampleaudiolength" />
        public static Sample WithAudioLength(Sample obj, double newLength) => SetAudioLength(obj, newLength);
        /// <inheritdoc cref="_sampleaudiolength" />
        public static Sample SetAudioLength(Sample obj, double newLength)
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
                
        public static double AudioLength(AudioInfoWish infoWish) => GetAudioLength(infoWish);
        public static double GetAudioLength(AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return AudioLengthFromFrameCount(infoWish.FrameCount, infoWish.SamplingRate);
        }
        
        public static AudioInfoWish AudioLength(AudioInfoWish infoWish, double value) => SetAudioLength(infoWish, value);
        public static AudioInfoWish WithAudioLength(AudioInfoWish infoWish, double value) => SetAudioLength(infoWish, value);
        public static AudioInfoWish SetAudioLength(AudioInfoWish infoWish, double value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = FrameCountFromAudioLength(value, infoWish.SamplingRate);
            return infoWish;
        }
        
        public static double AudioLength(AudioFileInfo info) => GetAudioLength(info);
        public static double GetAudioLength(AudioFileInfo info)
        {
            return info.ToInfo().AudioLength();
        }
        
        public static AudioFileInfo AudioLength(AudioFileInfo info, double value) => SetAudioLength(info, value);
        public static AudioFileInfo WithAudioLength(AudioFileInfo info, double value) => SetAudioLength(info, value);
        public static AudioFileInfo SetAudioLength(AudioFileInfo info, double value)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = FrameCountFromAudioLength(value, info.SamplingRate);
            return info;
        }

        // Immutable
        
        public static double AudioLength(WavHeaderStruct obj) => GetAudioLength(obj);
        public static double GetAudioLength(WavHeaderStruct obj)
        {
            return obj.ToInfo().AudioLength();
        }
        
        public static WavHeaderStruct AudioLength(WavHeaderStruct obj, double value) => SetAudioLength(obj, value);
        public static WavHeaderStruct WithAudioLength(WavHeaderStruct obj, double value) => SetAudioLength(obj, value);
        public static WavHeaderStruct SetAudioLength(WavHeaderStruct obj, double value)
        {
            return obj.ToInfo().AudioLength(value).ToWavHeader();
        }

        // Conversion Formula
        
        // From FrameCount

        public static double? AudioLength(int? frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);
        
        public static double AudioLength(int frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);

        public static double? GetAudioLength(int? frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);
        
        public static double GetAudioLength(int frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);

        public static double? ToAudioLength(int? frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);
        
        public static double ToAudioLength(int frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);

        public static double? FrameCountToAudioLength(int? frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);
        
        public static double FrameCountToAudioLength(int frameCount, int samplingRate) 
            => AudioLengthFromFrameCount(frameCount, samplingRate);
        
        public static double? AudioLengthFromFrameCount(int? frameCount, int samplingRate) 
            => (double?)AssertFrameCount(frameCount) / AssertSamplingRate(samplingRate);

        public static double AudioLengthFromFrameCount(int frameCount, int samplingRate) 
            => (double)AssertFrameCount(frameCount) / AssertSamplingRate(samplingRate);

        // From ByteCount
 
        public static double? AudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
        
        public static double AudioLength(int byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
 
        public static double? GetAudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
        
        public static double GetAudioLength(int byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
 
        public static double? ToAudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
        
        public static double ToAudioLength(int byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
 
        public static double? ByteCountToAudioLength(int? byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
        
        public static double ByteCountToAudioLength(int byteCount, int frameSize, int samplingRate, int headerLength)
            => AudioLengthFromByteCount(byteCount, frameSize, samplingRate, headerLength);
        
        public static double? AudioLengthFromByteCount(int? byteCount, int frameSize, int samplingRate, int headerLength)
        {
            if (!Has(byteCount)) return byteCount;
            return AudioLengthFromByteCount(byteCount.Value, frameSize, samplingRate, headerLength);
        }

        public static double AudioLengthFromByteCount(int byteCount, int frameSize, int samplingRate, int headerLength)
        {
            AssertByteCount(byteCount);
            AssertHeaderLength(headerLength);
            AssertFrameSize(frameSize);
            AssertSamplingRate(samplingRate);
            AssertFrameSize(frameSize);

            double frameCount = (double)(byteCount - headerLength) / frameSize;
            return frameCount / samplingRate;
        }
    }
}
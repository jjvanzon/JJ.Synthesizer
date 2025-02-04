using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class SamplingRateExtensionWishes
    {
        // A Primary Audio Attribute

        // Synth-Bound

        public static int SamplingRate(this SynthWishes obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }
        
        public static SynthWishes SamplingRate(this SynthWishes obj, int? value) => SetSamplingRate(obj, value);
        public static SynthWishes WithSamplingRate(this SynthWishes obj, int? value) => SetSamplingRate(obj, value);
        public static SynthWishes SetSamplingRate(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }
        
        public static int SamplingRate(this FlowNode obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }
        
        public static FlowNode SamplingRate(this FlowNode obj, int? value) => SetSamplingRate(obj, value);
        public static FlowNode WithSamplingRate(this FlowNode obj, int? value) => SetSamplingRate(obj, value);
        public static FlowNode SetSamplingRate(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }
        
        internal static int SamplingRate(this ConfigResolver obj) => GetSamplingRate(obj);
        internal static int GetSamplingRate(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }
        
        [UsedImplicitly] internal static ConfigResolver SamplingRate(this ConfigResolver obj, int? value) => SetSamplingRate(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithSamplingRate(this ConfigResolver obj, int? value) => SetSamplingRate(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetSamplingRate(this ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        // Global-Bound

        internal static int? SamplingRate(this ConfigSection obj) => GetSamplingRate(obj);
        internal static int? GetSamplingRate(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }

        // Tape-Bound

        public static int SamplingRate(this Tape obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.SamplingRate;
        }
        
        public static Tape SamplingRate(this Tape obj, int value) => SetSamplingRate(obj, value);
        public static Tape WithSamplingRate(this Tape obj, int value) => SetSamplingRate(obj, value);
        public static Tape SetSamplingRate(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(this TapeConfig obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static TapeConfig SamplingRate(this TapeConfig obj, int value) => SetSamplingRate(obj, value);
        public static TapeConfig SetSamplingRate(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(this TapeActions obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }
        
        public static TapeActions SamplingRate(this TapeActions obj, int value) => SetSamplingRate(obj, value);
        public static TapeActions WithSamplingRate(this TapeActions obj, int value) => SetSamplingRate(obj, value);
        public static TapeActions SetSamplingRate(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(this TapeAction obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }
        
        public static TapeAction SamplingRate(this TapeAction obj, int value) => SetSamplingRate(obj, value);
        public static TapeAction WithSamplingRate(this TapeAction obj, int value) => SetSamplingRate(obj, value);
        public static TapeAction SetSamplingRate(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }

        // Buff-Bound

        public static int SamplingRate(this Buff obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.UnderlyingAudioFileOutput.SamplingRate();
        }
        
        public static Buff SamplingRate(this Buff obj, int value) => SetSamplingRate(obj, value);
        public static Buff WithSamplingRate(this Buff obj, int value) => SetSamplingRate(obj, value);
        public static Buff SetSamplingRate(this Buff obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.SamplingRate(value);
            return obj;
        }
        
        public static int SamplingRate(this AudioFileOutput obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static AudioFileOutput SamplingRate(this AudioFileOutput obj, int value) => SetSamplingRate(obj, value);
        public static AudioFileOutput WithSamplingRate(this AudioFileOutput obj, int value) => SetSamplingRate(obj, value);
        public static AudioFileOutput SetSamplingRate(this AudioFileOutput obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = AssertSamplingRate(value);
            return obj;
        }

        // Independent after Taping  
        
        public static int SamplingRate(this Sample obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static Sample SamplingRate(this Sample obj, int value) => SetSamplingRate(obj, value);
        public static Sample WithSamplingRate(this Sample obj, int value) => SetSamplingRate(obj, value);
        public static Sample SetSamplingRate(this Sample obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = AssertSamplingRate(value);
            return obj;
        }
        
        public static int SamplingRate(this AudioInfoWish infoWish) => GetSamplingRate(infoWish);
        public static int GetSamplingRate(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.SamplingRate;
        }
        
        public static AudioInfoWish SamplingRate(this AudioInfoWish infoWish, int value) => SetSamplingRate(infoWish, value);
        public static AudioInfoWish WithSamplingRate(this AudioInfoWish infoWish, int value) => SetSamplingRate(infoWish, value);
        public static AudioInfoWish SetSamplingRate(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.SamplingRate = AssertSamplingRate(value);
            return infoWish;
        }
        
        public static int SamplingRate(this AudioFileInfo info) => GetSamplingRate(info);
        public static int GetSamplingRate(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SamplingRate;
        }
        
        public static AudioFileInfo SamplingRate(this AudioFileInfo info, int value) => SetSamplingRate(info, value);
        public static AudioFileInfo WithSamplingRate(this AudioFileInfo info, int value) => SetSamplingRate(info, value);
        public static AudioFileInfo SetSamplingRate(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SamplingRate = AssertSamplingRate(value);
            return info;
        }
        
        // Immutable
        
        public static int SamplingRate(this WavHeaderStruct obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(this WavHeaderStruct obj)
        {
            return obj.SamplingRate;
        }
        
        public static WavHeaderStruct SamplingRate(this WavHeaderStruct obj, int value) => SetSamplingRate(obj, value);
        public static WavHeaderStruct WithSamplingRate(this WavHeaderStruct obj, int value) => SetSamplingRate(obj, value);
        public static WavHeaderStruct SetSamplingRate(this WavHeaderStruct obj, int value)
        {
            return obj.ToWish().SamplingRate(value).ToWavHeader();
        }
    }
}
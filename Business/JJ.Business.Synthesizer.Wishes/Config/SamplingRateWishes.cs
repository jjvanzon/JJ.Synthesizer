using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // SamplingRate: A Primary Audio Attribute

    /// <inheritdoc cref="_configextensionwishes"/>
    public static class SamplingRateExtensionWishes
    {
        // Synth-Bound

        /// <inheritdoc cref="_getsamplingrate" />
        public static int SamplingRate(this SynthWishes obj) => ConfigWishes.SamplingRate(obj);
        /// <inheritdoc cref="_getsamplingrate" />
        public static int GetSamplingRate(this SynthWishes obj) => ConfigWishes.GetSamplingRate(obj);

        /// <inheritdoc cref="_getsamplingrate" />
        public static SynthWishes SamplingRate(this SynthWishes obj, int? value) => ConfigWishes.SamplingRate(obj, value);
        /// <inheritdoc cref="_getsamplingrate" />
        public static SynthWishes WithSamplingRate(this SynthWishes obj, int? value) => ConfigWishes.WithSamplingRate(obj, value);
        /// <inheritdoc cref="_getsamplingrate" />
        public static SynthWishes SetSamplingRate(this SynthWishes obj, int? value) => ConfigWishes.SetSamplingRate(obj, value);

        /// <inheritdoc cref="_getsamplingrate" />
        public static int SamplingRate(this FlowNode obj) => ConfigWishes.SamplingRate(obj);
        /// <inheritdoc cref="_getsamplingrate" />
        public static int GetSamplingRate(this FlowNode obj) => ConfigWishes.GetSamplingRate(obj);

        /// <inheritdoc cref="_getsamplingrate" />
        public static FlowNode SamplingRate(this FlowNode obj, int? value) => ConfigWishes.SamplingRate(obj, value);
        /// <inheritdoc cref="_getsamplingrate" />
        public static FlowNode WithSamplingRate(this FlowNode obj, int? value) => ConfigWishes.WithSamplingRate(obj, value);
        /// <inheritdoc cref="_getsamplingrate" />
        public static FlowNode SetSamplingRate(this FlowNode obj, int? value) => ConfigWishes.SetSamplingRate(obj, value);

        /// <inheritdoc cref="_getsamplingrate" />
        [UsedImplicitly] internal static int SamplingRate(this ConfigResolver obj) => ConfigWishes.SamplingRate(obj);
        /// <inheritdoc cref="_getsamplingrate" />
        [UsedImplicitly] internal static int GetSamplingRate(this ConfigResolver obj) => ConfigWishes.GetSamplingRate(obj);

        /// <inheritdoc cref="_getsamplingrate" />
        [UsedImplicitly] internal static ConfigResolver SamplingRate(this ConfigResolver obj, int? value) => ConfigWishes.SamplingRate(obj, value);
        /// <inheritdoc cref="_getsamplingrate" />
        [UsedImplicitly] internal static ConfigResolver WithSamplingRate(this ConfigResolver obj, int? value) => ConfigWishes.WithSamplingRate(obj, value);
        /// <inheritdoc cref="_getsamplingrate" />
        [UsedImplicitly] internal static ConfigResolver SetSamplingRate(this ConfigResolver obj, int? value) => ConfigWishes.SetSamplingRate(obj, value);

        // Global-Bound

        internal static int? SamplingRate(this ConfigSection obj) => ConfigWishes.SamplingRate(obj);
        internal static int? GetSamplingRate(this ConfigSection obj) => ConfigWishes.GetSamplingRate(obj);

        // Tape-Bound

        public static int SamplingRate(this Tape obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this Tape obj) => ConfigWishes.GetSamplingRate(obj);

        public static Tape SamplingRate(this Tape obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static Tape WithSamplingRate(this Tape obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static Tape SetSamplingRate(this Tape obj, int value) => ConfigWishes.SetSamplingRate(obj, value);

        public static int SamplingRate(this TapeConfig obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this TapeConfig obj) => ConfigWishes.GetSamplingRate(obj);

        public static TapeConfig SamplingRate(this TapeConfig obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static TapeConfig WithSamplingRate(this TapeConfig obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static TapeConfig SetSamplingRate(this TapeConfig obj, int value) => ConfigWishes.SetSamplingRate(obj, value);

        public static int SamplingRate(this TapeActions obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this TapeActions obj) => ConfigWishes.GetSamplingRate(obj);

        public static TapeActions SamplingRate(this TapeActions obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static TapeActions WithSamplingRate(this TapeActions obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static TapeActions SetSamplingRate(this TapeActions obj, int value) => ConfigWishes.SetSamplingRate(obj, value);

        public static int SamplingRate(this TapeAction obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this TapeAction obj) => ConfigWishes.GetSamplingRate(obj);

        public static TapeAction SamplingRate(this TapeAction obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static TapeAction WithSamplingRate(this TapeAction obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static TapeAction SetSamplingRate(this TapeAction obj, int value) => ConfigWishes.SetSamplingRate(obj, value);

        // Buff-Bound

        public static int SamplingRate(this Buff obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this Buff obj) => ConfigWishes.GetSamplingRate(obj);

        public static Buff SamplingRate(this Buff obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static Buff WithSamplingRate(this Buff obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static Buff SetSamplingRate(this Buff obj, int value) => ConfigWishes.SetSamplingRate(obj, value);

        public static int SamplingRate(this AudioFileOutput obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this AudioFileOutput obj) => ConfigWishes.GetSamplingRate(obj);

        public static AudioFileOutput SamplingRate(this AudioFileOutput obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static AudioFileOutput WithSamplingRate(this AudioFileOutput obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static AudioFileOutput SetSamplingRate(this AudioFileOutput obj, int value) => ConfigWishes.SetSamplingRate(obj, value);

        // Independent after Taping  

        public static int SamplingRate(this Sample obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this Sample obj) => ConfigWishes.GetSamplingRate(obj);
        
        public static Sample SamplingRate(this Sample obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static Sample WithSamplingRate(this Sample obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static Sample SetSamplingRate(this Sample obj, int value) => ConfigWishes.SetSamplingRate(obj, value);

        public static int SamplingRate(this AudioInfoWish infoWish) => ConfigWishes.SamplingRate(infoWish);
        public static int GetSamplingRate(this AudioInfoWish infoWish) => ConfigWishes.GetSamplingRate(infoWish);

        public static AudioInfoWish SamplingRate(this AudioInfoWish infoWish, int value) => ConfigWishes.SamplingRate(infoWish, value);
        public static AudioInfoWish WithSamplingRate(this AudioInfoWish infoWish, int value) => ConfigWishes.WithSamplingRate(infoWish, value); 
        public static AudioInfoWish SetSamplingRate(this AudioInfoWish infoWish, int value) => ConfigWishes.SetSamplingRate(infoWish, value);
        
        public static int SamplingRate(this AudioFileInfo info) => ConfigWishes.SamplingRate(info);
        public static int GetSamplingRate(this AudioFileInfo info) => ConfigWishes.GetSamplingRate(info);

        public static AudioFileInfo SamplingRate(this AudioFileInfo info, int value) => ConfigWishes.SamplingRate(info, value);
        public static AudioFileInfo WithSamplingRate(this AudioFileInfo info, int value) => ConfigWishes.WithSamplingRate(info, value);
        public static AudioFileInfo SetSamplingRate(this AudioFileInfo info, int value) => ConfigWishes.SetSamplingRate(info, value);

        // Immutable

        public static int SamplingRate(this WavHeaderStruct obj) => ConfigWishes.SamplingRate(obj);
        public static int GetSamplingRate(this WavHeaderStruct obj) => ConfigWishes.GetSamplingRate(obj);

        public static WavHeaderStruct SamplingRate(this WavHeaderStruct obj, int value) => ConfigWishes.SamplingRate(obj, value);
        public static WavHeaderStruct WithSamplingRate(this WavHeaderStruct obj, int value) => ConfigWishes.WithSamplingRate(obj, value);
        public static WavHeaderStruct SetSamplingRate(this WavHeaderStruct obj, int value) => ConfigWishes.SetSamplingRate(obj, value);
    }
    
    public partial class ConfigWishes
    {
        // Synth-Bound
        
        public static int SamplingRate(SynthWishes obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }
        
        public static SynthWishes SamplingRate(SynthWishes obj, int? value) => SetSamplingRate(obj, value);
        public static SynthWishes WithSamplingRate(SynthWishes obj, int? value) => SetSamplingRate(obj, value);
        public static SynthWishes SetSamplingRate(SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }
        
        public static int SamplingRate(FlowNode obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }
        
        public static FlowNode SamplingRate(FlowNode obj, int? value) => SetSamplingRate(obj, value);
        public static FlowNode WithSamplingRate(FlowNode obj, int? value) => SetSamplingRate(obj, value);
        public static FlowNode SetSamplingRate(FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }
        
        internal static int SamplingRate(ConfigResolver obj) => GetSamplingRate(obj);
        internal static int GetSamplingRate(ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }
        
        [UsedImplicitly] internal static ConfigResolver SamplingRate(ConfigResolver obj, int? value) => SetSamplingRate(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithSamplingRate(ConfigResolver obj, int? value) => SetSamplingRate(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetSamplingRate(ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        // Global-Bound

        internal static int? SamplingRate(ConfigSection obj) => GetSamplingRate(obj);
        internal static int? GetSamplingRate(ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }

        // Tape-Bound

        public static int SamplingRate(Tape obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.SamplingRate;
        }
        
        public static Tape SamplingRate(Tape obj, int value) => SetSamplingRate(obj, value);
        public static Tape WithSamplingRate(Tape obj, int value) => SetSamplingRate(obj, value);
        public static Tape SetSamplingRate(Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(TapeConfig obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static TapeConfig SamplingRate(TapeConfig obj, int value) => SetSamplingRate(obj, value);
        public static TapeConfig WithSamplingRate(TapeConfig obj, int value) => SetSamplingRate(obj, value);
        public static TapeConfig SetSamplingRate(TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(TapeActions obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }
        
        public static TapeActions SamplingRate(TapeActions obj, int value) => SetSamplingRate(obj, value);
        public static TapeActions WithSamplingRate(TapeActions obj, int value) => SetSamplingRate(obj, value);
        public static TapeActions SetSamplingRate(TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(TapeAction obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }
        
        public static TapeAction SamplingRate(TapeAction obj, int value) => SetSamplingRate(obj, value);
        public static TapeAction WithSamplingRate(TapeAction obj, int value) => SetSamplingRate(obj, value);
        public static TapeAction SetSamplingRate(TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }

        // Buff-Bound

        public static int SamplingRate(Buff obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.UnderlyingAudioFileOutput.SamplingRate();
        }
        
        public static Buff SamplingRate(Buff obj, int value) => SetSamplingRate(obj, value);
        public static Buff WithSamplingRate(Buff obj, int value) => SetSamplingRate(obj, value);
        public static Buff SetSamplingRate(Buff obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.SamplingRate(value);
            return obj;
        }
        
        public static int SamplingRate(AudioFileOutput obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static AudioFileOutput SamplingRate(AudioFileOutput obj, int value) => SetSamplingRate(obj, value);
        public static AudioFileOutput WithSamplingRate(AudioFileOutput obj, int value) => SetSamplingRate(obj, value);
        public static AudioFileOutput SetSamplingRate(AudioFileOutput obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = AssertSamplingRate(value);
            return obj;
        }

        // Independent after Taping  
        
        public static int SamplingRate(Sample obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static Sample SamplingRate(Sample obj, int value) => SetSamplingRate(obj, value);
        public static Sample WithSamplingRate(Sample obj, int value) => SetSamplingRate(obj, value);
        public static Sample SetSamplingRate(Sample obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = AssertSamplingRate(value);
            return obj;
        }
        
        public static int SamplingRate(AudioInfoWish infoWish) => GetSamplingRate(infoWish);
        public static int GetSamplingRate(AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.SamplingRate;
        }
        
        public static AudioInfoWish SamplingRate(AudioInfoWish infoWish, int value) => SetSamplingRate(infoWish, value);
        public static AudioInfoWish WithSamplingRate(AudioInfoWish infoWish, int value) => SetSamplingRate(infoWish, value);
        public static AudioInfoWish SetSamplingRate(AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.SamplingRate = AssertSamplingRate(value);
            return infoWish;
        }
        
        public static int SamplingRate(AudioFileInfo info) => GetSamplingRate(info);
        public static int GetSamplingRate(AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SamplingRate;
        }
        
        public static AudioFileInfo SamplingRate(AudioFileInfo info, int value) => SetSamplingRate(info, value);
        public static AudioFileInfo WithSamplingRate(AudioFileInfo info, int value) => SetSamplingRate(info, value);
        public static AudioFileInfo SetSamplingRate(AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SamplingRate = AssertSamplingRate(value);
            return info;
        }
        
        // Immutable
        
        public static int SamplingRate(WavHeaderStruct obj) => GetSamplingRate(obj);
        public static int GetSamplingRate(WavHeaderStruct obj)
        {
            return obj.SamplingRate;
        }
        
        public static WavHeaderStruct SamplingRate(WavHeaderStruct obj, int value) => SetSamplingRate(obj, value);
        public static WavHeaderStruct WithSamplingRate(WavHeaderStruct obj, int value) => SetSamplingRate(obj, value);
        public static WavHeaderStruct SetSamplingRate(WavHeaderStruct obj, int value)
        {
            return obj.ToInfo().SamplingRate(value).ToWavHeader();
        }
    }
}
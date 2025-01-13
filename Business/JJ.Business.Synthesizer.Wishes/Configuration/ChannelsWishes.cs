using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ChannelsExtensionWishes
    {
        // A Primary Audio Attribute
        
        public static int Channels(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static SynthWishes Channels(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        public static int Channels(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static FlowNode Channels(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        public static int Channels(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static ConfigWishes Channels(this ConfigWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        internal static int? Channels(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
        
        public static int Channels(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Channels;
        }
        
        public static Tape Channels(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Channels = value;
            return obj;
        }
        
        public static int Channels(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
        
        public static TapeConfig Channels(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channels = value;
            return obj;
        }
        
        public static int Channels(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channels;
        }
        
        public static TapeActions Channels(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channels = value;
            return obj;
        }
        
        public static int Channels(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channels;
        }
        
        public static TapeAction Channels(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channels = value;
            return obj;
        }
        
        public static int Channels(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return ChannelsEmpty;
            return obj.UnderlyingAudioFileOutput.Channels();
        }
        
        public static Buff Channels(this Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.Channels(value, context);
            return obj;
        }
        
        public static int Channels(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannelCount();
        }
        
        public static Sample Channels(this Sample obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSpeakerSetupEnum(value.ChannelsToEnum(), context);
            return obj;
        }
        
        public static int Channels(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannelCount();
        }
        
        public static AudioFileOutput Channels(this AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(value, context);
            // Do not adjust channels, to accommodate Left-Only and Right-Only scenarios with 1 channel, but Stereo speaker setup.
            //CreateOrRemoveChannels(obj, value, context); 
            return obj;
        }
        
        public static int Channels(this WavHeaderStruct obj)
            => obj.ChannelCount;
        
        public static WavHeaderStruct Channels(this WavHeaderStruct obj, int value)
            => obj.ToWish().Channels(value).ToWavHeader();
        
        public static int Channels(this AudioInfoWish obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
        
        public static AudioInfoWish Channels(this AudioInfoWish obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channels = value;
            return obj;
        }
        
        public static int Channels(this AudioFileInfo obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.ChannelCount;
        }
        
        public static AudioFileInfo Channels(this AudioFileInfo obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.ChannelCount = value;
            return obj;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int Channels(this SpeakerSetupEnum obj) 
            => obj.EnumToChannels();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static SpeakerSetupEnum Channels(this SpeakerSetupEnum oldEnumValue, int newChannels) 
            => newChannels.ChannelsToEnum();
        
        [Obsolete(ObsoleteMessage)] 
        public static int Channels(this SpeakerSetup obj) 
            => obj.EntityToChannels();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static SpeakerSetup Channels(this SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) 
            => newChannels.ChannelsToEntity(context);
        
        // Channels Conversion-Style
        
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum ChannelsToEnum(this int channels)
        {
            switch (channels)
            {
                case ChannelsEmpty: return SpeakerSetupEnum.Undefined;
                case MonoChannels: return SpeakerSetupEnum.Mono;
                case StereoChannels: return SpeakerSetupEnum.Stereo;
                default: throw new Exception($"{new { channels }} not supported.");
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int EnumToChannels(this SpeakerSetupEnum enumValue)
        {
            switch (enumValue)
            {
                case SpeakerSetupEnum.Mono: return MonoChannels;
                case SpeakerSetupEnum.Stereo: return StereoChannels;
                case SpeakerSetupEnum.Undefined: return ChannelsEmpty;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetup ChannelsToEntity(this int channels, IContext context)
            => channels.ChannelsToEnum().ToEntity(context);
        
        [Obsolete(ObsoleteMessage)] 
        public static int EntityToChannels(this SpeakerSetup entity) 
            => entity.ToEnum().EnumToChannels();
        
        // Channels Shorthand
        
        public   static bool IsMono  (this SynthWishes     obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this FlowNode        obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this ConfigWishes    obj) => obj.Channels() == MonoChannels;
        internal static bool IsMono  (this ConfigSection   obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this Tape            obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this TapeConfig      obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this TapeActions     obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this TapeAction      obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this Buff            obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this Sample          obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this AudioFileOutput obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this WavHeaderStruct obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this AudioInfoWish   obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this AudioFileInfo   obj) => obj.Channels() == MonoChannels;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetupEnum obj) => obj == SpeakerSetupEnum.Mono;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetup     obj) => obj.ToEnum().IsMono();
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this ChannelEnum      obj) => obj == ChannelEnum.Single;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this Channel          obj) => obj.ToEnum().IsMono();
        
        public   static bool IsStereo(this SynthWishes     obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this FlowNode        obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this ConfigWishes    obj) => obj.Channels() == StereoChannels;
        internal static bool IsStereo(this ConfigSection   obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this Tape            obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this TapeConfig      obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this TapeActions     obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this TapeAction      obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this Buff            obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this Sample          obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this AudioFileOutput obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this WavHeaderStruct obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this AudioInfoWish   obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this AudioFileInfo   obj) => obj.Channels() == StereoChannels;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetupEnum obj) => obj == SpeakerSetupEnum.Stereo;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetup     obj) => obj.ToEnum().IsStereo();
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this ChannelEnum      obj) => obj == ChannelEnum.Left || obj == ChannelEnum.Right;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this Channel          obj) => obj.ToEnum().IsStereo();
        
        public   static SynthWishes      Mono  (this SynthWishes     obj) => obj.Channels(MonoChannels);
        public   static FlowNode         Mono  (this FlowNode        obj) => obj.Channels(MonoChannels);
        public   static ConfigWishes     Mono  (this ConfigWishes    obj) => obj.Channels(MonoChannels);
        public   static Tape             Mono  (this Tape            obj) => obj.Channels(MonoChannels);
        public   static TapeConfig       Mono  (this TapeConfig      obj) => obj.Channels(MonoChannels);
        public   static TapeActions      Mono  (this TapeActions     obj) => obj.Channels(MonoChannels);
        public   static TapeAction       Mono  (this TapeAction      obj) => obj.Channels(MonoChannels);
        public   static Buff             Mono  (this Buff            obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static Sample           Mono  (this Sample          obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static AudioFileOutput  Mono  (this AudioFileOutput obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static WavHeaderStruct  Mono  (this WavHeaderStruct obj) => obj.Channels(MonoChannels);
        public   static AudioInfoWish    Mono  (this AudioInfoWish   obj) => obj.Channels(MonoChannels);
        public   static AudioFileInfo    Mono  (this AudioFileInfo   obj) => obj.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum Mono(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup Mono(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(MonoChannels, context);
        
        public   static SynthWishes      Stereo(this SynthWishes     obj) => obj.Channels(StereoChannels);
        public   static FlowNode         Stereo(this FlowNode        obj) => obj.Channels(StereoChannels);
        public   static ConfigWishes     Stereo(this ConfigWishes    obj) => obj.Channels(StereoChannels);
        public   static Tape             Stereo(this Tape            obj) => obj.Channels(StereoChannels);
        public   static TapeConfig       Stereo(this TapeConfig      obj) => obj.Channels(StereoChannels);
        public   static TapeActions      Stereo(this TapeActions     obj) => obj.Channels(StereoChannels);
        public   static TapeAction       Stereo(this TapeAction      obj) => obj.Channels(StereoChannels);
        public   static Buff             Stereo(this Buff            obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static Sample           Stereo(this Sample          obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static AudioFileOutput  Stereo(this AudioFileOutput obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static WavHeaderStruct  Stereo(this WavHeaderStruct obj) => obj.Channels(StereoChannels);
        public   static AudioInfoWish    Stereo(this AudioInfoWish   obj) => obj.Channels(StereoChannels);
        public   static AudioFileInfo    Stereo(this AudioFileInfo   obj) => obj.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum Stereo(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetup Stereo(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(StereoChannels, context);
    }
}
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
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Primary Audio Attribute
        
        public static int Channels(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static SynthWishes Channels(this SynthWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        public static int Channels(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static FlowNode Channels(this FlowNode obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        public static int Channels(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static ConfigWishes Channels(this ConfigWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        internal static int Channels(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels ?? DefaultChannels;
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
            return Channels(obj.UnderlyingAudioFileOutput);
        }
        
        public static Buff Channels(this Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            Channels(obj.UnderlyingAudioFileOutput, value, context);
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
            obj.SpeakerSetup = SynthWishes.GetSubstituteSpeakerSetup(value, context);
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
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public static int Channels(this SpeakerSetupEnum obj) 
            => EnumToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static SpeakerSetupEnum Channels(this SpeakerSetupEnum obj, int value) 
            => ChannelsToEnum(value);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static int Channels(this SpeakerSetup obj) 
            => EntityToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static SpeakerSetup Channels(this SpeakerSetup obj, int value, IContext context) 
            => ChannelsToEntity(value, context);
        
        // Channels Conversion-Style
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static SpeakerSetupEnum ChannelsToEnum(this int channels)
        {
            switch (channels)
            {
                case NoChannels: return SpeakerSetupEnum.Undefined;
                case MonoChannels: return SpeakerSetupEnum.Mono;
                case StereoChannels: return SpeakerSetupEnum.Stereo;
                default: throw new Exception($"{new { channels }} not supported.");
            }
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public static int EnumToChannels(this SpeakerSetupEnum enumValue)
        {
            switch (enumValue)
            {
                case SpeakerSetupEnum.Mono: return MonoChannels;
                case SpeakerSetupEnum.Stereo: return StereoChannels;
                case SpeakerSetupEnum.Undefined: return NoChannels;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static SpeakerSetup ChannelsToEntity(this int channels, IContext context)
            => ChannelsToEnum(channels).ToEntity(context);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static int EntityToChannels(this SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return EnumToChannels(entity.ToEnum());
        }
        
        // Channels Shorthand
        
        public   static bool IsMono  (this SynthWishes      obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this FlowNode         obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this ConfigWishes     obj) => Channels(obj) == MonoChannels;
        internal static bool IsMono  (this ConfigSection    obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this Tape             obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this TapeConfig       obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this TapeActions      obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this TapeAction       obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this Buff             obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this Sample           obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this AudioFileOutput  obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this WavHeaderStruct  obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this AudioInfoWish    obj) => Channels(obj) == MonoChannels;
        public   static bool IsMono  (this AudioFileInfo    obj) => Channels(obj) == MonoChannels;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono(this SpeakerSetupEnum obj) => Channels(obj) == MonoChannels;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono(this SpeakerSetup     obj) => Channels(obj) == MonoChannels;
        
        public   static bool IsStereo(this SynthWishes      obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this FlowNode         obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this ConfigWishes     obj) => Channels(obj) == StereoChannels;
        internal static bool IsStereo(this ConfigSection    obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this Tape             obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this TapeConfig       obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this TapeActions      obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this TapeAction       obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this Buff             obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this Sample           obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this AudioFileOutput  obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this WavHeaderStruct  obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this AudioInfoWish    obj) => Channels(obj) == StereoChannels;
        public   static bool IsStereo(this AudioFileInfo    obj) => Channels(obj) == StereoChannels;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this SpeakerSetupEnum obj) => Channels(obj) == StereoChannels;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this SpeakerSetup     obj) => Channels(obj) == StereoChannels;
        
        public   static SynthWishes      Mono  (this SynthWishes      obj) => Channels(obj, MonoChannels);
        public   static FlowNode         Mono  (this FlowNode         obj) => Channels(obj, MonoChannels);
        public   static ConfigWishes     Mono  (this ConfigWishes     obj) => Channels(obj, MonoChannels);
        public   static Tape             Mono  (this Tape             obj) => Channels(obj, MonoChannels);
        public   static TapeConfig       Mono  (this TapeConfig       obj) => Channels(obj, MonoChannels);
        public   static TapeActions      Mono  (this TapeActions      obj) => Channels(obj, MonoChannels);
        public   static TapeAction       Mono  (this TapeAction       obj) => Channels(obj, MonoChannels);
        public   static Buff             Mono  (this Buff             obj, IContext context) => Channels(obj, MonoChannels, context);
        public   static Sample           Mono  (this Sample           obj, IContext context) => Channels(obj, MonoChannels, context);
        public   static AudioFileOutput  Mono  (this AudioFileOutput  obj, IContext context) => Channels(obj, MonoChannels, context);
        public   static WavHeaderStruct  Mono  (this WavHeaderStruct  obj) => Channels(obj, MonoChannels);
        public   static AudioInfoWish    Mono  (this AudioInfoWish    obj) => Channels(obj, MonoChannels);
        public   static AudioFileInfo    Mono  (this AudioFileInfo    obj) => Channels(obj, MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetupEnum Mono(this SpeakerSetupEnum obj) => Channels(obj, MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetup Mono(this SpeakerSetup obj, IContext context) => Channels(obj, MonoChannels, context);
        
        public   static SynthWishes      Stereo(this SynthWishes      obj) => Channels(obj, StereoChannels);
        public   static FlowNode         Stereo(this FlowNode         obj) => Channels(obj, StereoChannels);
        public   static ConfigWishes     Stereo(this ConfigWishes     obj) => Channels(obj, StereoChannels);
        public   static Tape             Stereo(this Tape             obj) => Channels(obj, StereoChannels);
        public   static TapeConfig       Stereo(this TapeConfig       obj) => Channels(obj, StereoChannels);
        public   static TapeActions      Stereo(this TapeActions      obj) => Channels(obj, StereoChannels);
        public   static TapeAction       Stereo(this TapeAction       obj) => Channels(obj, StereoChannels);
        public   static Buff             Stereo(this Buff             obj, IContext context) => Channels(obj, StereoChannels, context);
        public   static Sample           Stereo(this Sample           obj, IContext context) => Channels(obj, StereoChannels, context);
        public   static AudioFileOutput  Stereo(this AudioFileOutput  obj, IContext context) => Channels(obj, StereoChannels, context);
        public   static WavHeaderStruct  Stereo(this WavHeaderStruct  obj) => Channels(obj, StereoChannels);
        public   static AudioInfoWish    Stereo(this AudioInfoWish    obj) => Channels(obj, StereoChannels);
        public   static AudioFileInfo    Stereo(this AudioFileInfo    obj) => Channels(obj, StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetupEnum Stereo(this SpeakerSetupEnum obj) => Channels(obj, StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetup Stereo(this SpeakerSetup obj, IContext context) => Channels(obj, StereoChannels, context);
    }
}
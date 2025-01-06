using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

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
            return obj.Channels ?? ConfigWishes.DefaultChannels;
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
        public static int Channels(this SpeakerSetupEnum obj) => EnumToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetupEnum Channels(this SpeakerSetupEnum obj, int value) => ChannelsToEnum(value);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int Channels(this ChannelEnum obj) => EnumToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static ChannelEnum Channels(this ChannelEnum obj, int value) => ChannelsToEnum(value, Channel(obj));
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int Channels(this SpeakerSetup obj) => EntityToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetup Channels(this SpeakerSetup obj, int value, IContext context) => ChannelsToEntity(value, context);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int Channels(this Channel obj) => EntityToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static Channel Channels(this Channel obj, int channels, IContext context) => ChannelsToEntity(channels, Channel(obj), context);
        
        // Channels Conversion-Style
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetupEnum ChannelsToEnum(this int channels)
        {
            switch (channels)
            {
                case 0: return SpeakerSetupEnum.Undefined;
                case 1: return SpeakerSetupEnum.Mono;
                case 2: return SpeakerSetupEnum.Stereo;
                default: throw new Exception($"{new { channels }} not supported.");
            }
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int EnumToChannels(this SpeakerSetupEnum enumValue)
        {
            switch (enumValue)
            {
                case SpeakerSetupEnum.Mono: return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                case SpeakerSetupEnum.Undefined: return 0;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static ChannelEnum ChannelsToEnum(this int channels, int? channel)
        {
            if (!FilledInWishes.Has(channel)) return ChannelEnum.Undefined;
            if (channels == 1 && channel == 0) return ChannelEnum.Single;
            if (channels == 2 && channel == 0) return ChannelEnum.Left;
            if (channels == 2 && channel == 1) return ChannelEnum.Right;
            throw new Exception($"Unsupported combination of values {new { channels, channel }}");
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int EnumToChannels(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Single: return 1;
                case ChannelEnum.Left: return 2;
                case ChannelEnum.Right: return 2;
                default: throw new ValueNotSupportedException(channelEnum);
            }
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetup ChannelsToEntity(this int channels, IContext context)
            => ChannelsToEnum(channels).ToEntity(context);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int EntityToChannels(this SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return EnumToChannels(entity.ToEnum());
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static Channel ChannelsToEntity(this int channels, int? channel, IContext context)
            => ChannelsToEnum(channels, channel).ToEntity(context);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int EntityToChannels(this Channel entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return EnumToChannels(entity.ToEnum());
        }
        
        // Channels Shorthand
        
        public   static bool IsMono  (this SynthWishes      obj) => Channels(obj) == 1;
        public   static bool IsMono  (this FlowNode         obj) => Channels(obj) == 1;
        public   static bool IsMono  (this ConfigWishes     obj) => Channels(obj) == 1;
        internal static bool IsMono  (this ConfigSection    obj) => Channels(obj) == 1;
        public   static bool IsMono  (this Tape             obj) => Channels(obj) == 1;
        public   static bool IsMono  (this TapeConfig       obj) => Channels(obj) == 1;
        public   static bool IsMono  (this TapeActions      obj) => Channels(obj) == 1;
        public   static bool IsMono  (this TapeAction       obj) => Channels(obj) == 1;
        public   static bool IsMono  (this Buff             obj) => Channels(obj) == 1;
        public   static bool IsMono  (this Sample           obj) => Channels(obj) == 1;
        public   static bool IsMono  (this AudioFileOutput  obj) => Channels(obj) == 1;
        public   static bool IsMono  (this WavHeaderStruct  obj) => Channels(obj) == 1;
        public   static bool IsMono  (this AudioInfoWish    obj) => Channels(obj) == 1;
        public   static bool IsMono  (this AudioFileInfo    obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono(this SpeakerSetupEnum obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono(this SpeakerSetup     obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono(this ChannelEnum      obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono(this Channel          obj) => Channels(obj) == 1;
        
        public   static bool IsStereo(this SynthWishes      obj) => Channels(obj) == 2;
        public   static bool IsStereo(this FlowNode         obj) => Channels(obj) == 2;
        public   static bool IsStereo(this ConfigWishes     obj) => Channels(obj) == 2;
        internal static bool IsStereo(this ConfigSection    obj) => Channels(obj) == 2;
        public   static bool IsStereo(this Tape             obj) => Channels(obj) == 2;
        public   static bool IsStereo(this TapeConfig       obj) => Channels(obj) == 2;
        public   static bool IsStereo(this TapeActions      obj) => Channels(obj) == 2;
        public   static bool IsStereo(this TapeAction       obj) => Channels(obj) == 2;
        public   static bool IsStereo(this Buff             obj) => Channels(obj) == 2;
        public   static bool IsStereo(this Sample           obj) => Channels(obj) == 2;
        public   static bool IsStereo(this AudioFileOutput  obj) => Channels(obj) == 2;
        public   static bool IsStereo(this WavHeaderStruct  obj) => Channels(obj) == 2;
        public   static bool IsStereo(this AudioInfoWish    obj) => Channels(obj) == 2;
        public   static bool IsStereo(this AudioFileInfo    obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this SpeakerSetupEnum obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this SpeakerSetup     obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this ChannelEnum      obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this Channel          obj) => Channels(obj) == 2;
        
        public   static SynthWishes      Mono  (this SynthWishes      obj) => Channels(obj, 1);
        public   static FlowNode         Mono  (this FlowNode         obj) => Channels(obj, 1);
        public   static ConfigWishes     Mono  (this ConfigWishes     obj) => Channels(obj, 1);
        public   static Tape             Mono  (this Tape             obj) => Channels(obj, 1);
        public   static TapeConfig       Mono  (this TapeConfig       obj) => Channels(obj, 1);
        public   static TapeActions      Mono  (this TapeActions      obj) => Channels(obj, 1);
        public   static TapeAction       Mono  (this TapeAction       obj) => Channels(obj, 1);
        public   static Buff             Mono  (this Buff             obj, IContext context) => Channels(obj, 1, context);
        public   static Sample           Mono  (this Sample           obj, IContext context) => Channels(obj, 1, context);
        public   static AudioFileOutput  Mono  (this AudioFileOutput  obj, IContext context) => Channels(obj, 1, context);
        public   static WavHeaderStruct  Mono  (this WavHeaderStruct  obj) => Channels(obj, 1);
        public   static AudioInfoWish    Mono  (this AudioInfoWish    obj) => Channels(obj, 1);
        public   static AudioFileInfo    Mono  (this AudioFileInfo    obj) => Channels(obj, 1);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetupEnum Mono(this SpeakerSetupEnum obj) => Channels(obj, 1);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetup Mono(this SpeakerSetup obj, IContext context) => Channels(obj, 1, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static ChannelEnum Mono(this ChannelEnum obj) => Channels(obj, 1);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static Channel Mono(this Channel obj, IContext context) => Channels(obj, 1, context);
        
        public   static SynthWishes      Stereo(this SynthWishes      obj) => Channels(obj, 2);
        public   static FlowNode         Stereo(this FlowNode         obj) => Channels(obj, 2);
        public   static ConfigWishes     Stereo(this ConfigWishes     obj) => Channels(obj, 2);
        public   static Tape             Stereo(this Tape             obj) => Channels(obj, 2);
        public   static TapeConfig       Stereo(this TapeConfig       obj) => Channels(obj, 2);
        public   static TapeActions      Stereo(this TapeActions      obj) => Channels(obj, 2);
        public   static TapeAction       Stereo(this TapeAction       obj) => Channels(obj, 2);
        public   static Buff             Stereo(this Buff             obj, IContext context) => Channels(obj, 2, context);
        public   static Sample           Stereo(this Sample           obj, IContext context) => Channels(obj, 2, context);
        public   static AudioFileOutput  Stereo(this AudioFileOutput  obj, IContext context) => Channels(obj, 2, context);
        public   static WavHeaderStruct  Stereo(this WavHeaderStruct  obj) => Channels(obj, 2);
        public   static AudioInfoWish    Stereo(this AudioInfoWish    obj) => Channels(obj, 2);
        public   static AudioFileInfo    Stereo(this AudioFileInfo    obj) => Channels(obj, 2);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetupEnum Stereo(this SpeakerSetupEnum obj) => Channels(obj, 2);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static SpeakerSetup Stereo(this SpeakerSetup obj, IContext context) => Channels(obj, 2, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static ChannelEnum Stereo(this ChannelEnum obj) => Channels(obj, 2);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static Channel Stereo(this Channel obj, IContext context) => Channels(obj, 2, context);
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.Environment;
using static System.IO.File;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._audioinfowish"/>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class AudioInfoWish
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);

        public int Bits { get; set; }
        public int Channels { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="docs._framecount"/>
        public int FrameCount { get; set; }
    }

    /// <inheritdoc cref="docs._audiopropertywishes"/>
    public static class AudioPropertyWishes
    {
        // Primary Audio Properties
        
        #region Bits
        
        public static int Bits(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetBits;
        }

        public static SynthWishes Bits(this SynthWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithBits(value);
        }

        public static int Bits(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetBits;
        }

        public static FlowNode Bits(this FlowNode entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithBits(value);
        }

        public static int Bits(this ConfigWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetBits;
        }

        public static ConfigWishes Bits(this ConfigWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithBits(value);
        }

        internal static int Bits(this ConfigSection entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Bits ?? DefaultBits;
        }

        internal static ConfigSection Bits(this ConfigSection entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Bits = value;
            return entity;
        }
        
        public static int Bits(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Config.Bits;
        }
        
        public static Tape Bits(this Tape entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Config.Bits = value;
            return entity;
        }

        public static int Bits(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Bits;
        }

        public static TapeConfig Bits(this TapeConfig entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Bits = value;
            return entity;
        }

        public static int Bits(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Bits;
        }

        public static TapeActions Bits(this TapeActions entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Bits = value;
            return entity;
        }

        public static int Bits(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Bits;
        }

        public static TapeAction Bits(this TapeAction entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Bits = value;
            return entity;
        }

        public static int Bits(this Buff entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return Bits(entity.UnderlyingAudioFileOutput);
        }

        public static Buff Bits(this Buff entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            Bits(entity.UnderlyingAudioFileOutput, value);
            return entity;
        }

        public static int Bits(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return Bits(entity.GetSampleDataTypeEnum());
        }
        
        public static Sample Bits(this Sample entity, int value, IContext context = null)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return entity;
        }

        public static int Bits(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return Bits(entity.GetSampleDataTypeEnum());
        }

        public static AudioFileOutput Bits(this AudioFileOutput entity, int value, IContext context = null)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return entity;
        }

        public static int Bits(this WavHeaderStruct entity)
            => entity.BitsPerValue;

        public static WavHeaderStruct Bits(this WavHeaderStruct entity, int value) 
            => entity.ToWish().Bits(value).ToWavHeader();

        public static int Bits(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.Bits;
        }

        public static AudioInfoWish Bits(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.Bits = value;
            return infoWish;
        }
        
        public static int Bits(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.BytesPerValue * 8;
        }

        public static AudioFileInfo Bits(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.BytesPerValue = value / 8;
            return info;
        }
                
        public static int Bits<TValueType>() => Bits(typeof(TValueType));
        
        public static int Bits(this Type valueType) 
        {
            switch (valueType)
            {
                case Type t when t == typeof(Byte): return 8;
                case Type t when t == typeof(Int16): return 16;
                case Type t when t == typeof(Single): return 32;
                default: throw new ValueNotSupportedException(valueType);
            }
        }

        [Obsolete(ObsoleteMessage)]
        public static int Bits(this SampleDataTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case SampleDataTypeEnum.Byte: return 8;
                case SampleDataTypeEnum.Int16: return 16;
                case SampleDataTypeEnum.Float32: return 32;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int Bits(this SampleDataType enumEntity)
        {
            if (enumEntity == null) throw new NullException(() => enumEntity);
            return enumEntity.ToEnum().Bits();
        }

        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum BitsToEnum(this int bits)
        {
            switch (AssertBits(bits))
            {
                case 32: return SampleDataTypeEnum.Float32;
                case 16: return SampleDataTypeEnum.Int16;
                case 8: return SampleDataTypeEnum.Byte;
                default: return default;
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType BitsToEntity(this int bits, IContext context) 
            => bits.BitsToEnum().ToEntity(context);
        
        public static bool Is8Bit (SynthWishes entity)          { if (entity == null) throw new NullException(() => entity); return entity.Is8Bit     ; }
        public static bool Is16Bit(SynthWishes entity)          { if (entity == null) throw new NullException(() => entity); return entity.Is16Bit    ; }
        public static bool Is32Bit(SynthWishes entity)          { if (entity == null) throw new NullException(() => entity); return entity.Is32Bit    ; }
        public static SynthWishes With8Bit (SynthWishes entity) { if (entity == null) throw new NullException(() => entity); return entity.With8Bit() ; }
        public static SynthWishes With16Bit(SynthWishes entity) { if (entity == null) throw new NullException(() => entity); return entity.With16Bit(); }
        public static SynthWishes With32Bit(SynthWishes entity) { if (entity == null) throw new NullException(() => entity); return entity.With16Bit(); }
        
        #endregion
                
        #region Channels
        
        public static int Channels(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannels;
        }

        public static SynthWishes Channels(this SynthWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithChannels(value);
        }

        public static int Channels(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannels;
        }

        public static FlowNode Channels(this FlowNode entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithChannels(value);
        }

        public static int Channels(this ConfigWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannels;
        }

        public static ConfigWishes Channels(this ConfigWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithChannels(value);
        }

        internal static int Channels(this ConfigSection entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Channels ?? DefaultChannels;
        }

        internal static ConfigSection Channels(this ConfigSection entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Channels = value;
            return entity;
        }
        
        public static int Channels(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Config.Channels;
        }
        
        public static Tape Channels(this Tape entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Config.Channels = value;
            return entity;
        }

        public static int Channels(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Channels;
        }

        public static TapeConfig Channels(this TapeConfig entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Channels = value;
            return entity;
        }

        public static int Channels(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Channels;
        }

        public static TapeActions Channels(this TapeActions entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Channels = value;
            return entity;
        }

        public static int Channels(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Channels;
        }

        public static TapeAction Channels(this TapeAction entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Channels = value;
            return entity;
        }

        public static int Channels(this Buff entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return Channels(entity.UnderlyingAudioFileOutput);
        }

        public static Buff Channels(this Buff entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            Channels(entity.UnderlyingAudioFileOutput, value);
            return entity;
        }

        public static int Channels(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannelCount();
        }
        
        public static Sample Channels(this Sample entity, int value, IContext context = null)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SetSpeakerSetupEnum(value.ChannelsToEnum(), context);
            return entity;
        }
        
        public static int Channels(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannelCount();
        }

        public static AudioFileOutput Channels(this AudioFileOutput entity, int value, IContext context = null)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SpeakerSetup = GetSubstituteSpeakerSetup(value, context);
            return entity;
        }
        
        public static int Channels(this WavHeaderStruct entity)
            => entity.ChannelCount;

        public static WavHeaderStruct Channels(this WavHeaderStruct entity, int value) 
            => entity.ToWish().Channels(value).ToWavHeader();

        public static int Channels(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.Channels;
        }

        public static AudioInfoWish Channels(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.Channels = value;
            return infoWish;
        }
                                
        public static int Channels(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.ChannelCount;
        }

        public static AudioFileInfo Channels(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.ChannelCount = value;
            return info;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int Channels(this SpeakerSetupEnum enumValue)
        {
            switch (enumValue)
            {
                case SpeakerSetupEnum.Mono: return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int Channels(this SpeakerSetup enumEntity)
        {
            if (enumEntity == null) throw new NullException(() => enumEntity);
            return enumEntity.ToEnum().Channels();
        }

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetupEnum ChannelsToEnum(this int channels)
        {
            switch (AssertChannels(channels))
            {
                case 1: return SpeakerSetupEnum.Mono;
                case 2: return SpeakerSetupEnum.Stereo;
                default: return default;
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup ChannelsToEntity(this int channels, IContext context) 
            => channels.ChannelsToEnum().ToEntity(context);

        #endregion

        #region Channel
        
        public static int? Channel(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannel;
        }

        public static SynthWishes Channel(this SynthWishes entity, int? value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithChannel(value);
        }

        public static int? Channel(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannel;
        }

        public static FlowNode Channel(this FlowNode entity, int? value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithChannel(value);
        }

        public static int? Channel(this ConfigWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannel;
        }

        public static ConfigWishes Channel(this ConfigWishes entity, int? value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithChannel(value);
        }
        
        public static int? Channel(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Config.Channel;
        }
        
        public static Tape Channel(this Tape entity, int? value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Config.Channel = value;
            return entity;
        }

        public static int? Channel(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Channel;
        }

        public static TapeConfig Channel(this TapeConfig entity, int? value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Channel = value;
            return entity;
        }

        public static int? Channel(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Channel;
        }

        public static TapeActions Channel(this TapeActions entity, int? value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Channel = value;
            return entity;
        }

        public static int? Channel(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Channel;
        }

        public static TapeAction Channel(this TapeAction entity, int? value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Channel = value;
            return entity;
        }

        public static int? Channel(this Buff entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return Channel(entity.UnderlyingAudioFileOutput);
        }
        
        public static int? Channel(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.AudioFileOutputChannels == null) throw new NullException(() => entity.AudioFileOutputChannels);

            int channels = entity.Channels();
            int signalCount = entity.AudioFileOutputChannels.Count;
            int? firstChannelNumber = entity.AudioFileOutputChannels.ElementAtOrDefault(0)?.Channel();
            
            // Mono has channel 0 only.
            if (channels == 1) return 0;
            
            if (channels == 2)
            {
                if (signalCount == 2)
                {
                    // Handles stereo with 2 channels defined, so not specific channel can be returned,
                    return null;
                }
                if (signalCount == 1)
                {
                    // By returning index, we handle both "Left-only" and "Right-only" (single channel 1) scenarios.
                    if (firstChannelNumber != null)
                    {
                        return firstChannelNumber;
                    }
                }
            }

            throw new Exception(
                "Unsupported combination of values: " + NewLine +
                $"entity.Channels = {channels}, " + NewLine +
                $"entity.AudioFileOutputChannels.Count = {signalCount} ({nameof(signalCount)})" + NewLine +
                $"entity.AudioFileOutputChannels[0].Index = {firstChannelNumber} ({nameof(firstChannelNumber)})");
        }
        
        public static int Channel(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Index;
        }
        
        public static AudioFileOutputChannel Channel(this AudioFileOutputChannel entity, int channel)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Index = channel;
            return entity;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int? Channel(this Channel enumEntity)
        {
            return enumEntity?.Index;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int? Channel(this ChannelEnum enumValue)
        {
            switch (enumValue)
            {
                case ChannelEnum.Single: return 0;
                case ChannelEnum.Left: return 0;
                case ChannelEnum.Right: return 1;
                case ChannelEnum.Undefined: return null;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }

        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum ToEnum(this int? channel, int channels)
            => ToEnum(channel, channels.ChannelsToEnum());

        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum ToEnum(this int? channel, SpeakerSetupEnum speakerSetupEnum)
        {
            if (channel == default) return default;
            
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:
                    if (channel == 0) return ChannelEnum.Single;
                    break;
                
                case SpeakerSetupEnum.Stereo:
                    if (channel == 0) return ChannelEnum.Left;
                    if (channel == 1) return ChannelEnum.Right;
                    break;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { speakerSetupEnum, channel });
        }

        [Obsolete(ObsoleteMessage)]
        public static Channel ToEntity(this int channel, int channels, IContext context)
            => ToEnum(channel, channels).ToEntity(context);

        [Obsolete(ObsoleteMessage)]
        public static Channel ToEntity(this int channel, SpeakerSetupEnum speakerSetupEnum, IContext context)
            => ToEnum(channel, speakerSetupEnum).ToEntity(context);
        
        #endregion

        #region SamplingRate
        
        public static int SamplingRate(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetSamplingRate;
        }

        public static SynthWishes SamplingRate(this SynthWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithSamplingRate(value);
        }

        public static int SamplingRate(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetSamplingRate;
        }

        public static FlowNode SamplingRate(this FlowNode entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithSamplingRate(value);
        }

        public static int SamplingRate(this ConfigWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetSamplingRate;
        }

        public static ConfigWishes SamplingRate(this ConfigWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithSamplingRate(value);
        }

        internal static int SamplingRate(this ConfigSection entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.SamplingRate ?? DefaultSamplingRate;
        }

        internal static ConfigSection SamplingRate(this ConfigSection entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SamplingRate = value;
            return entity;
        }
        
        public static int SamplingRate(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Config.SamplingRate;
        }
        
        public static Tape SamplingRate(this Tape entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Config.SamplingRate = value;
            return entity;
        }

        public static int SamplingRate(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.SamplingRate;
        }

        public static TapeConfig SamplingRate(this TapeConfig entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SamplingRate = value;
            return entity;
        }

        public static int SamplingRate(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.SamplingRate;
        }

        public static TapeActions SamplingRate(this TapeActions entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.SamplingRate = value;
            return entity;
        }

        public static int SamplingRate(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.SamplingRate;
        }

        public static TapeAction SamplingRate(this TapeAction entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.SamplingRate = value;
            return entity;
        }

        public static int SamplingRate(this Buff entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return SamplingRate(entity.UnderlyingAudioFileOutput);
        }

        public static Buff SamplingRate(this Buff entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            SamplingRate(entity.UnderlyingAudioFileOutput, value);
            return entity;
        }

        public static int SamplingRate(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.SamplingRate;
        }
        
        public static Sample SamplingRate(this Sample entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SamplingRate = value;
            return entity;
        }
        
        public static int SamplingRate(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.SamplingRate;
        }

        public static AudioFileOutput SamplingRate(this AudioFileOutput entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SamplingRate = value;
            return entity;
        }
        
        public static int SamplingRate(this WavHeaderStruct entity)
            => entity.SamplingRate;

        public static WavHeaderStruct SamplingRate(this WavHeaderStruct entity, int value) 
            => entity.ToWish().SamplingRate(value).ToWavHeader();

        public static int SamplingRate(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.SamplingRate;
        }

        public static AudioInfoWish SamplingRate(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.SamplingRate = value;
            return infoWish;
        }
                                
        public static int SamplingRate(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SamplingRate;
        }

        public static AudioFileInfo SamplingRate(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SamplingRate = value;
            return info;
        }

        #endregion

        #region AudioFormat
        
        public static AudioFileFormatEnum AudioFormat(this string fileExtension)
        {
            if (Is(fileExtension, ".wav")) return Wav;
            if (Is(fileExtension, ".raw")) return Raw;
            throw new Exception($"{new{fileExtension}} not supported.");
        }

        public static AudioFileFormatEnum AudioFormat(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioFormat;
        }

        public static SynthWishes AudioFormat(this SynthWishes entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithAudioFormat(value);
        }

        public static AudioFileFormatEnum AudioFormat(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioFormat;
        }

        public static FlowNode AudioFormat(this FlowNode entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithAudioFormat(value);
        }

        public static AudioFileFormatEnum AudioFormat(this ConfigWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioFormat;
        }

        public static ConfigWishes AudioFormat(this ConfigWishes entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithAudioFormat(value);
        }

        internal static AudioFileFormatEnum AudioFormat(this ConfigSection entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.AudioFormat ?? DefaultAudioFormat;
        }

        internal static ConfigSection AudioFormat(this ConfigSection entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.AudioFormat = value;
            return entity;
        }

        public static AudioFileFormatEnum AudioFormat(this Buff entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return AudioFormat(entity.UnderlyingAudioFileOutput);
        }

        public static Buff AudioFormat(this Buff entity, AudioFileFormatEnum value, IContext context = null)
        {
            if (entity == null) throw new NullException(() => entity);
            AudioFormat(entity.UnderlyingAudioFileOutput, value, context);
            return entity;
        }

        public static AudioFileFormatEnum AudioFormat(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Config.AudioFormat;
        }

        public static Tape AudioFormat(this Tape entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Config.AudioFormat = value;
            return entity;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.AudioFormat;
        }

        public static TapeConfig AudioFormat(this TapeConfig entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.AudioFormat = value;
            return entity;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.AudioFormat;
        }

        public static TapeAction AudioFormat(this TapeAction entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.AudioFormat = value;
            return entity;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.AudioFormat;
        }

        public static TapeActions AudioFormat(this TapeActions entity, AudioFileFormatEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.AudioFormat = value;
            return entity;
        }

        public static AudioFileFormatEnum AudioFormat(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioFileFormatEnum();
        }

        public static Sample AudioFormat(this Sample entity, AudioFileFormatEnum value, IContext context)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SetAudioFileFormatEnum(value, context);
            return entity;
        }

        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioFileFormatEnum();
        }

        public static AudioFileOutput AudioFormat(this AudioFileOutput entity, AudioFileFormatEnum value, IContext context)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SetAudioFileFormatEnum(value, context);
            return entity;
        }
        
        public static AudioFileFormatEnum AudioFormat([UsedImplicitly] WavHeaderStruct entity) => Wav;
        
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormat entity) 
            => ToEnum(entity);
        
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormatEnum ToEnum(this AudioFileFormat enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (AudioFileFormatEnum)enumEntity.ID;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToEntity(this AudioFileFormatEnum audioFormat, IContext context) 
            => CreateRepository<IAudioFileFormatRepository>(context).Get(audioFormat.ToID());
        
        #endregion

        #region Interpolation

        public static InterpolationTypeEnum Interpolation(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetInterpolation;
        }

        public static SynthWishes Interpolation(this SynthWishes entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithInterpolation(value);
        }

        public static InterpolationTypeEnum Interpolation(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetInterpolation;
        }

        public static FlowNode Interpolation(this FlowNode entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithInterpolation(value);
        }

        public static InterpolationTypeEnum Interpolation(this ConfigWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetInterpolation;
        }

        public static ConfigWishes Interpolation(this ConfigWishes entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithInterpolation(value);
        }

        internal static InterpolationTypeEnum Interpolation(this ConfigSection entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Interpolation ?? DefaultInterpolation;
        }

        internal static ConfigSection Interpolation(this ConfigSection entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Interpolation = value;
            return entity;
        }

        public static InterpolationTypeEnum Interpolation(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Config.Interpolation;
        }

        public static Tape Interpolation(this Tape entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Config.Interpolation = value;
            return entity;
        }

        public static InterpolationTypeEnum Interpolation(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Interpolation;
        }

        public static TapeConfig Interpolation(this TapeConfig entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Interpolation = value;
            return entity;
        }

        public static InterpolationTypeEnum Interpolation(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Interpolation;
        }

        public static TapeAction Interpolation(this TapeAction entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Interpolation = value;
            return entity;
        }

        public static InterpolationTypeEnum Interpolation(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Config.Interpolation;
        }

        public static TapeActions Interpolation(this TapeActions entity, InterpolationTypeEnum value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Config.Interpolation = value;
            return entity;
        }

        public static InterpolationTypeEnum Interpolation(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetInterpolationTypeEnum();
        }

        public static Sample Interpolation(this Sample entity, InterpolationTypeEnum value, IContext context)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.SetInterpolationTypeEnum(value, context);
            return entity;
        }
    
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum Interpolation(this InterpolationType entity) 
            => ToEnum(entity);
        
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum ToEnum(this InterpolationType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (InterpolationTypeEnum)enumEntity.ID;
        }

        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        #endregion
        
        #region CourtesyFrames
        
        public static int CourtesyFrames(int courtesyBytes, int frameSize)
        {
            if (courtesyBytes < 0) throw new Exception(nameof(frameSize) + " less than 0.");
            if (frameSize < 1) throw new Exception(nameof(frameSize) + " less than 1.");
            return courtesyBytes / frameSize;
        }
        
        public static int CourtesyFrames(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetCourtesyFrames;
        }
        
        public static SynthWishes CourtesyFrames(this SynthWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithCourtesyFrames(value);
        }
        
        public static int CourtesyFrames(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetCourtesyFrames;
        }
        
        public static FlowNode CourtesyFrames(this FlowNode entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithCourtesyFrames(value);
        }

        public static int CourtesyFrames(this ConfigWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetCourtesyFrames;
        }
        
        public static ConfigWishes CourtesyFrames(this ConfigWishes entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.WithCourtesyFrames(value);
        }

        internal static int CourtesyFrames(this ConfigSection entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.CourtesyFrames ?? DefaultCourtesyFrames;
        }
        
        internal static ConfigSection CourtesyFrames(this ConfigSection entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.CourtesyFrames = value;
            return entity;
        }

        public static int CourtesyFrames(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Config.CourtesyFrames;
        }
        
        public static Tape CourtesyFrames(this Tape entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Config.CourtesyFrames = value;
            return entity;
        }

        public static int CourtesyFrames(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.CourtesyFrames;
        }

        public static TapeConfig CourtesyFrames(this TapeConfig entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.CourtesyFrames = value;
            return entity;
        }

        public static int CourtesyFrames(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return CourtesyFrames(entity.Tape);
        }

        public static TapeActions CourtesyFrames(this TapeActions entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            CourtesyFrames(entity.Tape, value);
            return entity;
        }

        public static int CourtesyFrames(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return CourtesyFrames(entity.Tape);
        }

        public static TapeAction CourtesyFrames(this TapeAction entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            CourtesyFrames(entity.Tape, value);
            return entity;
        }

        #endregion

        // Derived Properties
        
        #region SizeOfBitDepth
        
        public static int SizeOfBitDepth(this SynthWishes entity) => Bits(entity) / 8;
        public static SynthWishes SizeOfBitDepth(this SynthWishes entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this FlowNode entity) => Bits(entity) / 8;
        public static FlowNode SizeOfBitDepth(this FlowNode entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this ConfigWishes entity) => Bits(entity) / 8;
        public static ConfigWishes SizeOfBitDepth(this ConfigWishes entity, int value) => Bits(entity, value * 8);
        internal static int SizeOfBitDepth(this ConfigSection entity) => Bits(entity) / 8;
        internal static ConfigSection SizeOfBitDepth(this ConfigSection entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this Tape entity) => Bits(entity) / 8;
        public static Tape SizeOfBitDepth(this Tape entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this TapeConfig entity) => Bits(entity) / 8;
        public static TapeConfig SizeOfBitDepth(this TapeConfig entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this TapeActions entity) => Bits(entity) / 8;
        public static TapeActions SizeOfBitDepth(this TapeActions entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this TapeAction entity) => Bits(entity) / 8;
        public static TapeAction SizeOfBitDepth(this TapeAction entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this Buff entity) => Bits(entity) / 8;
        public static Buff SizeOfBitDepth(this Buff entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this Sample entity) => Bits(entity) / 8;
        public static Sample SizeOfBitDepth(this Sample entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this AudioFileOutput entity) => Bits(entity) / 8;
        public static AudioFileOutput SizeOfBitDepth(this AudioFileOutput entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this WavHeaderStruct entity) => Bits(entity) / 8;
        public static WavHeaderStruct SizeOfBitDepth(this WavHeaderStruct entity, int value) => Bits(entity, value * 8);
        public static int SizeOfBitDepth(this AudioInfoWish infoWish) => Bits(infoWish) / 8;
        public static AudioInfoWish SizeOfBitDepth(this AudioInfoWish infoWish, int value) => Bits(infoWish, value * 8);
        public static int SizeOfBitDepth(this AudioFileInfo info) => Bits(info) / 8;
        public static AudioFileInfo SizeOfBitDepth(this AudioFileInfo info, int value) => Bits(info, value * 8);
        public static int SizeOfBitDepth(this int bits) => bits / 8;
        public static int SizeOfBitDepth(Type sampleDataType)
        {
            if (sampleDataType == typeof(Byte)) return 1;
            if (sampleDataType == typeof(Int16)) return 2;
            if (sampleDataType == typeof(Single)) return 4;
            throw new ValueNotSupportedException(sampleDataType);
        }
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataTypeEnum enumValue) => SampleDataTypeHelper.SizeOf(enumValue);
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataType enumEntity) => SampleDataTypeHelper.SizeOf(enumEntity);

        #endregion
        
        #region FrameSize
        
        public static int FrameSize(this SynthWishes entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this FlowNode entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this ConfigWishes entity) => SizeOfBitDepth(entity) * Channels(entity);
        internal static int FrameSize(this ConfigSection entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this Tape entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this TapeConfig entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this TapeAction entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this TapeActions entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this Buff entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this Sample entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this AudioFileOutput entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this WavHeaderStruct entity) => SizeOfBitDepth(entity) * Channels(entity);
        public static int FrameSize(this AudioInfoWish infoWish) => SizeOfBitDepth(infoWish) * Channels(infoWish);
        public static int FrameSize(this AudioFileInfo info) => SizeOfBitDepth(info) * Channels(info);
        
        [Obsolete(ObsoleteMessage)] 
        public static int FrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities) 
            => SizeOfBitDepth(entities.sampleDataType) * Channels(entities.speakerSetup);
        
        [Obsolete(ObsoleteMessage)] 
        public static int FrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums) 
            => SizeOfBitDepth(enums.sampleDataTypeEnum) * Channels(enums.speakerSetupEnum);

        #endregion
        
        #region MaxValue
        
        public static double MaxValue(this SynthWishes entity) => MaxValue(Bits(entity));
        public static double MaxValue(this FlowNode entity) => MaxValue(Bits(entity));
        public static double MaxValue(this ConfigWishes entity) => MaxValue(Bits(entity));
        internal static double MaxValue(this ConfigSection entity) => MaxValue(Bits(entity));
        public static double MaxValue(this Buff entity) => MaxValue(Bits(entity));
        public static double MaxValue(this Tape entity) => MaxValue(Bits(entity));
        public static double MaxValue(this TapeConfig entity) => MaxValue(Bits(entity));
        public static double MaxValue(this TapeAction entity) => MaxValue(Bits(entity));
        public static double MaxValue(this TapeActions entity) => MaxValue(Bits(entity));
        public static double MaxValue(this Sample entity) => MaxValue(Bits(entity));
        public static double MaxValue(this AudioFileOutput entity) => MaxValue(Bits(entity));
        public static double MaxValue(this WavHeaderStruct entity) => MaxValue(Bits(entity));
        public static double MaxValue(this AudioFileInfo info) => MaxValue(Bits(info));
        public static double MaxValue(this AudioInfoWish infoWish) => MaxValue(Bits(infoWish));
        public static double MaxValue(this int bits)
        {
            switch (AssertBits(bits))
            {
                case 32: return 1;
                case 16: return Int16.MaxValue; // ReSharper disable once PossibleLossOfFraction
                case 8: return byte.MaxValue / 2;
                default: return default;
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double GetMaxValue(this SampleDataType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return enumEntity.ToEnum().MaxValue();
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double MaxValue(this SampleDataTypeEnum enumValue) => enumValue.Bits().MaxValue();
        
        #endregion
                
        #region FileExtension
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormatEnum audioFormat)
        {
            switch (audioFormat)
            {
                case Wav: return ".wav";
                case Raw: return ".raw";
                default: throw new ValueNotSupportedException(audioFormat);
            }
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this SynthWishes entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static SynthWishes FileExtension(this SynthWishes entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this FlowNode entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static FlowNode FileExtension(this FlowNode entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this ConfigWishes entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static ConfigWishes FileExtension(this ConfigWishes entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string FileExtension(this ConfigSection entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigSection FileExtension(this ConfigSection entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Tape entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape FileExtension(this Tape entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeConfig entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig FileExtension(this TapeConfig entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeActions entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions FileExtension(this TapeActions entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeAction entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction FileExtension(this TapeAction entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Buff entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff FileExtension(this Buff entity, string value) => AudioFormat(entity, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Sample entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample FileExtension(this Sample entity, string value, IContext context) => AudioFormat(entity, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput entity) => AudioFormat(entity).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput FileExtension(this AudioFileOutput entity, string value, IContext context) => AudioFormat(entity, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension([UsedImplicitly] this WavHeaderStruct entity) => AudioFormat(entity).FileExtension();
        
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static string FileExtension(this AudioFileFormat enumEntity)
        {
            if (enumEntity == null) throw new NullException(() => enumEntity);
            return enumEntity.ToEnum().FileExtension();
        }

        #endregion
                
        #region HeaderLength
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum audioFormat)
        {
            switch (audioFormat)
            {
                case Wav: return 44;
                case Raw: return 0;
                default: throw new ValueNotSupportedException(audioFormat);
            }
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this SynthWishes entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this FlowNode entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this ConfigWishes entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int HeaderLength(this ConfigSection entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Buff entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Tape entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeConfig entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeAction entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeActions entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput entity) => AudioFormat(entity).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int HeaderLength(this WavHeaderStruct entity) => HeaderLength(Wav);
        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] 
        public static int HeaderLength(this AudioFileFormat enumEntity) => AudioFormat(enumEntity).HeaderLength();
        
        #endregion
        
        #region CourtesyBytes
        
        public static int CourtesyBytes(int courtesyFrames, int frameSize)
        {
            if (courtesyFrames < 0) throw new Exception(nameof(frameSize) + " less than 0.");
            if (frameSize < 1) throw new Exception(nameof(frameSize) + " less than 1.");
            return courtesyFrames * frameSize;
        }
        
        public static int CourtesyBytes(this SynthWishes entity)
            => CourtesyBytes(CourtesyFrames(entity), FrameSize(entity));

        public static SynthWishes CourtesyBytes(this SynthWishes entity, int value) 
            => CourtesyFrames(entity, CourtesyFrames(value, FrameSize(entity)));
        
        public static int CourtesyBytes(this FlowNode entity)
            => CourtesyBytes(CourtesyFrames(entity), FrameSize(entity));

        public static FlowNode CourtesyBytes(this FlowNode entity, int value) 
            => CourtesyFrames(entity, CourtesyFrames(value, FrameSize(entity)));
        
        public static int CourtesyBytes(this ConfigWishes entity)
            => CourtesyBytes(CourtesyFrames(entity), FrameSize(entity));

        public static ConfigWishes CourtesyBytes(this ConfigWishes entity, int value) 
            => CourtesyFrames(entity, CourtesyFrames(value, FrameSize(entity)));

        internal static int CourtesyBytes(this ConfigSection entity)
            => CourtesyBytes(CourtesyFrames(entity), FrameSize(entity));

        internal static ConfigSection CourtesyBytes(this ConfigSection entity, int value) 
            => CourtesyFrames(entity, CourtesyFrames(value, FrameSize(entity)));

        public static int CourtesyBytes(this Tape entity)
            => CourtesyBytes(CourtesyFrames(entity), FrameSize(entity));

        public static Tape CourtesyBytes(this Tape entity, int value) 
            => CourtesyFrames(entity, CourtesyFrames(value, FrameSize(entity)));

        public static int CourtesyBytes(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return CourtesyBytes(entity.Tape);
        }

        public static TapeConfig CourtesyBytes(this TapeConfig entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            CourtesyBytes(entity.Tape, value);
            return entity;
        }

        public static int CourtesyBytes(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return CourtesyBytes(entity.Tape);
        }

        public static TapeActions CourtesyBytes(this TapeActions entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            CourtesyBytes(entity.Tape, value);
            return entity;
        }

        public static int CourtesyBytes(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return CourtesyBytes(entity.Tape);
        }

        public static TapeAction CourtesyBytes(this TapeAction entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            CourtesyBytes(entity.Tape, value);
            return entity;
        }

        #endregion
        
        // Durations
        
        #region AudioLength
        
        public static double AudioLength(int frameCount, int samplingRate)
            => (double)frameCount / samplingRate;
        
        public static double AudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames = 0)
            => (double)(byteCount - headerLength) / frameSize / samplingRate - courtesyFrames * frameSize;

        public static double AudioLength(this SynthWishes entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioLength.Value;
        }

        public static SynthWishes AudioLength(this SynthWishes entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.WithAudioLength(value);
            return entity;
        }

        public static double AudioLength(this FlowNode entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioLength.Value;
        }

        public static FlowNode AudioLength(this FlowNode entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.WithAudioLength(value);
            return entity;
        }

        public static double AudioLength(this ConfigWishes entity, SynthWishes synthWishes)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioLength(synthWishes).Value;
        }

        public static ConfigWishes AudioLength(this ConfigWishes entity, double value, SynthWishes synthWishes)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.WithAudioLength(value, synthWishes);
            return entity;
        }

        internal static double AudioLength(this ConfigSection entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.AudioLength ?? DefaultAudioLength;
        }

        internal static ConfigSection AudioLength(this ConfigSection entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.AudioLength = value;
            return entity;
        }

        public static double AudioLength(this Buff entity)
        {
            if (entity == null) throw new NullException(() => entity);
            // TODO: From bytes[] / filePath?
            return AudioLength(entity.UnderlyingAudioFileOutput);
        }

        public static Buff AudioLength(this Buff entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.UnderlyingAudioFileOutput == null) throw new NullException(() => entity.UnderlyingAudioFileOutput);
            entity.UnderlyingAudioFileOutput.AudioLength(value);
            return entity;
        }

        public static double AudioLength(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Duration;
        }

        public static Tape AudioLength(this Tape entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Duration = value;
            return entity;
        }

        public static double AudioLength(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Duration;
        }

        public static TapeConfig AudioLength(this TapeConfig entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Duration = value;
            return entity;
        }

        public static double AudioLength(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Duration;
        }

        public static TapeAction AudioLength(this TapeAction entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Duration = value;
            return entity;
        }

        public static double AudioLength(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Tape.Duration;
        }

        public static TapeActions AudioLength(this TapeActions entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Tape.Duration = value;
            return entity;
        }

        public static double AudioLength(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetDuration();
        }

        public static Sample AudioLength(this Sample entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            double originalAudioLength = entity.AudioLength();
            entity.SamplingRate = (int)(entity.SamplingRate * value / originalAudioLength);
            return entity;
        }

        public static double AudioLength(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.Duration;
        }

        public static AudioFileOutput AudioLength(this AudioFileOutput entity, double value)
        {
            if (entity == null) throw new NullException(() => entity);
            entity.Duration = value;
            return entity;
        }

        public static double AudioLength(this WavHeaderStruct entity) 
            => entity.ToWish().AudioLength();

        public static WavHeaderStruct AudioLength(this WavHeaderStruct entity, double value)
        {
            return entity.ToWish().AudioLength(value).ToWavHeader();
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

        #endregion

        #region FrameCount

        public static int FrameCount(int byteCount, int frameSize, int headerLength)
            => (byteCount - headerLength) / frameSize;

        public static int FrameCount(byte[] bytes, string filePath, int frameSize, int headerLength) 
            => (ByteCount(bytes, filePath) - headerLength) / frameSize;

        public static int FrameCount(double audioLength, int samplingRate)
            => (int)(audioLength * samplingRate);

        public static int FrameCount(this SynthWishes entity) 
            => FrameCount(AudioLength(entity), SamplingRate(entity));

        public static SynthWishes FrameCount(this SynthWishes entity, int value) 
            => AudioLength(entity, AudioLength(value, SamplingRate(entity)));
        
        public static int FrameCount(this FlowNode entity) 
            => FrameCount(AudioLength(entity), SamplingRate(entity));

        public static FlowNode FrameCount(this FlowNode entity, int value) 
            => AudioLength(entity, AudioLength(value, SamplingRate(entity)));
        
        public static int FrameCount(this ConfigWishes entity, SynthWishes synthWishes) 
            => FrameCount(AudioLength(entity, synthWishes), SamplingRate(entity));

        public static ConfigWishes FrameCount(this ConfigWishes entity, int value, SynthWishes synthWishes) 
            => AudioLength(entity, AudioLength(value, SamplingRate(entity)), synthWishes);
        
        internal static int FrameCount(this ConfigSection entity) 
            => FrameCount(AudioLength(entity), SamplingRate(entity));

        internal static ConfigSection FrameCount(this ConfigSection entity, int value) 
            => AudioLength(entity, AudioLength(value, SamplingRate(entity)));
        
        public static int FrameCount(this Tape entity)
        {
            if (entity.IsBuff)
            {
                return FrameCount(entity.Bytes, entity.FilePathResolved, FrameSize(entity), HeaderLength(entity));
            }
            else
            {
                return FrameCount(AudioLength(entity), SamplingRate(entity));
            }
        }

        public static Tape FrameCount(this Tape entity, int value) 
            => AudioLength(entity, AudioLength(value, SamplingRate(entity)));
        
        public static int FrameCount(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return FrameCount(entity.Tape);
        }

        public static TapeConfig FrameCount(this TapeConfig entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            FrameCount(entity.Tape, value);
            return entity;
        }

        public static int FrameCount(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return FrameCount(entity.Tape);
        }

        public static TapeAction FrameCount(this TapeAction entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            FrameCount(entity.Tape, value);
            return entity;
        }

        public static int FrameCount(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return FrameCount(entity.Tape);
        }

        public static TapeActions FrameCount(this TapeActions entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            FrameCount(entity.Tape, value);
            return entity;
        }

        public static int FrameCount(this Buff entity)
        {
            if (entity == null) throw new NullException(() => entity);

            int frameCount = FrameCount(entity.Bytes, entity.FilePath, FrameSize(entity), HeaderLength(entity));

            if (Has(frameCount))
            {
                return frameCount;
            }

            if (entity.UnderlyingAudioFileOutput != null)
            {
                return FrameCount(entity.UnderlyingAudioFileOutput);
            }

            return 0;
        }

        public static int FrameCount(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return FrameCount(entity.Bytes, entity.Location, FrameSize(entity), HeaderLength(entity));
        }

        public static int FrameCount(this AudioFileOutput entity) 
            => FrameCount(AudioLength(entity), SamplingRate(entity));

        public static AudioFileOutput FrameCount(this AudioFileOutput entity, int value) 
            => AudioLength(entity, AudioLength(value, SamplingRate(entity)));
        
        public static int FrameCount(this WavHeaderStruct entity) 
            => entity.ToWish().FrameCount();

        public static WavHeaderStruct FrameCount(this WavHeaderStruct entity, int value)
        {
            AudioInfoWish infoWish = entity.ToWish();
            AudioLength(infoWish, AudioLength(value, infoWish.SamplingRate));
            return infoWish.ToWavHeader();
        }

        public static int FrameCount(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.FrameCount;
        }

        public static AudioInfoWish FrameCount(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = value;
            return infoWish;
        }

        public static int FrameCount(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SampleCount;
        }

        public static AudioFileInfo FrameCount(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = value;
            return info;
        }

        #endregion
        
        #region ByteCount

        public static int ByteCount(byte[] bytes, string filePath)
        {
            if (Has(bytes))
            {
                return bytes.Length;
            }

            if (Exists(filePath))
            {
                long fileSize = new FileInfo(filePath).Length;
                int maxSize = int.MaxValue;
                if (fileSize > maxSize) throw new Exception($"File is too large. Max size = {PrettyByteCount(maxSize)}");
                return (int)fileSize;
            }

            return 0;
        }

        public static int ByteCount(int frameCount, int frameSize, int headerLength, int courtesyFrames = 0)
            => frameCount * frameSize + headerLength + CourtesyBytes(courtesyFrames, frameSize);

        public static int ByteCount(this SynthWishes entity) 
            => ByteCount(FrameCount(entity), FrameSize(entity), HeaderLength(entity), CourtesyFrames(entity));

        public static SynthWishes ByteCount(this SynthWishes entity, int value) 
            => AudioLength(entity, AudioLength(value, FrameSize(entity), SamplingRate(entity), HeaderLength(entity), CourtesyFrames(entity)));
        
        public static int ByteCount(this FlowNode entity) 
            => ByteCount(FrameCount(entity), FrameSize(entity), HeaderLength(entity), CourtesyFrames(entity));
        
        public static FlowNode ByteCount(this FlowNode entity, int value) 
            => AudioLength(entity, AudioLength(value, FrameSize(entity), SamplingRate(entity), HeaderLength(entity), CourtesyFrames(entity)));

        public static int ByteCount(this ConfigWishes entity, SynthWishes synthWishes) 
            => ByteCount(FrameCount(entity, synthWishes), FrameSize(entity), HeaderLength(entity), CourtesyFrames(entity));
       
        public static ConfigWishes ByteCount(this ConfigWishes entity, int value, SynthWishes synthWishes)
        {
            double audioLength = AudioLength(value, FrameSize(entity), SamplingRate(entity), HeaderLength(entity), CourtesyFrames(entity));
            AudioLength(entity, audioLength, synthWishes);
            return entity;
        }

        internal static int ByteCount(this ConfigSection entity) 
            => ByteCount(FrameCount(entity), FrameSize(entity), HeaderLength(entity), CourtesyFrames(entity));

        internal static ConfigSection ByteCount(this ConfigSection entity, int value) 
            => AudioLength(entity, AudioLength(value, FrameSize(entity), SamplingRate(entity), HeaderLength(entity), CourtesyFrames(entity)));

        public static int ByteCount(this Tape entity)
        {
            if (entity == null) throw new NullException(() => entity);
            
            if (entity.IsBuff)
            {
                return ByteCount(entity.Bytes, entity.FilePathResolved);
            }
            else
            {
                return ByteCount(FrameCount(entity), FrameSize(entity), HeaderLength(entity), entity.Config.CourtesyFrames);
            }
        }

        public static Tape ByteCount(this Tape entity, int value) 
            => AudioLength(entity, AudioLength(value, FrameSize(entity), SamplingRate(entity), HeaderLength(entity), CourtesyFrames(entity)));
        
        public static int ByteCount(this TapeConfig entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return ByteCount(entity.Tape);
        }

        public static TapeConfig ByteCount(this TapeConfig entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            ByteCount(entity.Tape, value);
            return entity;
        }
        
        public static int ByteCount(this TapeActions entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return ByteCount(entity.Tape);
        }

        public static TapeActions ByteCount(this TapeActions entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            ByteCount(entity.Tape, value);
            return entity;
        }

        public static int ByteCount(this TapeAction entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return ByteCount(entity.Tape);
        }

        public static TapeAction ByteCount(this TapeAction entity, int value)
        {
            if (entity == null) throw new NullException(() => entity);
            ByteCount(entity.Tape, value);
            return entity;
        }

        public static int ByteCount(this Buff entity, int courtesyFrames = 0)
        {
            if (entity == null) throw new NullException(() => entity);

            int byteCount = ByteCount(entity.Bytes, entity.FilePath);

            if (Has(byteCount))
            {
                return byteCount;
            }

            if (entity.UnderlyingAudioFileOutput != null)
            {
                return BytesNeeded(entity.UnderlyingAudioFileOutput, courtesyFrames);
            }

            return 0;
        }

        public static int ByteCount(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return ByteCount(entity.Bytes, entity.Location);
        }

        public static int ByteCount(this AudioFileOutput entity) 
            => ByteCount(FrameCount(entity), FrameSize(entity), HeaderLength(entity));

        public static int BytesNeeded(this AudioFileOutput entity, int courtesyFrames = 0) 
            => ByteCount(FrameCount(entity), FrameSize(entity), HeaderLength(entity), courtesyFrames);

        public static AudioFileOutput ByteCount(this AudioFileOutput entity, int value, int courtesyFrames = 0) 
            => AudioLength(entity, AudioLength(value, FrameSize(entity), SamplingRate(entity), HeaderLength(entity), courtesyFrames));

        public static int ByteCount(this WavHeaderStruct entity) 
            => ByteCount(FrameCount(entity), FrameSize(entity), HeaderLength(entity));

        public static WavHeaderStruct ByteCount(this WavHeaderStruct entity, int value, int courtesyFrames = 0)
        {
            var wish = entity.ToWish();
            double audioLength = AudioLength(value, FrameSize(wish), SamplingRate(wish), HeaderLength(Wav), courtesyFrames);
            return wish.AudioLength(audioLength).ToWavHeader();
        }
 
        #endregion
    }
}

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
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.Environment;
using static System.IO.File;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes;
using static JJ.Business.Synthesizer.Helpers.SampleDataTypeHelper;

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
        
        public static int Bits(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }

        public static SynthWishes Bits(this SynthWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        public static int Bits(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }

        public static FlowNode Bits(this FlowNode obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        public static int Bits(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }

        public static ConfigWishes Bits(this ConfigWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        internal static int Bits(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits ?? DefaultBits;
        }
        
        public static int Bits(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Bits;
        }
        
        public static Tape Bits(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Bits = value;
            return obj;
        }

        public static int Bits(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }

        public static TapeConfig Bits(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = value;
            return obj;
        }

        public static int Bits(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }

        public static TapeActions Bits(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }

        public static int Bits(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }

        public static TapeAction Bits(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }

        public static int Bits(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return Bits(obj.UnderlyingAudioFileOutput);
        }

        public static Buff Bits(this Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            Bits(obj.UnderlyingAudioFileOutput, value, context);
            return obj;
        }

        public static int Bits(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return EnumToBits(obj.GetSampleDataTypeEnum());
        }
        
        public static Sample Bits(this Sample obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }

        public static int Bits(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return EnumToBits(obj.GetSampleDataTypeEnum());
        }

        public static AudioFileOutput Bits(this AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }

        public static int Bits(this WavHeaderStruct obj)
            => obj.BitsPerValue;

        public static WavHeaderStruct Bits(this WavHeaderStruct obj, int value) 
            => obj.ToWish().Bits(value).ToWavHeader();

        public static int Bits(this AudioInfoWish obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }

        public static AudioInfoWish Bits(this AudioInfoWish obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = value;
            return obj;
        }
        
        public static int Bits(this AudioFileInfo obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.BytesPerValue * 8;
        }

        public static AudioFileInfo Bits(this AudioFileInfo obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.BytesPerValue = value / 8;
            return obj;
        }
        
        public static int Bits(this Type valueType) => TypeToBits(valueType);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static Type Bits(this Type valueType, int value) => BitsToType(value);
        
        public static int Bits<TValueType>() => TypeToBits<TValueType>();
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedTypeParameter
        public static Type Bits<TValueType>(int value) => BitsToType(value);

        [Obsolete(ObsoleteMessage)] public static int Bits(this SampleDataTypeEnum obj) => EnumToBits(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Bits(this SampleDataTypeEnum obj, int value) => BitsToEnum(value);
        
        [Obsolete(ObsoleteMessage)] public static int Bits(this SampleDataType obj) => EntityToBits(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataType Bits(this SampleDataType obj, int value, IContext context) => BitsToEntity(value, context);

        // Bits Conversion-Style
        
        public static int TypeToBits(this Type obj) 
        {
            if (obj == typeof(byte)) return 8;
            if (obj == typeof(Int16)) return 16;
            if (obj == typeof(float)) return 32;
            throw new ValueNotSupportedException(obj);
        }
        
        public static int TypeToBits<T>() => TypeToBits(typeof(T));
        
        public static Type BitsToType(this int value) 
        {
            switch (AssertBits(value))
            {
                case 8 : return typeof(byte);
                case 16: return typeof(Int16);
                case 32: return typeof(float);
                default: return default; // ncrunch: no coverage
            }
        }
                
        [Obsolete(ObsoleteMessage)] public static int EnumToBits(this SampleDataTypeEnum obj)
        {
            switch (obj)
            {
                case SampleDataTypeEnum.Byte: return 8;
                case SampleDataTypeEnum.Int16: return 16;
                case SampleDataTypeEnum.Float32: return 32;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum BitsToEnum(this int bits)
        {
            switch (AssertBits(bits))
            {
                case 32: return SampleDataTypeEnum.Float32;
                case 16: return SampleDataTypeEnum.Int16;
                case 8: return SampleDataTypeEnum.Byte;
                default: return default; // ncrunch: no coverage
            }
        }
        
        [Obsolete(ObsoleteMessage)] public static int EntityToBits(this SampleDataType obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.ToEnum().EnumToBits();
        }

        [Obsolete(ObsoleteMessage)] public static SampleDataType BitsToEntity(this int bits, IContext context) 
            => bits.BitsToEnum().ToEntity(context);
        
        // Bits Shorthand
        
        public   static bool Is8Bit (this SynthWishes        obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this FlowNode           obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this ConfigWishes       obj) => Bits(obj)      == 8;
        internal static bool Is8Bit (this ConfigSection      obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this Tape               obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this TapeConfig         obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this TapeActions        obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this TapeAction         obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this Buff               obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this Sample             obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this AudioFileOutput    obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this WavHeaderStruct    obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this AudioInfoWish      obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this AudioFileInfo      obj) => Bits(obj)      == 8;
        [Obsolete(ObsoleteMessage)]                                            
        public   static bool Is8Bit (this SampleDataTypeEnum obj) => Bits(obj)      == 8;
        [Obsolete(ObsoleteMessage)]                                            
        public   static bool Is8Bit (this SampleDataType     obj) => Bits(obj)      == 8;
        public   static bool Is8Bit (this Type               obj) => Bits(obj)      == 8;
        public   static bool Is8Bit <TValue>                   () => Bits<TValue>() == 8;
        
        public   static bool Is16Bit(this SynthWishes        obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this FlowNode           obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this ConfigWishes       obj) => Bits(obj)      == 16;
        internal static bool Is16Bit(this ConfigSection      obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this Tape               obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this TapeConfig         obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this TapeActions        obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this TapeAction         obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this Buff               obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this Sample             obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this AudioFileOutput    obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this WavHeaderStruct    obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this AudioInfoWish      obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this AudioFileInfo      obj) => Bits(obj)      == 16;
        [Obsolete(ObsoleteMessage)]                                          
        public   static bool Is16Bit(this SampleDataTypeEnum obj) => Bits(obj)      == 16;
        [Obsolete(ObsoleteMessage)]                                          
        public   static bool Is16Bit(this SampleDataType     obj) => Bits(obj)      == 16;
        public   static bool Is16Bit(this Type               obj) => Bits(obj)      == 16;
        public   static bool Is16Bit<TValue>                   () => Bits<TValue>() == 16;

        public   static bool Is32Bit(this SynthWishes        obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this FlowNode           obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this ConfigWishes       obj) => Bits(obj)      == 32;
        internal static bool Is32Bit(this ConfigSection      obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this Tape               obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this TapeConfig         obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this TapeActions        obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this TapeAction         obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this Buff               obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this Sample             obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this AudioFileOutput    obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this WavHeaderStruct    obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this AudioInfoWish      obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this AudioFileInfo      obj) => Bits(obj)      == 32;
        [Obsolete(ObsoleteMessage)]                                           
        public   static bool Is32Bit(this SampleDataTypeEnum obj) => Bits(obj)      == 32;
        [Obsolete(ObsoleteMessage)]                                           
        public   static bool Is32Bit(this SampleDataType     obj) => Bits(obj)      == 32;
        public   static bool Is32Bit(this Type               obj) => Bits(obj)      == 32;
        public   static bool Is32Bit<TValue>                   () => Bits<TValue>() == 32;

        public   static Tape               With8Bit (this Tape               obj)                   => Bits(obj,    8);
        public   static TapeConfig         With8Bit (this TapeConfig         obj)                   => Bits(obj,    8);
        public   static TapeActions        With8Bit (this TapeActions        obj)                   => Bits(obj,    8);
        public   static TapeAction         With8Bit (this TapeAction         obj)                   => Bits(obj,    8);
        public   static Buff               With8Bit (this Buff               obj, IContext context) => Bits(obj,    8, context);
        public   static Sample             With8Bit (this Sample             obj, IContext context) => Bits(obj,    8, context);
        public   static AudioFileOutput    With8Bit (this AudioFileOutput    obj, IContext context) => Bits(obj,    8, context);
        public   static WavHeaderStruct    With8Bit (this WavHeaderStruct    obj)                   => Bits(obj,    8);
        public   static AudioInfoWish      With8Bit (this AudioInfoWish      obj)                   => Bits(obj,    8);
        public   static AudioFileInfo      With8Bit (this AudioFileInfo      obj)                   => Bits(obj,    8);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit(this Type obj) => Bits(obj, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit<TValue>() => Bits<TValue>(8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With8Bit(this SampleDataTypeEnum obj) => Bits(obj, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType With8Bit(this SampleDataType obj, IContext context) => Bits(obj, 8, context);

        public   static Tape               With16Bit(this Tape               obj)                   => Bits(obj,    16);
        public   static TapeConfig         With16Bit(this TapeConfig         obj)                   => Bits(obj,    16);
        public   static TapeActions        With16Bit(this TapeActions        obj)                   => Bits(obj,    16);
        public   static TapeAction         With16Bit(this TapeAction         obj)                   => Bits(obj,    16);
        public   static Buff               With16Bit(this Buff               obj, IContext context) => Bits(obj,    16, context);
        public   static Sample             With16Bit(this Sample             obj, IContext context) => Bits(obj,    16, context);
        public   static AudioFileOutput    With16Bit(this AudioFileOutput    obj, IContext context) => Bits(obj,    16, context);
        public   static WavHeaderStruct    With16Bit(this WavHeaderStruct    obj)                   => Bits(obj,    16);
        public   static AudioInfoWish      With16Bit(this AudioInfoWish      obj)                   => Bits(obj,    16);
        public   static AudioFileInfo      With16Bit(this AudioFileInfo      obj)                   => Bits(obj,    16);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit(this Type obj) => Bits(obj, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit<TValue>() => Bits<TValue>(16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With16Bit(this SampleDataTypeEnum obj) => Bits(obj, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType With16Bit(this SampleDataType obj, IContext context) => Bits(obj, 16, context);

        public   static Tape               With32Bit(this Tape               obj)                   => Bits(obj,    32);
        public   static TapeConfig         With32Bit(this TapeConfig         obj)                   => Bits(obj,    32);
        public   static TapeActions        With32Bit(this TapeActions        obj)                   => Bits(obj,    32);
        public   static TapeAction         With32Bit(this TapeAction         obj)                   => Bits(obj,    32);
        public   static Buff               With32Bit(this Buff               obj, IContext context) => Bits(obj,    32, context);
        public   static Sample             With32Bit(this Sample             obj, IContext context) => Bits(obj,    32, context);
        public   static AudioFileOutput    With32Bit(this AudioFileOutput    obj, IContext context) => Bits(obj,    32, context);
        public   static WavHeaderStruct    With32Bit(this WavHeaderStruct    obj)                   => Bits(obj,    32);
        public   static AudioInfoWish      With32Bit(this AudioInfoWish      obj)                   => Bits(obj,    32);
        public   static AudioFileInfo      With32Bit(this AudioFileInfo      obj)                   => Bits(obj,    32);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit(this Type obj) => Bits(obj, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit<TValue>() => Bits<TValue>(32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With32Bit(this SampleDataTypeEnum obj) => Bits(obj, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType With32Bit(this SampleDataType obj, IContext context) => Bits(obj, 32, context);

        #endregion
                
        #region Channels
        
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
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(value, context);
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
        public static int Channels(this SpeakerSetupEnum obj) => EnumToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Channels(this SpeakerSetupEnum obj, int value) => ChannelsToEnum(value);
        
        [Obsolete(ObsoleteMessage)] public static int Channels(this ChannelEnum obj) => EnumToChannels(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Channels(this ChannelEnum obj, int value) => ChannelsToEnum(value, Channel(obj));

        [Obsolete(ObsoleteMessage)] public static int Channels(this SpeakerSetup obj) => EntityToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Channels(this SpeakerSetup obj, int value, IContext context) => ChannelsToEntity(value, context);

        [Obsolete(ObsoleteMessage)] public static int Channels(this Channel obj) => EntityToChannels(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Channels(this Channel obj, int channels, IContext context) => ChannelsToEntity(channels, Channel(obj), context);
        
        // Channels Conversion-Style

        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum ChannelsToEnum(this int channels)
        {
            switch (channels)
            {
                case 0: return SpeakerSetupEnum.Undefined;
                case 1: return SpeakerSetupEnum.Mono;
                case 2: return SpeakerSetupEnum.Stereo;
                default: throw new Exception($"{new { channels }} not supported.");
            }
        }

        [Obsolete(ObsoleteMessage)] public static int EnumToChannels(this SpeakerSetupEnum enumValue)
        {
            switch (enumValue)
            {
                case SpeakerSetupEnum.Mono: return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                case SpeakerSetupEnum.Undefined: return 0;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }

        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelsToEnum(this int channels, int? channel)
        {
            if (!Has(channel)) return ChannelEnum.Undefined;
            if (channels == 1 && channel == 0) return ChannelEnum.Single;
            if (channels == 2 && channel == 0) return ChannelEnum.Left;
            if (channels == 2 && channel == 1) return ChannelEnum.Right;
            throw new Exception($"Unsupported combination of values {new { channels, channel }}");
        }

        [Obsolete(ObsoleteMessage)] public static int EnumToChannels(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Single: return 1;
                case ChannelEnum.Left: return 2;
                case ChannelEnum.Right: return 2;
                default: throw new ValueNotSupportedException(channelEnum);
            }
        }
        
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup ChannelsToEntity(this int channels, IContext context) 
            => ChannelsToEnum(channels).ToEntity(context);
        
        [Obsolete(ObsoleteMessage)] public static int EntityToChannels(this SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return EnumToChannels(entity.ToEnum());
        }

        [Obsolete(ObsoleteMessage)] public static Channel ChannelsToEntity(this int channels, int? channel, IContext context) 
            => ChannelsToEnum(channels, channel).ToEntity(context);
        
        [Obsolete(ObsoleteMessage)] public static int EntityToChannels(this Channel entity)
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
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetupEnum obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetup     obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this ChannelEnum      obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this Channel          obj) => Channels(obj) == 1;
        
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
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetupEnum obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetup     obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this ChannelEnum      obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this Channel          obj) => Channels(obj) == 2;

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
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Mono(this SpeakerSetupEnum obj) => Channels(obj, 1);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Mono(this SpeakerSetup obj, IContext context) => Channels(obj, 1, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Mono(this ChannelEnum obj) => Channels(obj, 1);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Mono(this Channel obj, IContext context) => Channels(obj, 1, context);

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
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Stereo(this SpeakerSetupEnum obj) => Channels(obj, 2);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Stereo(this SpeakerSetup obj, IContext context) => Channels(obj, 2, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Stereo(this ChannelEnum obj) => Channels(obj, 2);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Stereo(this Channel obj, IContext context) => Channels(obj, 2, context);

        #endregion

        #region Channel
        
        public static int? Channel(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }

        public static SynthWishes Channel(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }

        public static int? Channel(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }

        public static FlowNode Channel(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }

        public static int? Channel(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }

        public static ConfigWishes Channel(this ConfigWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        public static int? Channel(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Channel;
        }
        
        public static Tape Channel(this Tape obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Channel = value;
            return obj;
        }

        public static int? Channel(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channel;
        }

        public static TapeConfig Channel(this TapeConfig obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channel = value;
            return obj;
        }

        public static int? Channel(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }

        public static TapeActions Channel(this TapeActions obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }

        public static int? Channel(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }

        public static TapeAction Channel(this TapeAction obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }

        public static int? Channel(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return default;
            return Channel(obj.UnderlyingAudioFileOutput);
        }

        public static Buff Channel(this Buff obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);

            if (obj.UnderlyingAudioFileOutput == null && value == null) 
            {
                // Both null is ok.
                return obj;
            }

            // Otherwise, let this method throw error upon null UnderlyingAudioFileOutput.
            Channel(obj.UnderlyingAudioFileOutput, value);
            
            return obj;
        }
        
        public static int? Channel(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.AudioFileOutputChannels == null) throw new NullException(() => obj.AudioFileOutputChannels);

            int channels = obj.Channels();
            int signalCount = obj.AudioFileOutputChannels.Count;
            int? firstChannelNumber = obj.AudioFileOutputChannels.ElementAtOrDefault(0)?.Channel();
            
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
                $"obj.Channels = {channels}, " + NewLine +
                $"obj.AudioFileOutputChannels.Count = {signalCount} ({nameof(signalCount)})" + NewLine +
                $"obj.AudioFileOutputChannels[0].Index = {firstChannelNumber} ({nameof(firstChannelNumber)})");
        }

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Channel(this AudioFileOutput obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.AudioFileOutputChannels == null) throw new NullException(() => obj.AudioFileOutputChannels);
            if (obj.AudioFileOutputChannels.Contains(null)) throw new Exception("obj.AudioFileOutputChannels contains nulls.");

            if (value.HasValue)
            {
                if (obj.AudioFileOutputChannels.Count != 1)
                {
                    throw new Exception("Can only set Channel property for AudioFileOutputs with only 1 channel.");
                }

                obj.AudioFileOutputChannels[0].Index = value.Value;
            }
            else
            {
                for (int i = 0; i < obj.AudioFileOutputChannels.Count; i++)
                {
                    obj.AudioFileOutputChannels[i].Index = i;
                }
            }
            
            return obj;
        }

        public static int Channel(this AudioFileOutputChannel obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return obj.Index;
        }
        
        public static AudioFileOutputChannel Channel(this AudioFileOutputChannel obj, int value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            obj.Index = value;
            return obj;
        }
        
        [Obsolete(ObsoleteMessage)] public static int? Channel(this ChannelEnum obj) => EnumToChannel(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Channel(this ChannelEnum obj, int? channel) => ChannelToEnum(channel, Channels(obj));
        [Obsolete(ObsoleteMessage)] public static int? Channel(this Channel obj) => obj?.Index;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Channel(this Channel obj, int? channel, IContext context) => ChannelToEntity(channel, Channels(obj), context);
        
        // Channel, Conversion-Style

        [Obsolete(ObsoleteMessage)] public static int? EnumToChannel(this ChannelEnum obj)
        {
            switch (obj)
            {
                case ChannelEnum.Single: return 0;
                case ChannelEnum.Left: return 0;
                case ChannelEnum.Right: return 1;
                case ChannelEnum.Undefined: return null;
                default: throw new ValueNotSupportedException(obj);
            }
        }

        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelToEnum(this int? channel, int channels)
            => ChannelToEnum(channel, channels.ChannelsToEnum());

        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelToEnum(this int? channel, SpeakerSetupEnum speakerSetupEnum)
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

        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(this int? channel, int channels, IContext context)
            => ChannelToEnum(channel, channels).ToEntity(context);

        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(this int? channel, SpeakerSetupEnum speakerSetupEnum, IContext context)
            => ChannelToEnum(channel, speakerSetupEnum).ToEntity(context);
        
        [Obsolete(ObsoleteMessage)] public static int? EntityToChannel(this Channel entity)
        {
            if (entity == null) return null;
            return EnumToChannel(entity.ToEnum());
        }

        // Channel Shorthand
        
        public static bool IsCenter (this SynthWishes     obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this FlowNode        obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this ConfigWishes    obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this Tape            obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this TapeConfig      obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this TapeActions     obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this TapeAction      obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this Buff            obj) => IsMono  (obj) && Channel(obj) == 0;
        public static bool IsCenter (this AudioFileOutput obj) => IsMono  (obj) && Channel(obj) == 0;
        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this ChannelEnum obj) => IsMono(obj) && Channel(obj) == 0;
        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this Channel     obj) => IsMono(obj) && Channel(obj) == 0;
        
        public static bool IsLeft   (this SynthWishes     obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this FlowNode        obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this ConfigWishes    obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this Tape            obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this TapeConfig      obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this TapeActions     obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this TapeAction      obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this Buff            obj) => IsStereo(obj) && Channel(obj) == 0;
        public static bool IsLeft   (this AudioFileOutput obj) => IsStereo(obj) && Channel(obj) == 0;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this ChannelEnum obj) => IsStereo(obj) && Channel(obj) == 0;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this Channel     obj) => IsStereo(obj) && Channel(obj) == 0;
        
        public static bool IsRight (this SynthWishes     obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this FlowNode        obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this ConfigWishes    obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this Tape            obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this TapeConfig      obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this TapeActions     obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this TapeAction      obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this Buff            obj) => IsStereo(obj) && Channel(obj) == 1;
        public static bool IsRight (this AudioFileOutput obj) => IsStereo(obj) && Channel(obj) == 1;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this ChannelEnum obj) => IsStereo(obj) && Channel(obj) == 1;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this Channel     obj) => IsStereo(obj) && Channel(obj) == 1;
        
        public static SynthWishes     Center (this SynthWishes     obj                  ) => Mono(obj         ).Channel(0);
        public static FlowNode        Center (this FlowNode        obj                  ) => Mono(obj         ).Channel(0);
        public static ConfigWishes    Center (this ConfigWishes    obj                  ) => Mono(obj         ).Channel(0);
        public static Tape            Center (this Tape            obj                  ) => Mono(obj         ).Channel(0);
        public static TapeConfig      Center (this TapeConfig      obj                  ) => Mono(obj         ).Channel(0);
        public static TapeActions     Center (this TapeActions     obj                  ) => Mono(obj         ).Channel(0);
        public static TapeAction      Center (this TapeAction      obj                  ) => Mono(obj         ).Channel(0);
        public static Buff            Center (this Buff            obj, IContext context) => Mono(obj, context).Channel(0);
        public static AudioFileOutput Center (this AudioFileOutput obj, IContext context) => Mono(obj, context).Channel(0);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Center(this ChannelEnum obj) => Mono(obj).Channel(0);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Center(this Channel obj, IContext context) => Mono(obj, context).Channel(0, context);
        
        public static SynthWishes     Left (this SynthWishes     obj                  ) => Stereo(obj         ).Channel(0);
        public static FlowNode        Left (this FlowNode        obj                  ) => Stereo(obj         ).Channel(0);
        public static ConfigWishes    Left (this ConfigWishes    obj                  ) => Stereo(obj         ).Channel(0);
        public static Tape            Left (this Tape            obj                  ) => Stereo(obj         ).Channel(0);
        public static TapeConfig      Left (this TapeConfig      obj                  ) => Stereo(obj         ).Channel(0);
        public static TapeActions     Left (this TapeActions     obj                  ) => Stereo(obj         ).Channel(0);
        public static TapeAction      Left (this TapeAction      obj                  ) => Stereo(obj         ).Channel(0);
        public static Buff            Left (this Buff            obj, IContext context) => Stereo(obj, context).Channel(0);
        public static AudioFileOutput Left (this AudioFileOutput obj, IContext context) => Stereo(obj, context).Channel(0);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Left(this ChannelEnum obj) => Stereo(obj).Channel(0);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Left(this Channel obj, IContext context) => Stereo(obj, context).Channel(0, context);
        
        public static SynthWishes     Right (this SynthWishes     obj                  ) => Stereo(obj         ).Channel(1);
        public static FlowNode        Right (this FlowNode        obj                  ) => Stereo(obj         ).Channel(1);
        public static ConfigWishes    Right (this ConfigWishes    obj                  ) => Stereo(obj         ).Channel(1);
        public static Tape            Right (this Tape            obj                  ) => Stereo(obj         ).Channel(1);
        public static TapeConfig      Right (this TapeConfig      obj                  ) => Stereo(obj         ).Channel(1);
        public static TapeActions     Right (this TapeActions     obj                  ) => Stereo(obj         ).Channel(1);
        public static TapeAction      Right (this TapeAction      obj                  ) => Stereo(obj         ).Channel(1);
        public static Buff            Right (this Buff            obj, IContext context) => Stereo(obj, context).Channel(1);
        public static AudioFileOutput Right (this AudioFileOutput obj, IContext context) => Stereo(obj, context).Channel(1);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Right(this ChannelEnum obj) => Stereo(obj).Channel(1);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Right(this Channel obj, IContext context) => Stereo(obj, context).Channel(1, context);
        
        #endregion

        #region SamplingRate

        public static int SamplingRate(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }

        public static SynthWishes SamplingRate(this SynthWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        public static int SamplingRate(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }

        public static FlowNode SamplingRate(this FlowNode obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        public static int SamplingRate(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }

        public static ConfigWishes SamplingRate(this ConfigWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        internal static int SamplingRate(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate ?? DefaultSamplingRate;
        }
        
        public static int SamplingRate(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.SamplingRate;
        }
        
        public static Tape SamplingRate(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }

        public static TapeConfig SamplingRate(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }

        public static TapeActions SamplingRate(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }

        public static TapeAction SamplingRate(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return SamplingRate(obj.UnderlyingAudioFileOutput);
        }

        public static Buff SamplingRate(this Buff obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            SamplingRate(obj.UnderlyingAudioFileOutput, value);
            return obj;
        }

        public static int SamplingRate(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static Sample SamplingRate(this Sample obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }

        public static AudioFileOutput SamplingRate(this AudioFileOutput obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(this WavHeaderStruct obj)
            => obj.SamplingRate;

        public static WavHeaderStruct SamplingRate(this WavHeaderStruct obj, int value) 
            => obj.ToWish().SamplingRate(value).ToWavHeader();

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
        
        public static AudioFileFormatEnum AudioFormat(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static SynthWishes AudioFormat(this SynthWishes obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        public static AudioFileFormatEnum AudioFormat(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static FlowNode AudioFormat(this FlowNode obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        public static AudioFileFormatEnum AudioFormat(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static ConfigWishes AudioFormat(this ConfigWishes obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        internal static AudioFileFormatEnum AudioFormat(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat ?? DefaultAudioFormat;
        }

        public static AudioFileFormatEnum AudioFormat(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.AudioFormat;
        }

        public static Tape AudioFormat(this Tape obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat;
        }

        public static TapeConfig AudioFormat(this TapeConfig obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }

        public static TapeAction AudioFormat(this TapeAction obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }

        public static TapeActions AudioFormat(this TapeActions obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return AudioFormat(obj.UnderlyingAudioFileOutput);
        }

        public static Buff AudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            AudioFormat(obj.UnderlyingAudioFileOutput, value, context);
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFileFormatEnum();
        }

        public static Sample AudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFileFormatEnum();
        }

        public static AudioFileOutput AudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }
        
        // ReSharper disable once UnusedParameter.Global
        public static AudioFileFormatEnum AudioFormat(this WavHeaderStruct obj) => Wav;

        public static AudioFileFormatEnum AudioFormat(this string fileExtension) => ExtensionToAudioFormat(fileExtension);

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static string AudioFormat(this string fileExtension, AudioFileFormatEnum audioFormat) 
            => FileExtension(audioFormat);

        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum obj) => obj;

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum obj, AudioFileFormatEnum value)
            => value;

        [Obsolete(ObsoleteMessage)] public static AudioFileFormatEnum AudioFormat(this AudioFileFormat obj) => ToEnum(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat AudioFormat(this AudioFileFormat obj, AudioFileFormatEnum value, IContext context) 
            => ToEntity(value, context);
        
        // Conversion-Style AudioFormat
        
        [Obsolete(ObsoleteMessage)] public static AudioFileFormatEnum ToEnum(this AudioFileFormat enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (AudioFileFormatEnum)enumEntity.ID;
        }
        
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat ToEntity(this AudioFileFormatEnum audioFormat, IContext context) 
            => CreateRepository<IAudioFileFormatRepository>(context).Get(audioFormat.ToID());

        public static AudioFileFormatEnum ExtensionToAudioFormat(this string fileExtension)
        {
            if (Is(fileExtension, ".wav")) return Wav;
            if (Is(fileExtension, ".raw")) return Raw;
            throw new Exception($"{new{fileExtension}} not supported.");
        }

        public static string AudioFormatToExtension(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case Wav: return ".wav";
                case Raw: return ".raw";
                default: throw new ValueNotSupportedException(obj);
            }
        }

        // AudioFormat Shorthand
        
        public   static bool IsWav(this SynthWishes         obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this FlowNode            obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this ConfigWishes        obj) => AudioFormat(obj) == Wav;
        internal static bool IsWav(this ConfigSection       obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this TapeConfig          obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this TapeAction          obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this TapeActions         obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this Buff                obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this Sample              obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this AudioFileOutput     obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this WavHeaderStruct     obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this string    fileExtension) => AudioFormat(fileExtension) == Wav;
        public   static bool IsWav(this AudioFileFormatEnum obj) => AudioFormat(obj) == Wav;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsWav(this AudioFileFormat     obj) => AudioFormat(obj) == Wav;
        
        public   static bool IsRaw(this SynthWishes         obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this FlowNode            obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this ConfigWishes        obj) => AudioFormat(obj) == Raw;
        internal static bool IsRaw(this ConfigSection       obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this TapeConfig          obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this TapeAction          obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this TapeActions         obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this Buff                obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this Sample              obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this AudioFileOutput     obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this WavHeaderStruct     obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this string    fileExtension) => AudioFormat(fileExtension) == Raw;
        public   static bool IsRaw(this AudioFileFormatEnum obj) => AudioFormat(obj) == Raw;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsRaw(this AudioFileFormat  obj) => AudioFormat(obj) == Raw;
        
        public   static SynthWishes         AsWav(this SynthWishes         obj) => AudioFormat(obj, Wav);
        public   static FlowNode            AsWav(this FlowNode            obj) => AudioFormat(obj, Wav);
        public   static ConfigWishes        AsWav(this ConfigWishes        obj) => AudioFormat(obj, Wav);
        public   static Buff                AsWav(this Buff                obj, IContext context) => AudioFormat(obj, Wav, context);
        public   static Tape                AsWav(this Tape                obj) => AudioFormat(obj, Wav);
        public   static TapeConfig          AsWav(this TapeConfig          obj) => AudioFormat(obj, Wav);
        public   static TapeAction          AsWav(this TapeAction          obj) => AudioFormat(obj, Wav);
        public   static TapeActions         AsWav(this TapeActions         obj) => AudioFormat(obj, Wav);
        public   static Sample              AsWav(this Sample              obj, IContext context) => AudioFormat(obj, Wav, context);
        public   static AudioFileOutput     AsWav(this AudioFileOutput     obj, IContext context) => AudioFormat(obj, Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string              AsWav(this string    fileExtension) => AudioFormat(fileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsWav(this AudioFileFormatEnum obj) => AudioFormat(obj, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static AudioFileFormat     AsWav(this AudioFileFormat     obj, IContext context) => AudioFormat(obj, Wav, context);
        
        public   static SynthWishes         AsRaw(this SynthWishes         obj) => AudioFormat(obj, Raw);
        public   static FlowNode            AsRaw(this FlowNode            obj) => AudioFormat(obj, Raw);
        public   static ConfigWishes        AsRaw(this ConfigWishes        obj) => AudioFormat(obj, Raw);
        public   static Buff                AsRaw(this Buff                obj, IContext context) => AudioFormat(obj, Raw, context);
        public   static Tape                AsRaw(this Tape                obj) => AudioFormat(obj, Raw);
        public   static TapeConfig          AsRaw(this TapeConfig          obj) => AudioFormat(obj, Raw);
        public   static TapeAction          AsRaw(this TapeAction          obj) => AudioFormat(obj, Raw);
        public   static TapeActions         AsRaw(this TapeActions         obj) => AudioFormat(obj, Raw);
        public   static Sample              AsRaw(this Sample              obj, IContext context) => AudioFormat(obj, Raw, context);
        public   static AudioFileOutput     AsRaw(this AudioFileOutput     obj, IContext context) => AudioFormat(obj, Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string              AsRaw(this string    fileExtension) => AudioFormat(fileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsRaw(this AudioFileFormatEnum obj) => AudioFormat(obj, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static AudioFileFormat     AsRaw(this AudioFileFormat     obj, IContext context) => AudioFormat(obj, Raw, context);

        #endregion

        #region Interpolation

        public static InterpolationTypeEnum Interpolation(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }

        public static SynthWishes Interpolation(this SynthWishes obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        public static InterpolationTypeEnum Interpolation(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }

        public static FlowNode Interpolation(this FlowNode obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        public static InterpolationTypeEnum Interpolation(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }

        public static ConfigWishes Interpolation(this ConfigWishes obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        internal static InterpolationTypeEnum Interpolation(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation ?? DefaultInterpolation;
        }

        public static InterpolationTypeEnum Interpolation(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Interpolation;
        }

        public static Tape Interpolation(this Tape obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }

        public static TapeConfig Interpolation(this TapeConfig obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }

        public static TapeAction Interpolation(this TapeAction obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }

        public static TapeActions Interpolation(this TapeActions obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolationTypeEnum();
        }

        public static Sample Interpolation(this Sample obj, InterpolationTypeEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetInterpolationTypeEnum(value, context);
            return obj;
        }
    
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj) => obj;

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj, InterpolationTypeEnum value) => value;

        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum Interpolation(this InterpolationType obj) => ToEnum(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static InterpolationType Interpolation(this InterpolationType obj, InterpolationTypeEnum value, IContext context) => ToEntity(value, context);

        // Interpolation, Conversion-Style
        
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum ToEnum(this InterpolationType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (InterpolationTypeEnum)enumEntity.ID;
        }

        [Obsolete(ObsoleteMessage)] public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        // Interpolation Shorthand
        
        public   static bool IsLinear(this SynthWishes           obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this FlowNode              obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this ConfigWishes          obj) => Interpolation(obj) == Line;
        internal static bool IsLinear(this ConfigSection         obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this TapeConfig            obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this TapeAction            obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this TapeActions           obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this Sample                obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this InterpolationTypeEnum obj) => Interpolation(obj) == Line;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsLinear(this InterpolationType     obj) => Interpolation(obj) == Line;
        
        public   static bool IsBlocky(this SynthWishes           obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this FlowNode              obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this ConfigWishes          obj) => Interpolation(obj) == Block;
        internal static bool IsBlocky(this ConfigSection         obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this TapeConfig            obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this TapeAction            obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this TapeActions           obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this Sample                obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this InterpolationTypeEnum obj) => Interpolation(obj) == Block;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsBlocky(this InterpolationType  obj) => Interpolation(obj) == Block;
        
        public   static SynthWishes           Linear(this SynthWishes         obj) => Interpolation(obj, Line);
        public   static FlowNode              Linear(this FlowNode            obj) => Interpolation(obj, Line);
        public   static ConfigWishes          Linear(this ConfigWishes        obj) => Interpolation(obj, Line);
        public   static Tape                  Linear(this Tape                obj) => Interpolation(obj, Line);
        public   static TapeConfig            Linear(this TapeConfig          obj) => Interpolation(obj, Line);
        public   static TapeAction            Linear(this TapeAction          obj) => Interpolation(obj, Line);
        public   static TapeActions           Linear(this TapeActions         obj) => Interpolation(obj, Line);
        public   static Sample                Linear(this Sample              obj, IContext context) => Interpolation(obj, Line, context);
        /// <inheritdoc cref="docs._quisetter" />
        public   static InterpolationTypeEnum Linear(this InterpolationTypeEnum obj) => Interpolation(obj, Block);
        /// <inheritdoc cref="docs._quisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static InterpolationType     Linear(this InterpolationType     obj, IContext context) => Interpolation(obj, Line, context);
        
        public   static SynthWishes           Blocky(this SynthWishes           obj) => Interpolation(obj, Block);
        public   static FlowNode              Blocky(this FlowNode              obj) => Interpolation(obj, Block);
        public   static ConfigWishes          Blocky(this ConfigWishes          obj) => Interpolation(obj, Block);
        public   static Tape                  Blocky(this Tape                  obj) => Interpolation(obj, Block);
        public   static TapeConfig            Blocky(this TapeConfig            obj) => Interpolation(obj, Block);
        public   static TapeAction            Blocky(this TapeAction            obj) => Interpolation(obj, Block);
        public   static TapeActions           Blocky(this TapeActions           obj) => Interpolation(obj, Block);
        public   static Sample                Blocky(this Sample                obj, IContext context) => Interpolation(obj, Block, context);
        /// <inheritdoc cref="docs._quisetter" />
        public   static InterpolationTypeEnum Blocky(this InterpolationTypeEnum obj) => Interpolation(obj, Block);
        /// <inheritdoc cref="docs._quisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static InterpolationType     Blocky(this InterpolationType     obj, IContext context) => Interpolation(obj, Block, context);
        
        #endregion
        
        #region CourtesyFrames
        
        public static int CourtesyFrames(int courtesyBytes, int frameSize)
        {
            if (courtesyBytes < 0) throw new Exception(nameof(frameSize) + " less than 0.");
            if (frameSize < 1) throw new Exception(nameof(frameSize) + " less than 1.");
            return courtesyBytes / frameSize;
        }
        
        public static int CourtesyFrames(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static SynthWishes CourtesyFrames(this SynthWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        public static int CourtesyFrames(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static FlowNode CourtesyFrames(this FlowNode obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }

        public static int CourtesyFrames(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static ConfigWishes CourtesyFrames(this ConfigWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }

        internal static int CourtesyFrames(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames ?? DefaultCourtesyFrames;
        }

        public static int CourtesyFrames(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.CourtesyFrames;
        }
        
        public static Tape CourtesyFrames(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.CourtesyFrames = value;
            return obj;
        }

        public static int CourtesyFrames(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames;
        }

        public static TapeConfig CourtesyFrames(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.CourtesyFrames = value;
            return obj;
        }

        public static int CourtesyFrames(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyFrames(obj.Tape);
        }

        public static TapeActions CourtesyFrames(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyFrames(obj.Tape, value);
            return obj;
        }

        public static int CourtesyFrames(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyFrames(obj.Tape);
        }

        public static TapeAction CourtesyFrames(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyFrames(obj.Tape, value);
            return obj;
        }

        #endregion

        // Derived Properties
        
        #region SizeOfBitDepth
        
        public   static int                SizeOfBitDepth(this SynthWishes        obj) => Bits(obj) / 8;
        public   static SynthWishes        SizeOfBitDepth(this SynthWishes        obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this FlowNode           obj) => Bits(obj) / 8;
        public   static FlowNode           SizeOfBitDepth(this FlowNode           obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this ConfigWishes       obj) => Bits(obj) / 8;
        public   static ConfigWishes       SizeOfBitDepth(this ConfigWishes       obj, int byteSize) => Bits(obj, byteSize * 8);
        internal static int                SizeOfBitDepth(this ConfigSection      obj) => Bits(obj) / 8;
        public   static int                SizeOfBitDepth(this Tape               obj) => Bits(obj) / 8;
        public   static Tape               SizeOfBitDepth(this Tape               obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this TapeConfig         obj) => Bits(obj) / 8;
        public   static TapeConfig         SizeOfBitDepth(this TapeConfig         obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this TapeActions        obj) => Bits(obj) / 8;
        public   static TapeActions        SizeOfBitDepth(this TapeActions        obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this TapeAction         obj) => Bits(obj) / 8;
        public   static TapeAction         SizeOfBitDepth(this TapeAction         obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this Buff               obj) => Bits(obj) / 8;
        public   static Buff               SizeOfBitDepth(this Buff               obj, int byteSize, IContext context) => Bits(obj, byteSize * 8, context);
        public   static int                SizeOfBitDepth(this Sample             obj) => Bits(obj) / 8;
        public   static Sample             SizeOfBitDepth(this Sample             obj, int byteSize, IContext context) => Bits(obj, byteSize * 8, context);
        public   static int                SizeOfBitDepth(this AudioFileOutput    obj) => Bits(obj) / 8;
        public   static AudioFileOutput    SizeOfBitDepth(this AudioFileOutput    obj, int byteSize, IContext context) => Bits(obj, byteSize * 8, context);
        public   static int                SizeOfBitDepth(this WavHeaderStruct    obj) => Bits(obj) / 8;
        public   static WavHeaderStruct    SizeOfBitDepth(this WavHeaderStruct    obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this AudioInfoWish      obj) => Bits(obj) / 8;
        public   static AudioInfoWish      SizeOfBitDepth(this AudioInfoWish      obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this AudioFileInfo      obj) => Bits(obj) / 8;
        public   static AudioFileInfo      SizeOfBitDepth(this AudioFileInfo      obj, int byteSize) => Bits(obj, byteSize * 8);
        
        public static int SizeOfBitDepth(this int bits) => bits / 8;
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static int SizeOfBitDepth(this int bits, int byteSize) => byteSize;
        public static int SizeOfBitDepth(this Type obj) => TypeToBits(obj) / 8;
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static Type SizeOfBitDepth(this Type obj, int byteSize) => BitsToType(byteSize * 8);
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataTypeEnum obj) => SizeOf(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SizeOfBitDepth(this SampleDataTypeEnum obj, int byteSize) => BitsToEnum(byteSize * 8);
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataType obj) => SizeOf(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataType SizeOfBitDepth(this SampleDataType obj, int byteSize, IContext context) => BitsToEntity(byteSize * 8, context);

        #endregion
        
        #region FrameSize
        
        public static int FrameSize(this SynthWishes obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this FlowNode obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this ConfigWishes obj) => SizeOfBitDepth(obj) * Channels(obj);
        internal static int FrameSize(this ConfigSection obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Tape obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeConfig obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeAction obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeActions obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Buff obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Sample obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this AudioFileOutput obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this WavHeaderStruct obj) => SizeOfBitDepth(obj) * Channels(obj);
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
        
        public   static double MaxValue(this SynthWishes     obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this FlowNode        obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this ConfigWishes    obj) => Bits(obj).MaxValue();
        internal static double MaxValue(this ConfigSection   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Buff            obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Tape            obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeConfig      obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeAction      obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeActions     obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Sample          obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioFileOutput obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this WavHeaderStruct obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioFileInfo   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioInfoWish   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Type valueType) => Bits(valueType).MaxValue();
        public   static double MaxValue<TValue>() => Bits<TValue>().MaxValue();
        
        [Obsolete(ObsoleteMessage)] public static double MaxValue(this SampleDataType     obj) => Bits(obj).MaxValue();
        [Obsolete(ObsoleteMessage)] public static double MaxValue(this SampleDataTypeEnum obj) => Bits(obj).MaxValue();
        
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
        
        #endregion
                
        #region FileExtension

        /// <inheritdoc cref="docs._fileextension"/>
        public static string      FileExtension(this SynthWishes obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static SynthWishes FileExtension(this SynthWishes obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string      FileExtension(this FlowNode obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static FlowNode     FileExtension(this FlowNode obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this ConfigWishes obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static ConfigWishes FileExtension(this ConfigWishes obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string FileExtension(this ConfigSection obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Tape obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape FileExtension(this Tape obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeConfig obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig FileExtension(this TapeConfig obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeActions obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions FileExtension(this TapeActions obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeAction obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction FileExtension(this TapeAction obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Buff obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff FileExtension(this Buff obj, string value, IContext context) => AudioFormat(obj, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Sample obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample FileExtension(this Sample obj, string value, IContext context) => AudioFormat(obj, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput FileExtension(this AudioFileOutput obj, string value, IContext context) => AudioFormat(obj, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension([UsedImplicitly] this WavHeaderStruct obj) => AudioFormat(obj).FileExtension();
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormatEnum obj)
            => AudioFormatToExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        // ReSharper disable once UnusedParameter.Global
        public static AudioFileFormatEnum FileExtension(this AudioFileFormatEnum obj, string value)
            => ExtensionToAudioFormat(value);

        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] public static string FileExtension(this AudioFileFormat obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.ToEnum().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat FileExtension(this AudioFileFormat obj, string value, IContext context)
            => ExtensionToAudioFormat(value).ToEntity(context);

        #endregion
                
        #region HeaderLength
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this SynthWishes obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this FlowNode obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this ConfigWishes obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int HeaderLength(this ConfigSection obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Buff obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Tape obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeConfig obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeAction obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeActions obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int HeaderLength(this WavHeaderStruct obj) => HeaderLength(Wav);
                
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case Wav: return 44;
                case Raw: return 0;
                default: throw new ValueNotSupportedException(obj);
            }
        }

        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int HeaderLength(this AudioFileFormat obj) => AudioFormat(obj).HeaderLength();
        
        #endregion
        
        #region CourtesyBytes
        
        public static int CourtesyBytes(int courtesyFrames, int frameSize)
        {
            if (courtesyFrames < 0) throw new Exception(nameof(frameSize) + " less than 0.");
            if (frameSize < 1) throw new Exception(nameof(frameSize) + " less than 1.");
            return courtesyFrames * frameSize;
        }
        
        public static int CourtesyBytes(this SynthWishes obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static SynthWishes CourtesyBytes(this SynthWishes obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        public static int CourtesyBytes(this FlowNode obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static FlowNode CourtesyBytes(this FlowNode obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        public static int CourtesyBytes(this ConfigWishes obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static ConfigWishes CourtesyBytes(this ConfigWishes obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));

        internal static int CourtesyBytes(this ConfigSection obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static int CourtesyBytes(this Tape obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static Tape CourtesyBytes(this Tape obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));

        public static int CourtesyBytes(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }

        public static TapeConfig CourtesyBytes(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }

        public static int CourtesyBytes(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }

        public static TapeActions CourtesyBytes(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }

        public static int CourtesyBytes(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }

        public static TapeAction CourtesyBytes(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }

        #endregion
        
        // Durations
        
        #region AudioLength
        
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
            return obj.AudioLength ?? DefaultAudioLength;
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

        #endregion

        #region FrameCount

        public static int FrameCount(int byteCount, int frameSize, int headerLength)
            => (byteCount - headerLength) / frameSize;

        public static int FrameCount(byte[] bytes, string filePath, int frameSize, int headerLength) 
            => (ByteCount(bytes, filePath) - headerLength) / frameSize;

        public static int FrameCount(double audioLength, int samplingRate)
            => (int)(audioLength * samplingRate);

        public static int FrameCount(this SynthWishes obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));

        public static SynthWishes FrameCount(this SynthWishes obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this FlowNode obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));

        public static FlowNode FrameCount(this FlowNode obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this ConfigWishes obj, SynthWishes synthWishes) 
            => FrameCount(AudioLength(obj, synthWishes), SamplingRate(obj));

        public static ConfigWishes FrameCount(this ConfigWishes obj, int value, SynthWishes synthWishes) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)), synthWishes);
        
        internal static int FrameCount(this ConfigSection obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));
        
        public static int FrameCount(this Tape obj)
        {
            if (obj.IsBuff)
            {
                return FrameCount(obj.Bytes, obj.FilePathResolved, FrameSize(obj), HeaderLength(obj));
            }
            else
            {
                return FrameCount(AudioLength(obj), SamplingRate(obj));
            }
        }

        public static Tape FrameCount(this Tape obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }

        public static TapeConfig FrameCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }

        public static int FrameCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }

        public static TapeAction FrameCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }

        public static int FrameCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }

        public static TapeActions FrameCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }

        public static int FrameCount(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);

            int frameCount = FrameCount(obj.Bytes, obj.FilePath, FrameSize(obj), HeaderLength(obj));

            if (Has(frameCount))
            {
                return frameCount;
            }

            if (obj.UnderlyingAudioFileOutput != null)
            {
                return FrameCount(obj.UnderlyingAudioFileOutput);
            }

            return 0;
        }

        public static int FrameCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Bytes, obj.Location, FrameSize(obj), HeaderLength(obj));
        }

        public static int FrameCount(this AudioFileOutput obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));

        public static AudioFileOutput FrameCount(this AudioFileOutput obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this WavHeaderStruct obj) 
            => obj.ToWish().FrameCount();

        public static WavHeaderStruct FrameCount(this WavHeaderStruct obj, int value)
        {
            AudioInfoWish infoWish = obj.ToWish();
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

        public static int ByteCount(this SynthWishes obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));

        public static SynthWishes ByteCount(this SynthWishes obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));
        
        public static int ByteCount(this FlowNode obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));
        
        public static FlowNode ByteCount(this FlowNode obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));

        public static int ByteCount(this ConfigWishes obj, SynthWishes synthWishes) 
            => ByteCount(FrameCount(obj, synthWishes), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));
       
        public static ConfigWishes ByteCount(this ConfigWishes obj, int value, SynthWishes synthWishes)
        {
            double audioLength = AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj));
            AudioLength(obj, audioLength, synthWishes);
            return obj;
        }

        internal static int ByteCount(this ConfigSection obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));

        public static int ByteCount(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.IsBuff)
            {
                return ByteCount(obj.Bytes, obj.FilePathResolved);
            }
            else
            {
                return ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), obj.Config.CourtesyFrames);
            }
        }

        public static Tape ByteCount(this Tape obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));
        
        public static int ByteCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeConfig ByteCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }
        
        public static int ByteCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeActions ByteCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }

        public static int ByteCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeAction ByteCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }

        public static int ByteCount(this Buff obj, int courtesyFrames = 0)
        {
            if (obj == null) throw new NullException(() => obj);

            int byteCount = ByteCount(obj.Bytes, obj.FilePath);

            if (Has(byteCount))
            {
                return byteCount;
            }

            if (obj.UnderlyingAudioFileOutput != null)
            {
                return BytesNeeded(obj.UnderlyingAudioFileOutput, courtesyFrames);
            }

            return 0;
        }

        public static int ByteCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Bytes, obj.Location);
        }

        public static int ByteCount(this AudioFileOutput obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj));

        public static int BytesNeeded(this AudioFileOutput obj, int courtesyFrames = 0) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), courtesyFrames);

        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int courtesyFrames = 0) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), courtesyFrames));

        public static int ByteCount(this WavHeaderStruct obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj));

        public static WavHeaderStruct ByteCount(this WavHeaderStruct obj, int value, int courtesyFrames = 0)
        {
            var wish = obj.ToWish();
            double audioLength = AudioLength(value, FrameSize(wish), SamplingRate(wish), HeaderLength(Wav), courtesyFrames);
            return wish.AudioLength(audioLength).ToWavHeader();
        }
 
        #endregion
    }
}

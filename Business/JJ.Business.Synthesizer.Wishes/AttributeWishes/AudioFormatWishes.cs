using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Primary Audio Attribute
        
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
            return obj.AudioFormat ?? ConfigWishes.DefaultAudioFormat;
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
        public static AudioFileFormatEnum AudioFormat(this WavHeaderStruct obj) => AudioFileFormatEnum.Wav;
        
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
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static AudioFileFormatEnum AudioFormat(this AudioFileFormat obj) => ToEnum(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static AudioFileFormat AudioFormat(this AudioFileFormat obj, AudioFileFormatEnum value, IContext context)
            => ToEntity(value, context);
        
        // Conversion-Style AudioFormat
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static AudioFileFormatEnum ToEnum(this AudioFileFormat enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (AudioFileFormatEnum)enumEntity.ID;
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static AudioFileFormat ToEntity(this AudioFileFormatEnum audioFormat, IContext context)
            => ServiceFactory.CreateRepository<IAudioFileFormatRepository>(context).Get(audioFormat.ToID());
        
        public static AudioFileFormatEnum ExtensionToAudioFormat(this string fileExtension)
        {
            if (FilledInWishes.Is(fileExtension, ".wav")) return AudioFileFormatEnum.Wav;
            if (FilledInWishes.Is(fileExtension, ".raw")) return AudioFileFormatEnum.Raw;
            throw new Exception($"{new{fileExtension}} not supported.");
        }
        
        public static string AudioFormatToExtension(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case AudioFileFormatEnum.Wav: return ".wav";
                case AudioFileFormatEnum.Raw: return ".raw";
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        // AudioFormat Shorthand
        
        public   static bool IsWav(this SynthWishes         obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this FlowNode            obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this ConfigWishes        obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        internal static bool IsWav(this ConfigSection       obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this TapeConfig          obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this TapeAction          obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this TapeActions         obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this Buff                obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this Sample              obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this AudioFileOutput     obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this WavHeaderStruct     obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this string    fileExtension) => AudioFormat(fileExtension) == AudioFileFormatEnum.Wav;
        public   static bool IsWav(this AudioFileFormatEnum obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static bool IsWav(this AudioFileFormat     obj) => AudioFormat(obj) == AudioFileFormatEnum.Wav;
        
        public   static bool IsRaw(this SynthWishes         obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this FlowNode            obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this ConfigWishes        obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        internal static bool IsRaw(this ConfigSection       obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this TapeConfig          obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this TapeAction          obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this TapeActions         obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this Buff                obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this Sample              obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this AudioFileOutput     obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this WavHeaderStruct     obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this string    fileExtension) => AudioFormat(fileExtension) == AudioFileFormatEnum.Raw;
        public   static bool IsRaw(this AudioFileFormatEnum obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static bool IsRaw(this AudioFileFormat  obj) => AudioFormat(obj) == AudioFileFormatEnum.Raw;
        
        public   static SynthWishes         AsWav(this SynthWishes         obj) => AudioFormat(obj, AudioFileFormatEnum.Wav);
        public   static FlowNode            AsWav(this FlowNode            obj) => AudioFormat(obj, AudioFileFormatEnum.Wav);
        public   static ConfigWishes        AsWav(this ConfigWishes        obj) => AudioFormat(obj, AudioFileFormatEnum.Wav);
        public   static Buff                AsWav(this Buff                obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Wav, context);
        public   static Tape                AsWav(this Tape                obj) => AudioFormat(obj, AudioFileFormatEnum.Wav);
        public   static TapeConfig          AsWav(this TapeConfig          obj) => AudioFormat(obj, AudioFileFormatEnum.Wav);
        public   static TapeAction          AsWav(this TapeAction          obj) => AudioFormat(obj, AudioFileFormatEnum.Wav);
        public   static TapeActions         AsWav(this TapeActions         obj) => AudioFormat(obj, AudioFileFormatEnum.Wav);
        public   static Sample              AsWav(this Sample              obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Wav, context);
        public   static AudioFileOutput     AsWav(this AudioFileOutput     obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string              AsWav(this string    fileExtension) => AudioFormat(fileExtension, AudioFileFormatEnum.Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsWav(this AudioFileFormatEnum obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static AudioFileFormat     AsWav(this AudioFileFormat     obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Wav, context);
        
        public   static SynthWishes         AsRaw(this SynthWishes         obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        public   static FlowNode            AsRaw(this FlowNode            obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        public   static ConfigWishes        AsRaw(this ConfigWishes        obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        public   static Buff                AsRaw(this Buff                obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Raw, context);
        public   static Tape                AsRaw(this Tape                obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        public   static TapeConfig          AsRaw(this TapeConfig          obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        public   static TapeAction          AsRaw(this TapeAction          obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        public   static TapeActions         AsRaw(this TapeActions         obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        public   static Sample              AsRaw(this Sample              obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Raw, context);
        public   static AudioFileOutput     AsRaw(this AudioFileOutput     obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string              AsRaw(this string    fileExtension) => AudioFormat(fileExtension, AudioFileFormatEnum.Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsRaw(this AudioFileFormatEnum obj) => AudioFormat(obj, AudioFileFormatEnum.Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static AudioFileFormat     AsRaw(this AudioFileFormat     obj, IContext context) => AudioFormat(obj, AudioFileFormatEnum.Raw, context);
    }
}
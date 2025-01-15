using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class AudioFormatExtensionWishes
    {
        // A Primary Audio Attribute
        
        // Synth-Bound
        
        public static AudioFileFormatEnum AudioFormat(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }
        
        public static SynthWishes AudioFormat(this SynthWishes obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }
        
        public static AudioFileFormatEnum AudioFormat(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }
        
        public static FlowNode AudioFormat(this FlowNode obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }
        
        internal static AudioFileFormatEnum AudioFormat(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }
        
        internal static ConfigResolver AudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }
        
        // Global-Bound
        
        internal static AudioFileFormatEnum? AudioFormat(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat;
        }
        
        // Tape-Bound
        
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
        
        // Buff-Bound
        
        public static AudioFileFormatEnum AudioFormat(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return Undefined;
            return obj.UnderlyingAudioFileOutput.AudioFormat();
        }
        
        public static Buff AudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj.UnderlyingAudioFileOutput);
            obj.UnderlyingAudioFileOutput.AudioFormat(value, context);
            return obj;
        }
        
        public static AudioFileFormatEnum AudioFormat(this Sample obj) => obj.GetAudioFileFormatEnum();
        
        public static Sample AudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }
        
        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput obj) => obj.GetAudioFileFormatEnum();
        
        public static AudioFileOutput AudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }
        
        // Immutable

        public static AudioFileFormatEnum AudioFormat(this WavHeaderStruct obj) => Wav;
        
        public static AudioFileFormatEnum AudioFormat(this string fileExtension) => fileExtension.FileExtensionToAudioFormat();
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => newAudioFormat.FileExtension();
        
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum obj) => obj;
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => newAudioFormat;
        
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormat obj) => ToEnum(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context)
            => ToEntity(newAudioFormat, context);
        
        // Conversion-Style AudioFormat
        
        [Obsolete(ObsoleteMessage)] public static AudioFileFormatEnum ToEnum(this AudioFileFormat enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (AudioFileFormatEnum)enumEntity.ID;
        }
        
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat ToEntity(this AudioFileFormatEnum audioFormat, IContext context)
            => ServiceFactory.CreateRepository<IAudioFileFormatRepository>(context).Get(audioFormat.ToID());
        
        // AudioFormat Shorthand
        
        public   static bool IsWav(this SynthWishes         obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this FlowNode            obj) => obj.AudioFormat() == Wav;
        internal static bool IsWav(this ConfigResolver      obj) => obj.AudioFormat() == Wav;
        internal static bool IsWav(this ConfigSection       obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this Tape                obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this TapeConfig          obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this TapeAction          obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this TapeActions         obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this Buff                obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this Sample              obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this AudioFileOutput     obj) => obj.AudioFormat() == Wav;
        public   static bool IsWav(this WavHeaderStruct     obj) => true;
        public   static bool IsWav(this string    fileExtension) => fileExtension.AudioFormat() == Wav;
        public   static bool IsWav(this AudioFileFormatEnum obj) => obj.AudioFormat() == Wav;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsWav(this AudioFileFormat     obj) => obj.AudioFormat() == Wav;
        
        public   static bool IsRaw(this SynthWishes         obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this FlowNode            obj) => obj.AudioFormat() == Raw;
        internal static bool IsRaw(this ConfigResolver      obj) => obj.AudioFormat() == Raw;
        internal static bool IsRaw(this ConfigSection       obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this Tape                obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this TapeConfig          obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this TapeAction          obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this TapeActions         obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this Buff                obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this Sample              obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this AudioFileOutput     obj) => obj.AudioFormat() == Raw;
        public   static bool IsRaw(this WavHeaderStruct     obj) => false;
        public   static bool IsRaw(this string    fileExtension) => fileExtension.AudioFormat() == Raw;
        public   static bool IsRaw(this AudioFileFormatEnum obj) => obj.AudioFormat() == Raw;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsRaw(this AudioFileFormat     obj) => AudioFormat(obj) == Raw;
        
        public   static Buff            AsWav(this Buff            obj, IContext context) => obj.AudioFormat(Wav, context);
        public   static Tape            AsWav(this Tape            obj                  ) => obj.AudioFormat(Wav         );
        public   static TapeConfig      AsWav(this TapeConfig      obj                  ) => obj.AudioFormat(Wav         );
        public   static TapeAction      AsWav(this TapeAction      obj                  ) => obj.AudioFormat(Wav         );
        public   static TapeActions     AsWav(this TapeActions     obj                  ) => obj.AudioFormat(Wav         );
        public   static Sample          AsWav(this Sample          obj, IContext context) => obj.AudioFormat(Wav, context);
        public   static AudioFileOutput AsWav(this AudioFileOutput obj, IContext context) => obj.AudioFormat(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string AsWav(this string oldFileExtension) => oldFileExtension.AudioFormat(Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsWav(this AudioFileFormatEnum oldAudioFormat) => oldAudioFormat.AudioFormat(Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static AudioFileFormat AsWav(this AudioFileFormat oldEnumEntity, IContext context) => oldEnumEntity.AudioFormat(Wav, context);
        
        public   static Buff            AsRaw(this Buff            obj, IContext context) => obj.AudioFormat(Raw, context);
        public   static Tape            AsRaw(this Tape            obj                  ) => obj.AudioFormat(Raw         );
        public   static TapeConfig      AsRaw(this TapeConfig      obj                  ) => obj.AudioFormat(Raw         );
        public   static TapeAction      AsRaw(this TapeAction      obj                  ) => obj.AudioFormat(Raw         );
        public   static TapeActions     AsRaw(this TapeActions     obj                  ) => obj.AudioFormat(Raw         );
        public   static Sample          AsRaw(this Sample          obj, IContext context) => obj.AudioFormat(Raw, context);
        public   static AudioFileOutput AsRaw(this AudioFileOutput obj, IContext context) => obj.AudioFormat(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string AsRaw(this string oldFileExtension) => oldFileExtension.AudioFormat(Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsRaw(this AudioFileFormatEnum oldAudioFormat) => oldAudioFormat.AudioFormat(Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static AudioFileFormat AsRaw(this AudioFileFormat oldEnumEntity, IContext context) => oldEnumEntity.AudioFormat(Raw, context);
    }
}
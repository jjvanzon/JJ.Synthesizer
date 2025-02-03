using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class FileExtensionWishes
    {
        // Derived from AudioFormat
        
        // Synth-Bound
        
        /// <inheritdoc cref="docs._fileextension" />
        public static string FileExtension(this SynthWishes obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        public static string GetFileExtension(this SynthWishes obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes FileExtension(this SynthWishes obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes WithFileExtension(this SynthWishes obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes AsFileExtension(this SynthWishes obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes SetFileExtension(this SynthWishes obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension" />
        public static string FileExtension(this FlowNode obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        public static string GetFileExtension(this FlowNode obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode FileExtension(this FlowNode obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode WithFileExtension(this FlowNode obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode AsFileExtension(this FlowNode obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode SetFileExtension(this FlowNode obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        // TODO
        
        /// <inheritdoc cref="docs._fileextension" />
        internal static string FileExtension(this ConfigResolver obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        internal static string GetFileExtension(this ConfigResolver obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver FileExtension(this ConfigResolver obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver WithFileExtension(this ConfigResolver obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver AsFileExtension(this ConfigResolver obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver SetFileExtension(this ConfigResolver obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        // Global-Bound
        
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string FileExtension(this ConfigSection obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string GetFileExtension(this ConfigSection obj)
        {
            return obj.AudioFormat()?.FileExtension();
        }
        
        // Tape-Bound
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Tape obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Tape obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape FileExtension(this Tape obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape WithFileExtension(this Tape obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape AsFileExtension(this Tape obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape SetFileExtension(this Tape obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeConfig obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this TapeConfig obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig FileExtension(this TapeConfig obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig WithFileExtension(this TapeConfig obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig AsFileExtension(this TapeConfig obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig SetFileExtension(this TapeConfig obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeActions obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this TapeActions obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions FileExtension(this TapeActions obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions WithFileExtension(this TapeActions obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions AsFileExtension(this TapeActions obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions SetFileExtension(this TapeActions obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeAction obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this TapeAction obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction FileExtension(this TapeAction obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction WithFileExtension(this TapeAction obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction AsFileExtension(this TapeAction obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction SetFileExtension(this TapeAction obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        // Buff-Bound
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Buff obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Buff obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff FileExtension(this Buff obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff WithFileExtension(this Buff obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff AsFileExtension(this Buff obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff SetFileExtension(this Buff obj, string value, IContext context)
        {
            return obj.SetAudioFormat(value.ToAudioFormat(), context);
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this AudioFileOutput obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput FileExtension(this AudioFileOutput obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput WithFileExtension(this AudioFileOutput obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput AsFileExtension(this AudioFileOutput obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput SetFileExtension(this AudioFileOutput obj, string value, IContext context)
        {
            return obj.SetAudioFormat(value.ToAudioFormat(), context);
        }
        
        // Independent after Taping
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Sample obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Sample obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample FileExtension(this Sample obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample WithFileExtension(this Sample obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample AsFileExtension(this Sample obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample SetFileExtension(this Sample obj, string value, IContext context)
        {
            return obj.SetAudioFormat(value.ToAudioFormat(), context);
        }
        
        // Immutable
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this WavHeaderStruct obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this WavHeaderStruct obj)
        {
            return obj.AudioFormat().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string AsFileExtension(this AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string ToFileExtension(this AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string AudioFormatToFileExtension(this AudioFileFormatEnum obj)
        {
            return ConfigWishes.AudioFormatToFileExtension(obj);
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum FileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => FileExtensionToAudioFormat(newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum SetFileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension)
            => FileExtensionToAudioFormat(newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum WithFileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => FileExtensionToAudioFormat(newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum AsFileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => FileExtensionToAudioFormat(newExtension);
        ///// <inheritdoc cref="docs._fileextension"/>
        //public static AudioFileFormatEnum AsAudioFormat(this string fileExtension)
        //    => FileExtensionToAudioFormat(fileExtension);
        ///// <inheritdoc cref="docs._fileextension"/>
        //public static AudioFileFormatEnum ToAudioFormat(this string fileExtension)
        //    => FileExtensionToAudioFormat(fileExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum FileExtensionToAudioFormat(this string fileExtension)
        {
            return ConfigWishes.FileExtensionToAudioFormat(fileExtension);
        }

        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] public static string FileExtension(this AudioFileFormat obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] public static string GetFileExtension(this AudioFileFormat obj)
        {
            return obj.ToEnum().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat FileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context) 
            => SetFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat WithFileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => SetFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat AsFileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => SetFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat SetFileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context)
        {
            return newExtension.FileExtensionToAudioFormat().ToEntity(context);
        }
    }
    
    public partial class ConfigWishes
    {
        // Conversion-Style
        
        public static AudioFileFormatEnum FileExtensionToAudioFormat(string fileExtension)
        {
            if (Is(fileExtension, ".wav")) return Wav;
            if (Is(fileExtension, ".raw")) return Raw;
            if (!Has(fileExtension)) return Undefined;
            AssertFileExtension(fileExtension); return default;
        }
        
        public static string AudioFormatToFileExtension(AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case Wav: return ".wav";
                case Raw: return ".raw";
                case Undefined: return default;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        // Synonym
        
        public static string FileExtension(AudioFileFormatEnum obj) 
            => AudioFormatToFileExtension(obj);
    }
}
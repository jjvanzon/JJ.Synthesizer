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
    // File Extension: Derived from AudioFormat
    
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class FileExtensionWishes
    {
        // Synth-Bound
        
        /// <inheritdoc cref="docs._fileextension" />
        public static string FileExtension(this SynthWishes obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        public static string GetFileExtension(this SynthWishes obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes FileExtension(this SynthWishes obj, string value) => ConfigWishes.FileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes WithFileExtension(this SynthWishes obj, string value) => ConfigWishes.WithFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes AsFileExtension(this SynthWishes obj, string value) => ConfigWishes.AsFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes SetFileExtension(this SynthWishes obj, string value) => ConfigWishes.SetFileExtension(obj, value);

        /// <inheritdoc cref="docs._fileextension" />
        public static string FileExtension(this FlowNode obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        public static string GetFileExtension(this FlowNode obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode FileExtension(this FlowNode obj, string value) => ConfigWishes.FileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode WithFileExtension(this FlowNode obj, string value) => ConfigWishes.WithFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode AsFileExtension(this FlowNode obj, string value) => ConfigWishes.AsFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode SetFileExtension(this FlowNode obj, string value) => ConfigWishes.SetFileExtension(obj, value);

        /// <inheritdoc cref="docs._fileextension" />
        internal static string FileExtension(this ConfigResolver obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        internal static string GetFileExtension(this ConfigResolver obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver FileExtension(this ConfigResolver obj, string value) => ConfigWishes.FileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver WithFileExtension(this ConfigResolver obj, string value) => ConfigWishes.WithFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver AsFileExtension(this ConfigResolver obj, string value) => ConfigWishes.AsFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver SetFileExtension(this ConfigResolver obj, string value) => ConfigWishes.SetFileExtension(obj, value);

        // Global-Bound

        /// <inheritdoc cref="docs._fileextension"/>
        internal static string FileExtension(this ConfigSection obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string GetFileExtension(this ConfigSection obj) => ConfigWishes.GetFileExtension(obj);

        // Tape-Bound

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Tape obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Tape obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape FileExtension(this Tape obj, string value) => ConfigWishes.FileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape WithFileExtension(this Tape obj, string value) => ConfigWishes.WithFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape AsFileExtension(this Tape obj, string value) => ConfigWishes.AsFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape SetFileExtension(this Tape obj, string value) => ConfigWishes.SetFileExtension(obj, value);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeConfig obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this TapeConfig obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig FileExtension(this TapeConfig obj, string value) => ConfigWishes.FileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig WithFileExtension(this TapeConfig obj, string value) => ConfigWishes.WithFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig AsFileExtension(this TapeConfig obj, string value) => ConfigWishes.AsFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig SetFileExtension(this TapeConfig obj, string value) => ConfigWishes.SetFileExtension(obj, value);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeActions obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this TapeActions obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions FileExtension(this TapeActions obj, string value) => ConfigWishes.FileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions WithFileExtension(this TapeActions obj, string value) => ConfigWishes.WithFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions AsFileExtension(this TapeActions obj, string value) => ConfigWishes.AsFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions SetFileExtension(this TapeActions obj, string value) => ConfigWishes.SetFileExtension(obj, value);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeAction obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this TapeAction obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction FileExtension(this TapeAction obj, string value) => ConfigWishes.FileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction WithFileExtension(this TapeAction obj, string value) => ConfigWishes.WithFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction AsFileExtension(this TapeAction obj, string value) => ConfigWishes.AsFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction SetFileExtension(this TapeAction obj, string value) => ConfigWishes.SetFileExtension(obj, value);

        // Buff-Bound

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Buff obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Buff obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff FileExtension(this Buff obj, string value, IContext context) => ConfigWishes.FileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff WithFileExtension(this Buff obj, string value, IContext context) => ConfigWishes.WithFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff AsFileExtension(this Buff obj, string value, IContext context) => ConfigWishes.AsFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff SetFileExtension(this Buff obj, string value, IContext context) => ConfigWishes.SetFileExtension(obj, value, context);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this AudioFileOutput obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput FileExtension(this AudioFileOutput obj, string value, IContext context) 
            => ConfigWishes.FileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput WithFileExtension(this AudioFileOutput obj, string value, IContext context) 
            => ConfigWishes.WithFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput AsFileExtension(this AudioFileOutput obj, string value, IContext context)
            => ConfigWishes.AsFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput SetFileExtension(this AudioFileOutput obj, string value, IContext context)
            => ConfigWishes.SetFileExtension(obj, value, context);
        
        // Independent after Taping
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Sample obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Sample obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample FileExtension(this Sample obj, string value, IContext context) => ConfigWishes.FileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample WithFileExtension(this Sample obj, string value, IContext context) => ConfigWishes.WithFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample AsFileExtension(this Sample obj, string value, IContext context) => ConfigWishes.AsFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample SetFileExtension(this Sample obj, string value, IContext context) => ConfigWishes.SetFileExtension(obj, value, context);

        // Immutable

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this WavHeaderStruct obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this WavHeaderStruct obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormatEnum obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this AudioFileFormatEnum obj) => ConfigWishes.GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string AsFileExtension(this AudioFileFormatEnum obj) => ConfigWishes.AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string ToFileExtension(this AudioFileFormatEnum obj) => ConfigWishes.AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string AudioFormatToFileExtension(this AudioFileFormatEnum obj) => ConfigWishes.AudioFormatToFileExtension(obj);
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum FileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => ConfigWishes.FileExtension(oldAudioFormat, newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum SetFileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => ConfigWishes.SetFileExtension(oldAudioFormat, newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum WithFileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension)
            => ConfigWishes.WithFileExtension(oldAudioFormat, newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum AsFileExtension(this AudioFileFormatEnum oldAudioFormat, string newExtension)
            => ConfigWishes.AsFileExtension(oldAudioFormat, newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum FileExtensionToAudioFormat(this string fileExtension)
            => ConfigWishes.FileExtensionToAudioFormat(fileExtension);
        
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static string FileExtension(this AudioFileFormat obj) => ConfigWishes.FileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static string GetFileExtension(this AudioFileFormat obj) => ConfigWishes.GetFileExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat FileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => ConfigWishes.FileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat WithFileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => ConfigWishes.WithFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat AsFileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => ConfigWishes.AsFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat SetFileExtension(this AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => ConfigWishes.SetFileExtension(oldAudioFormat, newExtension, context);
    }

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public partial class ConfigWishes
    {
        // Synth-Bound
        
        /// <inheritdoc cref="docs._fileextension" />
        public static string FileExtension(SynthWishes obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        public static string GetFileExtension(SynthWishes obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes FileExtension(SynthWishes obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes WithFileExtension(SynthWishes obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes AsFileExtension(SynthWishes obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static SynthWishes SetFileExtension(SynthWishes obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension" />
        public static string FileExtension(FlowNode obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        public static string GetFileExtension(FlowNode obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode FileExtension(FlowNode obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode WithFileExtension(FlowNode obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode AsFileExtension(FlowNode obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension" />
        public static FlowNode SetFileExtension(FlowNode obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension" />
        internal static string FileExtension(ConfigResolver obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension" />
        internal static string GetFileExtension(ConfigResolver obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver FileExtension(ConfigResolver obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver WithFileExtension(ConfigResolver obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver AsFileExtension(ConfigResolver obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static ConfigResolver SetFileExtension(ConfigResolver obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        // Global-Bound
        
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string FileExtension(ConfigSection obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string GetFileExtension(ConfigSection obj)
        {
            return obj.AudioFormat()?.FileExtension();
        }
        
        // Tape-Bound
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(Tape obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(Tape obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape FileExtension(Tape obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape WithFileExtension(Tape obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape AsFileExtension(Tape obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape SetFileExtension(Tape obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(TapeConfig obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(TapeConfig obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig FileExtension(TapeConfig obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig WithFileExtension(TapeConfig obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig AsFileExtension(TapeConfig obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig SetFileExtension(TapeConfig obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(TapeActions obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(TapeActions obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions FileExtension(TapeActions obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions WithFileExtension(TapeActions obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions AsFileExtension(TapeActions obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions SetFileExtension(TapeActions obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(TapeAction obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(TapeAction obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction FileExtension(TapeAction obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction WithFileExtension(TapeAction obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction AsFileExtension(TapeAction obj, string value) => SetFileExtension(obj, value);
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction SetFileExtension(TapeAction obj, string value)
        {
            return obj.SetAudioFormat(value.ToAudioFormat());
        }
        
        // Buff-Bound
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(Buff obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(Buff obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff FileExtension(Buff obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff WithFileExtension(Buff obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff AsFileExtension(Buff obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff SetFileExtension(Buff obj, string value, IContext context)
        {
            return obj.SetAudioFormat(value.ToAudioFormat(), context);
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(AudioFileOutput obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(AudioFileOutput obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput FileExtension(AudioFileOutput obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput WithFileExtension(AudioFileOutput obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput AsFileExtension(AudioFileOutput obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput SetFileExtension(AudioFileOutput obj, string value, IContext context)
        {
            return obj.SetAudioFormat(value.ToAudioFormat(), context);
        }
        
        // Independent after Taping
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(Sample obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(Sample obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample FileExtension(Sample obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample WithFileExtension(Sample obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample AsFileExtension(Sample obj, string value, IContext context) => SetFileExtension(obj, value, context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample SetFileExtension(Sample obj, string value, IContext context)
        {
            return obj.SetAudioFormat(value.ToAudioFormat(), context);
        }
        
        // Immutable
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(WavHeaderStruct obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(WavHeaderStruct obj)
        {
            return obj.GetAudioFormat().GetFileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string AsFileExtension(AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string ToFileExtension(AudioFileFormatEnum obj) => AudioFormatToFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
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
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum FileExtension(AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => FileExtensionToAudioFormat(newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum SetFileExtension(AudioFileFormatEnum oldAudioFormat, string newExtension)
            => FileExtensionToAudioFormat(newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum WithFileExtension(AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => FileExtensionToAudioFormat(newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum AsFileExtension(AudioFileFormatEnum oldAudioFormat, string newExtension) 
            => FileExtensionToAudioFormat(newExtension);
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum FileExtensionToAudioFormat(string fileExtension)
        {
            if (Is(fileExtension, ".wav")) return Wav;
            if (Is(fileExtension, ".raw")) return Raw;
            if (!Has(fileExtension)) return Undefined;
            AssertFileExtension(fileExtension); return default;
        }

        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static string FileExtension(AudioFileFormat obj) => GetFileExtension(obj);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static string GetFileExtension(AudioFileFormat obj)
        {
            return obj.ToEnum().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat FileExtension(AudioFileFormat oldAudioFormat, string newExtension, IContext context) 
            => SetFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat WithFileExtension(AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => SetFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat AsFileExtension(AudioFileFormat oldAudioFormat, string newExtension, IContext context)
            => SetFileExtension(oldAudioFormat, newExtension, context);
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat SetFileExtension(AudioFileFormat oldAudioFormat, string newExtension, IContext context)
        {
            return FileExtensionToAudioFormat(newExtension).ToEntity(context);
        }
    }
}
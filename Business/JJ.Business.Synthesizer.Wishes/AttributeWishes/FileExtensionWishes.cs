using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived from AudioFormat
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string      FileExtension(this SynthWishes obj) => obj.AudioFormat().FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static SynthWishes FileExtension(this SynthWishes obj, string value) => obj.AudioFormat(value.AudioFormat());
        /// <inheritdoc cref="docs._fileextension"/>
        public static string      FileExtension(this FlowNode obj) => obj.AudioFormat().FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static FlowNode     FileExtension(this FlowNode obj, string value) => obj.AudioFormat(value.AudioFormat());
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this ConfigWishes obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static ConfigWishes FileExtension(this ConfigWishes obj, string value) => obj.AudioFormat(value.AudioFormat());
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string FileExtension(this ConfigSection obj) => obj.AudioFormat()?.FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Tape obj) => obj.AudioFormat().FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape FileExtension(this Tape obj, string value) => obj.AudioFormat(value.AudioFormat());
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeConfig obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig FileExtension(this TapeConfig obj, string value) => obj.AudioFormat(value.AudioFormat());
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeActions obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions FileExtension(this TapeActions obj, string value) => obj.AudioFormat(value.AudioFormat());
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeAction obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction FileExtension(this TapeAction obj, string value) => obj.AudioFormat(value.AudioFormat());
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Buff obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff FileExtension(this Buff obj, string value, IContext context) => obj.AudioFormat(value.AudioFormat(), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Sample obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample FileExtension(this Sample obj, string value, IContext context) => obj.AudioFormat(value.AudioFormat(), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput obj) => obj.AudioFormat().FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput FileExtension(this AudioFileOutput obj, string value, IContext context) => obj.AudioFormat(value.AudioFormat(), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension([UsedImplicitly] this WavHeaderStruct obj) => obj.AudioFormat().FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormatEnum obj) => obj.AudioFormatToFileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileFormatEnum FileExtension(this AudioFileFormatEnum obj, string value) => value.FileExtensionToAudioFormat();
        
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static string FileExtension(this AudioFileFormat obj) 
            => obj.ToEnum().FileExtension();
        
        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat FileExtension(this AudioFileFormat obj, string value, IContext context)
            => value.FileExtensionToAudioFormat().ToEntity(context);
        
        // Conversion-Style FileExtension
                
        public static AudioFileFormatEnum FileExtensionToAudioFormat(this string fileExtension)
        {
            if (Is(fileExtension, ".wav")) return Wav;
            if (Is(fileExtension, ".raw")) return Raw;
            throw new Exception($"{new{fileExtension}} not supported.");
        }
        
        public static string AudioFormatToFileExtension(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case Wav: return ".wav";
                case Raw: return ".raw";
                default: throw new ValueNotSupportedException(obj);
            }
        }
    }
}
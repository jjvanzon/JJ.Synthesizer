using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
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
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static string FileExtension(this AudioFileFormat obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.ToEnum().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static AudioFileFormat FileExtension(this AudioFileFormat obj, string value, IContext context)
            => ExtensionToAudioFormat(value).ToEntity(context);
        
        #endregion
    }
}
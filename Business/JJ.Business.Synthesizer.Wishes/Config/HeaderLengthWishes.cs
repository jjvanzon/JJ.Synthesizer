using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class HeaderLengthExtensionWishes
    {
        // Derived from AudioFormat
     
        // Synth-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this SynthWishes obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this SynthWishes obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static SynthWishes HeaderLength(this SynthWishes obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static SynthWishes WithHeaderLength(this SynthWishes obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static SynthWishes SetHeaderLength(this SynthWishes obj, int? headerLength)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this FlowNode obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this FlowNode obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static FlowNode HeaderLength(this FlowNode obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static FlowNode WithHeaderLength(this FlowNode obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static FlowNode SetHeaderLength(this FlowNode obj, int? headerLength)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int HeaderLength(this ConfigResolver obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int GetHeaderLength(this ConfigResolver obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        internal static ConfigResolver HeaderLength(this ConfigResolver obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        internal static ConfigResolver WithHeaderLength(this ConfigResolver obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        internal static ConfigResolver SetHeaderLength(this ConfigResolver obj, int? headerLength)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        // Global-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int? HeaderLength(this ConfigSection obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int? GetHeaderLength(this ConfigSection obj)
        {
            return obj.AudioFormat()?.HeaderLength();
        }
        
        // Tape-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Tape obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this Tape obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static Tape HeaderLength(this Tape obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static Tape SetHeaderLength(this Tape obj, int headerLength)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeConfig obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this TapeConfig obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeConfig HeaderLength(this TapeConfig obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeConfig SetHeaderLength(this TapeConfig obj, int headerLength)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeAction obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this TapeAction obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeAction HeaderLength(this TapeAction obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeAction WithHeaderLength(this TapeAction obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeAction SetHeaderLength(this TapeAction obj, int headerLength)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeActions obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this TapeActions obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeActions HeaderLength(this TapeActions obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeActions WithHeaderLength(this TapeActions obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeActions SetHeaderLength(this TapeActions obj, int headerLength)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        // Buff-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Buff obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this Buff obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static Buff HeaderLength(this Buff obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlength"/>
        public static Buff WithHeaderLength(this Buff obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlength"/>
        public static Buff SetHeaderLength(this Buff obj, int headerLength, IContext context)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int WithHeaderLength(this AudioFileOutput obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this AudioFileOutput obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static AudioFileOutput HeaderLength(this AudioFileOutput obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlength"/>
        public static AudioFileOutput WithHeaderLength(this AudioFileOutput obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlength"/>
        public static AudioFileOutput SetHeaderLength(this AudioFileOutput obj, int headerLength, IContext context)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
        
        // Independent after Taping
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this Sample obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static Sample HeaderLength(this Sample obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlength"/>
        public static Sample WithHeaderLength(this Sample obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlength"/>
        public static Sample SetHeaderLength(this Sample obj, int headerLength, IContext context)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
        
        // Immutable
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this WavHeaderStruct obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this WavHeaderStruct obj)
        {
            return Wav.HeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? HeaderLength(this AudioFileFormatEnum? obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? GetHeaderLength(this AudioFileFormatEnum? obj) => ConfigWishes.GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this AudioFileFormatEnum obj) => ConfigWishes.GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? ToHeaderLength(this AudioFileFormatEnum? obj) => ConfigWishes.ToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int ToHeaderLength(this AudioFileFormatEnum obj) => ConfigWishes.ToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? AudioFormatToHeaderLength(this AudioFileFormatEnum? audioFormat) => ConfigWishes.AudioFormatToHeaderLength(audioFormat);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int AudioFormatToHeaderLength(this AudioFileFormatEnum audioFormat) => ConfigWishes.AudioFormatToHeaderLength(audioFormat);

        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum? HeaderLength(this AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => ConfigWishes.HeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum HeaderLength(this AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => ConfigWishes.HeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum? WithHeaderLength(this AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => ConfigWishes.WithHeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum WithHeaderLength(this AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => ConfigWishes.WithHeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum? SetHeaderLength(this AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => ConfigWishes.SetHeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum SetHeaderLength(this AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => ConfigWishes.SetHeaderLength(oldAudioFormat, newHeaderLength);
        public static AudioFileFormatEnum? AudioFormat(this int? headerLength) => ConfigWishes.AudioFormat(headerLength);
        public static AudioFileFormatEnum AudioFormat(this int headerLength) => ConfigWishes.AudioFormat(headerLength);
        public static AudioFileFormatEnum? ToAudioFormat(this int? headerLength) => ConfigWishes.ToAudioFormat(headerLength);
        public static AudioFileFormatEnum ToAudioFormat(this int headerLength) => ConfigWishes.ToAudioFormat(headerLength);
        public static AudioFileFormatEnum? HeaderLengthToAudioFormat(this int? headerLength) => ConfigWishes.HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum HeaderLengthToAudioFormat(this int headerLength) => ConfigWishes.HeaderLengthToAudioFormat(headerLength);
        
        // With AudioFileFormat

        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int HeaderLength(this AudioFileFormat obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int ToHeaderLength(this AudioFileFormat obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int GetHeaderLength(this AudioFileFormat obj)
        {
            return obj.ToEnum().AudioFormatToHeaderLength();
        }
        
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat HeaderLength(this AudioFileFormat obj, int headerLength, IContext context)
            => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat WithHeaderLength(this AudioFileFormat obj, int headerLength, IContext context)
            => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat SetHeaderLength(this AudioFileFormat obj, int headerLength, IContext context)
        {
            return obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
    }

    public partial class ConfigWishes
    {
        // Constants
        
        public const int WavHeaderLength = 44;
        public const int RawHeaderLength = 0;
        
        // Conversion-Style
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? HeaderLength(AudioFileFormatEnum? obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(AudioFileFormatEnum obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? GetHeaderLength(AudioFileFormatEnum? obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(AudioFileFormatEnum obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? ToHeaderLength(AudioFileFormatEnum? obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int ToHeaderLength(AudioFileFormatEnum obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? AudioFormatToHeaderLength(AudioFileFormatEnum? audioFormat) => AudioFormatToHeaderLength(audioFormat.Coalesce());
        /// <inheritdoc cref="docs._headerlength"/>
        public static int AudioFormatToHeaderLength(AudioFileFormatEnum audioFormat)
        {
            if (audioFormat == Wav) return WavHeaderLength;
            if (audioFormat == Raw) return RawHeaderLength;
            AssertAudioFormat(audioFormat); return default;
        }


        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum? HeaderLength(AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum HeaderLength(AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum? WithHeaderLength(AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum WithHeaderLength(AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum? SetHeaderLength(AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum SetHeaderLength(AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        public static AudioFileFormatEnum? AudioFormat(int? headerLength) => HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum AudioFormat(int headerLength) => HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum? ToAudioFormat(int? headerLength) => HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum ToAudioFormat(int headerLength) => HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum? HeaderLengthToAudioFormat(int? headerLength) => HeaderLengthToAudioFormat(headerLength.CoalesceHeaderLength());
        public static AudioFileFormatEnum HeaderLengthToAudioFormat(int headerLength)
        {
            if (headerLength == WavHeaderLength) return Wav;
            if (headerLength == RawHeaderLength) return Raw;
            AssertHeaderLength(headerLength); return default;
        }
    }
}
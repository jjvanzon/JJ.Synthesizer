using System;
using System.Runtime.Remoting.Contexts;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class HeaderLengthExtensionWishes
    {
        // Derived from AudioFormat
     
        // Synth-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this SynthWishes obj) => obj.AudioFormat().HeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static SynthWishes HeaderLength(this SynthWishes obj, int? headerLength) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this FlowNode obj) => obj.AudioFormat().HeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static FlowNode HeaderLength(this FlowNode obj, int? headerLength) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));
        
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int HeaderLength(this ConfigResolver obj) => obj.AudioFormat().HeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        internal static ConfigResolver HeaderLength(this ConfigResolver obj, int? headerLength) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));

        // Global-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int? HeaderLength(this ConfigSection obj) => obj.AudioFormat()?.HeaderLength();
        
        // Tape-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Tape obj) => obj.AudioFormat().HeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static Tape HeaderLength(this Tape obj, int headerLength) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeConfig obj) => obj.AudioFormat().HeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeConfig HeaderLength(this TapeConfig obj, int headerLength) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeAction obj) => obj.AudioFormat().HeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeAction HeaderLength(this TapeAction obj, int headerLength) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeActions obj) => obj.AudioFormat().HeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        public static TapeActions HeaderLength(this TapeActions obj, int headerLength) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength));

        // Buff-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Buff obj) => obj.AudioFormat().HeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        public static Buff HeaderLength(this Buff obj, int headerLength, IContext context) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => obj.AudioFormat().HeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        public static AudioFileOutput HeaderLength(this AudioFileOutput obj, int headerLength, IContext context) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);

        // Independent after Taping
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample obj) => obj.AudioFormat().HeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static Sample HeaderLength(this Sample obj, int headerLength, IContext context) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        
        // Immutable
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this WavHeaderStruct obj) => Wav.HeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj) => AudioFormatToHeaderLength(obj);
        
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum HeaderLength(this AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => oldAudioFormat.AudioFormat(HeaderLengthToAudioFormat(newHeaderLength));

        /// <inheritdoc cref="docs._headerlength"/>
        public static int? HeaderLength(this AudioFileFormatEnum? obj) => AudioFormatToHeaderLength(obj);
        
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        public static AudioFileFormatEnum? HeaderLength(this AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => oldAudioFormat.AudioFormat(HeaderLengthToAudioFormat(newHeaderLength));
        
        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] 
        public static int HeaderLength(this AudioFileFormat obj) => obj.ToEnum().AudioFormatToHeaderLength();
        
        /// <inheritdoc cref="docs._headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormat HeaderLength(this AudioFileFormat obj, int headerLength, IContext context) => obj.AudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        
        // Conversion-Style
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int? AudioFormatToHeaderLength(this AudioFileFormatEnum? audioFormat) 
            => ConfigWishes.AudioFormatToHeaderLength(audioFormat);

        /// <inheritdoc cref="docs._headerlength"/>
        public static int AudioFormatToHeaderLength(this AudioFileFormatEnum audioFormat)
            => ConfigWishes.AudioFormatToHeaderLength(audioFormat);
    
        public static AudioFileFormatEnum? HeaderLengthToAudioFormat(this int? headerLength) 
            => ConfigWishes.HeaderLengthToAudioFormat(headerLength);

        public static AudioFileFormatEnum HeaderLengthToAudioFormat(this int headerLength)
            => ConfigWishes.HeaderLengthToAudioFormat(headerLength);

        // Synonyms

        public static AudioFileFormatEnum? AudioFormat(this int? headerLength) => HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum  AudioFormat(this int  headerLength) => HeaderLengthToAudioFormat(headerLength);
    }

    public partial class ConfigWishes
    {
        // Constants
        
        public const int WavHeaderLength = 44;
        public const int RawHeaderLength = 0;
        
        // Conversion-Style
        
        public static int? AudioFormatToHeaderLength(AudioFileFormatEnum? audioFormat) 
            => AudioFormatToHeaderLength(audioFormat.Coalesce());
        
        public static int AudioFormatToHeaderLength(AudioFileFormatEnum audioFormat)
        {
            if (audioFormat == Wav) return WavHeaderLength;
            if (audioFormat == Raw) return RawHeaderLength;
            AssertAudioFormat(audioFormat); return default;
        }
        
        public static AudioFileFormatEnum? HeaderLengthToAudioFormat(int? headerLength)
            => HeaderLengthToAudioFormat(headerLength.CoalesceHeaderLength());
        
        public static AudioFileFormatEnum HeaderLengthToAudioFormat(int headerLength)
        {
            if (headerLength == WavHeaderLength) return Wav;
            if (headerLength == RawHeaderLength) return Raw;
            AssertHeaderLength(headerLength); return default;
        }

        // Synonyms

        public static int? HeaderLength(AudioFileFormatEnum? audioFormat) => AudioFormatToHeaderLength(audioFormat);
        public static int  HeaderLength(AudioFileFormatEnum  audioFormat) => AudioFormatToHeaderLength(audioFormat);
        public static AudioFileFormatEnum? AudioFormat(int? headerLength) => HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum  AudioFormat(int  headerLength) => HeaderLengthToAudioFormat(headerLength);
    }
}
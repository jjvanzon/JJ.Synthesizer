using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // HeaderLength: Derived from AudioFormat

    /// <inheritdoc cref="_configextensionwishes"/>
    public static class HeaderLengthExtensionWishes
    {
        // Synth-Bound
        
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this SynthWishes obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this FlowNode obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this FlowNode obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static FlowNode HeaderLength(this FlowNode obj, int? headerLength) => ConfigWishes.HeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static FlowNode WithHeaderLength(this FlowNode obj, int? headerLength) => ConfigWishes.WithHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static FlowNode SetHeaderLength(this FlowNode obj, int? headerLength) => ConfigWishes.SetHeaderLength(obj, headerLength);

        /// <inheritdoc cref="_headerlength"/>
        internal static int HeaderLength(this ConfigResolver obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        internal static int GetHeaderLength(this ConfigResolver obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        internal static ConfigResolver HeaderLength(this ConfigResolver obj, int? headerLength) => ConfigWishes.HeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        internal static ConfigResolver WithHeaderLength(this ConfigResolver obj, int? headerLength) => ConfigWishes.WithHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        internal static ConfigResolver SetHeaderLength(this ConfigResolver obj, int? headerLength) => ConfigWishes.SetHeaderLength(obj, headerLength);

        // Global-Bound

        /// <inheritdoc cref="_headerlength"/>
        internal static int? HeaderLength(this ConfigSection obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        internal static int? GetHeaderLength(this ConfigSection obj) => ConfigWishes.GetHeaderLength(obj);

        // Tape-Bound

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this Tape obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this Tape obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static Tape HeaderLength(this Tape obj, int headerLength) => ConfigWishes.HeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static Tape WithHeaderLength(this Tape obj, int headerLength) => ConfigWishes.WithHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static Tape SetHeaderLength(this Tape obj, int headerLength) => ConfigWishes.SetHeaderLength(obj, headerLength);

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this TapeConfig obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this TapeConfig obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static TapeConfig HeaderLength(this TapeConfig obj, int headerLength) => ConfigWishes.HeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeConfig WithHeaderLength(this TapeConfig obj, int headerLength) => ConfigWishes.WithHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeConfig SetHeaderLength(this TapeConfig obj, int headerLength) => ConfigWishes.SetHeaderLength(obj, headerLength);

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this TapeAction obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this TapeAction obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static TapeAction HeaderLength(this TapeAction obj, int headerLength) => ConfigWishes.HeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeAction WithHeaderLength(this TapeAction obj, int headerLength) => ConfigWishes.WithHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeAction SetHeaderLength(this TapeAction obj, int headerLength) => ConfigWishes.SetHeaderLength(obj, headerLength);

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this TapeActions obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this TapeActions obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static TapeActions HeaderLength(this TapeActions obj, int headerLength) => ConfigWishes.HeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeActions WithHeaderLength(this TapeActions obj, int headerLength) => ConfigWishes.WithHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeActions SetHeaderLength(this TapeActions obj, int headerLength) => ConfigWishes.SetHeaderLength(obj, headerLength);

        // Buff-Bound

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this Buff obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this Buff obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static Buff HeaderLength(this Buff obj, int headerLength, IContext context) => ConfigWishes.HeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Buff WithHeaderLength(this Buff obj, int headerLength, IContext context) => ConfigWishes.WithHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Buff SetHeaderLength(this Buff obj, int headerLength, IContext context) => ConfigWishes.SetHeaderLength(obj, headerLength, context);

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileOutput obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static AudioFileOutput HeaderLength(this AudioFileOutput obj, int headerLength, IContext context) => ConfigWishes.HeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static AudioFileOutput WithHeaderLength(this AudioFileOutput obj, int headerLength, IContext context) => ConfigWishes.WithHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static AudioFileOutput SetHeaderLength(this AudioFileOutput obj, int headerLength, IContext context) => ConfigWishes.SetHeaderLength(obj, headerLength, context);

        // Independent after Taping

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this Sample obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this Sample obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static Sample HeaderLength(this Sample obj, int headerLength, IContext context) => ConfigWishes.HeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Sample WithHeaderLength(this Sample obj, int headerLength, IContext context) => ConfigWishes.WithHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Sample SetHeaderLength(this Sample obj, int headerLength, IContext context) => ConfigWishes.SetHeaderLength(obj, headerLength, context);

        // Immutable

        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this WavHeaderStruct obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this WavHeaderStruct obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlength"/>
        public static int? HeaderLength(this AudioFileFormatEnum? obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int? GetHeaderLength(this AudioFileFormatEnum? obj) => ConfigWishes.GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileFormatEnum obj) => ConfigWishes.GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int? ToHeaderLength(this AudioFileFormatEnum? obj) => ConfigWishes.ToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int ToHeaderLength(this AudioFileFormatEnum obj) => ConfigWishes.ToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int? AudioFormatToHeaderLength(this AudioFileFormatEnum? audioFormat) => ConfigWishes.AudioFormatToHeaderLength(audioFormat);
        /// <inheritdoc cref="_headerlength"/>
        public static int AudioFormatToHeaderLength(this AudioFileFormatEnum audioFormat) => ConfigWishes.AudioFormatToHeaderLength(audioFormat);

        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum? HeaderLength(this AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => ConfigWishes.HeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum HeaderLength(this AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => ConfigWishes.HeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum? WithHeaderLength(this AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => ConfigWishes.WithHeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum WithHeaderLength(this AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => ConfigWishes.WithHeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum? SetHeaderLength(this AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => ConfigWishes.SetHeaderLength(oldAudioFormat, newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum SetHeaderLength(this AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => ConfigWishes.SetHeaderLength(oldAudioFormat, newHeaderLength);
        public static AudioFileFormatEnum? AudioFormat(this int? headerLength) => ConfigWishes.AudioFormat(headerLength);
        public static AudioFileFormatEnum AudioFormat(this int headerLength) => ConfigWishes.AudioFormat(headerLength);
        public static AudioFileFormatEnum? ToAudioFormat(this int? headerLength) => ConfigWishes.ToAudioFormat(headerLength);
        public static AudioFileFormatEnum ToAudioFormat(this int headerLength) => ConfigWishes.ToAudioFormat(headerLength);
        public static AudioFileFormatEnum? HeaderLengthToAudioFormat(this int? headerLength) => ConfigWishes.HeaderLengthToAudioFormat(headerLength);
        public static AudioFileFormatEnum HeaderLengthToAudioFormat(this int headerLength) => ConfigWishes.HeaderLengthToAudioFormat(headerLength);

        /// <inheritdoc cref="_headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int HeaderLength(this AudioFileFormat obj) => ConfigWishes.HeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int ToHeaderLength(this AudioFileFormat obj) => ConfigWishes.ToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int GetHeaderLength(this AudioFileFormat obj) => ConfigWishes.GetHeaderLength(obj);

        /// <inheritdoc cref="_headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat HeaderLength(this AudioFileFormat obj, int headerLength, IContext context)
            => ConfigWishes.HeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat WithHeaderLength(this AudioFileFormat obj, int headerLength, IContext context)
            => ConfigWishes.WithHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat SetHeaderLength(this AudioFileFormat obj, int headerLength, IContext context)
            => ConfigWishes.SetHeaderLength(obj, headerLength, context);
    }

    public partial class ConfigWishes
    {
        // Constants
        
        public const int WavHeaderLength = 44;
        public const int RawHeaderLength = 0;
     
        // Synth-Bound
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(SynthWishes obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(SynthWishes obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static SynthWishes HeaderLength(SynthWishes obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static SynthWishes WithHeaderLength(SynthWishes obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static SynthWishes SetHeaderLength(SynthWishes obj, int? headerLength)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(FlowNode obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(FlowNode obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static FlowNode HeaderLength(FlowNode obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static FlowNode WithHeaderLength(FlowNode obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static FlowNode SetHeaderLength(FlowNode obj, int? headerLength)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="_headerlength"/>
        internal static int HeaderLength(ConfigResolver obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        internal static int GetHeaderLength(ConfigResolver obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        internal static ConfigResolver HeaderLength(ConfigResolver obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        internal static ConfigResolver WithHeaderLength(ConfigResolver obj, int? headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        internal static ConfigResolver SetHeaderLength(ConfigResolver obj, int? headerLength)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        // Global-Bound
        
        /// <inheritdoc cref="_headerlength"/>
        internal static int? HeaderLength(ConfigSection obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        internal static int? GetHeaderLength(ConfigSection obj)
        {
            return obj.AudioFormat()?.HeaderLength();
        }
        
        // Tape-Bound
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(Tape obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(Tape obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static Tape HeaderLength(Tape obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static Tape WithHeaderLength(Tape obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static Tape SetHeaderLength(Tape obj, int headerLength)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(TapeConfig obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(TapeConfig obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static TapeConfig HeaderLength(TapeConfig obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeConfig WithHeaderLength(TapeConfig obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeConfig SetHeaderLength(TapeConfig obj, int headerLength)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(TapeAction obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(TapeAction obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static TapeAction HeaderLength(TapeAction obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeAction WithHeaderLength(TapeAction obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeAction SetHeaderLength(TapeAction obj, int headerLength)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(TapeActions obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(TapeActions obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static TapeActions HeaderLength(TapeActions obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeActions WithHeaderLength(TapeActions obj, int headerLength) => SetHeaderLength(obj, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public static TapeActions SetHeaderLength(TapeActions obj, int headerLength)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength));
        }
        
        // Buff-Bound
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(Buff obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(Buff obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static Buff HeaderLength(Buff obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Buff WithHeaderLength(Buff obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Buff SetHeaderLength(Buff obj, int headerLength, IContext context)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(AudioFileOutput obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(AudioFileOutput obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static AudioFileOutput HeaderLength(AudioFileOutput obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static AudioFileOutput WithHeaderLength(AudioFileOutput obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static AudioFileOutput SetHeaderLength(AudioFileOutput obj, int headerLength, IContext context)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
        
        // Independent after Taping
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(Sample obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(Sample obj)
        {
            return obj.AudioFormat().HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static Sample HeaderLength(Sample obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Sample WithHeaderLength(Sample obj, int headerLength, IContext context) => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlength"/>
        public static Sample SetHeaderLength(Sample obj, int headerLength, IContext context)
        {
            return obj.SetAudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
        
        // Immutable
        
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(WavHeaderStruct obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(WavHeaderStruct obj)
        {
            return Wav.HeaderLength();
        }
        
        /// <inheritdoc cref="_headerlength"/>
        public static int? HeaderLength(AudioFileFormatEnum? obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int HeaderLength(AudioFileFormatEnum obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int? GetHeaderLength(AudioFileFormatEnum? obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(AudioFileFormatEnum obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int? ToHeaderLength(AudioFileFormatEnum? obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int ToHeaderLength(AudioFileFormatEnum obj) => AudioFormatToHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        public static int? AudioFormatToHeaderLength(AudioFileFormatEnum? audioFormat) => AudioFormatToHeaderLength(audioFormat.Coalesce());
        /// <inheritdoc cref="_headerlength"/>
        public static int AudioFormatToHeaderLength(AudioFileFormatEnum audioFormat)
        {
            if (audioFormat == Wav) return WavHeaderLength;
            if (audioFormat == Raw) return RawHeaderLength;
            AssertAudioFormat(audioFormat); return default;
        }

        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum? HeaderLength(AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum HeaderLength(AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum? WithHeaderLength(AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum WithHeaderLength(AudioFileFormatEnum oldAudioFormat, int newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        public static AudioFileFormatEnum? SetHeaderLength(AudioFileFormatEnum? oldAudioFormat, int? newHeaderLength) => HeaderLengthToAudioFormat(newHeaderLength);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
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
        
        /// <inheritdoc cref="_headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int HeaderLength(AudioFileFormat obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int ToHeaderLength(AudioFileFormat obj) => GetHeaderLength(obj);
        /// <inheritdoc cref="_headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int GetHeaderLength(AudioFileFormat obj)
        {
            return obj.ToEnum().AudioFormatToHeaderLength();
        }
        
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat HeaderLength(AudioFileFormat obj, int headerLength, IContext context)
            => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat WithHeaderLength(AudioFileFormat obj, int headerLength, IContext context)
            => SetHeaderLength(obj, headerLength, context);
        /// <inheritdoc cref="_headerlengthquasisetter"/>
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat SetHeaderLength(AudioFileFormat obj, int headerLength, IContext context)
        {
            return obj.WithAudioFormat(HeaderLengthToAudioFormat(headerLength), context);
        }
    }
}
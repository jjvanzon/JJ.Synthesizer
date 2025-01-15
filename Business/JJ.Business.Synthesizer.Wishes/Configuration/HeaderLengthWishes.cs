using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
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
        public static int HeaderLength(this FlowNode obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int HeaderLength(this ConfigResolver obj) => obj.AudioFormat().HeaderLength();
        
        // Global-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int? HeaderLength(this ConfigSection obj) => obj.AudioFormat()?.HeaderLength();
        
        // Tape-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Buff obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Tape obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeConfig obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeAction obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeActions obj) => obj.AudioFormat().HeaderLength();

        // Buff-Bound
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => obj.AudioFormat().HeaderLength();

        // Independent after Taping
        
        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int HeaderLength(this WavHeaderStruct obj) => Wav.HeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj) => ConfigWishes.HeaderLength(obj);
        
        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] 
        public static int HeaderLength(this AudioFileFormat obj) => obj.AudioFormat().HeaderLength();
    }

    public partial class ConfigWishes
    {
        // Conversion Formulas

        public static int HeaderLength(AudioFileFormatEnum? audioFormat) => HeaderLength(Coalesce(audioFormat));
        
        public static int HeaderLength(AudioFileFormatEnum audioFormat)
        {
            switch (Assert(audioFormat))
            {
                case Wav: return 44;
                case Raw: return 0;
                default: return default; // ncrunch: no coverage
            }
        }
    } 
}
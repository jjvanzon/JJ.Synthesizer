using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static partial class ConfigExtensionWishes
    {
        // A Derived Attribute
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this SynthWishes obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this FlowNode obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this ConfigWishes obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int? HeaderLength(this ConfigSection obj) => obj.AudioFormat()?.HeaderLength();
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
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => obj.AudioFormat().HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int HeaderLength(this WavHeaderStruct obj) => Wav.HeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case Wav: return 44;
                case Raw: return 0;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] 
        public static int HeaderLength(this AudioFileFormat obj) => obj.AudioFormat().HeaderLength();
    }
}
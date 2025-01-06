using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
        #region HeaderLength
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this SynthWishes obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this FlowNode obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this ConfigWishes obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int HeaderLength(this ConfigSection obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Buff obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Tape obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeConfig obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeAction obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeActions obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int HeaderLength(this WavHeaderStruct obj) => HeaderLength(AudioFileFormatEnum.Wav);
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case AudioFileFormatEnum.Wav: return 44;
                case AudioFileFormatEnum.Raw: return 0;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static int HeaderLength(this AudioFileFormat obj) => AudioFormat(obj).HeaderLength();
        
        #endregion
    }
}
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Helpers.SampleDataTypeHelper;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Derived Attribute
        
        public   static int             SizeOfBitDepth(this SynthWishes     obj) => obj.Bits() / 8;
        public   static SynthWishes     SizeOfBitDepth(this SynthWishes     obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this FlowNode        obj) => obj.Bits() / 8;
        public   static FlowNode        SizeOfBitDepth(this FlowNode        obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this ConfigWishes    obj) => obj.Bits() / 8;
        public   static ConfigWishes    SizeOfBitDepth(this ConfigWishes    obj, int byteSize) => obj.Bits(byteSize * 8);
        internal static int             SizeOfBitDepth(this ConfigSection   obj) => obj.Bits() / 8;
        public   static int             SizeOfBitDepth(this Tape            obj) => obj.Bits() / 8;
        public   static Tape            SizeOfBitDepth(this Tape            obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this TapeConfig      obj) => obj.Bits() / 8;
        public   static TapeConfig      SizeOfBitDepth(this TapeConfig      obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this TapeActions     obj) => obj.Bits() / 8;
        public   static TapeActions     SizeOfBitDepth(this TapeActions     obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this TapeAction      obj) => obj.Bits() / 8;
        public   static TapeAction      SizeOfBitDepth(this TapeAction      obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this Buff            obj) => obj.Bits() / 8;
        public   static Buff            SizeOfBitDepth(this Buff            obj, int byteSize, IContext context) => obj.Bits(byteSize * 8, context);
        public   static int             SizeOfBitDepth(this Sample          obj) => obj.Bits() / 8;
        public   static Sample          SizeOfBitDepth(this Sample          obj, int byteSize, IContext context) => obj.Bits(byteSize * 8, context);
        public   static int             SizeOfBitDepth(this AudioFileOutput obj) => obj.Bits() / 8;
        public   static AudioFileOutput SizeOfBitDepth(this AudioFileOutput obj, int byteSize, IContext context) => obj.Bits(byteSize * 8, context);
        public   static int             SizeOfBitDepth(this WavHeaderStruct obj) => obj.Bits() / 8;
        public   static WavHeaderStruct SizeOfBitDepth(this WavHeaderStruct obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this AudioInfoWish   obj) => obj.Bits() / 8;
        public   static AudioInfoWish   SizeOfBitDepth(this AudioInfoWish   obj, int byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this AudioFileInfo   obj) => obj.Bits() / 8;
        public   static AudioFileInfo   SizeOfBitDepth(this AudioFileInfo   obj, int byteSize) => obj.Bits(byteSize * 8);
        
        public static int SizeOfBitDepth(this int bits) => bits / 8;
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static int SizeOfBitDepth(this int bits, int byteSize) => byteSize;
        public static int SizeOfBitDepth(this Type obj) => obj.TypeToBits() / 8;
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static Type SizeOfBitDepth(this Type obj, int byteSize) => BitsToType(byteSize * 8);
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataTypeEnum obj) => SizeOf(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SizeOfBitDepth(this SampleDataTypeEnum obj, int byteSize) => BitsToEnum(byteSize * 8);
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataType obj) => SizeOf(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataType SizeOfBitDepth(this SampleDataType obj, int byteSize, IContext context) => BitsToEntity(byteSize * 8, context);
    }
}
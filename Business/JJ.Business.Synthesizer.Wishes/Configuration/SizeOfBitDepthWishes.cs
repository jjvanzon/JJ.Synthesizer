using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Helpers.SampleDataTypeHelper;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class SizeOfBitDepthExtensionWishes
    {
        // Derived from Bits
        
        public   static int             SizeOfBitDepth(this SynthWishes     obj) => obj.Bits() / 8;
        public   static SynthWishes     SizeOfBitDepth(this SynthWishes     obj, int? byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this FlowNode        obj) => obj.Bits() / 8;
        public   static FlowNode        SizeOfBitDepth(this FlowNode        obj, int? byteSize) => obj.Bits(byteSize * 8);
        public   static int             SizeOfBitDepth(this ConfigWishes    obj) => obj.Bits() / 8;
        public   static ConfigWishes    SizeOfBitDepth(this ConfigWishes    obj, int? byteSize) => obj.Bits(byteSize * 8);
        internal static int?            SizeOfBitDepth(this ConfigSection   obj) => obj.Bits() / 8;
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
        public static int SizeOfBitDepth(this int oldBits, int newByteSize) => newByteSize * 8;

        public static int SizeOfBitDepth(this Type obj) => obj.TypeToBits() / 8;
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepth(this Type oldType, int newByteSize) => newByteSize.SizeOfBitDepthToType();

        [Obsolete(ObsoleteMessage)] 
        public static int SizeOfBitDepth(this SampleDataTypeEnum obj) => SizeOf(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum SizeOfBitDepth(this SampleDataTypeEnum oldEnumValue, int newByteSize) => (newByteSize * 8).BitsToEnum();
        
        [Obsolete(ObsoleteMessage)] 
        public static int SizeOfBitDepth(this SampleDataType obj) => SizeOf(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType SizeOfBitDepth(this SampleDataType oldSampleDataType, int newByteSize, IContext context) => (newByteSize * 8).BitsToEntity(context);

        // Conversion-Style
        
        public static int TypeToSizeOfBitDepth(this Type obj)
        {
            if (obj == typeof(byte)) return 1;
            if (obj == typeof(Int16)) return 2;
            if (obj == typeof(float)) return 4;
            throw new ValueNotSupportedException(obj);
        }
        
        public static Type SizeOfBitDepthToType(this int value)
        {
            switch (value)
            {
                case 1: return typeof(byte);
                case 2: return typeof(Int16);
                case 4: return typeof(float);
                default: throw new ValueNotSupportedException(value);
            }
        }

    }

    // With Type Arguments
    
    public partial class ConfigWishes
    {
        public static int TypeToSizeOfBitDepth<T>() => typeof(T).TypeToSizeOfBitDepth();
                 
        public static int SizeOfBitDepth<TValueType>() => TypeToSizeOfBitDepth<TValueType>();
                 
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepth<TValueType>(int value) => value.SizeOfBitDepthToType();
  }
}
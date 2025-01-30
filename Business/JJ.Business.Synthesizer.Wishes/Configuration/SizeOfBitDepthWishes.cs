using System;
using JetBrains.Annotations;
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
// ReSharper disable UnusedTypeParameter

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class SizeOfBitDepthExtensionWishes
    {
        // Derived from Bits
        
        // Synth-Bound
        
        public   static int             SizeOfBitDepth(this SynthWishes     obj) => obj.Bits().ToSizeOfBitDepth();
        public   static SynthWishes     SizeOfBitDepth(this SynthWishes     obj, int? sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        public   static int             SizeOfBitDepth(this FlowNode        obj) => obj.Bits().ToSizeOfBitDepth();
        public   static FlowNode        SizeOfBitDepth(this FlowNode        obj, int? sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        internal static int             SizeOfBitDepth(this ConfigResolver  obj) => obj.Bits().ToSizeOfBitDepth();
        [UsedImplicitly]
        internal static ConfigResolver  SizeOfBitDepth(this ConfigResolver  obj, int? sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        
        // Global-Bound
        
        internal static int?            SizeOfBitDepth(this ConfigSection   obj) => obj.Bits().ToSizeOfBitDepth();
        
        // Tape-Bound
        
        public   static int             SizeOfBitDepth(this Tape            obj) => obj.Bits().ToSizeOfBitDepth();
        public   static Tape            SizeOfBitDepth(this Tape            obj, int sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        public   static int             SizeOfBitDepth(this TapeConfig      obj) => obj.Bits().ToSizeOfBitDepth();
        public   static TapeConfig      SizeOfBitDepth(this TapeConfig      obj, int sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        public   static int             SizeOfBitDepth(this TapeActions     obj) => obj.Bits().ToSizeOfBitDepth();
        public   static TapeActions     SizeOfBitDepth(this TapeActions     obj, int sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        public   static int             SizeOfBitDepth(this TapeAction      obj) => obj.Bits().ToSizeOfBitDepth();
        public   static TapeAction      SizeOfBitDepth(this TapeAction      obj, int sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        
        // Buff-Bound
        
        public   static int             SizeOfBitDepth(this Buff            obj) => obj.Bits().ToSizeOfBitDepth();
        public   static Buff            SizeOfBitDepth(this Buff            obj, int sizeOfBitDepth, IContext context) => obj.Bits(sizeOfBitDepth.ToBits(), context);
        public   static int             SizeOfBitDepth(this AudioFileOutput obj) => obj.Bits().ToSizeOfBitDepth();
        public   static AudioFileOutput SizeOfBitDepth(this AudioFileOutput obj, int sizeOfBitDepth, IContext context) => obj.Bits(sizeOfBitDepth.ToBits(), context);

        // Independent after Taping
        
        public   static int             SizeOfBitDepth(this Sample          obj) => obj.Bits().ToSizeOfBitDepth();
        public   static Sample          SizeOfBitDepth(this Sample          obj, int sizeOfBitDepth, IContext context) => obj.Bits(sizeOfBitDepth.ToBits(), context);
        public   static int             SizeOfBitDepth(this AudioInfoWish   obj) => obj.Bits().ToSizeOfBitDepth();
        public   static AudioInfoWish   SizeOfBitDepth(this AudioInfoWish   obj, int sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());
        public   static int             SizeOfBitDepth(this AudioFileInfo   obj) => obj.Bits().ToSizeOfBitDepth();
        public   static AudioFileInfo   SizeOfBitDepth(this AudioFileInfo   obj, int sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());

        // Immutable
        
        public   static int             SizeOfBitDepth(this WavHeaderStruct obj) => obj.Bits().ToSizeOfBitDepth();
        public   static WavHeaderStruct SizeOfBitDepth(this WavHeaderStruct obj, int sizeOfBitDepth) => obj.Bits(sizeOfBitDepth.ToBits());

        [Obsolete(ObsoleteMessage)] 
        public static int SizeOfBitDepth(this SampleDataTypeEnum obj) => SizeOf(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum SizeOfBitDepth(this SampleDataTypeEnum oldEnumValue, int newByteSize)
            => newByteSize.Bits().BitsToEnum();
        
        [Obsolete(ObsoleteMessage)] 
        public static int SizeOfBitDepth(this SampleDataType obj) => SizeOf(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType SizeOfBitDepth(this SampleDataType oldSampleDataType, int newByteSize, IContext context) 
            => newByteSize.Bits().BitsToEntity(context);

        public static int SizeOfBitDepth(this Type obj) => TypeToSizeOfBitDepth(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepth(this Type oldType, int newByteSize) => SizeOfBitDepthToType(newByteSize);

        /// <inheritdoc cref="docs._quasisetter" />
        public static int SizeOfBitDepth(this int oldBits, int newByteSize) => SizeOfBitDepthToBits(newByteSize);

        // Conversion-Style
        
        public static int  TypeToSizeOfBitDepth(this Type obj ) => ConfigWishes.TypeToSizeOfBitDepth(obj);
        public static Type SizeOfBitDepthToType(this int value) => ConfigWishes.SizeOfBitDepthToType(value);
        
        public static int  SizeOfBitDepthToBits(this int  sizeOfBitDepth) => ConfigWishes.SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? SizeOfBitDepthToBits(this int? sizeOfBitDepth) => ConfigWishes.SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int  BitsToSizeOfBitDepth(this int  bits)           => ConfigWishes.BitsToSizeOfBitDepth(bits);
        public static int? BitsToSizeOfBitDepth(this int? bits)           => ConfigWishes.BitsToSizeOfBitDepth(bits);
        
        public static int  Bits(this int  sizeOfBitDepth) => ConfigWishes.Bits(sizeOfBitDepth);
        public static int? Bits(this int? sizeOfBitDepth) => ConfigWishes.Bits(sizeOfBitDepth);
        public static int  SizeOfBitDepth(this int  bits) => ConfigWishes.SizeOfBitDepth(bits);
        public static int? SizeOfBitDepth(this int? bits) => ConfigWishes.SizeOfBitDepth(bits);

        public static int  ToBits(this int  sizeOfBitDepth) => ConfigWishes.ToBits(sizeOfBitDepth);
        public static int? ToBits(this int? sizeOfBitDepth) => ConfigWishes.ToBits(sizeOfBitDepth);
        public static int  ToSizeOfBitDepth(this int  bits) => ConfigWishes.ToSizeOfBitDepth(bits);
        public static int? ToSizeOfBitDepth(this int? bits) => ConfigWishes.ToSizeOfBitDepth(bits);

        public static int  GetBits(this int  sizeOfBitDepth) => ConfigWishes.GetBits(sizeOfBitDepth);
        public static int? GetBits(this int? sizeOfBitDepth) => ConfigWishes.GetBits(sizeOfBitDepth);
        public static int  GetSizeOfBitDepth(this int  bits) => ConfigWishes.GetSizeOfBitDepth(bits);
        public static int? GetSizeOfBitDepth(this int? bits) => ConfigWishes.GetSizeOfBitDepth(bits);
    }

    public partial class ConfigWishes
    {
        // Conversion-Style

        public static int TypeToSizeOfBitDepth(Type obj)
        {
            if (obj == typeof(byte)) return 1;
            if (obj == typeof(short)) return 2;
            if (obj == typeof(float)) return 4;
            throw new ValueNotSupportedException(obj);
        }
        
        public static Type SizeOfBitDepthToType(int value)
        {
            switch (value)
            {
                case 1: return typeof(byte);
                case 2: return typeof(short);
                case 4: return typeof(float);
            }
            AssertSizeOfBitDepth(value, strict: false); return default;
        }
        
        public static int  BitsToSizeOfBitDepth(int  bits) => AssertBits(bits, strict: false) / 8;
        public static int? BitsToSizeOfBitDepth(int? bits) => AssertBits(bits, strict: false) / 8;
        public static int  SizeOfBitDepthToBits(int  sizeOfBitDepth) => AssertSizeOfBitDepth(sizeOfBitDepth, strict: false) * 8;
        public static int? SizeOfBitDepthToBits(int? sizeOfBitDepth) => AssertSizeOfBitDepth(sizeOfBitDepth, strict: false) * 8;
        
        // Synonyms
        
        public static int  SizeOfBitDepth(int  bits) => BitsToSizeOfBitDepth(bits);
        public static int? SizeOfBitDepth(int? bits) => BitsToSizeOfBitDepth(bits);
        public static int  Bits(int  sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? Bits(int? sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);

        public static int  ToSizeOfBitDepth(int  bits) => BitsToSizeOfBitDepth(bits);
        public static int? ToSizeOfBitDepth(int? bits) => BitsToSizeOfBitDepth(bits);
        public static int  ToBits(int  sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? ToBits(int? sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);

        public static int  GetSizeOfBitDepth(int  bits) => BitsToSizeOfBitDepth(bits);
        public static int? GetSizeOfBitDepth(int? bits) => BitsToSizeOfBitDepth(bits);
        public static int  GetBits(int  sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? GetBits(int? sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);

        // Type Arguments
        
        public static int TypeToSizeOfBitDepth<TValueType>() => TypeToSizeOfBitDepth(typeof(TValueType));
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepthToType<TValueType>(int value) => SizeOfBitDepthToType(value);

        // Synonyms
        
        public static int SizeOfBitDepth<TValueType>() => TypeToSizeOfBitDepth<TValueType>();
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepth<TValueType>(int value) => SizeOfBitDepthToType<TValueType>(value);
  }
}
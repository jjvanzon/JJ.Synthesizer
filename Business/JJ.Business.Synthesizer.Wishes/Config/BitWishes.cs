using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedTypeParameter

#pragma warning disable CS0618

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class BitExtensionWishes
    {
        // A Primary Audio Attribute

        // Synth-Bound

        public static int Bits(this SynthWishes obj) => GetBits(obj);
        public static int GetBits(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        public static SynthWishes SetBits(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        public static SynthWishes Bits(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }
        
        public static int Bits(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        public static FlowNode Bits(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }
        
        internal static int Bits(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        internal static ConfigResolver Bits(this ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }
        
        // Global-Bound
        
        internal static int? Bits(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        // Tape-Bound
        
        public static int Bits(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Bits;
        }
        
        public static Tape Bits(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Bits = value;
            return obj;
        }
        
        public static int Bits(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        public static TapeConfig Bits(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = value;
            return obj;
        }
        
        public static int Bits(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }
        
        public static TapeActions Bits(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }
        
        public static int Bits(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }
        
        public static TapeAction Bits(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static int Bits(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.UnderlyingAudioFileOutput.Bits();
        }
        
        public static Buff Bits(this Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.Bits(value, context);
            return obj;
        }
        
        public static int Bits(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSampleDataTypeEnum().EnumToBits();
        }
        
        public static AudioFileOutput Bits(this AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }
        
        // Independent after Taping
        
        public static int Bits(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSampleDataTypeEnum().EnumToBits();
        }
        
        public static Sample Bits(this Sample obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }
        
        public static int Bits(this AudioInfoWish obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        public static AudioInfoWish Bits(this AudioInfoWish obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = AssertBits(value, strict: false);
            return obj;
        }
        
        public static int Bits(this AudioFileInfo obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.BytesPerValue.Bits();
        }
        
        public static AudioFileInfo Bits(this AudioFileInfo obj, int bits)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.BytesPerValue = bits.SizeOfBitDepth();
            return obj;
        }

        // Immutable        
        
        public static int Bits(this WavHeaderStruct obj) => obj.BitsPerValue;
        
        public static WavHeaderStruct Bits(this WavHeaderStruct obj, int value) => obj.ToWish().Bits(value).ToWavHeader();

        public static int Bits(this Type valueType) => valueType.TypeToBits();
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Bits(this Type oldValueType, int newBits) => newBits.BitsToType();
        
        [Obsolete(ObsoleteMessage)]
        public static int Bits(this SampleDataTypeEnum obj) => obj.EnumToBits();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum Bits(this SampleDataTypeEnum oldEnumValue, int newBits) => newBits.BitsToEnum();
        
        [Obsolete(ObsoleteMessage)]
        public static int Bits(this SampleDataType obj) => obj.EntityToBits();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Bits(this SampleDataType oldSampleDataType, int newBits, IContext context) 
            => newBits.BitsToEntity(context);
        
        // Shorthand
        
        public   static bool Is8Bit (this SynthWishes        obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this FlowNode           obj) => obj.Bits()     == 8;
        [UsedImplicitly]
        internal static bool Is8Bit (this ConfigResolver     obj) => obj.Bits()     == 8;
        [UsedImplicitly]
        internal static bool Is8Bit (this ConfigSection      obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this Tape               obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this TapeConfig         obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this TapeActions        obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this TapeAction         obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this Buff               obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this Sample             obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this AudioFileOutput    obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this WavHeaderStruct    obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this AudioInfoWish      obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this AudioFileInfo      obj) => obj.Bits()     == 8;
        [Obsolete(ObsoleteMessage)]
        public   static bool Is8Bit (this SampleDataTypeEnum obj) => obj.Bits()     == 8;
        [Obsolete(ObsoleteMessage)]
        public   static bool Is8Bit (this SampleDataType     obj) => obj.Bits()     == 8;
        public   static bool Is8Bit (this Type               obj) => obj.Bits()     == 8;
        
        public   static bool Is16Bit(this SynthWishes        obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this FlowNode           obj) => obj.Bits()     == 16;
        [UsedImplicitly]
        internal static bool Is16Bit(this ConfigResolver     obj) => obj.Bits()     == 16;
        [UsedImplicitly]
        internal static bool Is16Bit(this ConfigSection      obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this Tape               obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this TapeConfig         obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this TapeActions        obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this TapeAction         obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this Buff               obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this Sample             obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this AudioFileOutput    obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this WavHeaderStruct    obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this AudioInfoWish      obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this AudioFileInfo      obj) => obj.Bits()     == 16;
        [Obsolete(ObsoleteMessage)]
        public   static bool Is16Bit(this SampleDataTypeEnum obj) => obj.Bits()     == 16;
        [Obsolete(ObsoleteMessage)]
        public   static bool Is16Bit(this SampleDataType     obj) => obj.Bits()     == 16;
        public   static bool Is16Bit(this Type               obj) => obj.Bits()     == 16;
        
        public   static bool Is32Bit(this SynthWishes        obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this FlowNode           obj) => obj.Bits()     == 32;
        [UsedImplicitly]
        internal static bool Is32Bit(this ConfigResolver     obj) => obj.Bits()     == 32;
        [UsedImplicitly]
        internal static bool Is32Bit(this ConfigSection      obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this Tape               obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this TapeConfig         obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this TapeActions        obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this TapeAction         obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this Buff               obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this Sample             obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this AudioFileOutput    obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this WavHeaderStruct    obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this AudioInfoWish      obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this AudioFileInfo      obj) => obj.Bits()     == 32;
        [Obsolete(ObsoleteMessage)]
        public   static bool Is32Bit(this SampleDataTypeEnum obj) => obj.Bits()     == 32;
        [Obsolete(ObsoleteMessage)]
        public   static bool Is32Bit(this SampleDataType     obj) => obj.Bits()     == 32;
        public   static bool Is32Bit(this Type               obj) => obj.Bits()     == 32;
        
        public   static SynthWishes     With8Bit (this SynthWishes     obj)                   => obj.Bits(8);
        public   static FlowNode        With8Bit (this FlowNode        obj)                   => obj.Bits(8);
        [UsedImplicitly]
        internal static ConfigResolver  With8Bit (this ConfigResolver  obj)                   => obj.Bits(8);
        public   static Tape            With8Bit (this Tape            obj)                   => obj.Bits(8);
        public   static TapeConfig      With8Bit (this TapeConfig      obj)                   => obj.Bits(8);
        public   static TapeActions     With8Bit (this TapeActions     obj)                   => obj.Bits(8);
        public   static TapeAction      With8Bit (this TapeAction      obj)                   => obj.Bits(8);
        public   static Buff            With8Bit (this Buff            obj, IContext context) => obj.Bits(8, context);
        public   static Sample          With8Bit (this Sample          obj, IContext context) => obj.Bits(8, context);
        public   static AudioFileOutput With8Bit (this AudioFileOutput obj, IContext context) => obj.Bits(8, context);
        public   static WavHeaderStruct With8Bit (this WavHeaderStruct obj)                   => obj.Bits(8);
        public   static AudioInfoWish   With8Bit (this AudioInfoWish   obj)                   => obj.Bits(8);
        public   static AudioFileInfo   With8Bit (this AudioFileInfo   obj)                   => obj.Bits(8);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit(this Type oldValueType) => oldValueType.Bits(8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum With8Bit(this SampleDataTypeEnum oldEnumValue) => oldEnumValue.Bits(8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With8Bit(this SampleDataType oldSampleDataType, IContext context) => oldSampleDataType.Bits(8, context);
        
        public   static SynthWishes     With16Bit(this SynthWishes     obj)                   => obj.Bits(16);
        public   static FlowNode        With16Bit(this FlowNode        obj)                   => obj.Bits(16);
        [UsedImplicitly]
        internal static ConfigResolver  With16Bit(this ConfigResolver  obj)                   => obj.Bits(16);
        public   static Tape            With16Bit(this Tape            obj)                   => obj.Bits(16);
        public   static TapeConfig      With16Bit(this TapeConfig      obj)                   => obj.Bits(16);
        public   static TapeActions     With16Bit(this TapeActions     obj)                   => obj.Bits(16);
        public   static TapeAction      With16Bit(this TapeAction      obj)                   => obj.Bits(16);
        public   static Buff            With16Bit(this Buff            obj, IContext context) => obj.Bits(16, context);
        public   static Sample          With16Bit(this Sample          obj, IContext context) => obj.Bits(16, context);
        public   static AudioFileOutput With16Bit(this AudioFileOutput obj, IContext context) => obj.Bits(16, context);
        public   static WavHeaderStruct With16Bit(this WavHeaderStruct obj)                   => obj.Bits(16);
        public   static AudioInfoWish   With16Bit(this AudioInfoWish   obj)                   => obj.Bits(16);
        public   static AudioFileInfo   With16Bit(this AudioFileInfo   obj)                   => obj.Bits(16);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit(this Type oldValueType) => oldValueType.Bits(16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum With16Bit(this SampleDataTypeEnum oldEnumValue) => oldEnumValue.Bits(16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With16Bit(this SampleDataType oldSampleDataType, IContext context) => oldSampleDataType.Bits(16, context);
        
        public   static SynthWishes     With32Bit(this SynthWishes     obj)                   => obj.Bits(32);
        public   static FlowNode        With32Bit(this FlowNode        obj)                   => obj.Bits(32);
        [UsedImplicitly]
        internal static ConfigResolver  With32Bit(this ConfigResolver  obj)                   => obj.Bits(32);
        public   static Tape            With32Bit(this Tape            obj)                   => obj.Bits(32);
        public   static TapeConfig      With32Bit(this TapeConfig      obj)                   => obj.Bits(32);
        public   static TapeActions     With32Bit(this TapeActions     obj)                   => obj.Bits(32);
        public   static TapeAction      With32Bit(this TapeAction      obj)                   => obj.Bits(32);
        public   static Buff            With32Bit(this Buff            obj, IContext context) => obj.Bits(32, context);
        public   static Sample          With32Bit(this Sample          obj, IContext context) => obj.Bits(32, context);
        public   static AudioFileOutput With32Bit(this AudioFileOutput obj, IContext context) => obj.Bits(32, context);
        public   static WavHeaderStruct With32Bit(this WavHeaderStruct obj)                   => obj.Bits(32);
        public   static AudioInfoWish   With32Bit(this AudioInfoWish   obj)                   => obj.Bits(32);
        public   static AudioFileInfo   With32Bit(this AudioFileInfo   obj)                   => obj.Bits(32);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit(this Type oldValueType) => oldValueType.Bits(32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum With32Bit(this SampleDataTypeEnum oldEnumValue) => oldEnumValue.Bits(32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With32Bit(this SampleDataType oldSampleDataType, IContext context) => oldSampleDataType.Bits(32, context);

        // Conversion-Style
        
        public static int TypeToBits(this Type obj) => ConfigWishes.TypeToBits(obj);
        public static Type BitsToType(this int value) => ConfigWishes.BitsToType(value);
        
        [Obsolete(ObsoleteMessage)] 
        public static int EnumToBits(this SampleDataTypeEnum obj) => ConfigWishes.EnumToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static SampleDataTypeEnum BitsToEnum(this int bits) => ConfigWishes.BitsToEnum(bits);
        
        [Obsolete(ObsoleteMessage)]
        public static int EntityToBits(this SampleDataType obj) => ConfigWishes.EntityToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static SampleDataType BitsToEntity(this int bits, IContext context) => ConfigWishes.BitsToEntity(bits, context);
        
        // Synonyms

        public static int ToBits(this Type obj) => ConfigWishes.ToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static int ToBits(this SampleDataTypeEnum obj) => ConfigWishes.ToBits(obj);
        
        [Obsolete(ObsoleteMessage)]
        public static int ToBits(this SampleDataType obj) => ConfigWishes.ToBits(obj);
    }

    public partial class ConfigWishes
    {
        // With Type Arguments
        
        public static int TypeToBits<T>() => typeof(T).TypeToBits();
        public static int Bits<TValueType>() => TypeToBits<TValueType>();
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Bits<TValueType>(int value) => value.BitsToType();

        public static bool Is8Bit <TValue> () => Bits<TValue>() == 8;
        public static bool Is16Bit<TValue> () => Bits<TValue>() == 16;
        public static bool Is32Bit<TValue> () => Bits<TValue>() == 32;

        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit<TValue>() => Bits<TValue>(8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit<TValue>() => Bits<TValue>(16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit<TValue>() => Bits<TValue>(32);

        // Conversion-Style

        public static int TypeToBits(Type obj)
        {
            if (obj == typeof(byte)) return 8;
            if (obj == typeof(Int16)) return 16;
            if (obj == typeof(float)) return 32;
            throw new ValueNotSupportedException(obj);
        }
        
        public static Type BitsToType(int value)
        {
            switch (AssertBits(value, strict: false))
            {
                case 8 : return typeof(byte);
                case 16: return typeof(Int16);
                case 32: return typeof(float);
                default: return default; // ncrunch: no coverage
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int EnumToBits(SampleDataTypeEnum obj)
        {
            switch (obj)
            {
                case SampleDataTypeEnum.Byte: return 8;
                case SampleDataTypeEnum.Int16: return 16;
                case SampleDataTypeEnum.Float32: return 32;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum BitsToEnum(int bits)
        {
            switch (bits)
            {
                case 32: return SampleDataTypeEnum.Float32;
                case 16: return SampleDataTypeEnum.Int16;
                case 8: return SampleDataTypeEnum.Byte;
            }
            
            AssertBits(bits, strict: false); return default;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int EntityToBits(SampleDataType obj) => obj.ToEnum().EnumToBits();
        
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType BitsToEntity(int bits, IContext context) => bits.BitsToEnum().ToEntity(context);
        
        // Synonyms
        
        public static int ToBits(Type obj) => TypeToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static int ToBits(SampleDataTypeEnum obj) => EnumToBits(obj);
        
        [Obsolete(ObsoleteMessage)]
        public static int ToBits(SampleDataType obj) => EntityToBits(obj);
   }
}
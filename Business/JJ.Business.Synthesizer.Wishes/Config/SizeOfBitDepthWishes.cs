using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class SizeOfBitDepthExtensionWishes
    {
        // Derived from Bits

        // Synth-Bound

        public static int SizeOfBitDepth(this SynthWishes obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this SynthWishes obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static SynthWishes SizeOfBitDepth(this SynthWishes obj, int? sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static SynthWishes WithSizeOfBitDepth(this SynthWishes obj, int? sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static SynthWishes SetSizeOfBitDepth(this SynthWishes obj, int? sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        public static int SizeOfBitDepth(this FlowNode obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this FlowNode obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static FlowNode SizeOfBitDepth(this FlowNode obj, int? sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static FlowNode WithSizeOfBitDepth(this FlowNode obj, int? sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static FlowNode SetSizeOfBitDepth(this FlowNode obj, int? sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        internal static int SizeOfBitDepth(this ConfigResolver obj) => GetSizeOfBitDepth(obj);
        internal static int GetSizeOfBitDepth(this ConfigResolver obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        [UsedImplicitly] internal static ConfigResolver SizeOfBitDepth(this ConfigResolver obj, int? sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        [UsedImplicitly] internal static ConfigResolver WithSizeOfBitDepth(this ConfigResolver obj, int? sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        [UsedImplicitly] internal static ConfigResolver SetSizeOfBitDepth(this ConfigResolver obj, int? sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        // Global-Bound

        internal static int? SizeOfBitDepth(this ConfigSection obj) => GetSizeOfBitDepth(obj);
        internal static int? GetSizeOfBitDepth(this ConfigSection obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        // Tape-Bound

        public static int SizeOfBitDepth(this Tape obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this Tape obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static Tape SizeOfBitDepth(this Tape obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static Tape WithSizeOfBitDepth(this Tape obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static Tape SetSizeOfBitDepth(this Tape obj, int sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        public static int SizeOfBitDepth(this TapeConfig obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this TapeConfig obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static TapeConfig SizeOfBitDepth(this TapeConfig obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static TapeConfig WithSizeOfBitDepth(this TapeConfig obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static TapeConfig SetSizeOfBitDepth(this TapeConfig obj, int sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        public static int SizeOfBitDepth(this TapeActions obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this TapeActions obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static TapeActions SizeOfBitDepth(this TapeActions obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static TapeActions WithSizeOfBitDepth(this TapeActions obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static TapeActions SetSizeOfBitDepth(this TapeActions obj, int sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        public static int SizeOfBitDepth(this TapeAction obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this TapeAction obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static TapeAction SizeOfBitDepth(this TapeAction obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static TapeAction WithSizeOfBitDepth(this TapeAction obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static TapeAction SetSizeOfBitDepth(this TapeAction obj, int sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        // Buff-Bound

        public static int SizeOfBitDepth(this Buff obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this Buff obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static Buff SizeOfBitDepth(this Buff obj, int sizeOfBitDepth, IContext context) => SetSizeOfBitDepth(obj, sizeOfBitDepth, context);
        public static Buff WithSizeOfBitDepth(this Buff obj, int sizeOfBitDepth, IContext context) => SetSizeOfBitDepth(obj, sizeOfBitDepth, context);
        public static Buff SetSizeOfBitDepth(this Buff obj, int sizeOfBitDepth, IContext context)
        {
            return obj.Bits(sizeOfBitDepth.ToBits(), context);
        }
        
        public static int SizeOfBitDepth(this AudioFileOutput obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this AudioFileOutput obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static AudioFileOutput SizeOfBitDepth(this AudioFileOutput obj, int sizeOfBitDepth, IContext context)
            => SetSizeOfBitDepth(obj, sizeOfBitDepth, context);
        public static AudioFileOutput WithSizeOfBitDepth(this AudioFileOutput obj, int sizeOfBitDepth, IContext context)
            => SetSizeOfBitDepth(obj, sizeOfBitDepth, context);
        public static AudioFileOutput SetSizeOfBitDepth(this AudioFileOutput obj, int sizeOfBitDepth, IContext context)
        {
            return obj.Bits(sizeOfBitDepth.ToBits(), context);
        }
        
        // Independent after Taping

        public static int SizeOfBitDepth(this Sample obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this Sample obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static Sample SizeOfBitDepth(this Sample obj, int sizeOfBitDepth, IContext context)
            => SetSizeOfBitDepth(obj, sizeOfBitDepth, context);
        public static Sample WithSizeOfBitDepth(this Sample obj, int sizeOfBitDepth, IContext context)
            => SetSizeOfBitDepth(obj, sizeOfBitDepth, context);
        public static Sample SetSizeOfBitDepth(this Sample obj, int sizeOfBitDepth, IContext context)
        {
            return obj.Bits(sizeOfBitDepth.ToBits(), context);
        }
        
        public static int SizeOfBitDepth(this AudioInfoWish obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this AudioInfoWish obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static AudioInfoWish SizeOfBitDepth(this AudioInfoWish obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static AudioInfoWish WithSizeOfBitDepth(this AudioInfoWish obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static AudioInfoWish SetSizeOfBitDepth(this AudioInfoWish obj, int sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        public static int SizeOfBitDepth(this AudioFileInfo obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this AudioFileInfo obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static AudioFileInfo SizeOfBitDepth(this AudioFileInfo obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static AudioFileInfo WithSizeOfBitDepth(this AudioFileInfo obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static AudioFileInfo SetSizeOfBitDepth(this AudioFileInfo obj, int sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        // Immutable

        public static int SizeOfBitDepth(this WavHeaderStruct obj) => GetSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this WavHeaderStruct obj)
        {
            return obj.Bits().ToSizeOfBitDepth();
        }
        
        public static WavHeaderStruct SizeOfBitDepth(this WavHeaderStruct obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static WavHeaderStruct WithSizeOfBitDepth(this WavHeaderStruct obj, int sizeOfBitDepth) => SetSizeOfBitDepth(obj, sizeOfBitDepth);
        public static WavHeaderStruct SetSizeOfBitDepth(this WavHeaderStruct obj, int sizeOfBitDepth)
        {
            return obj.Bits(sizeOfBitDepth.ToBits());
        }
        
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataTypeEnum obj) => GetSizeOfBitDepth(obj);
        [Obsolete(ObsoleteMessage)] public static int ToSizeOfBitDepth(this SampleDataTypeEnum obj) => GetSizeOfBitDepth(obj);
        [Obsolete(ObsoleteMessage)] public static int AsSizeOfBitDepth(this SampleDataTypeEnum obj) => GetSizeOfBitDepth(obj);
        [Obsolete(ObsoleteMessage)] public static int GetSizeOfBitDepth(this SampleDataTypeEnum obj)
        {
            return SizeOf(obj);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SizeOfBitDepth(this SampleDataTypeEnum oldEnumValue, int newByteSize)
            => SetSizeOfBitDepth(oldEnumValue, newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum WithSizeOfBitDepth(this SampleDataTypeEnum oldEnumValue, int newByteSize)
            => SetSizeOfBitDepth(oldEnumValue, newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SetSizeOfBitDepth(this SampleDataTypeEnum oldEnumValue, int newByteSize)
        {
            return newByteSize.Bits().BitsToEnum();
        }
        
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataType obj) => GetSizeOfBitDepth(obj);
        [Obsolete(ObsoleteMessage)] public static int ToSizeOfBitDepth(this SampleDataType obj) => GetSizeOfBitDepth(obj);
        [Obsolete(ObsoleteMessage)] public static int AsSizeOfBitDepth(this SampleDataType obj) => GetSizeOfBitDepth(obj);
        [Obsolete(ObsoleteMessage)] public static int GetSizeOfBitDepth(this SampleDataType obj)
        {
            return SizeOf(obj);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType SizeOfBitDepth(this SampleDataType oldSampleDataType, int newByteSize, IContext context)
            => SetSizeOfBitDepth(oldSampleDataType, newByteSize, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType WithSizeOfBitDepth(this SampleDataType oldSampleDataType, int newByteSize, IContext context)
            => SetSizeOfBitDepth(oldSampleDataType, newByteSize, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType SetSizeOfBitDepth(this SampleDataType oldSampleDataType, int newByteSize, IContext context)
        {
            return newByteSize.Bits().BitsToEntity(context);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static int SizeOfBitDepth(this int oldBits, int newByteSize) => SizeOfBitDepthToBits(newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        public static int SetSizeOfBitDepth(this int oldBits, int newByteSize) => SizeOfBitDepthToBits(newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        public static int WithSizeOfBitDepth(this int oldBits, int newByteSize) => SizeOfBitDepthToBits(newByteSize);
        public static int Bits(this int sizeOfBitDepth) => ConfigWishes.Bits(sizeOfBitDepth);
        public static int? Bits(this int? sizeOfBitDepth) => ConfigWishes.Bits(sizeOfBitDepth);
        public static int GetBits(this int sizeOfBitDepth) => ConfigWishes.GetBits(sizeOfBitDepth);
        public static int? GetBits(this int? sizeOfBitDepth) => ConfigWishes.GetBits(sizeOfBitDepth);
        public static int AsBits(this int sizeOfBitDepth) => ConfigWishes.AsBits(sizeOfBitDepth);
        public static int? AsBits(this int? sizeOfBitDepth) => ConfigWishes.AsBits(sizeOfBitDepth);
        public static int ToBits(this int sizeOfBitDepth) => ConfigWishes.ToBits(sizeOfBitDepth);
        public static int? ToBits(this int? sizeOfBitDepth) => ConfigWishes.ToBits(sizeOfBitDepth);
        public static int SizeOfBitDepthToBits(this int sizeOfBitDepth) => ConfigWishes.SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? SizeOfBitDepthToBits(this int? sizeOfBitDepth) => ConfigWishes.SizeOfBitDepthToBits(sizeOfBitDepth);

        public static int SizeOfBitDepth(this int bits) => ConfigWishes.SizeOfBitDepth(bits);
        public static int? SizeOfBitDepth(this int? bits) => ConfigWishes.SizeOfBitDepth(bits);
        public static int GetSizeOfBitDepth(this int bits) => ConfigWishes.GetSizeOfBitDepth(bits);
        public static int? GetSizeOfBitDepth(this int? bits) => ConfigWishes.GetSizeOfBitDepth(bits);
        public static int ToSizeOfBitDepth(this int bits) => ConfigWishes.ToSizeOfBitDepth(bits);
        public static int? ToSizeOfBitDepth(this int? bits) => ConfigWishes.ToSizeOfBitDepth(bits);
        public static int AsSizeOfBitDepth(this int bits) => ConfigWishes.AsSizeOfBitDepth(bits);
        public static int? AsSizeOfBitDepth(this int? bits) => ConfigWishes.AsSizeOfBitDepth(bits);
        public static int BitsToSizeOfBitDepth(this int bits) => ConfigWishes.BitsToSizeOfBitDepth(bits);
        public static int? BitsToSizeOfBitDepth(this int? bits) => ConfigWishes.BitsToSizeOfBitDepth(bits);

        public static int SizeOfBitDepth(this Type obj) => TypeToSizeOfBitDepth(obj);
        public static int GetSizeOfBitDepth(this Type obj) => TypeToSizeOfBitDepth(obj);
        public static int AsSizeOfBitDepth(this Type obj) => TypeToSizeOfBitDepth(obj);
        public static int ToSizeOfBitDepth(this Type obj) => TypeToSizeOfBitDepth(obj);
        public static int TypeToSizeOfBitDepth(this Type obj) => ConfigWishes.TypeToSizeOfBitDepth(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepth(this Type oldType, int newByteSize) => SizeOfBitDepthToType(newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SetSizeOfBitDepth(this Type oldType, int newByteSize) => SizeOfBitDepthToType(newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type WithSizeOfBitDepth(this Type oldType, int newByteSize) => SizeOfBitDepthToType(newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type AsSizeOfBitDepth(this Type oldType, int newByteSize) => SizeOfBitDepthToType(newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type ToSizeOfBitDepth(this Type oldType, int newByteSize) => SizeOfBitDepthToType(newByteSize);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepthToType(this int value) => ConfigWishes.SizeOfBitDepthToType(value);
    }

    public partial class ConfigWishes
    {

        public static int SizeOfBitDepth(int bits) => BitsToSizeOfBitDepth(bits);
        public static int? SizeOfBitDepth(int? bits) => BitsToSizeOfBitDepth(bits);
        public static int GetSizeOfBitDepth(int bits) => BitsToSizeOfBitDepth(bits);
        public static int? GetSizeOfBitDepth(int? bits) => BitsToSizeOfBitDepth(bits);
        public static int AsSizeOfBitDepth(int bits) => BitsToSizeOfBitDepth(bits);
        public static int? AsSizeOfBitDepth(int? bits) => BitsToSizeOfBitDepth(bits);
        public static int ToSizeOfBitDepth(int bits) => BitsToSizeOfBitDepth(bits);
        public static int? ToSizeOfBitDepth(int? bits) => BitsToSizeOfBitDepth(bits);
        public static int BitsToSizeOfBitDepth(int bits) => AssertBits(bits, strict: false) / 8;
        public static int? BitsToSizeOfBitDepth(int? bits) => AssertBits(bits, strict: false) / 8;

        public static int Bits(int sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? Bits(int? sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int GetBits(int sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? GetBits(int? sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int AsBits(int sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? AsBits(int? sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int ToBits(int sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int? ToBits(int? sizeOfBitDepth) => SizeOfBitDepthToBits(sizeOfBitDepth);
        public static int SizeOfBitDepthToBits(int sizeOfBitDepth) => AssertSizeOfBitDepth(sizeOfBitDepth, strict: false) * 8;
        public static int? SizeOfBitDepthToBits(int? sizeOfBitDepth) => AssertSizeOfBitDepth(sizeOfBitDepth, strict: false) * 8;

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

        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepthToType<TValueType>(int value) => SizeOfBitDepthToType(value);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SizeOfBitDepth<TValueType>(int value) => SizeOfBitDepthToType<TValueType>(value);
        public static int SizeOfBitDepth<TValueType>() => TypeToSizeOfBitDepth<TValueType>();
        public static int TypeToSizeOfBitDepth<TValueType>() => TypeToSizeOfBitDepth(typeof(TValueType));
    }
}
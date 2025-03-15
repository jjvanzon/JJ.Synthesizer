using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Framework.Nully.Core.FilledInWishes;

// ReSharper disable PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // MaxAmplitude: A Derived Attribute

    /// <inheritdoc cref="_configextensionwishes"/>
    public static class MaxAmplitudeExtensionWishes
    {
        // Synth-Bound

        public static double GetMaxAmplitude(this SynthWishes obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        public static double MaxAmplitude(this FlowNode obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this FlowNode obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        [UsedImplicitly] internal static double MaxAmplitude(this ConfigResolver obj) => ConfigWishes.MaxAmplitude(obj);
        [UsedImplicitly] internal static double GetMaxAmplitude(this ConfigResolver obj) => ConfigWishes.GetMaxAmplitude(obj);

        // Global-Bound

        [UsedImplicitly] internal static double? MaxAmplitude(this ConfigSection obj) => ConfigWishes.MaxAmplitude(obj);
        [UsedImplicitly] internal static double? GetMaxAmplitude(this ConfigSection obj) => ConfigWishes.GetMaxAmplitude(obj);

        // Tape-Bound

        public static double MaxAmplitude(this Tape obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this Tape obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        public static double MaxAmplitude(this TapeConfig obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this TapeConfig obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        public static double MaxAmplitude(this TapeAction obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this TapeAction obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        public static double MaxAmplitude(this TapeActions obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this TapeActions obj) => ConfigWishes.GetMaxAmplitude(obj);

        // Buff-Bound

        public static double MaxAmplitude(this Buff obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this Buff obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        public static double MaxAmplitude(this AudioFileOutput obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this AudioFileOutput obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        // Independent after Taping

        public static double MaxAmplitude(this Sample obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this Sample obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        public static double MaxAmplitude(this AudioFileInfo obj) => ConfigWishes.MaxAmplitude(obj);
        public static double ToMaxAmplitude(this AudioFileInfo obj) => ConfigWishes.ToMaxAmplitude(obj);
        public static double GetMaxAmplitude(this AudioFileInfo obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        public static double MaxAmplitude(this AudioInfoWish obj) => ConfigWishes.MaxAmplitude(obj);
        public static double ToMaxAmplitude(this AudioInfoWish obj) => ConfigWishes.ToMaxAmplitude(obj);
        public static double GetMaxAmplitude(this AudioInfoWish obj) => ConfigWishes.GetMaxAmplitude(obj);
        
        // Immutable

        public static double MaxAmplitude(this WavHeaderStruct obj) => ConfigWishes.MaxAmplitude(obj);
        public static double GetMaxAmplitude(this WavHeaderStruct obj) => ConfigWishes.GetMaxAmplitude(obj);

        [Obsolete(ObsoleteMessage)] public static double MaxAmplitude(this SampleDataType obj) => ConfigWishes.MaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double ToMaxAmplitude(this SampleDataType obj) => ConfigWishes.ToMaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double GetMaxAmplitude(this SampleDataType obj) => ConfigWishes.GetMaxAmplitude(obj);

        [Obsolete(ObsoleteMessage)] public static double MaxAmplitude(this SampleDataTypeEnum obj) => ConfigWishes.MaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double ToMaxAmplitude(this SampleDataTypeEnum obj) => ConfigWishes.ToMaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double GetMaxAmplitude(this SampleDataTypeEnum obj) => ConfigWishes.GetMaxAmplitude(obj);

        public static double MaxAmplitude(this Type valueType) => ConfigWishes.MaxAmplitude(valueType);
        public static double ToMaxAmplitude(this Type valueType) => ConfigWishes.ToMaxAmplitude(valueType);
        public static double GetMaxAmplitude(this Type valueType) => ConfigWishes.GetMaxAmplitude(valueType);

        public static double MaxAmplitude(this int bits) => ConfigWishes.MaxAmplitude(bits);
        public static double ToMaxAmplitude(this int bits) => ConfigWishes.ToMaxAmplitude(bits);
        public static double GetMaxAmplitude(this int bits) => ConfigWishes.GetMaxAmplitude(bits);

        public static double? MaxAmplitude(this int? bits) => ConfigWishes.MaxAmplitude(bits);
        public static double? ToMaxAmplitude(this int? bits) => ConfigWishes.ToMaxAmplitude(bits);
        public static double? GetMaxAmplitude(this int? bits) => ConfigWishes.GetMaxAmplitude(bits);
    }

    public partial class ConfigWishes
    {
        // Synth-Bound

        public static double MaxAmplitude(SynthWishes obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(SynthWishes obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(FlowNode obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(FlowNode obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        [UsedImplicitly] internal static double MaxAmplitude(ConfigResolver obj) => GetMaxAmplitude(obj);
        [UsedImplicitly] internal static double GetMaxAmplitude(ConfigResolver obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        // Global-Bound

        [UsedImplicitly] internal static double? MaxAmplitude(ConfigSection obj) => GetMaxAmplitude(obj);
        [UsedImplicitly] internal static double? GetMaxAmplitude(ConfigSection obj)
        {
            return obj.Bits()?.MaxAmplitude();
        }
        
        // Tape-Bound

        public static double MaxAmplitude(Tape obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(Tape obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(TapeConfig obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(TapeConfig obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(TapeAction obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(TapeAction obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(TapeActions obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(TapeActions obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        // Buff-Bound

        public static double MaxAmplitude(Buff obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(Buff obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(AudioFileOutput obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(AudioFileOutput obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        // Independent after Taping

        public static double MaxAmplitude(Sample obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(Sample obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(AudioFileInfo obj) => GetMaxAmplitude(obj);
        public static double ToMaxAmplitude(AudioFileInfo obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(AudioFileInfo obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(AudioInfoWish obj) => GetMaxAmplitude(obj);
        public static double ToMaxAmplitude(AudioInfoWish obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(AudioInfoWish obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        // Immutable

        public static double MaxAmplitude(WavHeaderStruct obj) => GetMaxAmplitude(obj);
        public static double GetMaxAmplitude(WavHeaderStruct obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        [Obsolete(ObsoleteMessage)] public static double MaxAmplitude(SampleDataType obj) => GetMaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double ToMaxAmplitude(SampleDataType obj) => GetMaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double GetMaxAmplitude(SampleDataType obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        [Obsolete(ObsoleteMessage)] public static double MaxAmplitude(SampleDataTypeEnum obj) => GetMaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double ToMaxAmplitude(SampleDataTypeEnum obj) => GetMaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)] public static double GetMaxAmplitude(SampleDataTypeEnum obj)
        {
            return obj.Bits().MaxAmplitude();
        }
        
        public static double MaxAmplitude(Type valueType) => GetMaxAmplitude(valueType);
        public static double ToMaxAmplitude(Type valueType) => GetMaxAmplitude(valueType);
        public static double GetMaxAmplitude(Type valueType)
        {
            return valueType.Bits().MaxAmplitude();
        }
        
        public static double? MaxAmplitude(int? bits) => GetMaxAmplitude(bits);
        public static double? ToMaxAmplitude(int? bits) => GetMaxAmplitude(bits);
        public static double? GetMaxAmplitude(int? bits) 
        {
            if (!Has(bits)) return bits;
            return MaxAmplitude(bits.Value);
        }
        
        public static double MaxAmplitude(int bits) => GetMaxAmplitude(bits);
        public static double ToMaxAmplitude(int bits) => GetMaxAmplitude(bits);
        public static double GetMaxAmplitude(int bits) 
        {
            switch (bits)
            {
                case 32 : return 1;
                case 16 : return short .MaxValue;
                case  8 : return byte  .MaxValue / 2;
            }
            
            AssertBits(bits); return default;
        }
        
        // Type Arguments
        
        public static double MaxAmplitude<TValue>() => GetMaxAmplitude<TValue>();
        public static double ToMaxAmplitude<TValue>() => GetMaxAmplitude<TValue>();
        public static double GetMaxAmplitude<TValue>()
        {
            return Bits<TValue>().MaxAmplitude();
        }
    }
}
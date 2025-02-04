using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Framework.Wishes.Common.FilledInWishes;

// ReSharper disable PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // MaxAmplitude: A Derived Attribute

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class MaxAmplitudeExtensionWishes
    {

        // Synth-Bound

        public static double MaxAmplitude(this SynthWishes obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this FlowNode obj) => ConfigWishes.MaxAmplitude(obj);
        [UsedImplicitly]
        internal static double MaxAmplitude(this ConfigResolver obj) => ConfigWishes.MaxAmplitude(obj);

        // Global-Bound

        [UsedImplicitly]
        internal static double? MaxAmplitude(this ConfigSection obj) => ConfigWishes.MaxAmplitude(obj);

        // Tape-Bound

        public static double MaxAmplitude(this Tape obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this TapeConfig obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this TapeAction obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this TapeActions obj) => ConfigWishes.MaxAmplitude(obj);

        // Buff-Bound

        public static double MaxAmplitude(this Buff obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this AudioFileOutput obj) => ConfigWishes.MaxAmplitude(obj);

        // Independent after Taping

        public static double MaxAmplitude(this Sample obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this AudioFileInfo obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this AudioInfoWish obj) => ConfigWishes.MaxAmplitude(obj);

        // Immutable

        public static double MaxAmplitude(this WavHeaderStruct obj) => ConfigWishes.MaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)]
        public static double MaxAmplitude(this SampleDataType obj) => ConfigWishes.MaxAmplitude(obj);
        [Obsolete(ObsoleteMessage)]
        public static double MaxAmplitude(this SampleDataTypeEnum obj) => ConfigWishes.MaxAmplitude(obj);
        public static double MaxAmplitude(this Type valueType) => ConfigWishes.MaxAmplitude(valueType);
        public static double MaxAmplitude(this int bits) => ConfigWishes.MaxAmplitude(bits);
        public static double? MaxAmplitude(this int? bits) => ConfigWishes.MaxAmplitude(bits);
    }
    
    public partial class ConfigWishes
    {
        // Synth-Bound

        public static double MaxAmplitude(SynthWishes obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(FlowNode obj) => obj.Bits().MaxAmplitude();
        [UsedImplicitly]
        internal static double MaxAmplitude(ConfigResolver obj) => obj.Bits().MaxAmplitude();

        // Global-Bound

        [UsedImplicitly]
        internal static double? MaxAmplitude(ConfigSection obj) => obj.Bits()?.MaxAmplitude();

        // Tape-Bound

        public static double MaxAmplitude(Tape obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(TapeConfig obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(TapeAction obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(TapeActions obj) => obj.Bits().MaxAmplitude();

        // Buff-Bound

        public static double MaxAmplitude(Buff obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(AudioFileOutput obj) => obj.Bits().MaxAmplitude();

        // Independent after Taping

        public static double MaxAmplitude(Sample obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(AudioFileInfo obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(AudioInfoWish obj) => obj.Bits().MaxAmplitude();

        // Immutable

        public static double MaxAmplitude(WavHeaderStruct obj) => obj.Bits().MaxAmplitude();
        [Obsolete(ObsoleteMessage)]
        public static double MaxAmplitude(SampleDataType obj) => obj.Bits().MaxAmplitude();
        [Obsolete(ObsoleteMessage)]
        public static double MaxAmplitude(SampleDataTypeEnum obj) => obj.Bits().MaxAmplitude();
        public static double MaxAmplitude(Type valueType) => valueType.Bits().MaxAmplitude();

        public static double? MaxAmplitude(int? bits) 
        {
            if (!Has(bits)) return bits;
            return MaxAmplitude(bits.Value);
        }
        
        public static double MaxAmplitude(int bits) 
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
        
        public static double MaxAmplitude<TValue>() => Bits<TValue>().MaxAmplitude();
    }
}
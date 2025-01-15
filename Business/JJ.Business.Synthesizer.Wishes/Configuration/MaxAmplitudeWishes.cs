using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class MaxAmplitudeExtensionWishes
    {
        // A Derived Attribute
        
        // Synth-Bound
        
        public   static double  MaxAmplitude(this SynthWishes        obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this FlowNode           obj) => obj.Bits() .MaxAmplitude();
        [UsedImplicitly]                                            
        internal static double  MaxAmplitude(this ConfigResolver     obj) => obj.Bits() .MaxAmplitude();
        
        // Global-Bound
        
        [UsedImplicitly]
        internal static double? MaxAmplitude(this ConfigSection      obj) => obj.Bits()?.MaxAmplitude();
        
        // Tape-Bound
        
        public   static double  MaxAmplitude(this Tape               obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this TapeConfig         obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this TapeAction         obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this TapeActions        obj) => obj.Bits() .MaxAmplitude();
        
        // Buff-Bound
        
        public   static double  MaxAmplitude(this Buff               obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this AudioFileOutput    obj) => obj.Bits() .MaxAmplitude();
        
        // Independent after Taping
        
        public   static double  MaxAmplitude(this Sample             obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this AudioFileInfo      obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this AudioInfoWish      obj) => obj.Bits() .MaxAmplitude();
        
        // Immutable
        
        public   static double  MaxAmplitude(this WavHeaderStruct    obj) => obj.Bits() .MaxAmplitude();
        [Obsolete(ObsoleteMessage)]
        public   static double  MaxAmplitude(this SampleDataType     obj) => obj.Bits().MaxAmplitude();
        [Obsolete(ObsoleteMessage)]
        public   static double  MaxAmplitude(this SampleDataTypeEnum obj) => obj.Bits().MaxAmplitude();
        public   static double  MaxAmplitude(this Type valueType) => valueType.Bits().MaxAmplitude();
        public   static double  MaxAmplitude(this int bits) => ConfigWishes.MaxAmplitude(bits);
    }
    
    public partial class ConfigWishes
    {
        // Conversion Formula
        
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
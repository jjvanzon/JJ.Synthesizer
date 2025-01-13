using System;
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
        
        public   static double  MaxAmplitude(this SynthWishes     obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this FlowNode        obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this ConfigResolver    obj) => obj.Bits() .MaxAmplitude();
        internal static double? MaxAmplitude(this ConfigSection   obj) => obj.Bits()?.MaxAmplitude();
        public   static double  MaxAmplitude(this Buff            obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this Tape            obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this TapeConfig      obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this TapeAction      obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this TapeActions     obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this Sample          obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this AudioFileOutput obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this WavHeaderStruct obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this AudioFileInfo   obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this AudioInfoWish   obj) => obj.Bits() .MaxAmplitude();
        public   static double  MaxAmplitude(this Type valueType) => valueType.Bits().MaxAmplitude();
        
        [Obsolete(ObsoleteMessage)]
        public static double MaxAmplitude(this SampleDataType obj) => obj.Bits().MaxAmplitude();
        
        [Obsolete(ObsoleteMessage)]
        public static double MaxAmplitude(this SampleDataTypeEnum obj) => obj.Bits().MaxAmplitude();
        
        public static double MaxAmplitude(this int bits)
        {
            switch (AssertBits(bits))
            {
                case 32: return 1;
                case 16: return short.MaxValue;
                case 8: return byte.MaxValue / 2;
                default: return default; // ncrunch: no coverage
            }
        }
    }
    
    public partial class ConfigWish
    {
        public static double MaxAmplitude<TValue>() => Bits<TValue>().MaxAmplitude();
    }
}
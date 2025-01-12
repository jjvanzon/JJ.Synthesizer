using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Derived Attribute
        
        public   static double  MaxValue(this SynthWishes     obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this FlowNode        obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this ConfigWishes    obj) => obj.Bits() .MaxValue();
        internal static double? MaxValue(this ConfigSection   obj) => obj.Bits()?.MaxValue();
        public   static double  MaxValue(this Buff            obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this Tape            obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this TapeConfig      obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this TapeAction      obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this TapeActions     obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this Sample          obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this AudioFileOutput obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this WavHeaderStruct obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this AudioFileInfo   obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this AudioInfoWish   obj) => obj.Bits() .MaxValue();
        public   static double  MaxValue(this Type valueType) => valueType.Bits().MaxValue();
        public   static double  MaxValue<TValue>() => Bits<TValue>().MaxValue();
        
        [Obsolete(ObsoleteMessage)]
        public static double MaxValue(this SampleDataType obj) => obj.Bits().MaxValue();
        
        [Obsolete(ObsoleteMessage)]
        public static double MaxValue(this SampleDataTypeEnum obj) => obj.Bits().MaxValue();
        
        public static double MaxValue(this int bits)
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
}
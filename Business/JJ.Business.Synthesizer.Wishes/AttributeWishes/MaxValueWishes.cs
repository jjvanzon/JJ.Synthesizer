using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
        #region MaxValue
        
        public   static double MaxValue(this SynthWishes     obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this FlowNode        obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this ConfigWishes    obj) => Bits(obj).MaxValue();
        internal static double MaxValue(this ConfigSection   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Buff            obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Tape            obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeConfig      obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeAction      obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeActions     obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Sample          obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioFileOutput obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this WavHeaderStruct obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioFileInfo   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioInfoWish   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Type valueType) => Bits(valueType).MaxValue();
        public   static double MaxValue<TValue>() => Bits<TValue>().MaxValue();
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static double MaxValue(this SampleDataType     obj) => Bits(obj).MaxValue();
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static double MaxValue(this SampleDataTypeEnum obj) => Bits(obj).MaxValue();
        
        public static double MaxValue(this int bits)
        {
            switch (ConfigWishes.AssertBits(bits))
            {
                case 32: return 1;
                case 16: return Int16.MaxValue; // ReSharper disable once PossibleLossOfFraction
                case 8: return byte.MaxValue / 2;
                default: return default;
            }
        }
        
        #endregion
    }
}
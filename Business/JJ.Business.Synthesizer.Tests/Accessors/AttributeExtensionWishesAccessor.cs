using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class AttributeExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(AttributeExtensionWishes));
        
        public static int Bits(this ConfigSectionAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is8Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is16Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is32Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);

        public static int Channels(this ConfigSectionAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsMono(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsStereo(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);

        public static int SamplingRate(this ConfigSectionAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);

        public static InterpolationTypeEnum Interpolation(this ConfigSectionAccessor obj) => (InterpolationTypeEnum)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsLinear(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(),obj.Obj);
        public static bool IsBlocky(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
}

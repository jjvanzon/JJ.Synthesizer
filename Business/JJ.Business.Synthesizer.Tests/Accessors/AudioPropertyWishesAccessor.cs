using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class AudioPropertyWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(AudioPropertyWishes));
        
        public static int Bits(this ConfigSectionAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static ConfigSectionAccessor Bits(this ConfigSectionAccessor obj, int value) { _accessor.InvokeMethod(MemberName(), obj.Obj, value); return obj; }
        public static bool Is8Bit (this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is16Bit (this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is32Bit (this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static ConfigSectionAccessor With8Bit (this ConfigSectionAccessor obj) { _accessor.InvokeMethod(MemberName(), obj.Obj); return obj; }
        public static ConfigSectionAccessor With16Bit (this ConfigSectionAccessor obj) { _accessor.InvokeMethod(MemberName(), obj.Obj); return obj; }
        public static ConfigSectionAccessor With32Bit (this ConfigSectionAccessor obj) { _accessor.InvokeMethod(MemberName(), obj.Obj); return obj; }
    }
}

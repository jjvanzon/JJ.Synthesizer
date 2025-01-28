using System;
using System.Reflection;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class StringWishesAccessor
    {
        private static readonly Accessor _accessor;

        static StringWishesAccessor()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes";
            Type     type     = assembly.GetType(typeName, true);
            _accessor = new Accessor(type);
        }

        public static string PrettyByteCount(long byteCount)
            => (string)_accessor.InvokeMethod(nameof(PrettyByteCount), byteCount);

        public static int CountLines(this string str)
            => (int)_accessor.InvokeMethod(MemberName(), str);
        
        public static bool Contains(this string str, string substring, bool ignoreCase)
        => (bool)_accessor.InvokeMethod(MemberName(), str, substring, ignoreCase);
    }
}

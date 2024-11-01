using System;
using System.Reflection;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class FrameworkWishesAccessor
    {
        private static readonly Accessor _accessor;

        static FrameworkWishesAccessor()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Helpers.FrameworkWishes";
            Type     type     = assembly.GetType(typeName, true);
            _accessor = new Accessor(type);
        }

        public static string PrettyByteCount(long byteCount)
            => (string)_accessor.InvokeMethod(nameof(PrettyByteCount), byteCount);

    }
}

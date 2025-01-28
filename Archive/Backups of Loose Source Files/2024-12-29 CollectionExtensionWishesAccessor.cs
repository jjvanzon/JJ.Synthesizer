using System;
using System.Collections.Generic;
using System.Reflection;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class CollectionExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = CreateAccessor();

        private static Accessor CreateAccessor()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string typeName = "JJ.Business.Synthesizer.Wishes.JJ_Framework_Collection_Wishes.CollectionExtensionWishes";
            Type type = assembly.GetType(typeName, true);
            var accessor = new Accessor(type);
            return accessor;
        }

        // Does not seem to work with generics.
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
            => _accessor.InvokeMethod(MemberName(), enumerable, action);
    }
}

using System;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class OperatorExtensionsWishesAccessor
    {
        private static readonly Accessor _accessor;

        static OperatorExtensionsWishesAccessor()
        {
            Type type = typeof(OperatorExtensionsWishes);
            _accessor = new Accessor(type);
        }

        public static bool IsAdder(this Operator entity)
            => (bool)_accessor.InvokeMethod(nameof(IsAdder), entity);

        public static bool IsAdder(this Outlet entity)
            => (bool)_accessor.InvokeMethod(nameof(IsAdder), entity);
    }
}
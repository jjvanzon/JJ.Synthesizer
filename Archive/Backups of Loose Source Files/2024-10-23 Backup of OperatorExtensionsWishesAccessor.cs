using System.Collections.Generic;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class OperatorExtensionsWishesAccessor
    {
        static readonly Accessor _accessor = new Accessor(typeof(OperatorExtensionsWishes));

        public static IList<Outlet> FlattenTerms(Outlet sumOrAdd)
        {
            return (IList<Outlet>)_accessor.InvokeMethod(nameof(FlattenTerms), sumOrAdd);
        }

        public static IList<Outlet> FlattenFactors(IList<Outlet> operands)
        {
            return (IList<Outlet>)_accessor.InvokeMethod(nameof(FlattenFactors), operands);
        }

        public static IList<Outlet> FlattenFactors(Outlet multiply)
        {
            return (IList<Outlet>)_accessor.InvokeMethod(nameof(FlattenFactors), multiply);
        }
    }
}

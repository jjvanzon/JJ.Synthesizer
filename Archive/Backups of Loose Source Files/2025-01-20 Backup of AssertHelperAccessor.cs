using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using JJ.Framework.Reflection;
using JJ.Framework.Testing;
// ReSharper disable UnusedParameter.Global

namespace JJ.Framework.Wishes
{
    internal static class AssertHelperAccessor
    {
        private static Accessor _accessor = new Accessor(typeof(AssertHelper));
        
        public static void ExpectedActualCheck(Func<double, bool> condition, string methodName, double expected, Expression<Func<double>> actualExpression)
            => _accessor.InvokeMethod(() => ExpectedActualCheck(condition, methodName, expected, actualExpression));
        
        public static void ExpectedActualCheck(Func<int, bool> condition, string methodName, int expected, Expression<Func<int>> actualExpression)
            => _accessor.InvokeMethod(() => ExpectedActualCheck(condition, methodName, expected, actualExpression));

    }
}

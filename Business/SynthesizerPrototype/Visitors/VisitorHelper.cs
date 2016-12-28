using System;
using System.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Reflection;

namespace JJ.Business.SynthesizerPrototype.Visitors
{
    public static class VisitorHelper
    {
        public static void WithStackCheck(ICollection stack, Action action)
        {
            if (stack == null) throw new NullException(() => stack);
            if (action == null) throw new NullException(() => action);

            int stackCountBefore = stack.Count;

            action();

            int expectedStackCount = stackCountBefore + 1;

            if (stack.Count != expectedStackCount)
            {
                throw new Exception(String.Format(
                    "{2} was not incremented by exactly 1 after visitation. {0} = {1}, {2} = {3}.",
                    nameof(expectedStackCount),
                    expectedStackCount,
                    ExpressionHelper.GetText(() => stack.Count),
                    stack.Count));
            }
        }
    }
}

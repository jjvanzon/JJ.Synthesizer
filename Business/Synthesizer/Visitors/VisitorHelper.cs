using System;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Visitors
{
    internal static class VisitorHelper
    {
        public static T WithStackCountCheck<T>(Stack<T> stack, T item, Action action)
        {
            if (stack == null) throw new NullException(() => stack);
            if (item == null) throw new NullException(() => item);
            if (action == null) throw new NullException(() => action);

            int stackCountBefore = stack.Count;

            action();

            int expectedStackCount = stackCountBefore + 1;

            if (stack.Count != expectedStackCount)
            {
                throw new Exception(String.Format(
                    "{0} was not incremented by exactly 1 after visiting {1}. expectedStackCount = {2}, _stack.Count = {3}.",
                    ExpressionHelper.GetText(() => stack.Count),
                    item.GetType().Name,
                    expectedStackCount,
                    stack.Count));
            }
        }
    }
}

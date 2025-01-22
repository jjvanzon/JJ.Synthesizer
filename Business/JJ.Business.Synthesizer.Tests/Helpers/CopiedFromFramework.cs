using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable ReplaceWithSingleCallToAny

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    // TODO: Move to FrameworkWishes
    
    /// <inheritdoc cref="docs._copiedfromframework"/>
    internal static class CopiedFromFramework
    {
        public static void ThrowsException_OrInnerException(Action statement, Type expectedExceptionType, string expectedMessage)
        {
            if (statement == null) throw new NullException(() => statement);
            if (expectedExceptionType == null) throw new NullException(() => expectedExceptionType);

            string actualDescriptor = "";

            try
            {
                statement();
            }
            catch (Exception ex)
            {
                bool isMatch = ex.HasExceptionOrInnerExceptionsOfType(expectedExceptionType, expectedMessage);
                if (!isMatch)
                {
                    actualDescriptor = $"Actual exception: '{ex}'";
                }
                else
                {
                    return;
                }
            }

            throw new Exception($"Exception or inner exception was expected of type '{expectedExceptionType}' with message '{expectedMessage}'. {actualDescriptor}");
        }

        public static void ThrowsException_OrInnerException<T>(Action statement, string expectedMessage)
            => ThrowsException_OrInnerException(statement, typeof(T), expectedMessage);
        
        private static bool HasExceptionOrInnerExceptionsOfType(this Exception exception, Type exceptionType, string message)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));
            if (exceptionType == null) throw new ArgumentNullException(nameof(exceptionType));

            bool any = exception.SelfAndAncestors(x => x.InnerException)
                                .OfType(exceptionType)
                                .Where(x => string.Equals(x.Message, message, StringComparison.Ordinal))
                                .Any();
            return any;
        }
        
        public static IEnumerable<T> SelfAndAncestors<T>(this T sourceItem, Func<T, T> selector)
        {
            if (sourceItem == null) throw new ArgumentNullException(nameof(sourceItem));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            var destHashSet = new HashSet<T> { sourceItem };

            SelectAncestors(sourceItem, selector, destHashSet);

            return destHashSet;
        }

        private static void SelectAncestors<T>(T sourceItem, Func<T, T> selector, HashSet<T> destHashSet)
        {
            T ancestor = sourceItem;
            while (true)
            {
                ancestor = selector(ancestor);

                if (ancestor == null)
                {
                    break;
                }

                if (!destHashSet.Add(ancestor))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Similar to OfType&lt;T&gt; but now taking a Type.
        /// This misses the advantage of automatically casting items to the desired type.
        /// Still this may be useful for certain cases.
        /// </summary>
        private static IEnumerable<TSource> OfType<TSource>(this IEnumerable<TSource> source, Type type)
            => source.Where(x => x.GetType() == type);
            
        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static double Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, double> selector)
            => collection.Select(selector).Product();

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static double Product(this IEnumerable<double> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            double product = collection.FirstOrDefault();

            foreach (double value in collection.Skip(1))
            {
                product *= value;
            }

            return product;
        }
    }
}

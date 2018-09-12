using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using JJ.Framework.Common;
using JJ.Framework.Exceptions.Aggregates;
using JJ.Framework.Reflection;

// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable LoopCanBeConvertedToQuery

#pragma warning disable 1584, 1711, 1572, 1581, 1580

namespace JJ.Framework.Collections
{
    [PublicAPI]
    public static class CollectionExtensions
    {
        public static void Add<T>(this IList<T> collection, params T[] items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (T x in items)
            {
                collection.Add(x);
            }
        }

        public static void AddRange<T>(this HashSet<T> dest, IEnumerable<T> source)
        {
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            if (source == null) throw new ArgumentNullException(nameof(source));

            foreach (T item in source)
            {
                dest.Add(item);
            }
        }

        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (T x in items)
            {
                collection.Add(x);
            }
        }

        // The names 'ToArray' and 'ToList' conflicted with other overloads.

        public static TItem[] AsArray<TItem>(this TItem item) => new[] { item };

        public static IEnumerable<TItem> AsEnumerable<TItem>(this TItem item) => new[] { item };

        public static List<TItem> AsList<TItem>(this TItem item) => new List<TItem> { item };

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, T second) => first.Concat(new[] { second });

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] second) => first.Concat((IEnumerable<T>)second);

        public static IEnumerable<T> Concat<T>(this T first, IEnumerable<T> second) => new[] { first }.Concat(second);

        public static IEnumerable<(T1, T2)> CrossJoin<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2)
            => CrossJoin(collection1, collection2, (x, y) => (x, y));

        public static IEnumerable<TResult> CrossJoin<T1, T2, TResult>(
            this IEnumerable<T1> collection1,
            IEnumerable<T2> collection2,
            Func<T1, T2, TResult> resultSelector)
        {
            if (collection1 == null) throw new ArgumentNullException(nameof(collection1));
            if (collection2 == null) throw new ArgumentNullException(nameof(collection2));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            foreach (T1 item1 in collection1)
            {
                foreach (T2 item2 in collection2)
                {
                    TResult resultItem = resultSelector(item1, item2);
                    yield return resultItem;
                }
            }
        }

        public static IEnumerable<TResult> CrossJoin<TItem, TResult>(
            this IEnumerable<IEnumerable<TItem>> collections,
            Func<IEnumerable<TItem>, TResult> resultSelector)
        {
            // Pre-conditions
            if (collections == null) throw new ArgumentNullException(nameof(collections));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            if (!collections.Any())
            {
                yield break;
            }

            // Set up nested enumerators.
            NestedEnumerator nestedEnumerator = null;
            foreach (IEnumerable<TItem> collection in collections)
            {
                nestedEnumerator = new NestedEnumerator(collection.Cast<object>(), nestedEnumerator);
            }

            // Run nested enumerator

            // ReSharper disable once PossibleNullReferenceException
            foreach (object[] items in nestedEnumerator)
            {
                yield return resultSelector(items.Cast<TItem>());
            }
        }

        public static IEnumerable<TItem> Distinct<TItem, TKey>(this IEnumerable<TItem> enumerable, Func<TItem, TKey> keySelector)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var hashSet = new HashSet<TKey>();

            foreach (TItem item in enumerable)
            {
                TKey key = keySelector(item);

                if (hashSet.Add(key))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TItem[]> DistinctMany<TItem>(this IEnumerable<TItem[]> enumerables)
            => DistinctMany<TItem[], TItem>(enumerables);

        public static IEnumerable<IEnumerable<TItem>> DistinctMany<TItem>(this IEnumerable<IEnumerable<TItem>> enumerables)
            => DistinctMany<IEnumerable<TItem>, TItem>(enumerables);

        public static IEnumerable<TEnumerable> DistinctMany<TEnumerable, TItem>(this IEnumerable<TEnumerable> enumerables)
            where TEnumerable : IEnumerable<TItem>
        {
            if (enumerables == null) throw new ArgumentNullException(nameof(enumerables));

            var hashSet = new HashSet<string>();

            foreach (TEnumerable enumerable in enumerables)
            {
                string key = KeyHelper.CreateKey(enumerable);

                if (hashSet.Add(key))
                {
                    yield return enumerable;
                }
            }
        }

        //public static IEnumerable<TItem[]> DistinctMany<TItem>(this IEnumerable<TItem[]> enumerable)
        //    => enumerable.Distinct(ArrayEqualityComparer<TItem>.Instance);

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T x)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Except(new[] { x });
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return source.Where(x => !predicate(x));
        }

        /// <summary>
        /// The original Except() method from .NET automatically does a distinct, which is something you do not always
        /// want.
        /// </summary>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, IEnumerable<T> input, bool distinct)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (distinct)
            {
                return source.Except(input);
            }

            HashSet<T> inputHashSet = input.ToHashSet();

            return source.Where(x => !inputHashSet.Contains(x));
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T FirstWithClearException<T>(this IEnumerable<T> collection, object keyIndicator)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            T item = collection.FirstOrDefault();

            if (item == null)
            {
                throw new NotFoundException<T>(keyIndicator);
            }

            return item;
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T FirstWithClearException<T>(this IEnumerable<T> collection, Func<T, bool> predicate, object keyIndicator)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            T item = collection.Where(predicate).FirstOrDefault();

            if (item == null)
            {
                throw new NotFoundException<T>(keyIndicator);
            }

            return item;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (T x in enumerable)
            {
                action(x);
            }
        }

        /// <summary>
        /// Returns the list index of the first item that matches the predicate.
        /// Does not check duplicates, because that would make it slower.
        /// </summary>
        public static int IndexOf<TSource>(this IEnumerable<TSource> collection, Func<TSource, bool> predicate)
        {
            int? indexOf = TryGetIndexOf(collection, predicate);

            if (indexOf.HasValue)
            {
                return indexOf.Value;
            }

            throw new Exception("No item in the collection matches the predicate.");
        }

        /// <summary>
        /// IList&lt;T&gt; has an IndexOf method natively. This overload provides one for IEnumerable&lt;T&gt;,
        /// for both syntactic sugar and it prevents full materialization of the collection.
        /// </summary>
        public static int IndexOf<TItem>(this IEnumerable<TItem> collection, TItem item)
        {
            int? i = TryGetIndexOf(collection, item);

            if (!i.HasValue)
            {
                throw new Exception($"{nameof(item)} not found.");
            }

            return i.Value;
        }

        /// <summary>
        /// Returns the list index of the first item that matches the predicate.
        /// Does not check duplicates, because that would make it slower.
        /// </summary>
        public static int IndexOf<TSource>(this IList<TSource> collection, Func<TSource, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int? index = TryGetIndexOf(collection, predicate);

            if (index.HasValue)
            {
                return index.Value;
            }

            throw new Exception("No item in the collection matches the predicate.");
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Max();
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Max(selector);
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Min();
        }

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Min(selector);
        }

        public static T PeekOrDefault<T>(this Stack<T> stack)
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            if (stack.Count == 0)
            {
                return default;
            }

            return stack.Peek();
        }

        public static T PopOrDefault<T>(this Stack<T> stack)
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            if (stack.Count == 0)
            {
                return default;
            }

            return stack.Pop();
        }

        public static T PeekOrDefault<T>(this Queue<T> queue)
        {
            if (queue == null) throw new ArgumentNullException(nameof(queue));

            if (queue.Count == 0)
            {
                return default;
            }

            return queue.Peek();
        }

        /// <summary>
        /// For some polymorphism between Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Add<T>(this Stack<T> stack, T item) => stack.Push(item);

        /// <summary>
        /// For some polymorphism between Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Remove<T>(this Stack<T> stack) => stack.Pop();

        /// <summary>
        /// For some polymorphism between Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Add<T>(this Queue<T> stack, T item) => stack.Enqueue(item);

        /// <summary>
        /// For some polymorphism between Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Remove<T>(this Queue<T> stack) => stack.Dequeue();

        public static double Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, double> selector)
            => collection.Select(selector).Product();

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

        /// <summary>
        /// Removes the first occurrence that matches the predicate.
        /// Throws an exception no item matches the predicate.
        /// Does not check duplicates, which makes it faster if you are sure only one item is in it.
        /// </summary>
        public static void RemoveFirst<TSource>(this IList<TSource> collection, Func<TSource, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int index = IndexOf(collection, predicate);
            collection.RemoveAt(index);
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// </param>
        public static T SingleOrDefaultWithClearException<T>(this IEnumerable<T> collection, Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleOrDefaultWithClearException(collection, keyIndicatorString);
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// </param>
        public static T SingleOrDefaultWithClearException<T>(
            this IEnumerable<T> collection,
            Func<T, bool> predicate,
            Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleOrDefaultWithClearException(collection, predicate, keyIndicatorString);
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleOrDefaultWithClearException<T>(this IEnumerable<T> collection, object keyIndicator)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            IList<T> items = collection.ToArray();

            switch (items.Count)
            {
                case 0: return default;
                case 1: return items[0];
                default: throw new NotUniqueException<T>(keyIndicator);
            }
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleOrDefaultWithClearException<T>(this IEnumerable<T> collection, Func<T, bool> predicate, object keyIndicator)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            IList<T> items = collection.Where(predicate).ToArray();

            switch (items.Count)
            {
                case 0: return default;
                case 1: return items[0];
                default: throw new NotUniqueException<T>(keyIndicator);
            }
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// </param>
        public static T SingleWithClearException<T>(this IEnumerable<T> collection, Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleWithClearException(collection, keyIndicatorString);
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// </param>
        public static T SingleWithClearException<T>(this IEnumerable<T> collection, Func<T, bool> predicate, Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleWithClearException(collection, predicate, keyIndicatorString);
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleWithClearException<T>(this IEnumerable<T> collection, object keyIndicator)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            IList<T> items = collection.ToArray();

            switch (items.Count)
            {
                case 0: throw new NotFoundException<T>(keyIndicator);
                case 1: return items[0];
                default: throw new NotUniqueException<T>(keyIndicator);
            }
        }

        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleWithClearException<T>(this IEnumerable<T> collection, Func<T, bool> predicate, object keyIndicator)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            IList<T> items = collection.Where(predicate).ToArray();

            switch (items.Count)
            {
                case 0: throw new NotFoundException<T>(keyIndicator);
                case 1: return items[0];
                default: throw new NotUniqueException<T>(keyIndicator);
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            switch (source)
            {
                case null:
                    throw new ArgumentNullException(nameof(source));

                case HashSet<T> hashSet:
                    return hashSet;

                default:
                    return new HashSet<T>(source);
            }
        }

        public static Dictionary<TKey, IList<TItem>> ToNonUniqueDictionary<TKey, TItem>(
            this IEnumerable<TItem> sourceCollection,
            Func<TItem, TKey> keySelector)
        {
            if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return sourceCollection.ToNonUniqueDictionary(keySelector, x => x);
        }

        public static Dictionary<TKey, IList<TElement>> ToNonUniqueDictionary<TKey, TElement>(
            this IEnumerable<IGrouping<TKey, TElement>> sourceGroups)
        {
            // NOTE: You cannot delegate to the generalized ToNonUniqueDictionary,
            // because it expects a singular element selector, not plural. So the following would not work:
            // sourceGroups.ToNonUniqueDictionary(x => x.Key, x => x.ToArray());

            if (sourceGroups == null) throw new ArgumentNullException(nameof(sourceGroups));

            var dictionary = new Dictionary<TKey, IList<TElement>>();

            foreach (IGrouping<TKey, TElement> sourceGroup in sourceGroups)
            {
                TKey key = sourceGroup.Key;

                dictionary.Add(key, sourceGroup.ToArray());
            }

            return dictionary;
        }

        public static Dictionary<TKey, IList<TDestItem>> ToNonUniqueDictionary<TKey, TSourceItem, TDestItem>(
            this IEnumerable<TSourceItem> sourceCollection,
            Func<TSourceItem, TKey> keySelector,
            Func<TSourceItem, TDestItem> elementSelector)
        {
            if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var dictionary = new Dictionary<TKey, IList<TDestItem>>();

            foreach (TSourceItem item in sourceCollection)
            {
                TKey key = keySelector(item);

                if (!dictionary.TryGetValue(key, out IList<TDestItem> elementsUnderKey))
                {
                    elementsUnderKey = new List<TDestItem>();
                    dictionary.Add(key, elementsUnderKey);
                }

                TDestItem element = elementSelector(item);
                elementsUnderKey.Add(element);
            }

            return dictionary;
        }

        public static string[] TrimAll(this IEnumerable<string> values, params char[] trimChars)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            return values.Select(x => x.Trim(trimChars)).ToArray();
        }

        /// <summary>
        /// Returns the list index of the first item that matches the predicate.
        /// Does not check duplicates, because that would make it slower.
        /// </summary>
        public static int? TryGetIndexOf<TSource>(this IEnumerable<TSource> collection, Func<TSource, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var i = 0;

            foreach (TSource item in collection)
            {
                if (predicate(item))
                {
                    return i;
                }

                i++;
            }

            return null;
        }

        /// <summary>
        /// IList&lt;T&gt; has an IndexOf method natively. This overload provides one for IEnumerable&lt;T&gt;,
        /// for both syntactic sugar and it prevents full materialization of the collection.
        /// This method prefixed with 'TryGet' returns null if the item is not found.
        /// </summary>
        public static int? TryGetIndexOf<TItem>(this IEnumerable<TItem> collection, TItem item)
        {
            var i = 0;

            foreach (TItem item2 in collection)
            {
                if (Equals(item2, item))
                {
                    return i;
                }

                i++;
            }

            return null;
        }

        /// <summary>
        /// Returns the list index of the first item that matches the predicate.
        /// Does not check duplicates, because that would make it slower.
        /// </summary>
        public static int? TryGetIndexOf<TSource>(this IList<TSource> collection, Func<TSource, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            for (var i = 0; i < collection.Count; i++)
            {
                TSource item = collection[i];

                if (predicate(item))
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the first occurrence that matches the predicate.
        /// Does nothing if no item matches the predicate.
        /// Does not check duplicates, which makes it faster if you are sure only one item is in it.
        /// Returns whether an item was actually removed from the collection.
        /// </summary>
        public static bool TryRemoveFirst<TSource>(this IList<TSource> collection, Func<TSource, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int? index = TryGetIndexOf(collection, predicate);

            if (!index.HasValue)
            {
                return false;
            }

            collection.RemoveAt(index.Value);
            return true;
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, T second) => first.Union(new[] { second });

        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, params T[] second) => first.Union((IEnumerable<T>)second);

        public static IEnumerable<T> Union<T>(this T first, IEnumerable<T> second) => new[] { first }.Union(second);

        /// <summary>
        /// Overload of Zip when you do not want to produce a result,
        /// you just want to process two collections side by side in another way.
        /// </summary>
        /// <see cref="Enumerable.Zip" />
        public static void Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Action<TFirst, TSecond> action)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (action == null) throw new ArgumentNullException(nameof(action));

            // Little dirty to do ToArray, whatevs.
            TFirst[] firstArray = first as TFirst[] ?? first.ToArray();
            TSecond[] secondArray = second as TSecond[] ?? second.ToArray();

            int count = Math.Min(firstArray.Length, secondArray.Length);

            for (var i = 0; i < count; i++)
            {
                action(firstArray[i], secondArray[i]);
            }
        }

        /// <summary> Overload of Zip without a result selector that returns tuples. </summary>
        /// <see cref="Enumerable.Zip" />
        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second)
            => first.Zip(second, (x, y) => (x, y));
    }
}
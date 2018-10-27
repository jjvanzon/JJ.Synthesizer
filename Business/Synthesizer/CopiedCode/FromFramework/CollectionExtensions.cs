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
        /// <summary>
        /// Add multiple items to a collection by means of a comma separated argument list, e.g. myCollection.Add(1, 5,
        /// 12);
        /// </summary>
        public static void Add<T>(this IList<T> collection, params T[] items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (T x in items)
            {
                collection.Add(x);
            }
        }

        /// <summary>
        /// For some polymorphism between Lists, Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Add<T>(this Stack<T> stack, T item) => stack.Push(item);

        /// <summary>
        /// For some polymorphism between Lists, Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Add<T>(this Queue<T> stack, T item) => stack.Enqueue(item);

        /// <summary> AddRange is a member of List&lt;T&gt;. Here is an overload for HashSet&lt;T&gt;. </summary>
        public static void AddRange<T>(this HashSet<T> dest, IEnumerable<T> source)
        {
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            if (source == null) throw new ArgumentNullException(nameof(source));

            foreach (T item in source)
            {
                dest.Add(item);
            }
        }

        /// <summary> AddRange is a member of List&lt;T&gt;. Here is an overload for IEnumerable&lt;T&gt;. </summary>
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (T x in items)
            {
                collection.Add(x);
            }
        }

        /// <summary>
        /// Converts a single item to an array. Example: int[] myInts = 3.AsArray();
        /// (The names 'ToArray' and 'ToList' conflicted with other overloads.)
        /// </summary>
        public static TItem[] AsArray<TItem>(this TItem item) => new[] { item };

        /// <summary>
        /// Converts a single item to an enumerable.
        /// Example: IEnumerable&lt;int&gt; myEnumerable = 3.AsEnumerable();
        /// </summary>
        public static IEnumerable<TItem> AsEnumerable<TItem>(this TItem item) => new[] { item };

        /// <summary>
        /// Converts a single item to a list. Example: List&lt;int&gt; myInts = 3.AsList();
        /// (The names 'ToArray' and 'ToList' conflicted with other overloads.)
        /// </summary>
        public static List<TItem> AsList<TItem>(this TItem item) => new List<TItem> { item };

        /// <summary> Overload of Concat that takes a single item, e.g. myCollection.Concat(myItem); </summary>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, T second) => first.Concat(new[] { second });

        /// <summary> Overload of Concat that takes a comma separated argument list, e.g. myCollection.Concat(4, 7, 12); </summary>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] second) => first.Concat((IEnumerable<T>)second);

        /// <summary>
        /// Overload of Concat that starts with a single item and then adds a collection to it e.g.
        /// myItem.Concat(myCollection);
        /// </summary>
        public static IEnumerable<T> Concat<T>(this T first, IEnumerable<T> second) => new[] { first }.Concat(second);

        /// <summary>
        /// Returns all possible pairs of items combining one from collection1 with one from collection2. The pairs are
        /// returned as tuples.
        /// </summary>
        public static IEnumerable<(T1, T2)> CrossJoin<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2)
            => CrossJoin(collection1, collection2, (x, y) => (x, y));

        /// <summary>
        /// Returns all possible combinations of items, combining one from collection1 with one from collection2. The
        /// resultant item is produced by the resultSelector.
        /// </summary>
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

        /// <summary>
        /// Returns all possible combinations of items, combining one from the first collection with one from second collection and
        /// one from the third collection etc.
        /// The resultant item is produced by the resultSelector, which takes an enumerable of items as input.
        /// This overload will only work if all item types are the same but you can use enumerables of object to use items of
        /// multiple types.
        /// </summary>
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

            foreach (IEnumerable<TItem> collection in collections.Reverse())
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

        /// <summary>
        /// Distinct that takes a key selector that determines what makes an item unique, e.g. myItems.Distinct(x => x.LastName);
        /// For multi-part as keys, use `myItems.Distinct(x => new { x.FirstName, x.LastName });`
        /// </summary>
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

        /// <summary>
        /// Returns distinct arrays. So checks the equality of all items in the arrays and returns the unique arrays.
        /// (Actually, not only arrays, any enumerable.) E.g. severalArrays.Distinct();
        /// </summary>
        public static IEnumerable<TItem[]> DistinctMany<TItem>(this IEnumerable<TItem[]> enumerables)
            => DistinctMany<TItem[], TItem>(enumerables);

        /// <summary>
        /// Returns distinct arrays. So checks the equality of all items in the arrays and returns the unique arrays.
        /// (Actually, not only arrays, any enumerable.) E.g. severalArrays.Distinct();
        /// </summary>
        public static IEnumerable<IEnumerable<TItem>> DistinctMany<TItem>(this IEnumerable<IEnumerable<TItem>> enumerables)
            => DistinctMany<IEnumerable<TItem>, TItem>(enumerables);

        /// <summary>
        /// Returns distinct arrays. So checks the equality of all items in the arrays and returns the unique arrays.
        /// (Actually, not only arrays, any enumerable.) E.g. severalArrays.Distinct();
        /// </summary>
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

        /// <summary> An overload of Except that takes just a single item, e.g. myCollection.Except(myItem); </summary>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T x)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Except(new[] { x });
        }

        /// <summary>
        /// An overload of Except that takes a predicate, e.g. myCollection.Except(x => string.Equals("Blah");
        /// (This is the same as a negated Where predicate, but if you are already thinking in terms of Except,
        /// this might express your intent clearer.)
        /// </summary>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return source.Where(x => !predicate(x));
        }

        /// <summary>
        /// The original Except() method from .NET automatically does a distinct, which is something you do not always
        /// want, so there is one that gives you the choice.
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

        /// <summary>
        /// A regular First() will give you an exception if there are no elements in the collection.
        /// But not a very clear exception, like 'Sequence contains no elements.'
        /// FirstWithClearException() will allow you to do a First,
        /// but get a clearer exception message e.g. 'Product with key { group = "Shoes" } not found.'
        /// Only really works if you filtered by something.
        /// </summary>
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

        /// <summary>
        /// A regular First() will give you an exception if there are no elements in the collection.
        /// But not a very clear exception, like 'Sequence contains no elements.'
        /// FirstWithClearException() will allow you to do a First,
        /// but get a clearer exception message e.g. 'Product with key { group = "Shoes" } not found.'
        /// Only really works if you filtered by something.
        /// </summary>
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

        /// <summary>
        /// Not all collection types have the ForEach method. Here you have an overload for IEnumerable&lt;T&gt; so you
        /// can use it for more collection types.
        /// </summary>
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
        /// (List&lt;T&gt; has FindIndex natively, but other collection types do not.)
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
        /// Returns the list index of the first item that matches the predicate.
        /// E.g. int index = items.IndexOf(x => x.ID == 3);
        /// Does not check duplicates, because that would make it slower.
        /// (List&lt;T&gt; has FindIndex natively, but other collection types do not.)
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

        /// <summary> Same as Max(), but instead of crashing when zero items, returns default instead. </summary>
        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Max();
        }

        /// <summary> Same as Max(), but instead of crashing when zero items, returns default instead. </summary>
        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Max(selector);
        }

        /// <summary> Same as Min(), but instead of crashing when zero items, returns default instead. </summary>
        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Min();
        }

        /// <summary> Same as Min(), but instead of crashing when zero items, returns default instead. </summary>
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!source.Any())
            {
                return default;
            }

            return source.Min(selector);
        }

        /// <summary> Same as Peek(), but instead of crashing when zero items, returns default instead. </summary>
        public static T PeekOrDefault<T>(this Stack<T> stack)
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            if (stack.Count == 0)
            {
                return default;
            }

            return stack.Peek();
        }

        /// <summary> Same as Pop(), but instead of crashing when zero items, returns default instead. </summary>
        public static T PopOrDefault<T>(this Stack<T> stack)
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            if (stack.Count == 0)
            {
                return default;
            }

            return stack.Pop();
        }

        /// <summary> Same as Peek(), but instead of crashing when zero items, returns default instead. </summary>
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

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static float Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, float> selector)
            => collection.Select(selector).Product();

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static float Product(this IEnumerable<float> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            float product = collection.FirstOrDefault();

            foreach (float value in collection.Skip(1))
            {
                product *= value;
            }

            return product;
        }

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static int Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, int> selector)
            => collection.Select(selector).Product();

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static int Product(this IEnumerable<int> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int product = collection.FirstOrDefault();

            foreach (int value in collection.Skip(1))
            {
                product *= value;
            }

            return product;
        }

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static long Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, long> selector)
            => collection.Select(selector).Product();

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static long Product(this IEnumerable<long> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            long product = collection.FirstOrDefault();

            foreach (long value in collection.Skip(1))
            {
                product *= value;
            }

            return product;
        }

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static decimal Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, decimal> selector)
            => collection.Select(selector).Product();

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static decimal Product(this IEnumerable<decimal> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            decimal product = collection.FirstOrDefault();

            foreach (decimal value in collection.Skip(1))
            {
                product *= value;
            }

            return product;
        }

        /// <summary>
        /// For some polymorphism between Lists, Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Remove<T>(this Stack<T> stack) => stack.Pop();

        /// <summary>
        /// For some polymorphism between Lists, Stacks and Queues, there are these Add and Remove extension methods,
        /// in place of Push, Pop, Enqueue and Dequeue.
        /// </summary>
        public static void Remove<T>(this Queue<T> stack) => stack.Dequeue();

        /// <summary>
        /// Removes the first occurrence that matches the predicate.
        /// Throws an exception no item matches the predicate.
        /// Does not check duplicates, which makes it faster if you are sure only one item is in it.
        /// Example: myCollection.RemoveFirst(x => x.ID == 3);
        /// </summary>
        public static void RemoveFirst<TSource>(this IList<TSource> collection, Func<TSource, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int index = IndexOf(collection, predicate);
            collection.RemoveAt(index);
        }

        /// <summary>
        /// Repeats a collection a number of times and returns a new collection.
        /// E.g. new[] { 1, 2 }.Repeat(3);
        /// Returns a collection { 1, 2, 1, 2, 1, 2 }
        /// </summary>
        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            for (var i = 0; i < count; i++)
            {
                foreach (T item in collection)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// A regular SingleOrDefault() will give you an exception if there are multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleOrDefaultWithClearException() will allow you to do a SingleOrDefault,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 1234 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleOrDefaultWithClearException<T>(this IEnumerable<T> collection, Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleOrDefaultWithClearException(collection, keyIndicatorString);
        }

        /// <summary>
        /// A regular SingleOrDefault() will give you an exception if there are multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleOrDefaultWithClearException() will allow you to do a SingleOrDefault,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 123 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleOrDefaultWithClearException<T>(
            this IEnumerable<T> collection,
            Func<T, bool> predicate,
            Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleOrDefaultWithClearException(collection, predicate, keyIndicatorString);
        }

        /// <summary>
        /// A regular SingleOrDefault() will give you an exception if there are multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleOrDefaultWithClearException() will allow you to do a SingleOrDefault,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 123 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
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

        /// <summary>
        /// A regular SingleOrDefault() will give you an exception if there are multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleOrDefaultWithClearException() will allow you to do a SingleOrDefault,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 123 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleOrDefaultWithClearException<T>(
            this IEnumerable<T> collection,
            Func<T, bool> predicate,
            object keyIndicator)
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

        /// <summary>
        /// A regular Single() will give you an exception if there are no elements or multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleWithClearException() will allow you to do a Single,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 123 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleWithClearException<T>(this IEnumerable<T> collection, Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleWithClearException(collection, keyIndicatorString);
        }

        /// <summary>
        /// A regular Single() will give you an exception if there are no elements or multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleWithClearException() will allow you to do a Single,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 123 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
        /// <param name="keyIndicator">
        /// Not used for filtering, only used in the exception message.
        /// You can use an anonymous type e.g. new { id, cultureName } and it will translate that to something like { id = 1234,
        /// cultureName = nl-NL }.
        /// </param>
        public static T SingleWithClearException<T>(
            this IEnumerable<T> collection,
            Func<T, bool> predicate,
            Expression<Func<object>> keyIndicator)
        {
            string keyIndicatorString = ExpressionHelper.GetText(keyIndicator);
            return SingleWithClearException(collection, predicate, keyIndicatorString);
        }

        /// <summary>
        /// A regular Single() will give you an exception if there are no elements or multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleWithClearException() will allow you to do a Single,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 123 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
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

        /// <summary>
        /// A regular Single() will give you an exception if there are no elements or multiple elements in the collection.
        /// But not a very clear exception, like 'The input sequence contains more than one element.'
        /// SingleWithClearException() will allow you to do a Single,
        /// but get a clearer exception message e.g. 'Product with key { productNumber = 123 } not unique.'
        /// Only really works if you filtered by something.
        /// </summary>
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

        /// <summary>
        /// Yes, you could write "new HashSet(someCollection)".
        /// But there are already the ToArray() and ToList() methods, so why not a ToHashSet() method to make it consistent?
        /// </summary>
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

        /// <summary>
        /// Similar to ToDictionary, but allows the same key to be present more than once.
        /// A GroupBy can overcome that limitation too, but sometimes it is nice to have the return value be a Dictionary,
        /// which might make for a faster lookup too.
        /// </summary>
        public static Dictionary<TKey, IList<TItem>> ToNonUniqueDictionary<TKey, TItem>(
            this IEnumerable<TItem> sourceCollection,
            Func<TItem, TKey> keySelector)
        {
            if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return sourceCollection.ToNonUniqueDictionary(keySelector, x => x);
        }

        /// <summary>
        /// GroupBy might allow grouping by a non-unique key, but converting it to a Dictionary might make for a faster
        /// lookup.
        /// </summary>
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

        /// <summary>
        /// Similar to ToDictionary, but allows the same key to be present more than once.
        /// A GroupBy can overcome that limitation too, but sometimes it is nice to have the return value be a Dictionary,
        /// which might make for a faster lookup too.
        /// </summary>
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

        /// <summary> Trims all the strings in the collection. </summary>
        public static string[] TrimAll(this IEnumerable<string> values, params char[] trimChars)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            return values.Select(x => x.Trim(trimChars)).ToArray();
        }

        /// <summary>
        /// Returns the list index of the first item that matches the predicate.
        /// Does not check duplicates, because that would make it slower.
        /// This method prefixed with 'TryGet' returns null if the item is not found.
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
        /// This method prefixed with 'TryGet' returns null if the item is not found.
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
        /// Example: myCollection.TryRemoveFirst(x => x.ID == 3);
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

        /// <summary> Overload of Union that takes a single item, e.g. myCollection.Union(myItem); </summary>
        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, T second) => first.Union(new[] { second });

        /// <summary> Overload of Union that takes a comma separated argument list, e.g. myCollection.Union(4, 7, 12); </summary>
        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, params T[] second) => first.Union((IEnumerable<T>)second);

        /// <summary>
        /// Overload of Union that takes starts with a single item and then adds a collection to it e.g.
        /// myItem.Union(myCollection);
        /// </summary>
        public static IEnumerable<T> Union<T>(this T first, IEnumerable<T> second) => new[] { first }.Union(second);

        /// <summary>
        /// Overload of Zip when you do not want to produce a result,
        /// you just want to process two collections side by side in another way.
        /// </summary>
        /// <see cref="Enumerable.Zip" />
        public static void Zip<TFirst, TSecond>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Action<TFirst, TSecond> action)
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
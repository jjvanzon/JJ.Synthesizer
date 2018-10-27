

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
// ReSharper disable BuiltInTypeReferenceStyle
#pragma warning disable IDE0049 // Simplify Names

namespace JJ.Framework.Collections
{
    [PublicAPI]
    public static class CollectionExtensions_BinarySearchInexact
    {
					
			// Decimal

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Decimal valueBefore, Decimal valueAfter) BinarySearchInexact(this IEnumerable<Decimal> sortedCollection, Decimal input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Decimal valueBefore, Decimal valueAfter) BinarySearchInexact(this Decimal[] sortedCollection, Decimal input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Decimal valueBefore, Decimal valueAfter) BinarySearchInexact(
				this IEnumerable<Decimal> sortedCollection,
				int halfCount,
				Decimal min,
				Decimal max,
				Decimal input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), halfCount, min, max, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Decimal valueBefore, Decimal valueAfter) BinarySearchInexact(
				this Decimal[] sortedCollection,
				int halfCount,
				Decimal min,
				Decimal max,
				Decimal input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, halfCount, min, max, input);
				
			// Double

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Double valueBefore, Double valueAfter) BinarySearchInexact(this IEnumerable<Double> sortedCollection, Double input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Double valueBefore, Double valueAfter) BinarySearchInexact(this Double[] sortedCollection, Double input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Double valueBefore, Double valueAfter) BinarySearchInexact(
				this IEnumerable<Double> sortedCollection,
				int halfCount,
				Double min,
				Double max,
				Double input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), halfCount, min, max, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Double valueBefore, Double valueAfter) BinarySearchInexact(
				this Double[] sortedCollection,
				int halfCount,
				Double min,
				Double max,
				Double input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, halfCount, min, max, input);
				
			// Int16

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int16 valueBefore, Int16 valueAfter) BinarySearchInexact(this IEnumerable<Int16> sortedCollection, Int16 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int16 valueBefore, Int16 valueAfter) BinarySearchInexact(this Int16[] sortedCollection, Int16 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int16 valueBefore, Int16 valueAfter) BinarySearchInexact(
				this IEnumerable<Int16> sortedCollection,
				int halfCount,
				Int16 min,
				Int16 max,
				Int16 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), halfCount, min, max, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int16 valueBefore, Int16 valueAfter) BinarySearchInexact(
				this Int16[] sortedCollection,
				int halfCount,
				Int16 min,
				Int16 max,
				Int16 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, halfCount, min, max, input);
				
			// Int32

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int32 valueBefore, Int32 valueAfter) BinarySearchInexact(this IEnumerable<Int32> sortedCollection, Int32 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int32 valueBefore, Int32 valueAfter) BinarySearchInexact(this Int32[] sortedCollection, Int32 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int32 valueBefore, Int32 valueAfter) BinarySearchInexact(
				this IEnumerable<Int32> sortedCollection,
				int halfCount,
				Int32 min,
				Int32 max,
				Int32 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), halfCount, min, max, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int32 valueBefore, Int32 valueAfter) BinarySearchInexact(
				this Int32[] sortedCollection,
				int halfCount,
				Int32 min,
				Int32 max,
				Int32 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, halfCount, min, max, input);
				
			// Int64

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int64 valueBefore, Int64 valueAfter) BinarySearchInexact(this IEnumerable<Int64> sortedCollection, Int64 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int64 valueBefore, Int64 valueAfter) BinarySearchInexact(this Int64[] sortedCollection, Int64 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int64 valueBefore, Int64 valueAfter) BinarySearchInexact(
				this IEnumerable<Int64> sortedCollection,
				int halfCount,
				Int64 min,
				Int64 max,
				Int64 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), halfCount, min, max, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Int64 valueBefore, Int64 valueAfter) BinarySearchInexact(
				this Int64[] sortedCollection,
				int halfCount,
				Int64 min,
				Int64 max,
				Int64 input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, halfCount, min, max, input);
				
			// Single

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Single valueBefore, Single valueAfter) BinarySearchInexact(this IEnumerable<Single> sortedCollection, Single input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Single valueBefore, Single valueAfter) BinarySearchInexact(this Single[] sortedCollection, Single input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Single valueBefore, Single valueAfter) BinarySearchInexact(
				this IEnumerable<Single> sortedCollection,
				int halfCount,
				Single min,
				Single max,
				Single input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection.ToArray(), halfCount, min, max, input);

			/// <summary>
			/// Searches a sorted collection in an efficient way.
			/// Returns the value before and the value after
			/// if a value to search is in between two values in the list.
			/// Will only return a meaningful result if the collection is sorted.
			/// </summary>
			/// <param name="sortedCollection"> Not checked for null, for performance. </param>
			public static (Single valueBefore, Single valueAfter) BinarySearchInexact(
				this Single[] sortedCollection,
				int halfCount,
				Single min,
				Single max,
				Single input)
				=> CollectionHelper.BinarySearchInexact(sortedCollection, halfCount, min, max, input);
	    }
}

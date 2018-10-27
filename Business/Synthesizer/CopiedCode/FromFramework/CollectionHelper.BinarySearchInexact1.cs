

using System;
using System.Runtime.CompilerServices;
// ReSharper disable BuiltInTypeReferenceStyle
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable IDE0049 // Simplify Names

namespace JJ.Framework.Collections
{
	// Do not use JetBrains.Annotations, because this code is statically compiled into code that does not support it.

	public static partial class CollectionHelper
	{
		
			// Decimal

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			public static (Decimal valueBefore, Decimal valueAfter) BinarySearchInexact(Decimal[] sortedArray, Decimal input)
			{
				BinarySearchInexact(sortedArray, input, out Decimal valueBefore, out Decimal valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Decimal[] sortedArray,
				Decimal input,
				out Decimal valueBefore,
				out Decimal valueAfter)
			{
				int count = sortedArray.Length;
				Decimal min = sortedArray[0];
				Decimal max = sortedArray[count - 1];
				int halfCount = count >> 1;

				BinarySearchInexact(sortedArray, halfCount, min, max, input, out valueBefore, out valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static (Decimal valueBefore, Decimal valueAfter) BinarySearchInexact(
				Decimal[] sortedArray,
				int halfCount,
				Decimal min,
				Decimal max,
				Decimal input)
			{
				BinarySearchInexact(sortedArray, halfCount, min, max, input, out Decimal valueBefore, out Decimal valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Decimal[] sortedArray,
				int halfCount,
				Decimal min,
				Decimal max,
				Decimal input,
				out Decimal valueBefore,
				out Decimal valueAfter)
			{
				int sampleIndex = halfCount;
				int jump = halfCount;

				valueBefore = min;
				valueAfter = max;

				while (jump != 0)
				{
					Decimal sample = sortedArray[sampleIndex];

					jump = jump >> 1;

					if (input >= sample)
					{
						valueBefore = sample;

						sampleIndex += jump;
					}
					else
					{
						valueAfter = sample;

						sampleIndex -= jump;
					}
				}
			}
	
			// Double

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			public static (Double valueBefore, Double valueAfter) BinarySearchInexact(Double[] sortedArray, Double input)
			{
				BinarySearchInexact(sortedArray, input, out Double valueBefore, out Double valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Double[] sortedArray,
				Double input,
				out Double valueBefore,
				out Double valueAfter)
			{
				int count = sortedArray.Length;
				Double min = sortedArray[0];
				Double max = sortedArray[count - 1];
				int halfCount = count >> 1;

				BinarySearchInexact(sortedArray, halfCount, min, max, input, out valueBefore, out valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static (Double valueBefore, Double valueAfter) BinarySearchInexact(
				Double[] sortedArray,
				int halfCount,
				Double min,
				Double max,
				Double input)
			{
				BinarySearchInexact(sortedArray, halfCount, min, max, input, out Double valueBefore, out Double valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Double[] sortedArray,
				int halfCount,
				Double min,
				Double max,
				Double input,
				out Double valueBefore,
				out Double valueAfter)
			{
				int sampleIndex = halfCount;
				int jump = halfCount;

				valueBefore = min;
				valueAfter = max;

				while (jump != 0)
				{
					Double sample = sortedArray[sampleIndex];

					jump = jump >> 1;

					if (input >= sample)
					{
						valueBefore = sample;

						sampleIndex += jump;
					}
					else
					{
						valueAfter = sample;

						sampleIndex -= jump;
					}
				}
			}
	
			// Int16

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			public static (Int16 valueBefore, Int16 valueAfter) BinarySearchInexact(Int16[] sortedArray, Int16 input)
			{
				BinarySearchInexact(sortedArray, input, out Int16 valueBefore, out Int16 valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Int16[] sortedArray,
				Int16 input,
				out Int16 valueBefore,
				out Int16 valueAfter)
			{
				int count = sortedArray.Length;
				Int16 min = sortedArray[0];
				Int16 max = sortedArray[count - 1];
				int halfCount = count >> 1;

				BinarySearchInexact(sortedArray, halfCount, min, max, input, out valueBefore, out valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static (Int16 valueBefore, Int16 valueAfter) BinarySearchInexact(
				Int16[] sortedArray,
				int halfCount,
				Int16 min,
				Int16 max,
				Int16 input)
			{
				BinarySearchInexact(sortedArray, halfCount, min, max, input, out Int16 valueBefore, out Int16 valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Int16[] sortedArray,
				int halfCount,
				Int16 min,
				Int16 max,
				Int16 input,
				out Int16 valueBefore,
				out Int16 valueAfter)
			{
				int sampleIndex = halfCount;
				int jump = halfCount;

				valueBefore = min;
				valueAfter = max;

				while (jump != 0)
				{
					Int16 sample = sortedArray[sampleIndex];

					jump = jump >> 1;

					if (input >= sample)
					{
						valueBefore = sample;

						sampleIndex += jump;
					}
					else
					{
						valueAfter = sample;

						sampleIndex -= jump;
					}
				}
			}
	
			// Int32

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			public static (Int32 valueBefore, Int32 valueAfter) BinarySearchInexact(Int32[] sortedArray, Int32 input)
			{
				BinarySearchInexact(sortedArray, input, out Int32 valueBefore, out Int32 valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Int32[] sortedArray,
				Int32 input,
				out Int32 valueBefore,
				out Int32 valueAfter)
			{
				int count = sortedArray.Length;
				Int32 min = sortedArray[0];
				Int32 max = sortedArray[count - 1];
				int halfCount = count >> 1;

				BinarySearchInexact(sortedArray, halfCount, min, max, input, out valueBefore, out valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static (Int32 valueBefore, Int32 valueAfter) BinarySearchInexact(
				Int32[] sortedArray,
				int halfCount,
				Int32 min,
				Int32 max,
				Int32 input)
			{
				BinarySearchInexact(sortedArray, halfCount, min, max, input, out Int32 valueBefore, out Int32 valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Int32[] sortedArray,
				int halfCount,
				Int32 min,
				Int32 max,
				Int32 input,
				out Int32 valueBefore,
				out Int32 valueAfter)
			{
				int sampleIndex = halfCount;
				int jump = halfCount;

				valueBefore = min;
				valueAfter = max;

				while (jump != 0)
				{
					Int32 sample = sortedArray[sampleIndex];

					jump = jump >> 1;

					if (input >= sample)
					{
						valueBefore = sample;

						sampleIndex += jump;
					}
					else
					{
						valueAfter = sample;

						sampleIndex -= jump;
					}
				}
			}
	
			// Int64

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			public static (Int64 valueBefore, Int64 valueAfter) BinarySearchInexact(Int64[] sortedArray, Int64 input)
			{
				BinarySearchInexact(sortedArray, input, out Int64 valueBefore, out Int64 valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Int64[] sortedArray,
				Int64 input,
				out Int64 valueBefore,
				out Int64 valueAfter)
			{
				int count = sortedArray.Length;
				Int64 min = sortedArray[0];
				Int64 max = sortedArray[count - 1];
				int halfCount = count >> 1;

				BinarySearchInexact(sortedArray, halfCount, min, max, input, out valueBefore, out valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static (Int64 valueBefore, Int64 valueAfter) BinarySearchInexact(
				Int64[] sortedArray,
				int halfCount,
				Int64 min,
				Int64 max,
				Int64 input)
			{
				BinarySearchInexact(sortedArray, halfCount, min, max, input, out Int64 valueBefore, out Int64 valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Int64[] sortedArray,
				int halfCount,
				Int64 min,
				Int64 max,
				Int64 input,
				out Int64 valueBefore,
				out Int64 valueAfter)
			{
				int sampleIndex = halfCount;
				int jump = halfCount;

				valueBefore = min;
				valueAfter = max;

				while (jump != 0)
				{
					Int64 sample = sortedArray[sampleIndex];

					jump = jump >> 1;

					if (input >= sample)
					{
						valueBefore = sample;

						sampleIndex += jump;
					}
					else
					{
						valueAfter = sample;

						sampleIndex -= jump;
					}
				}
			}
	
			// Single

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			public static (Single valueBefore, Single valueAfter) BinarySearchInexact(Single[] sortedArray, Single input)
			{
				BinarySearchInexact(sortedArray, input, out Single valueBefore, out Single valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary> Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted. </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Single[] sortedArray,
				Single input,
				out Single valueBefore,
				out Single valueAfter)
			{
				int count = sortedArray.Length;
				Single min = sortedArray[0];
				Single max = sortedArray[count - 1];
				int halfCount = count >> 1;

				BinarySearchInexact(sortedArray, halfCount, min, max, input, out valueBefore, out valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static (Single valueBefore, Single valueAfter) BinarySearchInexact(
				Single[] sortedArray,
				int halfCount,
				Single min,
				Single max,
				Single input)
			{
				BinarySearchInexact(sortedArray, halfCount, min, max, input, out Single valueBefore, out Single valueAfter);
				return (valueBefore, valueAfter);
			}

			/// <summary>
			/// Searches a sorted collection in an efficient way. Returns the value before and the value after, if a value to search is in between two values in the list. Will only return a meaningful result if the collection is sorted.
			/// Overload with more values you supply yourself: halfLength, min and max, that you could cache yourself for performance.
			/// </summary>
			/// <param name="sortedArray"> Not checked for null, for performance. </param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void BinarySearchInexact(
				Single[] sortedArray,
				int halfCount,
				Single min,
				Single max,
				Single input,
				out Single valueBefore,
				out Single valueAfter)
			{
				int sampleIndex = halfCount;
				int jump = halfCount;

				valueBefore = min;
				valueAfter = max;

				while (jump != 0)
				{
					Single sample = sortedArray[sampleIndex];

					jump = jump >> 1;

					if (input >= sample)
					{
						valueBefore = sample;

						sampleIndex += jump;
					}
					else
					{
						valueAfter = sample;

						sampleIndex -= jump;
					}
				}
			}
		}
}
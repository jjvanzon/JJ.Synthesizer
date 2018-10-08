using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace JJ.Framework.Collections
{
    [PublicAPI]
	public static class CollectionExtensions_Recursive_IEnumerable
	{
		/// <summary> Does not include the collection it is executed upon in the result. </summary>
		public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> sourceCollection, Func<T, IEnumerable<T>> selector)
		{
			var destHashSet = new HashSet<T>();
			SelectRecursive(sourceCollection, selector, destHashSet);
			return destHashSet;
		}

		/// <summary> Does not include the collection it is executed upon in the result. </summary>
		private static void SelectRecursive<T>(IEnumerable<T> sourceCollection, Func<T, IEnumerable<T>> selector, HashSet<T> destHashSet)
		{
			if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			foreach (T sourceItem in sourceCollection)
			{
				if (sourceItem != null)
				{
					SelectRecursive(sourceItem, selector, destHashSet);
				}
			}
		}

		/// <summary> Does not include the item it is executed upon in the result. </summary>
		public static IEnumerable<T> SelectRecursive<T>(this T sourceItem, Func<T, IEnumerable<T>> selector)
		{
			var destHashSet = new HashSet<T>();
			SelectRecursive(sourceItem, selector, destHashSet);
			return destHashSet;
		}

		/// <summary> Does not include the item it is executed upon in the result. </summary>
		private static void SelectRecursive<T>(T sourceItem, Func<T, IEnumerable<T>> selector, HashSet<T> destHashSet)
		{
			if (sourceItem == null) throw new ArgumentNullException(nameof(sourceItem));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			IEnumerable<T> destItems = selector(sourceItem);

			UnionRecursive(destItems, selector, destHashSet);
		}

		/// <summary> Includes the collection it is executed upon in the result. </summary>
		public static IEnumerable<T> UnionRecursive<T>(this IEnumerable<T> sourceCollection, Func<T, IEnumerable<T>> selector)
		{
			var destHashSet = new HashSet<T>();
			UnionRecursive(sourceCollection, selector, destHashSet);
			return destHashSet;
		}

		/// <summary> Includes the collection it is executed upon in the result. </summary>
		private static void UnionRecursive<T>(IEnumerable<T> sourceCollection, Func<T, IEnumerable<T>> selector, HashSet<T> destHashSet)
		{
			if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			foreach (T sourceItem in sourceCollection)
			{
				if (!destHashSet.Add(sourceItem))
				{
					break;
				}

				SelectRecursive(sourceItem, selector, destHashSet);
			}
		}

		/// <summary> Includes the item it is executed upon in the result. </summary>
		public static IEnumerable<T> UnionRecursive<T>(this T sourceItem, Func<T, IEnumerable<T>> selector)
		{
			var destHashSet = new HashSet<T>();
			UnionRecursive(sourceItem, selector, destHashSet);
			return destHashSet;
		}

		/// <summary> Includes the item it is executed upon in the result. </summary>
		private static void UnionRecursive<T>(T sourceItem, Func<T, IEnumerable<T>> selector, HashSet<T> destHashSet)
		{
			if (sourceItem == null) throw new ArgumentNullException(nameof(sourceItem));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			if (!destHashSet.Add(sourceItem))
			{
				return;
			}

			SelectRecursive(sourceItem, selector, destHashSet);
		}
	}
}

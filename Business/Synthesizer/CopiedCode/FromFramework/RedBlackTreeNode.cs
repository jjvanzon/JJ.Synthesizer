using System;

namespace JJ.Framework.Collections
{
	internal class RedBlackTreeNode<TKey, TValue>
		where TKey : IComparable
	{
		public TKey Key { get; set; }
		public TValue Value { get; set; }
		public RedBlackTreeColorEnum Color { get; set; }
		public RedBlackTreeNode<TKey, TValue> Left { get; set; }
		public RedBlackTreeNode<TKey, TValue> Right { get; set; }
		public RedBlackTreeNode<TKey, TValue> Parent { get; set; }

		public RedBlackTreeNode(TKey key, TValue value, RedBlackTreeColorEnum color)
		{
			Key = key;
			Value = value;
			Color = color;
		}
		public RedBlackTreeNode<TKey, TValue> GetGrandparent()
		{
			if (Parent == null) throw new ArgumentNullException(nameof(Parent)); // Not the root node
			// ReSharper disable once NotResolvedInText
			if (Parent.Parent == null) throw new ArgumentNullException("Parent.Parent");  // Not child of root

			return Parent.Parent;
		}

		public RedBlackTreeNode<TKey, TValue> TryGetSibling()
		{
			if (Parent == null) throw new ArgumentNullException(nameof(Parent));

			if (this == Parent.Left)
			{
				return Parent.Right;
			}
			else
			{
				return Parent.Left;
			}
		}

		public RedBlackTreeNode<TKey, TValue> GetUncle()
		{
			if (Parent == null) throw new ArgumentNullException(nameof(Parent)); // Root node has no uncle
			if (Parent.Parent == null) throw new ArgumentNullException(nameof(Parent.Parent)); // Children of root have no uncle

			return Parent.TryGetSibling();
		}
	}
}
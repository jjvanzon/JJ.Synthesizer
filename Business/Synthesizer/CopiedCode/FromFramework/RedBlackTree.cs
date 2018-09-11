/* Jan-Joost van Zon, 2016-02-03:
This C# implementation was derived from the Java implementation retrieved from the internet. See next comment block. */

/* The authors of this work have released all rights to it and placed it
in the public domain under the Creative Commons CC0 1.0 waiver
(http://creativecommons.org/publicdomain/zero/1.0/).

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Retrieved from: http://en.literateprograms.org/Red-black_tree_(Java)?oldid=19200
*/

using System;
using JetBrains.Annotations;

namespace JJ.Framework.Collections
{
	/// <summary>
	/// Provides insert, delete and lookup operators that all have complexity O(log n).
	/// It can also at any time return the maximum and minimum with the same performance.
	/// It will prevent duplicates from being added (duplicates are ignored).
	/// You can use key-value pairs, instead of just single values.
	/// </summary>
	[PublicAPI]
	public class RedBlackTree<TKey, TValue>
		where TKey : IComparable
	{
		private RedBlackTreeNode<TKey, TValue> _root;

		private static RedBlackTreeColorEnum NullCoalescedNodeColor(RedBlackTreeNode<TKey, TValue> node) => node?.Color ?? RedBlackTreeColorEnum.Black;

	    public TValue Lookup(TKey key)
		{
			RedBlackTreeNode<TKey, TValue> node = LookupNode(key);

			if (node == null)
			{
				//return null;
				return default; // DIRTY: default does not say much about whether the value is in there.
			}

			return node.Value;
		}

		public void Insert(TKey key, TValue value)
		{
			var insertedNode = new RedBlackTreeNode<TKey, TValue>(key, value, RedBlackTreeColorEnum.Red);

			if (_root == null)
			{
				_root = insertedNode;
			}
			else
			{
				RedBlackTreeNode<TKey, TValue> node = _root;
				while (true)
				{
					int compResult = key.CompareTo(node.Key);
					if (compResult == 0)
					{
						node.Value = value;
						return;
					}
					else if (compResult < 0)
					{
						if (node.Left == null)
						{
							node.Left = insertedNode;
							break;
						}
						else
						{
							node = node.Left;
						}
					}
					else
					{
						if (node.Right == null)
						{
							node.Right = insertedNode;
							break;
						}
						else
						{
							node = node.Right;
						}
					}
				}
				insertedNode.Parent = node;
			}

			InsertCase1(insertedNode);
		}

		public void Delete(TKey key)
		{
			RedBlackTreeNode<TKey, TValue> node = LookupNode(key);

			if (node == null)
			{
				return;  // Key not found, do nothing
			}

			if (node.Left != null && node.Right != null)
			{
				// Copy key/value from predecessor and then delete it instead
				RedBlackTreeNode<TKey, TValue> predecessor = GetMaximumNode(node.Left);
				node.Key = predecessor.Key;
				node.Value = predecessor.Value;
				node = predecessor;
			}

			RedBlackTreeNode<TKey, TValue> child = node.Right ?? node.Left;
			if (NullCoalescedNodeColor(node) == RedBlackTreeColorEnum.Black)
			{
				node.Color = NullCoalescedNodeColor(child);
				DeleteCase1(node);
			}

			ReplaceNode(node, child);
		}

		public TValue GetMaximum()
		{
			if (_root == null)
			{
				// TODO: This is dirty.
				return default;
			}
			else
			{
				RedBlackTreeNode<TKey, TValue> maximumNode = GetMaximumNode(_root);

				return maximumNode.Value;
			}
		}

		public TValue GetMinimum()
		{
			if (_root == null)
			{
				// TODO: This is dirty.
				return default;
			}
			else
			{
				RedBlackTreeNode<TKey, TValue> minimumNode = GetMinimumNode(_root);

				return minimumNode.Value;
			}
		}

		private RedBlackTreeNode<TKey, TValue> LookupNode(TKey key)
		{
			RedBlackTreeNode<TKey, TValue> node = _root;
			while (node != null)
			{
				int compResult = key.CompareTo(node.Key);
				if (compResult == 0)
				{
					return node;
				}
				if (compResult < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}

			return null;
		}

		private void RotateLeft(RedBlackTreeNode<TKey, TValue> node)
		{
			RedBlackTreeNode<TKey, TValue> right = node.Right;
			ReplaceNode(node, right);
			node.Right = right.Left;

			if (right.Left != null)
			{
				right.Left.Parent = node;
			}

			right.Left = node;
			node.Parent = right;
		}

		private void RotateRight(RedBlackTreeNode<TKey, TValue> node)
		{
			RedBlackTreeNode<TKey, TValue> left = node.Left;
			ReplaceNode(node, left);
			node.Left = left.Right;

			if (left.Right != null)
			{
				left.Right.Parent = node;
			}

			left.Right = node;
			node.Parent = left;
		}

		private void ReplaceNode(RedBlackTreeNode<TKey, TValue> oldNode, RedBlackTreeNode<TKey, TValue> newNode)
		{
			if (oldNode.Parent == null)
			{
				_root = newNode;
			}
			else
			{
				if (oldNode == oldNode.Parent.Left)
				{
					oldNode.Parent.Left = newNode;
				}
				else
				{
					oldNode.Parent.Right = newNode;
				}
			}

			if (newNode != null)
			{
				newNode.Parent = oldNode.Parent;
			}
		}

		private void InsertCase1(RedBlackTreeNode<TKey, TValue> node)
		{
			if (node.Parent == null)
			{
				node.Color = RedBlackTreeColorEnum.Black;
			}
			else
			{
				InsertCase2(node);
			}
		}

		private void InsertCase2(RedBlackTreeNode<TKey, TValue> node)
		{
			if (NullCoalescedNodeColor(node.Parent) == RedBlackTreeColorEnum.Black)
			{
				return; // Tree is still valid
			}

			InsertCase3(node);
		}

		private void InsertCase3(RedBlackTreeNode<TKey, TValue> node)
		{
			if (NullCoalescedNodeColor(node.GetUncle()) == RedBlackTreeColorEnum.Red)
			{
				node.Parent.Color = RedBlackTreeColorEnum.Black;
				node.GetUncle().Color = RedBlackTreeColorEnum.Black;
				node.GetGrandparent().Color = RedBlackTreeColorEnum.Red;
				InsertCase1(node.GetGrandparent());
			}
			else
			{
				InsertCase4(node);
			}
		}

		private void InsertCase4(RedBlackTreeNode<TKey, TValue> node)
		{
			if (node == node.Parent.Right && node.Parent == node.GetGrandparent().Left)
			{
				RotateLeft(node.Parent);
				node = node.Left;
			}
			else if (node == node.Parent.Left && node.Parent == node.GetGrandparent().Right)
			{
				RotateRight(node.Parent);
				node = node.Right;
			}

			InsertCase5(node);
		}

		private void InsertCase5(RedBlackTreeNode<TKey, TValue> node)
		{
			node.Parent.Color = RedBlackTreeColorEnum.Black;
			node.GetGrandparent().Color = RedBlackTreeColorEnum.Red;
			if (node == node.Parent.Left && node.Parent == node.GetGrandparent().Left)
			{
				RotateRight(node.GetGrandparent());
			}
			else
			{
				RotateLeft(node.GetGrandparent());
			}
		}

		private RedBlackTreeNode<TKey, TValue> GetMinimumNode(RedBlackTreeNode<TKey, TValue> node)
		{
			while (node.Left != null)
			{
				node = node.Left;
			}
			return node;
		}

		private RedBlackTreeNode<TKey, TValue> GetMaximumNode(RedBlackTreeNode<TKey, TValue> node)
		{
			while (node.Right != null)
			{
				node = node.Right;
			}
			return node;
		}

		private void DeleteCase1(RedBlackTreeNode<TKey, TValue> node)
		{
			if (node.Parent == null)
			{
				return;
			}

			DeleteCase2(node);
		}

		private void DeleteCase2(RedBlackTreeNode<TKey, TValue> node)
		{
			if (NullCoalescedNodeColor(node.TryGetSibling()) == RedBlackTreeColorEnum.Red)
			{
				node.Parent.Color = RedBlackTreeColorEnum.Red;
				node.TryGetSibling().Color = RedBlackTreeColorEnum.Black;
				if (node == node.Parent.Left)
				{
					RotateLeft(node.Parent);
				}
				else
				{
					RotateRight(node.Parent);
				}
			}
			DeleteCase3(node);
		}

		private void DeleteCase3(RedBlackTreeNode<TKey, TValue> node)
		{
			if (NullCoalescedNodeColor(node.Parent) == RedBlackTreeColorEnum.Black &&
				NullCoalescedNodeColor(node.TryGetSibling()) == RedBlackTreeColorEnum.Black &&
				NullCoalescedNodeColor(node.TryGetSibling().Left) == RedBlackTreeColorEnum.Black &&
				NullCoalescedNodeColor(node.TryGetSibling().Right) == RedBlackTreeColorEnum.Black)
			{
				node.TryGetSibling().Color = RedBlackTreeColorEnum.Red;
				DeleteCase1(node.Parent);
			}
			else
			{
				DeleteCase4(node);
			}
		}

		private void DeleteCase4(RedBlackTreeNode<TKey, TValue> node)
		{
			if (NullCoalescedNodeColor(node.Parent) == RedBlackTreeColorEnum.Red &&
				NullCoalescedNodeColor(node.TryGetSibling()) == RedBlackTreeColorEnum.Black &&
				NullCoalescedNodeColor(node.TryGetSibling().Left) == RedBlackTreeColorEnum.Black &&
				NullCoalescedNodeColor(node.TryGetSibling().Right) == RedBlackTreeColorEnum.Black)
			{
				node.TryGetSibling().Color = RedBlackTreeColorEnum.Red;
				node.Parent.Color = RedBlackTreeColorEnum.Black;
			}
			else
			{
				DeleteCase5(node);
			}
		}

		private void DeleteCase5(RedBlackTreeNode<TKey, TValue> node)
		{
			if (node == node.Parent.Left &&
				NullCoalescedNodeColor(node.TryGetSibling()) == RedBlackTreeColorEnum.Black &&
				NullCoalescedNodeColor(node.TryGetSibling().Left) == RedBlackTreeColorEnum.Red &&
				NullCoalescedNodeColor(node.TryGetSibling().Right) == RedBlackTreeColorEnum.Black)
			{
				node.TryGetSibling().Color = RedBlackTreeColorEnum.Red;
				node.TryGetSibling().Left.Color = RedBlackTreeColorEnum.Black;
				RotateRight(node.TryGetSibling());
			}
			else if (node == node.Parent.Right &&
					 NullCoalescedNodeColor(node.TryGetSibling()) == RedBlackTreeColorEnum.Black &&
					 NullCoalescedNodeColor(node.TryGetSibling().Right) == RedBlackTreeColorEnum.Red &&
					 NullCoalescedNodeColor(node.TryGetSibling().Left) == RedBlackTreeColorEnum.Black)
			{
				node.TryGetSibling().Color = RedBlackTreeColorEnum.Red;
				node.TryGetSibling().Right.Color = RedBlackTreeColorEnum.Black;
				RotateLeft(node.TryGetSibling());
			}

			DeleteCase6(node);
		}

		private void DeleteCase6(RedBlackTreeNode<TKey, TValue> node)
		{
			node.TryGetSibling().Color = NullCoalescedNodeColor(node.Parent);
			node.Parent.Color = RedBlackTreeColorEnum.Black;
			if (node == node.Parent.Left)
			{
				//assert nodeColor(node.sibling().right) == Color.RED;

				node.TryGetSibling().Right.Color = RedBlackTreeColorEnum.Black;
				RotateLeft(node.Parent);
			}
			else
			{
				//assert nodeColor(node.sibling().left) == Color.RED;

				node.TryGetSibling().Left.Color = RedBlackTreeColorEnum.Black;
				RotateRight(node.Parent);
			}
		}
	}
}
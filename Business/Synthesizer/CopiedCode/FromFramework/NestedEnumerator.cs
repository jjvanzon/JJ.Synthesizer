using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable InvertIf

namespace JJ.Framework.Collections
{
    internal class NestedEnumerator : IEnumerable<object[]>, IEnumerator<object[]>
    {
        private readonly IEnumerable<object> _enumerable;
        private readonly NestedEnumerator _deeperEnumerator;
        private IEnumerator<object> _enumerator;
        private bool _enumeratorHasMovedFirst;

        // Construction, Destruction

        public NestedEnumerator(IEnumerable<object> enumerable, NestedEnumerator deeperEnumerator = null)
        {
            _enumerable = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
            _deeperEnumerator = deeperEnumerator;

            Current = new object[GetDepth()];
            Reset();
        }

        private int GetDepth() => this.SelfAndAncestors(x => x._deeperEnumerator).Count();

        ~NestedEnumerator() => Dispose();

        public void Dispose()
        {
            _deeperEnumerator?.Dispose();
            _enumerator?.Dispose();
        }

        // IEnumerable

        public object[] Current { get; }

        public void Reset()
        {
            _deeperEnumerator?.Reset();
            _enumerator = _enumerable.GetEnumerator(); // NOTE: _enumerator.Reset(); gave 'unsupported' exception at one point. Therefor we create a get a new enumerator here.
            _enumeratorHasMovedFirst = false;
        }

        public bool MoveNext()
        {
            // Case 1: There is no deeper enumerator.
            if (_deeperEnumerator == null)
            {
                return EnumeratorMoveNext();
            }

            // Case 2: Deeper enumerator can be advanced.
            if (DeeperEnumeratorMoveNext())
            {
                // But make sure this level's enumerator is moved to the first item too.
                if (!_enumeratorHasMovedFirst)
                {
                    if (!EnumeratorMoveNext())
                    {
                        return false;
                    }
                }

                return true;
            }

            // Case 3:
            // Deeper enumerator is done.
            // Reset deeper enumerator.
            // Move next on deeper enumerator to set it to the first item.
            // Advance this level's enumerator.
            _deeperEnumerator.Reset();

            if (DeeperEnumeratorMoveNext())
            {
                if (EnumeratorMoveNext())
                {
                    return true;
                }
            }

            // Case 4: This level's enumerator is done.
            return false;
        }

        private bool EnumeratorMoveNext()
        {
            _enumeratorHasMovedFirst = true;

            bool result = _enumerator.MoveNext();

            if (result)
            {
                Current[0] = _enumerator.Current;
            }

            return result;
        }

        private bool DeeperEnumeratorMoveNext()
        {
            bool result = _deeperEnumerator.MoveNext();

            _deeperEnumerator.Current.CopyTo(Current, 1);

            return result;
        }

        // IEnumerator

        public IEnumerator<object[]> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        object IEnumerator.Current => Current;
    }
}
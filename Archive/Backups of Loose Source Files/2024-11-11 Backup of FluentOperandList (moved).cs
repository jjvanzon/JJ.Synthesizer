using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public class FluentOperandList : IList<FluentOutlet>
    {
        private readonly FluentOutlet _parent;
        private readonly OperandList _underlyingList;

        public FluentOperandList(FluentOutlet parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _underlyingList = new OperandList(parent.WrappedOutlet);
        }

        public int Count => _underlyingList.Count;

        public FluentOutlet this[int index]
        {
            get
            {
                Outlet outlet = _underlyingList[index];
                return outlet == null ? null : _parent._[outlet];
            }
            set => _underlyingList[index] = value?.WrappedOutlet;
        }

        public int IndexOf(FluentOutlet item) => _underlyingList.IndexOf(item?.WrappedOutlet);
        public bool Contains(FluentOutlet item) => _underlyingList.Contains(item?.WrappedOutlet);
        public void Add(FluentOutlet item) => _underlyingList.Add(item?.WrappedOutlet);
        public void Insert(int index, FluentOutlet item) => _underlyingList.Insert(index, item?.WrappedOutlet);
        public bool Remove(FluentOutlet item) => _underlyingList.Remove(item?.WrappedOutlet);
        public void RemoveAt(int index) => _underlyingList.RemoveAt(index);
        public void Clear() => _underlyingList.Clear();
        public bool IsReadOnly => _underlyingList.IsReadOnly;

        public void CopyTo(FluentOutlet[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _underlyingList.Count)
                throw new ArgumentException("The number of elements in the source OperandList is greater than the available space from arrayIndex to the end of the destination array.");

            for (int i = 0; i < _underlyingList.Count; i++)
            {
                Outlet outlet = _underlyingList[i];
                FluentOutlet fluentOutlet = null;
                if (outlet != null)
                {
                    fluentOutlet = _parent._[outlet];
                }
                array[arrayIndex + i] = fluentOutlet;
            }
        }

        public IEnumerator<FluentOutlet> GetEnumerator()
        {
            return _underlyingList
                   // Convert each Outlet to FluentOutlet
                   .Select(outlet => outlet == null ? null : _parent._[outlet])
                   .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.OperandWishes
{
    public class FluentOperandList : IList<FlowNode>
    {
        private readonly FlowNode    _parent;
        private readonly OperandList _underlyingList;
        
        public FluentOperandList(FlowNode parent)
        {
            _parent         = parent ?? throw new ArgumentNullException(nameof(parent));
            _underlyingList = new OperandList(parent.UnderlyingOutlet);
        }
        
        public int Count => _underlyingList.Count;
        
        public FlowNode this[int index]
        {
            get
            {
                Outlet outlet = _underlyingList[index];
                return outlet == null ? null : _parent._[outlet];
            }
            set => _underlyingList[index] = value?.UnderlyingOutlet;
        }
        
        public int  IndexOf(FlowNode  item)                 => _underlyingList.IndexOf(item?.UnderlyingOutlet);
        public bool Contains(FlowNode item)                 => _underlyingList.Contains(item?.UnderlyingOutlet);
        public void Add(FlowNode      item)                 => _underlyingList.Add(item?.UnderlyingOutlet);
        public void Insert(int        index, FlowNode item) => _underlyingList.Insert(index, item?.UnderlyingOutlet);
        public bool Remove(FlowNode   item)  => _underlyingList.Remove(item?.UnderlyingOutlet);
        public void RemoveAt(int      index) => _underlyingList.RemoveAt(index);
        public void Clear()                  => _underlyingList.Clear();
        public bool IsReadOnly               => _underlyingList.IsReadOnly;
        
        public void CopyTo(FlowNode[] array, int arrayIndex)
        {
            if (array                     == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex                < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _underlyingList.Count) throw new ArgumentException("The number of elements in the source OperandList is greater than the available space from arrayIndex to the end of the destination array.");
            
            for (int i = 0; i < _underlyingList.Count; i++)
            {
                Outlet   outlet   = _underlyingList[i];
                FlowNode flowNode = null;
                if (outlet != null)
                {
                    flowNode = _parent[outlet];
                }
                array[arrayIndex + i] = flowNode;
            }
        }
        
        public IEnumerator<FlowNode> GetEnumerator()
        {
            return _underlyingList
                   // Convert each Outlet to FlowNode
                   .Select(outlet => outlet == null ? null : _parent[outlet])
                   .GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
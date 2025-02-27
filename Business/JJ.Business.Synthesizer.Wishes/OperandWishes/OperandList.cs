using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.OperandWishes
{
    public class OperandList : IList<Outlet>
    {
        private IList<Inlet> _inlets;
        
        internal OperandList(Outlet   outlet) => Initialize(outlet);
        internal OperandList(Operator op) => Initialize(op);
        
        public int Count => _inlets.Count;
        
        public Outlet this[int index]
        {
            get => _inlets[index].Input;
            set => _inlets[index].LinkTo(value);
        }
        
        public int IndexOf(Outlet item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i] == item)
                {
                    return i;
                }
            }
            
            return -1;
        }
        
        public bool Contains(Outlet item) => _inlets.Any(x => x.Input == item);
        
        public void Add(Outlet item) => ThrowListCannotBeExtended();
        
        public void Insert(int index, Outlet item) => ThrowListCannotBeExtended();
        
        public bool Remove(Outlet item)
        {
            var inlet = _inlets.FirstOrDefault(x => x.Input == item);
            if (inlet != null)
            {
                // TODO: Add UnlinkOutlet to Wishes project. UnlinkWishes or something.
                inlet.LinkTo((Outlet)null);
                return true;
            }
            
            return false;
        }
        
        public void RemoveAt(int index) => _inlets[index].LinkTo((Outlet)null);
        
        public void Clear() => _inlets.ForEach(x => LinkToExtensions.LinkTo((Inlet)x, (Outlet)null));
        
        public void CopyTo(Outlet[] array, int arrayIndex)
        {
            if (array                     == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex                < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _inlets.Count) throw new ArgumentException("The number of elements in the source OperandList is greater than the available space from arrayIndex to the end of the destination array.");
            
            for (var i = 0; i < _inlets.Count; i++)
            {
                array[arrayIndex + i] = _inlets[i].Input;
            }
        }
        
        public bool IsReadOnly => false;
        
        public IEnumerator<Outlet> GetEnumerator()
        {
            foreach (var inlet in _inlets)
            {
                yield return inlet.Input;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        // Helpers
        
        private void ThrowListCannotBeExtended()
        {
            throw new NotSupportedException(
                "List cannot be extended."                       +
                "For it would require internal entity creation " +
                "for which it is missing its internal services.");
        }
        
        // Field Assignment with Null Checks
        
        private void Initialize(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);
            Initialize(outlet.Operator);
        }
        
        private void Initialize(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            _inlets = op.Inlets;
        }
    }
}
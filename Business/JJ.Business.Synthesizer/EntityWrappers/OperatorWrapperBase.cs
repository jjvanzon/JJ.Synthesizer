using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase
    {
        protected Operator _operator;

        public OperatorWrapperBase(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            _operator = op;
        }

        /// <summary> Wrapped object summary>
        public Operator Operator { get { return _operator; } }

        public string Name
        {
            get { return _operator.Name; }
            set { _operator.Name = value; }
        }

        /// <summary> Gets an item out of the sorted _operator.Inlets and verifies that the index is valid in the list. </summary>
        protected Inlet GetInlet(int index)
        {
            IList<Inlet> sortedInlets = GetSortedInlets();
            if (index >= sortedInlets.Count)
            {
                throw new Exception(String.Format("Sorted inlets does not have index [{0}].", index));
            }
            return sortedInlets[index];
        }

        /// <summary> Gets an item out of the sorted _operator.Outlets and verifies that the index is valid in the list. </summary>
        protected Outlet GetOutlet(int index)
        {
            IList<Outlet> sortedOutlets = GetSortedOutlets();
            if (index >= sortedOutlets.Count)
            {
                throw new Exception(String.Format("Sorted outlets does not have index [{0}].", index));
            }
            return sortedOutlets[index];
        }

        public static implicit operator Operator(OperatorWrapperBase wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Operator;
        }

        // Helpers

        private IList<Inlet> GetSortedInlets()
        {
            IList<Inlet> sortedInlets = _operator.Inlets.OrderBy(x => x.ListIndex).ToArray();
            return sortedInlets;
        }

        private IList<Outlet> GetSortedOutlets()
        {
            IList<Outlet> sortedOutlets = _operator.Outlets.OrderBy(x => x.ListIndex).ToArray();
            return sortedOutlets;
        }
    }
}

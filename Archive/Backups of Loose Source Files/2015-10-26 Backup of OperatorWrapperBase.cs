//using JJ.Framework.Reflection.Exceptions;
//using JJ.Data.Synthesizer;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public abstract class OperatorWrapperBase
//    {
//        protected Operator _operator;
//        private IList<Inlet> _sortedInlets;
//        private IList<Outlet> _sortedOutlets;

//        public OperatorWrapperBase(Operator op)
//        {
//            if (op == null) throw new NullException(() => op);

//            _operator = op;
//            _sortedInlets = op.Inlets.OrderBy(x => x.SortOrder).ToArray();
//            _sortedOutlets = op.Outlets.OrderBy(x => x.SortOrder).ToArray();
//        }

//        /// <summary> Wrapped object summary>
//        public Operator Operator { get { return _operator; } }

//        public string Name
//        {
//            get { return _operator.Name; }
//            set { _operator.Name = value; }
//        }

//        /// <summary> Gets an item out of the sorted _operator.Inlets and verifies that the index is valid in the list. </summary>
//        protected Inlet GetInlet(int index)
//        {
//            if (index >= _sortedInlets.Count)
//            {
//                throw new Exception(String.Format("Sorted _operator.Inlets does not have index [{0}].", index));
//            }
//            return _sortedInlets[index];
//        }

//        /// <summary> Gets an item out of the sorted _operator.Outlets and verifies that the index is valid in the list. </summary>
//        protected Outlet GetOutlet(int index)
//        {
//            if (index >= _sortedOutlets.Count)
//            {
//                throw new Exception(String.Format("Sorted _operator.Outlets does not have index [{0}].", index));
//            }
//            return _sortedOutlets[index];
//        }

//        public static implicit operator Operator(OperatorWrapperBase wrapper)
//        {
//            if (wrapper == null) return null;

//            return wrapper.Operator;
//        }
//    }
//}

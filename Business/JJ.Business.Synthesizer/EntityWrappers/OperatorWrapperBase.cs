using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Wrapped object
        /// </summary>
        public Operator Operator { get { return _operator; } }

        public string Name
        {
            get { return _operator.Name; }
            set { _operator.Name = value; }
        }

        /// <summary>
        /// Gets an item out of _operator.Inlets and verifies that the index is valid in the list.
        /// </summary>
        protected Inlet GetInlet(int index)
        {
            if (index >= _operator.Inlets.Count)
            {
                throw new Exception(String.Format("_operator.Inlets does not have index [{0}].", index));
            }
            return _operator.Inlets[index];
        }

        /// <summary>
        /// Gets an item out of _operator.Outlets and verifies that the index is valid in the list.
        /// </summary>
        protected Outlet GetOutlet(int index)
        {
            if (index >= _operator.Outlets.Count)
            {
                throw new Exception(String.Format("_operator.Outlets does not have index [{0}].", index));
            }
            return _operator.Outlets[index];
        }

        public static implicit operator Operator(OperatorWrapperBase wrapper)
        {
            return wrapper.Operator;
        }
    }
}

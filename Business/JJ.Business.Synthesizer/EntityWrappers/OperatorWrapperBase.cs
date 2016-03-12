using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase
    {
        protected Operator _wrappedOperator;

        public OperatorWrapperBase(Operator wrappedOperator)
        {
            if (wrappedOperator == null) throw new NullException(() => wrappedOperator);

            _wrappedOperator = wrappedOperator;
        }

        public Operator WrappedOperator { get { return _wrappedOperator; } }

        public string Name
        {
            get { return _wrappedOperator.Name; }
            set { _wrappedOperator.Name = value; }
        }

        // TODO: Make abstract after you implemented in all OperatorWrappers.
        public virtual string GetInletDisplayName(int listIndex)
        {
            // HACK
            return null;
        }

        // TODO: Make abstract after you implemented in all OperatorWrappers.
        public virtual string GetOutletDisplayName(int listIndex)
        {
            // HACK
            return null;
        }

        public static implicit operator Operator(OperatorWrapperBase wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.WrappedOperator;
        }
    }
}

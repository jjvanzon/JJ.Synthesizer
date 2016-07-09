using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    [DebuggerDisplay("{DebuggerDisplay}")]
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

        public abstract string GetInletDisplayName(int listIndex);

        public abstract string GetOutletDisplayName(int listIndex);

        public static implicit operator Operator(OperatorWrapperBase wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.WrappedOperator;
        }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}

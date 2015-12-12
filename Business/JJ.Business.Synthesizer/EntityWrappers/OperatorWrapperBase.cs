using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;

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

        public static implicit operator Operator(OperatorWrapperBase wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Operator;
        }
    }
}

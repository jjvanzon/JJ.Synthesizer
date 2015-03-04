using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.OperatorWrappers
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
    }
}

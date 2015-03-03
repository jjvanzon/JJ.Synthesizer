using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.OperatorWrappers
{
    public class Add
    {
        private Operator _operator;
        public Operator Operator { get { return _operator; } }

        public Add(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            _operator = op;
        }

        public string Name
        {
            get { return _operator.Name; }
            set { _operator.Name = value; }
        }

        public Outlet OperandA
        {
            get { return _operator.Inlets[0].Input; }
            set { _operator.Inlets[0].LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return _operator.Inlets[1].Input; }
            set { _operator.Inlets[1].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[0]; }
        }
    }
}

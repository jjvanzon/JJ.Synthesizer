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
    public class Substract : OperatorWrapperBase
    {
        public Substract(Operator op)
            : base(op)
        { }

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

        public static implicit operator Outlet(Substract wrapper)
        {
            return wrapper.Result;
        }
    }
}

using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Adder : OperatorWrapperBase
    {
        public Adder(Operator op)
            :base(op)
        { }

        public IList<Inlet> Inlets
        {
            get { return _operator.Inlets; }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[0]; }
        }

        public static implicit operator Outlet(Adder wrapper)
        {
            return wrapper.Result;
        }
    }
}

using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeSubstract : OperatorWrapperBase
    {
        public TimeSubstract(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return _operator.Inlets[0].Input; }
            set { _operator.Inlets[0].LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return _operator.Inlets[1].Input; }
            set { _operator.Inlets[1].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[0]; }
        }

        public static implicit operator Outlet(TimeSubstract wrapper)
        {
            return wrapper.Result;
        }
    }
}

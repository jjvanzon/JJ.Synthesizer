using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimePower : OperatorWrapperBase
    {
        public const int SIGNAL_INDEX = 0;
        public const int EXPONENT_INDEX = 1;
        public const int ORIGIN_INDEX = 2;
        public const int RESULT_INDEX = 0;

        public TimePower(Operator op)
            :base(op)
        { }

        public Outlet Signal
        {
            get { return _operator.Inlets[SIGNAL_INDEX].Input; }
            set { _operator.Inlets[SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return _operator.Inlets[EXPONENT_INDEX].Input; }
            set { _operator.Inlets[EXPONENT_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return _operator.Inlets[ORIGIN_INDEX].Input; }
            set { _operator.Inlets[ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimePower wrapper)
        {
            return wrapper.Result;
        }
    }
}

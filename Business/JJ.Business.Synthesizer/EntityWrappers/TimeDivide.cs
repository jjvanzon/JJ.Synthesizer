using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeDivide : OperatorWrapperBase
    {
        public const int SIGNAL_INDEX = 0;
        public const int TIME_DIVIDER_INDEX = 1;
        public const int ORIGIN_INDEX = 2;
        public const int RESULT_INDEX = 0;

        public TimeDivide(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return _operator.Inlets[SIGNAL_INDEX].InputOutlet; }
            set { _operator.Inlets[SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet TimeDivider
        {
            get { return _operator.Inlets[TIME_DIVIDER_INDEX].InputOutlet; }
            set { _operator.Inlets[TIME_DIVIDER_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return _operator.Inlets[ORIGIN_INDEX].InputOutlet; }
            set { _operator.Inlets[ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimeDivide wrapper)
        {
            return wrapper.Result;
        }
    }
}

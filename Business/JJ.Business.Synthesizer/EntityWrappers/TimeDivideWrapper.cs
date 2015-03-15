using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeDivideWrapper : OperatorWrapperBase
    {
        public TimeDivideWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return _operator.Inlets[OperatorConstants.TIME_DIVIDE_SIGNAL_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_DIVIDE_SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet TimeDivider
        {
            get { return _operator.Inlets[OperatorConstants.TIME_DIVIDE_TIME_DIVIDER_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_DIVIDE_TIME_DIVIDER_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return _operator.Inlets[OperatorConstants.TIME_DIVIDE_ORIGIN_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_DIVIDE_ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[OperatorConstants.TIME_DIVIDE_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimeDivideWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}

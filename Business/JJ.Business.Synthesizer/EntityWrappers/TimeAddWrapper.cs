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
    public class TimeAddWrapper : OperatorWrapperBase
    {
        public TimeAddWrapper(Operator op)
            :base(op)
        { }

        public Outlet Signal
        {
            get { return _operator.Inlets[OperatorConstants.TIME_ADD_SIGNAL_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_ADD_SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return _operator.Inlets[OperatorConstants.TIME_ADD_TIME_DIFFERENCE_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_ADD_TIME_DIFFERENCE_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[OperatorConstants.TIME_ADD_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimeAddWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}

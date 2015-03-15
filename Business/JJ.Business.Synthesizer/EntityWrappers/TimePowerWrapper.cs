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
    public class TimePowerWrapper : OperatorWrapperBase
    {
        public TimePowerWrapper(Operator op)
            :base(op)
        { }

        public Outlet Signal
        {
            get { return _operator.Inlets[OperatorConstants.TIME_POWER_SIGNAL_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_POWER_SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return _operator.Inlets[OperatorConstants.TIME_POWER_EXPONENT_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_POWER_EXPONENT_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return _operator.Inlets[OperatorConstants.TIME_POWER_ORIGIN_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.TIME_POWER_ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[OperatorConstants.TIME_POWER_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimePowerWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}

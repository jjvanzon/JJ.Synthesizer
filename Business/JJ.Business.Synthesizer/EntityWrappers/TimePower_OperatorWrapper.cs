using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimePower_OperatorWrapper : OperatorWrapperBase
    {
        public TimePower_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.TIME_POWER_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_POWER_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return GetInlet(OperatorConstants.TIME_POWER_EXPONENT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_POWER_EXPONENT_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return GetInlet(OperatorConstants.TIME_POWER_ORIGIN_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_POWER_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TIME_POWER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(TimePower_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Result;
        }
    }
}
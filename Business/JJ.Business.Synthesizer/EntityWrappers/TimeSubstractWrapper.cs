using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeSubstractWrapper : OperatorWrapperBase
    {
        public TimeSubstractWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.TIME_SUBSTRACT_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_SUBSTRACT_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return GetInlet(OperatorConstants.TIME_SUBSTRACT_TIME_DIFFERENCE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_SUBSTRACT_TIME_DIFFERENCE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TIME_SUBSTRACT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(TimeSubstractWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}

using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeMultiplyWrapper : OperatorWrapperBase
    {
        public TimeMultiplyWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.TIME_MULTIPLY_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_MULTIPLY_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeMultiplier
        {
            get { return GetInlet(OperatorConstants.TIME_MULTIPLY_TIME_MULTIPLIER_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_MULTIPLY_TIME_MULTIPLIER_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return GetInlet(OperatorConstants.TIME_MULTIPLY_ORIGIN_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_MULTIPLY_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TIME_MULTIPLY_RESULT_INDEX); }
        }

        public static implicit operator Outlet(TimeMultiplyWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}

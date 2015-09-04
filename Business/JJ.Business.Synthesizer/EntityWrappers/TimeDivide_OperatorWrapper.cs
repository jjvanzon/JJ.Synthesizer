using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeDivide_OperatorWrapper : OperatorWrapperBase
    {
        public TimeDivide_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.TIME_DIVIDE_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_DIVIDE_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeDivider
        {
            get { return GetInlet(OperatorConstants.TIME_DIVIDE_TIME_DIVIDER_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_DIVIDE_TIME_DIVIDER_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return GetInlet(OperatorConstants.TIME_DIVIDE_ORIGIN_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_DIVIDE_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TIME_DIVIDE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(TimeDivide_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

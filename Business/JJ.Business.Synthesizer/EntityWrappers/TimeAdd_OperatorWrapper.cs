using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeAdd_OperatorWrapper : OperatorWrapperBase
    {
        public TimeAdd_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.TIME_ADD_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_ADD_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return GetInlet(OperatorConstants.TIME_ADD_TIME_DIFFERENCE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_ADD_TIME_DIFFERENCE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TIME_ADD_RESULT_INDEX); }
        }

        public static implicit operator Outlet(TimeAdd_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
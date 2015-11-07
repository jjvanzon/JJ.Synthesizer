using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Earlier : OperatorWrapperBase
    {
        public OperatorWrapper_Earlier(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.EARLIER_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.EARLIER_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return GetInlet(OperatorConstants.EARLIER_TIME_DIFFERENCE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.EARLIER_TIME_DIFFERENCE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.EARLIER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Earlier wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

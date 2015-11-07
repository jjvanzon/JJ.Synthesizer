using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_TimeSubtract : OperatorWrapperBase
    {
        public OperatorWrapper_TimeSubtract(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.TIME_SUBTRACT_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_SUBTRACT_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return GetInlet(OperatorConstants.TIME_SUBTRACT_TIME_DIFFERENCE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TIME_SUBTRACT_TIME_DIFFERENCE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TIME_SUBTRACT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_TimeSubtract wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

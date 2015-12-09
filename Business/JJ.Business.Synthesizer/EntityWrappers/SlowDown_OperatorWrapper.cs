using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SlowDown_OperatorWrapper : OperatorWrapperBase
    {
        public SlowDown_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.SLOW_DOWN_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SLOW_DOWN_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeMultiplier
        {
            get { return GetInlet(OperatorConstants.SLOW_DOWN_TIME_MULTIPLIER_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SLOW_DOWN_TIME_MULTIPLIER_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return GetInlet(OperatorConstants.SLOW_DOWN_ORIGIN_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SLOW_DOWN_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SLOW_DOWN_RESULT_INDEX); }
        }

        public static implicit operator Outlet(SlowDown_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

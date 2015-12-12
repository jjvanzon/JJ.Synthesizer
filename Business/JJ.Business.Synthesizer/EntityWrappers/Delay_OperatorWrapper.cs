using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Delay_OperatorWrapper : OperatorWrapperBase
    {
        public Delay_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.DELAY_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.DELAY_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.DELAY_TIME_DIFFERENCE_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.DELAY_TIME_DIFFERENCE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_operator, OperatorConstants.DELAY_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Delay_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
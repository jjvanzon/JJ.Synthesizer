using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Earlier_OperatorWrapper : OperatorWrapperBase
    {
        public Earlier_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.EARLIER_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.EARLIER_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.EARLIER_TIME_DIFFERENCE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.EARLIER_TIME_DIFFERENCE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.EARLIER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Earlier_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

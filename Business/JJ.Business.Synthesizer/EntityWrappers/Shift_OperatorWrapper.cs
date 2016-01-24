using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Shift_OperatorWrapper : OperatorWrapperBase
    {
        public Shift_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SHIFT_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SHIFT_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Difference
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SHIFT_DIFFERENCE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SHIFT_DIFFERENCE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.SHIFT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Shift_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
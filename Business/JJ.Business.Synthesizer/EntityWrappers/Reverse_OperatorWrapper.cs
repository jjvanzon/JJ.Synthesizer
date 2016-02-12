using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Reverse_OperatorWrapper : OperatorWrapperBase
    {
        public Reverse_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.REVERSE_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.REVERSE_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Speed
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.REVERSE_SPEED_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.REVERSE_SPEED_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.REVERSE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Reverse_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

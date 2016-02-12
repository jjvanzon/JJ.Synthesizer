using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Round_OperatorWrapper : OperatorWrapperBase
    {
        public Round_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.ROUND_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.ROUND_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Step
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.ROUND_STEP_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.ROUND_STEP_INDEX).LinkTo(value); }
        }

        public Outlet Offset
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.ROUND_OFFSET_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.ROUND_OFFSET_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.ROUND_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Round_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
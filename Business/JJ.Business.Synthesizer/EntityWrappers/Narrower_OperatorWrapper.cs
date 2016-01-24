using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Narrower_OperatorWrapper : OperatorWrapperBase
    {
        public Narrower_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.NARROWER_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.NARROWER_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Factor
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.NARROWER_FACTOR_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.NARROWER_FACTOR_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.NARROWER_ORIGIN_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.NARROWER_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.NARROWER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Narrower_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

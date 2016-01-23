using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Stretch_OperatorWrapper : OperatorWrapperBase
    {
        public Stretch_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.STRETCH_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.STRETCH_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Factor
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.STRETCH_FACTOR_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.STRETCH_FACTOR_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.STRETCH_ORIGIN_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.STRETCH_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.STRETCH_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Stretch_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

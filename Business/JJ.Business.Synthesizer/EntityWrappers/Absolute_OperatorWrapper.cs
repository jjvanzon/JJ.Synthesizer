using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Absolute_OperatorWrapper : OperatorWrapperBase
    {
        public Absolute_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet X
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.ABSOLUTE_X_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.ABSOLUTE_X_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.ABSOLUTE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Absolute_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

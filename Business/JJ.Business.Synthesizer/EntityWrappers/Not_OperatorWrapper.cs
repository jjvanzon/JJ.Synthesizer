using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Not_OperatorWrapper : OperatorWrapperBase
    {
        public Not_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet X
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.NOT_X_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.NOT_X_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.NOT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Not_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

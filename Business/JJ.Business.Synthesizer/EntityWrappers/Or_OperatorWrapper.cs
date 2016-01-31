using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Or_OperatorWrapper : OperatorWrapperBase
    {
        public Or_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet A
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.OR_A_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.OR_A_INDEX).LinkTo(value); }
        }

        public Outlet B
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.OR_B_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.OR_B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.OR_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Or_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

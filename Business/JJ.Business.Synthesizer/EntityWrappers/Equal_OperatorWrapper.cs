using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Equal_OperatorWrapper : OperatorWrapperBase
    {
        public Equal_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet A
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.EQUAL_A_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.EQUAL_A_INDEX).LinkTo(value); }
        }

        public Outlet B
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.EQUAL_B_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.EQUAL_B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.EQUAL_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Equal_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

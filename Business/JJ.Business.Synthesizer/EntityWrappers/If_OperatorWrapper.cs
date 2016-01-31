using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class If_OperatorWrapper : OperatorWrapperBase
    {
        public If_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Condition
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.IF_CONDITION_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.IF_CONDITION_INDEX).LinkTo(value); }
        }

        public Outlet Then
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.IF_THEN_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.IF_THEN_INDEX).LinkTo(value); }
        }

        public Outlet Else
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.IF_ELSE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.IF_ELSE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.IF_RESULT_INDEX); }
        }

        public static implicit operator Outlet(If_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
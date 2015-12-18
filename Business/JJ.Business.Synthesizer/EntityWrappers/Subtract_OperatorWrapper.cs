using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Subtract_OperatorWrapper : OperatorWrapperBase
    {
        public Subtract_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet OperandA
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SUBTRACT_OPERAND_A_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SUBTRACT_OPERAND_A_INDEX).LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SUBTRACT_OPERAND_B_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SUBTRACT_OPERAND_B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.SUBTRACT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Subtract_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Multiply_OperatorWrapper : OperatorWrapperBase
    {
        public Multiply_OperatorWrapper(Operator op)
            :base(op)
        { }

        public Outlet OperandA
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.MULTIPLY_OPERAND_A_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.MULTIPLY_OPERAND_A_INDEX).LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.MULTIPLY_OPERAND_B_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.MULTIPLY_OPERAND_B_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.MULTIPLY_ORIGIN_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.MULTIPLY_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.MULTIPLY_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Multiply_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

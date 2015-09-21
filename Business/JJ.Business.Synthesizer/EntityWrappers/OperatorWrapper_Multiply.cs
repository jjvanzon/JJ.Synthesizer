using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Multiply : OperatorWrapperBase
    {
        public OperatorWrapper_Multiply(Operator op)
            :base(op)
        { }

        public Outlet OperandA
        {
            get { return GetInlet(OperatorConstants.MULTIPLY_OPERAND_A_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.MULTIPLY_OPERAND_A_INDEX).LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return GetInlet(OperatorConstants.MULTIPLY_OPERAND_B_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.MULTIPLY_OPERAND_B_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return GetInlet(OperatorConstants.MULTIPLY_ORIGIN_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.MULTIPLY_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.MULTIPLY_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Multiply wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

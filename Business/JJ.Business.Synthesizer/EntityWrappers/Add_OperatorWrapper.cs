using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Add_OperatorWrapper : OperatorWrapperBase
    {
        public Add_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet OperandA
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.ADD_OPERAND_A_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.ADD_OPERAND_A_INDEX).LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.ADD_OPERAND_B_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.ADD_OPERAND_B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_operator, OperatorConstants.ADD_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Add_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

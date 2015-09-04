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
            get { return GetInlet(OperatorConstants.ADD_OPERAND_A_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.ADD_OPERAND_A_INDEX).LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return GetInlet(OperatorConstants.ADD_OPERAND_B_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.ADD_OPERAND_B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.ADD_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Add_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

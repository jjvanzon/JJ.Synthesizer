using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Substract_OperatorWrapper : OperatorWrapperBase
    {
        public Substract_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet OperandA
        {
            get { return GetInlet(OperatorConstants.SUBSTRACT_OPERAND_A_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SUBSTRACT_OPERAND_A_INDEX).LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return GetInlet(OperatorConstants.SUBSTRACT_OPERAND_B_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SUBSTRACT_OPERAND_B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SUBSTRACT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Substract_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

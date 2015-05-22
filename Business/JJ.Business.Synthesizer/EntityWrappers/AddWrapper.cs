using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AddWrapper : OperatorWrapperBase
    {
        public AddWrapper(Operator op)
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

        public static implicit operator Outlet(AddWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}

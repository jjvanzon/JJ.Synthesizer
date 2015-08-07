using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Multiply_OperatorWrapper : OperatorWrapperBase
    {
        public Multiply_OperatorWrapper(Operator op)
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

        public static implicit operator Outlet(Multiply_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Result;
        }
    }
}

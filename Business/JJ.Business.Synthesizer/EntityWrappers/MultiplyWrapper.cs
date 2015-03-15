using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MultiplyWrapper : OperatorWrapperBase
    {
        public MultiplyWrapper(Operator op)
            :base(op)
        {
            Verify();
        }

        public Outlet OperandA
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.MULTIPLY_OPERAND_A_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.MULTIPLY_OPERAND_A_INDEX].LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.MULTIPLY_OPERAND_B_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.MULTIPLY_OPERAND_B_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.MULTIPLY_ORIGIN_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.MULTIPLY_ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { Verify(); return _operator.Outlets[OperatorConstants.MULTIPLY_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(MultiplyWrapper wrapper)
        {
            return wrapper.Result;
        }

        private void Verify()
        {
            IValidator validator = new MultiplyValidator(Operator);
            validator.Verify();
        }
    }
}

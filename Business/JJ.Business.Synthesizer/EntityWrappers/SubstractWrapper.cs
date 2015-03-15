using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SubstractWrapper : OperatorWrapperBase
    {
        public SubstractWrapper(Operator op)
            : base(op)
        { }

        public Outlet OperandA
        {
            get { return _operator.Inlets[OperatorConstants.SUBSTRACT_OPERAND_A_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.SUBSTRACT_OPERAND_A_INDEX].LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return _operator.Inlets[OperatorConstants.SUBSTRACT_OPERAND_B_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.SUBSTRACT_OPERAND_B_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[OperatorConstants.SUBSTRACT_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(SubstractWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}

using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Substract : OperatorWrapperBase
    {
        public const int OPERAND_A_INDEX = 0;
        public const int OPERAND_B_INDEX = 1;
        public const int RESULT_INDEX = 0;

        public Substract(Operator op)
            : base(op)
        { }

        public Outlet OperandA
        {
            get { return _operator.Inlets[OPERAND_A_INDEX].Input; }
            set { _operator.Inlets[OPERAND_A_INDEX].LinkTo(value); }
        }

        public Outlet OperandB
        {
            get { return _operator.Inlets[OPERAND_B_INDEX].Input; }
            set { _operator.Inlets[OPERAND_B_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public static implicit operator Outlet(Substract wrapper)
        {
            return wrapper.Result;
        }
    }
}

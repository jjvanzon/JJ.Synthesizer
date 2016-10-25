using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Add_OperatorDto_Vars_1Const : OperatorDto_Vars_1Const
    {
        public Add_OperatorDto_Vars_1Const(IList<InletDto> vars, double constValue)
            : base(vars, constValue)
        { }
    }
}

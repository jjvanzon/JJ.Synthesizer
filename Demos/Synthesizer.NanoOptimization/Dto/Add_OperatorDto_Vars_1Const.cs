using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Add_OperatorDto_Vars_1Const : OperatorDto_Vars_1Const
    {
        public override string OperatorName => OperatorNames.Add;

        public Add_OperatorDto_Vars_1Const(IList<OperatorDto> vars, double constValue)
            : base(vars, constValue)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Add_OperatorDto_Vars : OperatorDto_Vars
    {
        public override string OperatorTypeName => OperatorNames.Add;

        public Add_OperatorDto_Vars(IList<OperatorDto> vars)
            : base(vars)
        { }
    }
}
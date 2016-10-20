using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Add_OperatorDto_VarA_ConstB : OperatorDto_VarA_ConstB
    {
        public Add_OperatorDto_VarA_ConstB(InletDto aInletDto, double b)
            : base(aInletDto, b)
        { }
    }
}

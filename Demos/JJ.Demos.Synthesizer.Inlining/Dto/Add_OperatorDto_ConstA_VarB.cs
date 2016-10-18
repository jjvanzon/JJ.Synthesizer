using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Add_OperatorDto_ConstA_VarB : OperatorDto_ConstA_VarB
    {
        public Add_OperatorDto_ConstA_VarB(double a, InletDto bInletDto)
            : base(a, bInletDto)
        { }
    }
}

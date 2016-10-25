using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto_ConstA_VarB : OperatorDto_ConstA_VarB
    {
        public Multiply_OperatorDto_ConstA_VarB(double a, OperatorDto bOperatorDto)
            : base(a, bOperatorDto)
        { }
    }
}

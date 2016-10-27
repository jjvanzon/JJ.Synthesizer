using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto_ConstA_VarB : OperatorDto_ConstA_VarB
    {
        public override string OperatorName => OperatorNames.Multiply;

        public Multiply_OperatorDto_ConstA_VarB(double a, OperatorDto bOperatorDto)
            : base(a, bOperatorDto)
        { }
    }
}

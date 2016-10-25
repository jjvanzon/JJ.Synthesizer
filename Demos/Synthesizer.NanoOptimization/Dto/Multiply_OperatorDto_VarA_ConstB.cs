using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto_VarA_ConstB : OperatorDto_VarA_ConstB
    {
        public Multiply_OperatorDto_VarA_ConstB(OperatorDto aOperatorDto, double b)
            : base(aOperatorDto, b)
        { }
    }
}

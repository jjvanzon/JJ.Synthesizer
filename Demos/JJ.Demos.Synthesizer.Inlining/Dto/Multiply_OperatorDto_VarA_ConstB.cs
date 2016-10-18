using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Multiply_OperatorDto_VarA_ConstB : OperatorDto_VarA_ConstB
    {
        public Multiply_OperatorDto_VarA_ConstB(InletDto aInletDto, double b)
            : base(aInletDto, b)
        { }
    }
}

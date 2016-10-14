using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Add_OperatorDto_VarA_ConstB : Add_OperatorDto
    {
        public double B { get; set; }

        public Add_OperatorDto_VarA_ConstB(InletDto aInletDto, InletDto bInletDto, double b)
            : base(aInletDto, bInletDto)
        {
            B = b;
        }
    }
}

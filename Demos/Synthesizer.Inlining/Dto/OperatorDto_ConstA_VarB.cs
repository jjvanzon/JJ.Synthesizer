using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal abstract class OperatorDto_ConstA_VarB : OperatorDto
    {
        public double A { get; set; }
        public InletDto BInletDto { get; set; }

        public OperatorDto_ConstA_VarB(double a, InletDto bInletDto)
            : base(new InletDto[] { bInletDto })
        {
            A = a;
            BInletDto = bInletDto;
        }
    }
}

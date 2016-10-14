using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Multiply_OperatorDto_ConstA_VarB : Multiply_OperatorDto
    {
        public double A { get; set; }

        public Multiply_OperatorDto_ConstA_VarB(InletDto aInletDto, InletDto bInletDto, double a)
            : base(aInletDto, bInletDto)
        {
            A = a;
        }
    }
}

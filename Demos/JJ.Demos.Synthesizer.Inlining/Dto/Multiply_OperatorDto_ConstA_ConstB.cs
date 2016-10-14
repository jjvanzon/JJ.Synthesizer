using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Multiply_OperatorDto_ConstA_ConstB : Multiply_OperatorDto
    {
        public double A { get; set; }
        public double B { get; set; }

        public Multiply_OperatorDto_ConstA_ConstB(InletDto aInletDto, InletDto bInletDto, double a, double b)
            : base(aInletDto, bInletDto)
        {
            A = a;
            B = b;
        }
    }
}

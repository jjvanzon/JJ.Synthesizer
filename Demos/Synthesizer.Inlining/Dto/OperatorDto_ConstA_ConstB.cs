using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_ConstA_ConstB : OperatorDto
    {
        public double A { get; set; }
        public double B { get; set; }

        public OperatorDto_ConstA_ConstB(double a, double b)
            : base(new InletDto[0])
        {
            A = a;
            B = b;
        }
    }
}

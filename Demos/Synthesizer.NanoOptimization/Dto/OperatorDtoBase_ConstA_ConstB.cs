using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_ConstA_ConstB : OperatorDtoBase
    {
        public double A { get; set; }
        public double B { get; set; }

        public OperatorDtoBase_ConstA_ConstB(double a, double b)
            : base(new OperatorDtoBase[0])
        {
            A = a;
            B = b;
        }
    }
}

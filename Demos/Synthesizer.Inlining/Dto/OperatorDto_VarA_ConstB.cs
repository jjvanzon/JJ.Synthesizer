using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_VarA_ConstB : OperatorDto
    {
        public InletDto AInletDto { get; set; }
        public double B { get; set; }

        public OperatorDto_VarA_ConstB(InletDto aInletDto, double b)
            : base(new InletDto[] { aInletDto })
        {
            AInletDto = aInletDto;
            B = b;
        }
    }
}

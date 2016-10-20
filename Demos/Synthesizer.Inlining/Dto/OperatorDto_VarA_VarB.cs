using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_VarA_VarB : OperatorDto
    {
        public InletDto AInletDto { get; set; }
        public InletDto BInletDto { get; set; }

        public OperatorDto_VarA_VarB(InletDto aInletDto, InletDto bInletDto)
            : base(new InletDto[] { aInletDto, bInletDto })
        {
            AInletDto = aInletDto;
            BInletDto = bInletDto;
        }
    }
}

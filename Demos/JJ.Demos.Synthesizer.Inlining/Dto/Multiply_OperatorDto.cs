using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Multiply_OperatorDto : OperatorDto
    {
        public Multiply_OperatorDto(InletDto aInletDto, InletDto bInletDto)
        {
            AInletDto = aInletDto;
            BInletDto = bInletDto;
        }

        public InletDto AInletDto { get; set; }
        public InletDto BInletDto { get; set; }
    }
}

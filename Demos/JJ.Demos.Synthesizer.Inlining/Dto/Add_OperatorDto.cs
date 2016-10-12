using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Add_OperatorDto : OperatorDto
    {
        public Add_OperatorDto(InletDto aInletDto, InletDto bInletDto)
            : base(new InletDto[] { aInletDto, bInletDto })
        {
            AInletDto = aInletDto;
            BInletDto = bInletDto;
        }

        public InletDto AInletDto { get; set; }
        public InletDto BInletDto { get; set; }
    }
}

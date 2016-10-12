using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Sine_OperatorDto : OperatorDto
    {
        public Sine_OperatorDto(InletDto frequencyInletDto)
            : base(new InletDto[] { frequencyInletDto })
        {
            FrequencyInletDto = frequencyInletDto;
        }

        public InletDto FrequencyInletDto { get; set; }
    }
}

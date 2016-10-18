using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal abstract class OperatorDto_VarFrequency : OperatorDto
    {
        public InletDto FrequencyInletDto { get; set; }

        public OperatorDto_VarFrequency(InletDto frequencyInletDto)
            : base(new InletDto[] { frequencyInletDto })
        {
            FrequencyInletDto = frequencyInletDto;
        }
    }
}

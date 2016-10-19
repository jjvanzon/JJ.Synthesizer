using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Sine_OperatorDto : OperatorDto_VarFrequency
    {
        public Sine_OperatorDto(InletDto frequencyInletDto)
            : base(frequencyInletDto)
        { }
    }
}
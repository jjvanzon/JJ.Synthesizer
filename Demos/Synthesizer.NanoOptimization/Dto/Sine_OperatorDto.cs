using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Sine_OperatorDto : OperatorDto_VarFrequency
    {
        public Sine_OperatorDto(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}
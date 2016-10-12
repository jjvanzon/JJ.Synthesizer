using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Sine_OperatorDto_VarFrequency_NoPhaseTracking : Sine_OperatorDto
    {
        public Sine_OperatorDto_VarFrequency_NoPhaseTracking(InletDto frequencyInletDto)
            : base (frequencyInletDto)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Sine_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDto_VarFrequency
    {
        public Sine_OperatorDto_VarFrequency_NoPhaseTracking(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}

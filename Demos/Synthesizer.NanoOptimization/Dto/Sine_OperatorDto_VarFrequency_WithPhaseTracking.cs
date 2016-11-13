using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Sine_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => OperatorNames.Sine;

        public Sine_OperatorDto_VarFrequency_WithPhaseTracking(OperatorDtoBase frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}

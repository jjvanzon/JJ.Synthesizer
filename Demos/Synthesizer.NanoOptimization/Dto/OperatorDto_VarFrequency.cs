using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_VarFrequency : OperatorDto
    {
        public OperatorDto FrequencyOperatorDto { get { return ChildOperatorDtos[0]; } }

        public OperatorDto_VarFrequency(OperatorDto frequencyOperatorDto)
            : base(new OperatorDto[] { frequencyOperatorDto })
        { }
    }
}

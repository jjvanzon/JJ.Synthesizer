using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class VariableInput_OperatorDto : OperatorDto
    {
        public double DefaultValue { get; set; }

        public VariableInput_OperatorDto(double defaultValue)
            : base(new OperatorDto[0])
        {
            DefaultValue = defaultValue;
        }
    }
}

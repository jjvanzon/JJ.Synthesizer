using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class VariableInput_OperatorDto : OperatorDto
    {
        public VariableInput_OperatorDto(double defaultValue)
            : base(new InletDto[0])
        {
            DefaultValue = defaultValue;
        }

        public double DefaultValue { get; set; }
    }
}

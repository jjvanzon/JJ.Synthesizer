using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class VariableInput_OperatorDto : OperatorDto
    {
        public double DefaultValue { get; set; }
        public override string OperatorTypeName => OperatorNames.VariableInput;

        public VariableInput_OperatorDto(double defaultValue)
            : base(new OperatorDto[0])
        {
            DefaultValue = defaultValue;
        }
    }
}

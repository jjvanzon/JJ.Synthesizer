using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Number_OperatorDto : OperatorDto
    {
        public Number_OperatorDto(double value)
        {
            Value = value;
        }

        public double Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Number_OperatorDto : OperatorDto
    {
        public double Number { get; set; }

        public Number_OperatorDto(double number)
            : base(new InletDto[0])
        {
            Number = number;
        }
    }
}

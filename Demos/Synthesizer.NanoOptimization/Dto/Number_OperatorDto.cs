using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Number_OperatorDto : OperatorDto
    {
        public double Number { get; set; }
        public override string OperatorName => OperatorNames.Number;

        public Number_OperatorDto(double number)
            : base(new OperatorDto[0])
        {
            Number = number;
        }
    }
}

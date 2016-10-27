using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Number_OperatorDto_Zero : Number_OperatorDto
    {
        public override string OperatorName => OperatorNames.Number;

        public Number_OperatorDto_Zero()
            : base(0.0)
        { }
    }
}

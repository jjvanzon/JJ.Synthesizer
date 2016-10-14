using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Add_OperatorDto_ConstA_ConstB : Add_OperatorDto
    {
        public double AValue { get; set; }
        public double BValue { get; set; }

        public Add_OperatorDto_ConstA_ConstB(InletDto aInletDto, InletDto bInletDto, double aValue, double bValue)
            : base(aInletDto, bInletDto)
        {
            AValue = aValue;
            BValue = bValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto : OperatorDto_VarA_VarB
    {
        public Multiply_OperatorDto(InletDto aInletDto, InletDto bInletDto)
            : base(aInletDto, bInletDto)
        { }
    }
}
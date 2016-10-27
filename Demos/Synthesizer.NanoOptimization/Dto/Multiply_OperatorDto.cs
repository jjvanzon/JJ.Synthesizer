using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto : OperatorDto_VarA_VarB
    {
        public override string OperatorTypeName => OperatorNames.Multiply;

        public Multiply_OperatorDto(OperatorDto aOperatorDto, OperatorDto bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}
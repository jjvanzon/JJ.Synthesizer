using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_VarA_VarB : OperatorDto
    {
        public OperatorDto AOperatorDto => ChildOperatorDtos[0];
        public OperatorDto BOperatorDto => ChildOperatorDtos[1];

        public OperatorDto_VarA_VarB(OperatorDto aOperatorDto, OperatorDto bOperatorDto)
            : base(new OperatorDto[] { aOperatorDto, bOperatorDto })
        { }
    }
}

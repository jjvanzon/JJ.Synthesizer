using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_VarA_ConstB : OperatorDto
    {
        public OperatorDto AOperatorDto => ChildOperatorDtos[0];
        public double B { get; set; }

        public OperatorDto_VarA_ConstB(OperatorDto aOperatorDto, double b)
            : base(new OperatorDto[] { aOperatorDto })
        {
            B = b;
        }
    }
}

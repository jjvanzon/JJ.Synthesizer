using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_VarA_ConstB : OperatorDtoBase
    {
        public OperatorDtoBase AOperatorDto => InputOperatorDtos[0];
        public double B { get; set; }

        public OperatorDtoBase_VarA_ConstB(OperatorDtoBase aOperatorDto, double b)
            : base(new OperatorDtoBase[] { aOperatorDto })
        {
            B = b;
        }
    }
}

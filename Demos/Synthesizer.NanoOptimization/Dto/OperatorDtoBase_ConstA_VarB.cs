using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_ConstA_VarB : OperatorDtoBase
    {
        public double A { get; set; }
        public OperatorDtoBase BOperatorDto => InputOperatorDtos[0];

        public OperatorDtoBase_ConstA_VarB(double a, OperatorDtoBase bOperatorDto)
            : base(new OperatorDtoBase[] { bOperatorDto })
        {
            A = a;
        }
    }
}

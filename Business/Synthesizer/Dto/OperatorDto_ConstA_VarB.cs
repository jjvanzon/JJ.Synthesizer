using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDto_ConstA_VarB : OperatorDto
    {
        public double A { get; set; }
        public OperatorDto BOperatorDto => ChildOperatorDtos[0];

        public OperatorDto_ConstA_VarB(double a, OperatorDto bOperatorDto)
            : base(new OperatorDto[] { bOperatorDto })
        {
            A = a;
        }
    }
}

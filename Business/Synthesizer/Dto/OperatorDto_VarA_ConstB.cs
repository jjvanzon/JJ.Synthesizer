using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDto_VarA_ConstB : OperatorDto
    {
        public OperatorDto AOperatorDto => InputOperatorDtos[0];
        public double B { get; set; }

        public OperatorDto_VarA_ConstB(OperatorDto aOperatorDto, double b)
            : base(new OperatorDto[] { aOperatorDto })
        {
            B = b;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_Vars_1Const : OperatorDto
    {
        public IList<InletDto> Vars
        {
            get { return InletDtos; }
            set { InletDtos = value; }
        }

        public double ConstValue { get; set; }

        public OperatorDto_Vars_1Const(IList<InletDto> vars, double constValue)
            : base(vars)
        { }
    }
}

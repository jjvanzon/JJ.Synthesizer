using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Add_OperatorDto_Vars_Const : OperatorDto
    {
        public IList<InletDto> Vars
        {
            get { return InletDtos; }
            set { InletDtos = value; }
        }

        public double ConstValue { get; set; }

        public Add_OperatorDto_Vars_Const(IList<InletDto> vars, double constValue)
            : base(vars)
        { }
    }
}

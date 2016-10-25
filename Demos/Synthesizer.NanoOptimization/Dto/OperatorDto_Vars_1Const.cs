using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_Vars_1Const : OperatorDto
    {
        public IList<OperatorDto> Vars
        {
            get { return ChildOperatorDtos; }
            set { ChildOperatorDtos = value; }
        }

        public double ConstValue { get; set; }

        public OperatorDto_Vars_1Const(IList<OperatorDto> vars, double constValue)
            : base(vars)
        { }
    }
}

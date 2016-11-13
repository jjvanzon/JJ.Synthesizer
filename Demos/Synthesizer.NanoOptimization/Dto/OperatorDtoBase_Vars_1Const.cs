using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_Vars_1Const : OperatorDtoBase
    {
        public IList<OperatorDtoBase> Vars
        {
            get { return InputOperatorDtos; }
            set { InputOperatorDtos = value; }
        }

        public double ConstValue { get; set; }

        public OperatorDtoBase_Vars_1Const(IList<OperatorDtoBase> vars, double constValue)
            : base(vars)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_Vars : OperatorDto
    {
        public IList<OperatorDto> Vars
        {
            get { return InputOperatorDtos; }
            set { InputOperatorDtos = value; }
        }

        public OperatorDto_Vars(IList<OperatorDto> vars)
            : base(vars)
        { }
    }
}

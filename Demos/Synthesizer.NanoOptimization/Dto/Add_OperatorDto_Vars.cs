using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Add_OperatorDto_Vars : OperatorDto
    {
        public IList<InletDto> Vars
        {
            get { return InletDtos; }
            set { InletDtos = value; }
        }

        public Add_OperatorDto_Vars(IList<InletDto> vars)
            : base(vars)
        { }
    }
}

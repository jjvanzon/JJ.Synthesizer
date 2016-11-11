using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDto_Vars : OperatorDto
    {
        public IList<OperatorDto> Vars
        {
            get { return ChildOperatorDtos; }
            set { ChildOperatorDtos = value; }
        }

        public OperatorDto_Vars(IList<OperatorDto> vars)
            : base(vars)
        { }
    }
}

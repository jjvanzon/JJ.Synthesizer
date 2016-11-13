using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars : OperatorDtoBase
    {
        public IList<OperatorDtoBase> Vars
        {
            get { return InputOperatorDtos; }
            set { InputOperatorDtos = value; }
        }

        public OperatorDtoBase_Vars(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }
}

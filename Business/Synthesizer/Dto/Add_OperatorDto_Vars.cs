using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Add_OperatorDto_Vars : OperatorDto_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Add);

        public Add_OperatorDto_Vars(IList<OperatorDto> vars)
            : base(vars)
        { }
    }
}
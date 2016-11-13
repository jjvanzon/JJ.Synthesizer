using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Add_OperatorDto_Vars_1Const : OperatorDtoBase_Vars_1Const
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Add);

        public Add_OperatorDto_Vars_1Const(IList<OperatorDtoBase> vars, double constValue)
            : base(vars, constValue)
        { }
    }
}

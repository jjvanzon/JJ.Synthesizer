using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars_1Const : OperatorDtoBase
    {
        public IList<OperatorDtoBase> Vars { get; set; }
        public double ConstValue { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => Vars;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Subtract_OperatorDto_ConstA_VarB : OperatorDtoBase_ConstA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Subtract);

        public Subtract_OperatorDto_ConstA_VarB(double a, OperatorDtoBase bOperatorDto)
            : base(a, bOperatorDto)
        { }
    }
}

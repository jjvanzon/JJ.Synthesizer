﻿using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverInlets_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);

        public MinOverInlets_OperatorDto(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }
}
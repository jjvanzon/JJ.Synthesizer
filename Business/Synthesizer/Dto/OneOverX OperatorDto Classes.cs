﻿using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class OneOverX_OperatorDto : OneOverX_OperatorDto_VarX
    { }

    internal class OneOverX_OperatorDto_VarX : OperatorDtoBase_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.OneOverX);
    }

    internal class OneOverX_OperatorDto_ConstX : OperatorDtoBase_ConstX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.OneOverX);
    }
}
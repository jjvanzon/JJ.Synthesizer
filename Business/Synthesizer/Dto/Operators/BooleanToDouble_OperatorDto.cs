﻿using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class BooleanToDouble_OperatorDto : OperatorDtoBase_WithInput
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BooleanToDouble;
	}
}
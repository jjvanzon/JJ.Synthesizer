﻿using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class RangeOverDimension_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public RangeOverDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.RangeOverDimension,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
﻿using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SortOverDimension_OperatorValidator : OperatorValidator_Base_AggregateOverDimension
    {
        public SortOverDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SortOverDimension, outletDimensionEnum: DimensionEnum.Signal)
        { }
    }
}
﻿using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Subtract_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Subtract_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Subtract, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
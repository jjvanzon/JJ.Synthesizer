﻿using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Reset_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Reset_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Reset, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
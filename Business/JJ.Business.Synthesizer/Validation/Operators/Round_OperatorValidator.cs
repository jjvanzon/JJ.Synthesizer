﻿using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Round_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Round_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Round, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
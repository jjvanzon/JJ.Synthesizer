﻿using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SawUp_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SawUp_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SawUp, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
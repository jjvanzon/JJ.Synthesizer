﻿using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Or_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilled
    {
        public Or_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
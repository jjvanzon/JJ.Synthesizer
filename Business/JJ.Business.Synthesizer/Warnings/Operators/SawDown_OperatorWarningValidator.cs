﻿using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SawDown_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public SawDown_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MaxOverInlets_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults
    {
        public MaxOverInlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

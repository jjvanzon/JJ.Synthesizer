using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class AverageOverInlets_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults
    {
        public AverageOverInlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

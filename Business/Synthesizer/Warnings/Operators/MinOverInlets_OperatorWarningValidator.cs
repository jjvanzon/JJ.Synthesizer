using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MinOverInlets_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults
    {
        public MinOverInlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Bundle_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults
    {
        public Bundle_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

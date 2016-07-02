using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Average_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults
    {
        public Average_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

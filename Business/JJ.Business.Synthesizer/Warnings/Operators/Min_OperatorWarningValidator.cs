using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Min_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledIn
    {
        public Min_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

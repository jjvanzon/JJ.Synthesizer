using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MaxDiscrete_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledIn
    {
        public MaxDiscrete_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

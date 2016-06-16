using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MinDiscrete_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledIn
    {
        public MinDiscrete_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class AverageDiscrete_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledIn
    {
        public AverageDiscrete_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

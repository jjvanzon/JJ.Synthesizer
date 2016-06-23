using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Max_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledIn
    {
        public Max_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

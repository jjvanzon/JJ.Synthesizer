using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MakeContinuous_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledIn
    {
        public MakeContinuous_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

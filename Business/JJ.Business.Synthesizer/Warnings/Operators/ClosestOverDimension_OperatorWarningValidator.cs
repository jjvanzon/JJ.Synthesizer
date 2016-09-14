using System;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class ClosestOverDimension_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public ClosestOverDimension_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
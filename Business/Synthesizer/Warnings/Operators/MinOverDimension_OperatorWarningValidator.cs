using System;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MinOverDimension_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public MinOverDimension_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
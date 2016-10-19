using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class RangeOverOutlets_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public RangeOverOutlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
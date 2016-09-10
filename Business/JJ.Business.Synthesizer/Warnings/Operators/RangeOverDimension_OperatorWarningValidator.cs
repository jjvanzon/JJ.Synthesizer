using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class RangeOverDimension_OperatorWarningValidator : OperatorWarningValidator_Base_SpecificInletsFilledInOrHaveDefaults
    {
        public RangeOverDimension_OperatorWarningValidator(Operator obj)
            : base(obj, OperatorConstants.RANGE_TILL_INDEX)
        { }
    }
}
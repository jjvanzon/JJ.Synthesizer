using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MaxContinuous_OperatorWarningValidator : OperatorWarningValidator_Base_SpecificInletsFilledIn
    {
        public MaxContinuous_OperatorWarningValidator(Operator obj)
            : base(obj, PropertyNames.Signal, PropertyNames.Till, PropertyNames.SampleCount)
        { }
    }
}
using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class AverageContinuous_OperatorWarningValidator : OperatorWarningValidator_Base_SpecificInletsFilledIn
    {
        public AverageContinuous_OperatorWarningValidator(Operator obj)
            : base(obj, PropertyNames.Signal, PropertyNames.Till, PropertyNames.SampleCount)
        { }
    }
}
using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Range_OperatorWarningValidator : OperatorWarningValidator_Base_SpecificInletsFilledIn
    {
        private const string IMPORTANT_INLET_NAME = PropertyNames.Till;

        public Range_OperatorWarningValidator(Operator obj)
            : base(obj, IMPORTANT_INLET_NAME)
        { }
    }
}
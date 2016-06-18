using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MaxContinuous_OperatorWarningValidator : OperatorWarningValidator_Base_SpecificInletFilledIn
    {
        private const string IMPORTANT_INLET_NAME = PropertyNames.Till;

        public MaxContinuous_OperatorWarningValidator(Operator obj)
            : base(obj, IMPORTANT_INLET_NAME)
        { }
    }
}
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MaxOverDimension_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public MaxOverDimension_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
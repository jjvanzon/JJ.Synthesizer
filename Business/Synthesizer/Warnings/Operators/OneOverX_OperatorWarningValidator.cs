using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OneOverX_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public OneOverX_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

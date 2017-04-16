using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class NotEqual_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public NotEqual_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

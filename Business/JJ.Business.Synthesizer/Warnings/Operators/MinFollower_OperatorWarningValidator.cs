using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MinFollower_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public MinFollower_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

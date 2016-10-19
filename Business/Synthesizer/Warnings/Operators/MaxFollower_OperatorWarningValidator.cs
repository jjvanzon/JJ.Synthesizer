using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MaxFollower_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public MaxFollower_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

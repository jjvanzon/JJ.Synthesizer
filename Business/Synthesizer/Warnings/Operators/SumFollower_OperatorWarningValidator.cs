using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SumFollower_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public SumFollower_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

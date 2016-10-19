using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class AverageFollower_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public AverageFollower_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

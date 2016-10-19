using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Squash_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Squash_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Squash_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Squash_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

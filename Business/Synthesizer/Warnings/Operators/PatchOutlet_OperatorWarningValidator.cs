using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class PatchOutlet_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public PatchOutlet_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}

using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class PatchOutlet_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public PatchOutlet_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}

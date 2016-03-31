using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class PatchOutlet_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public PatchOutlet_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}

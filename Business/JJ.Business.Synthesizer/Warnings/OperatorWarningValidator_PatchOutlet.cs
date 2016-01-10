using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_PatchOutlet : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_PatchOutlet(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}

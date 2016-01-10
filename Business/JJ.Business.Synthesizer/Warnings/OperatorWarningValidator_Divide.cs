using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Divide : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_Divide(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

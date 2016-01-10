using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Multiply : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_Multiply(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

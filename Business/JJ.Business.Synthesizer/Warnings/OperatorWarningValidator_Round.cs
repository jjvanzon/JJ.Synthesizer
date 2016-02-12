using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Round : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_Round(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

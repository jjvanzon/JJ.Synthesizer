using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_SawDown : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_SawDown(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}

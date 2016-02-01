using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_SawUp : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_SawUp(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}

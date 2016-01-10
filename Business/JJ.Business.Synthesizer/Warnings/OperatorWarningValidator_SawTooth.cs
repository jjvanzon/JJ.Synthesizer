using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_SawTooth : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_SawTooth(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}

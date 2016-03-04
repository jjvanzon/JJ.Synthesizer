using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_SawUp : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_SawUp(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}

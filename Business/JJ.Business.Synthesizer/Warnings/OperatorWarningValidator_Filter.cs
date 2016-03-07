using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_Filter : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_Filter(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_If : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_If(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

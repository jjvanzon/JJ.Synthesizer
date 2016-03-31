using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Narrower_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Narrower_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class If_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public If_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

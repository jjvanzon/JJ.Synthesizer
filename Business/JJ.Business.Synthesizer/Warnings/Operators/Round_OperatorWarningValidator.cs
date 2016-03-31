using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Round_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Round_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}

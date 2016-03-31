using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Reverse_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Reverse_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
